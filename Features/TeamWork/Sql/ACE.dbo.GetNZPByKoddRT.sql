CREATE PROCEDURE dbo.GetNZPByKoddRT
    @xAnnID INT, @xRezType INT = 0
AS
BEGIN
    SET NOCOUNT ON; -- Рекомендуется для процедур, чтобы не возвращать количество обработанных строк

    -- 1. Получаем данные по заданному annID
    IF OBJECT_ID('tempdb..#articulKodSelect') IS NOT NULL
        DROP TABLE #articulKodSelect;

    SELECT 
        vsa.kod, 
        vsa.kodd_rt, 
        vsa.grup, 
        vsa.articul, 
        vsa.mod, 
        vsa.razm, 
        vsa.kod_v
        , vsa.annId
        , vsa.ko AS kodd
    INTO #articulKodSelect
    FROM View_sp_articul vsa WITH (NOLOCK)
    WHERE vsa.annID = @xAnnID;

    IF OBJECT_ID('tempdb..#Labels') IS NOT NULL
        DROP TABLE #Labels;

    ;WITH RankedData AS
    (
        SELECT
            aks.annId,
            aks.kodd_rt,
            aks.articul,
            MIN(r.razm_all) AS minSizeAll,
            MAX(r.razm_all) AS maxSizeAll,
            ROW_NUMBER() OVER (PARTITION BY aks.articul ORDER BY aks.kodd_rt) AS row_num,
            COUNT(*) OVER (PARTITION BY aks.articul) AS total_count
        FROM #articulKodSelect aks
        LEFT JOIN razm r
            ON aks.razm = r.razm
        GROUP BY
            aks.annId,
            aks.kodd_rt,
            aks.articul
    )
    SELECT
        annId,
        kodd_rt,
        articul,
        minSizeAll,
        maxSizeAll,
        CASE
            WHEN total_count = 1 THEN ''
            WHEN row_num = 1 THEN 'min'
            WHEN row_num = total_count THEN 'max'
            ELSE 'mid'
        END AS size_label,
        TRIM(articul) + ' ' +
        CASE
            WHEN total_count = 1 THEN ''
            WHEN row_num = 1 THEN 'min'
            WHEN row_num = total_count THEN 'max'
            ELSE 'mid'
        END AS articulForRT
    INTO #Labels
    FROM RankedData;

    -- 2. Собираем основную информацию по НЗП (незавершенное производство)
    IF OBJECT_ID('tempdb..#PachData') IS NOT NULL
        DROP TABLE #PachData;
      SELECT 
          ks.kod,
          LEFT(ks.kod, 7) AS kodd,
          ks.kodd_rt,
          ks.grup,
          ks.articul,
          ks.mod,
          ks.kod_v,
          ISNULL(vrrnz.kol_pach, 0) AS kolRVAll,
          SUM(ISNULL(nr.kol, 0)) AS kolNaklAll,
          SUM(IIF(ISNULL(n.dost_n, 0) <> 0, nr.kol, 0)) AS kolGI,
          MAX(vrrnz.data_r) AS data_r
          , vrrnz.dost_zeh
          , vrrnz.id_brig
          , ks.annId
      INTO #PachData
      FROM #articulKodSelect ks
        LEFT JOIN View_rzu_rzv_nom_zad vrrnz WITH (NOLOCK)
        ON ks.kod = vrrnz.kod_pach
        LEFT JOIN nakl_ras nr WITH (NOLOCK)
        ON vrrnz.pach_kod = nr.pach_kod
        LEFT JOIN nakl n WITH (NOLOCK)
        ON nr.iz = n.iz
      GROUP BY 
          ks.kod,
          LEFT(ks.kod, 7),
          ks.kodd_rt,
          ks.grup,
          ks.articul,
          ks.mod,
          ks.kod_v,
          vrrnz.pach_kod
          , ISNULL(vrrnz.kol_pach,0)
          , vrrnz.dost_zeh
          , vrrnz.id_brig
          , ks.annId
    
    IF OBJECT_ID('tempdb..#NZPData') IS NOT NULL
        DROP TABLE #NZPData;
    SELECT 
        s.kodd_rt, 
        s.kodd, 
        s.grup, 
        s.articul, 
        s.mod, 
        s.kod_v,
        SUM(s.kolRVAll) AS kolRVAll,
        SUM(s.kolNaklAll) AS kolNaklAll,
        SUM(s.kolGI) AS kolGI,
        SUM(CAST((ISNULL(s.kolRVAll,0) - isNull(s.kolGI, 0)) AS INT)) AS kolNZP,
        MAX(s.data_r) AS data_r
        , s.annId
    INTO #NZPData
    FROM #PachData s
    GROUP BY 
        s.kodd_rt, 
        s.kodd, 
        s.grup, 
        s.articul, 
        s.mod, 
        s.kod_v
        , s.annId;

    -- 3. Финальный SELECT с присоединением логики из VIEW
    IF @xRezType = 0
      BEGIN
        SELECT
            @xAnnID AS annId,
            nzp.kodd_rt, 
            nzp.kodd, 
            nzp.grup, 
            nzp.articul, 
            nzp.mod, 
            nzp.kod_v,
            nzp.kolRVAll,
            nzp.kolNaklAll,
            nzp.kolGI,
            nzp.kolNZP,
            nzp.data_r,
            -- Интегрируем поля из вьюшки
            labels.minSizeAll,
            labels.maxSizeAll,
            labels.size_label,
            labels.articulForRT,
            ISNULL(pzt.PztCount, 0) AS PztCount
        FROM #NZPData nzp
        LEFT JOIN #Labels labels
            ON nzp.annID = labels.annID AND nzp.kodd_rt = labels.kodd_rt AND nzp.articul = labels.articul
        OUTER APPLY
        (
            SELECT COUNT_BIG(*) AS PztCount
            FROM plan_zagr_two pzt WITH (NOLOCK)
            INNER JOIN norm_rasz nr WITH (NOLOCK)
                ON pzt.pztNrID = nr.nrID
            WHERE nr.annId = @xAnnID
              AND pzt.tab > 0
        ) pzt
        ORDER BY 
            nzp.kodd_rt, 
            nzp.kodd, 
            nzp.grup, 
            nzp.articul, 
            nzp.mod, 
            nzp.kod_v;
      END
    ELSE
      BEGIN
        SELECT dost_zeh, id_brig 
          FROM #PachData
          GROUP BY dost_zeh, id_brig
      END
    -- Очистка временных таблиц
    DROP TABLE #articulKodSelect;
    DROP TABLE #NZPData;
IF OBJECT_ID('tempdb..#Labels') IS NOT NULL
    DROP TABLE #Labels;
IF OBJECT_ID('tempdb..#PachData') IS NOT NULL
    DROP TABLE #PachData;

END

GO
