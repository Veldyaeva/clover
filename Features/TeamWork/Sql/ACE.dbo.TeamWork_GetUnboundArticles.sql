CREATE PROCEDURE dbo.TeamWork_GetUnboundArticles
    @SearchText NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SET @SearchText = NULLIF(LTRIM(RTRIM(@SearchText)), N'');

    ;WITH PlanTb AS
    (
        SELECT
            kodd,
            MAX(nn) AS nn,
            MAX(tb_Id) AS tb_Id
        FROM plan_sezon_all
        GROUP BY kodd
    ),
    FilteredArticles AS
    (
        SELECT
            vsa.grup,
            vsa.articul,
            vsa.mod,
            vsa.kodd_rt,
            vsa.ko AS kodd,
            vsa.razm,
            vsa.annId,
            p.tb_Id
        FROM View_sp_articul vsa
        LEFT JOIN kompl k
            ON vsa.kod = k.kod_k
        LEFT JOIN PlanTb p
            ON p.kodd = vsa.ko
        WHERE k.kod_k IS NULL
          AND (vsa.annId IS NULL OR vsa.annId = 0)
          AND (@SearchText IS NULL OR vsa.articul LIKE N'%' + @SearchText + N'%')
    ),
    RankedData AS
    (
        SELECT
            MAX(fa.grup) AS grup,
            fa.articul,
            fa.mod,
            fa.kodd_rt,
            fa.kodd,
            MIN(fa.razm) AS minSize,
            MAX(fa.razm) AS maxSize,
            ROW_NUMBER() OVER (PARTITION BY fa.articul ORDER BY fa.kodd_rt) AS row_num,
            COUNT(*) OVER (PARTITION BY fa.articul) AS total_count,
            MIN(r.razm_all) AS minSizeAll,
            MAX(r.razm_all) AS maxSizeAll,
            fa.annId,
            fa.tb_Id
        FROM FilteredArticles fa
        LEFT JOIN razm r
            ON fa.razm = r.razm
        GROUP BY
            fa.articul,
            fa.mod,
            fa.kodd_rt,
            fa.kodd,
            fa.annID,
            fa.tb_Id
    )
    SELECT
        annID,
        TRIM(grup) AS grup,
        TRIM(articul) AS articul,
        TRIM(mod) AS mod,
        kodd_rt,
        kodd,
        minSize,
        maxSize,
        row_num,
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
        END AS articulForRT,
        tb_Id
    FROM RankedData;
END
GO
