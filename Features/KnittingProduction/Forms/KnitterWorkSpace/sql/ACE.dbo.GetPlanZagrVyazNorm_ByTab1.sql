
-- Группировка с отбором операций до 14 план. часов для вязальной машины (ВМ) 
CREATE PROCEDURE dbo.GetPlanZagrVyazNorm_ByTab1
     @Tab             INT,--            = 9797,
     @KwsId           INT            = 0,        -- ID смены. открытая смена => фильтр по смене
     @ExpandAssignedByNrId BIT       = 0,        -- 1 = в AssignedToMe добавить операции других людей с теми же nrID
     @MaxHours        DECIMAL(18,2)  = 14.0,
     @OnlyActive      BIT            = 1,
     @KmaId           INT, --           = NULL,     -- зона
     @IncludeFinished BIT            = 1         -- для закрытой смены (и/или кандидатов) АДМИНСКИЙ РЕЖИМ
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @HasShift bit = CASE WHEN ISNULL(@KwsId,0) <> 0 THEN 1 ELSE 0 END;

IF ISNULL(@KmaId,0) = 0
    THROW 50001, N'Не задана зона @KmaId', 1;

IF @HasShift = 1 AND ISNULL(@Tab,0) = 0
    THROW 50002, N'Для открытой смены должен быть задан @Tab', 1;
    -----------------------------------------------------------------------
    -- 1) Фильтр по зоне через список машин (Kml) в зоне
    -----------------------------------------------------------------------
    IF OBJECT_ID('tempdb..#KmlByZone') IS NOT NULL DROP TABLE #KmlByZone;

    IF @KmaId IS NOT NULL
    BEGIN
      
      drop table if EXISTS #KmlByZone
      CREATE TABLE #KmlByZone (machNazn nchar(100), kwsTabStart int, fio nvarchar(100), kwsID int, kwsKmaID int, kmaNumber nvarchar(10), kwsmlKmlID int, kmlNumber nvarchar(10), idVyazClass int, nameVyazClass int, kmaNumberInt int, kmlNumberInt int, typeID int, typeName varchar(3), chasNaznZad decimal(38, 5), chasNaznZadGroup decimal(38, 5), chasNotConfirmedZad decimal(38, 5), chasNotConfirmedZadGroup decimal(38, 5), chasRemainZad decimal(38, 5), chasRemainZadGroup decimal(38, 5), shiftsRemainZad decimal(38, 6), shiftsRemainZadGroup decimal(38, 6), chasNaznSmen decimal(38, 5), chasNaznSmenGroup decimal(38, 5), chasNaznSmenProc decimal(38, 6), chasNaznSmenProcGroup decimal(38, 6), chasInWorkSmen decimal(38, 5), chasInWorkSmenGroup decimal(38, 5), chasDoneSmen decimal(38, 5), chasDoneSmenGroup decimal(38, 5), chasDoneSmenProc decimal(38, 6), chasDoneSmenProcGroup decimal(38, 6), chasRemainSmen decimal(38, 5), chasRemainSmenGroup decimal(38, 5), chasConfirmedSmen decimal(38, 5), chasConfirmedSmenGroup decimal(38, 5), koefObServ decimal(10, 3), smenLength int, kodOb int, longRep int)
      INSERT INTO #KmlByZone EXEC dbo.GetSmenZadanyVyaz @xKmaIDNazn = @KmaId
                          ,@xKodProizv = 1
                          ,@xKodPodr = 1
      delete FROM #KmlByZone WHERE kwsKmaID <> @KmaId
      delete FROM #KmlByZone WHERE typeID <> 2
--SELECT top 100 * FROM #KmlByZone
        CREATE UNIQUE CLUSTERED INDEX IX__KmlByZone ON #KmlByZone(kwsmlKmlID);
    END

    -----------------------------------------------------------------------
    -- 2) Собираем базовый набор в #Flags
    -----------------------------------------------------------------------
    IF OBJECT_ID('tempdb..#Flags') IS NOT NULL DROP TABLE #Flags;

    ;WITH ZoneOps AS (
        SELECT
            -- planZagrVyaz (pzv*)
            pzv.pzvID,
            pzv.pzvIDParent,
            pzv.pzvDivision,
            pzv.pzvIDMlOp,
            pzv.pzvAnnID,
            pzv.pzvNrID,
            pzv.pzvNomZad,
            pzv.pzvNom,
            pzv.pzvNomN,
            pzv.pzvArticul,
            pzv.pzvMod,
            pzv.pzvIdBrig,
            pzv.pzvSek,

            -- 
            -- pzvTab = 0 -> pzvKol/pzvNChasi = план, *Nazn пустые
            -- pzvTab > 0 -> pzvKol/pzvNChasi = факт, план в *Nazn
            pzv.pzvKol,
            pzv.pzvNChasi,
            pzv.pzvSekNazn,
            pzv.pzvChasNazn,
            pzv.pzvKolNazn,
            pzv.pzvKmlID,
            pzv.pzvDateNaznKm,
            pzv.pzvTab,
            pzv.pzvDateNaznTab,
            pzv.pzvDateStart,
            pzv.pzvDateEnd,
            pzv.pzvDateML,
            pzv.pzvDateMLUt,
            pzv.pzvDateMast,
            pzv.pzvRKol,
            pzv.pzvVidPr,
            pzv.pzvCompAdd,
            pzv.pzvDateAdd,
            pzv.pzvUpdDate,
            pzv.pzvKwsID,

            -- norm_rasz (nr*)
            nr.nrID                          AS nrID,
            nr.kod                           AS nrKod,
            nr.kod_o                         AS nrKodO,
            nr.kod_podr                      AS nrKodPodr,
            nr.kod_proizv                    AS nrKodProizv,
            TRIM(nr.[text])               AS nrText,
            nr.sek                           AS nrSek,
            nr.seb                           AS nrSeb,
            nr.n                             AS nrN,
            nr.n_ch                          AS nrNCh,
            nr.n1                            AS nrN1,
            nr.seb_s                         AS nrSebS,
            nr.razryd                        AS nrRazryd,
            nr.spec                          AS nrSpec,
            trim(nr.obor)                    AS nrObor,
            nr.sek12                         AS nrSek12,
            nr.sek7                          AS nrSek7,
            nr.sek5                          AS nrSek5,
            nr.kod_ob                        AS nrKodOb,
            nr.sql_pr_add                    AS nrSqlPrAdd,
            nr.date_add                      AS nrBizDateAdd,
            nr.komp_name                     AS nrKompName,
            nr.nrDateDel                     AS nrRowDateDel,
            nr.nrCompDel                     AS nrRowCompDel,
            nr.nrDateAdd                     AS nrRowDateAdd,
            nr.nrCompAdd                     AS nrRowCompAdd,
            nr.annId                         AS nrAnnId,

            mlv.kmlNumber                    AS kmlNumber,
            mlv.name_class                   AS name_class,
            mlv.koefObServ                   AS koefObServ,

            rzv.n_pach,
            rzv.pach_kod,
            rzv.razm,
            rzv.kod AS rzv_kod,
            rzv.kol AS rzv_kol,

            ISNULL(pm.data_cd_new, psz.data_cd) AS data_cd,

            CASE WHEN ISNULL(pzv.pzvTab,0) = 0 THEN 0 ELSE 1 END AS IsAssignedAny,
            CASE
    WHEN ISNULL(@KwsId,0) = 0 THEN 0      -- закрытая смена: таб игнорируем
    WHEN ISNULL(@Tab,0) = 0 THEN 0
    WHEN pzv.pzvTab = @Tab THEN 1
    ELSE 0
END AS IsAssignedToMe,

--            -- Для лимита часов по кандидатам (в закрытой смене / tab=0) берем плановые часы.
--            -- У неназначенных (pzvTab=0) план лежит в pzvNChasi
            CAST(ISNULL(pzv.pzvNChasi,0) AS DECIMAL(18,2)) AS TaskHours
--        FROM knitMachineAreaEmp mae
--          LEFT JOIN knitWorkingShiftNewCurrentSmen_view maev ON mae.kmaeKmaID = maev.kmaID
--          --LEFT JOIN knitWorkingShiftMachineListNew wsmln ON maev.kwsID = wsmln.kwsmlKwsID
--          LEFT JOIN #KmlByZone wsmln ON mae.kmaeKmaID = wsmln.kwsKmaID
--          LEFT JOIN knitMachineList_view mlv ON wsmln.kwsmlKmlID = mlv.kmlID
--          LEFT JOIN planZagrVyaz pzv ON wsmln.kwsmlKmlID = pzv.pzvKmlID
--          LEFT JOIN norm_rasz nr ON pzv.pzvNrID = nr.nrID
--          LEFT JOIN plan_sezon_zad psz ON pzv.pzvNomZad = psz.nom
--          LEFT JOIN View_proizv_modify pm ON psz.nom = pm.nom
--          LEFT JOIN dbo.raskr_zeh_vyaz AS rzv
--               ON rzv.zad_pl = pzv.pzvNomZad AND rzv.nom = pzv.pzvNom AND rzv.nom_n = pzv.pzvNomN
        FROM #KmlByZone wsmln
JOIN knitMachineList_view mlv 
     ON wsmln.kwsmlKmlID = mlv.kmlID
JOIN planZagrVyaz pzv 
     ON wsmln.kwsmlKmlID = pzv.pzvKmlID
JOIN norm_rasz nr 
     ON pzv.pzvNrID = nr.nrID
LEFT JOIN plan_sezon_zad psz 
     ON pzv.pzvNomZad = psz.nom
LEFT JOIN View_proizv_modify pm 
     ON psz.nom = pm.nom
LEFT JOIN dbo.raskr_zeh_vyaz AS rzv
     ON rzv.zad_pl = pzv.pzvNomZad
    AND rzv.nom = pzv.pzvNom
    AND rzv.nom_n = pzv.pzvNomN
        WHERE

            pzv.pzvKmlID IS NOT NULL
            AND pzv.pzvDateNaznKm IS NOT NULL
            AND (@OnlyActive = 0 OR nr.nrDateDel IS NULL)
            AND (
                   @IncludeFinished = 1
                OR pzv.pzvDateEnd IS NULL
               OR (@HasShift = 1 AND pzv.pzvKwsID = @KwsId)
            )
    ),

    Flags AS (
        SELECT
            z.*,
            CASE WHEN z.pach_kod IS NULL THEN 0
                 ELSE MAX(z.IsAssignedAny) OVER (PARTITION BY z.pach_kod)
            END AS HasAssignedInPack,
            MAX(z.IsAssignedAny) OVER (PARTITION BY z.pzvAnnID) AS HasAssignedInAnn
        FROM ZoneOps z
    )
    SELECT *
    INTO #Flags
    FROM Flags;
    -----------------------------------------------------------------------
    -- 3) #MyNr: nrId которые "мои" (для ExpandAssignedByNrId)
    -----------------------------------------------------------------------
    IF OBJECT_ID('tempdb..#MyNr') IS NOT NULL DROP TABLE #MyNr;

    SELECT DISTINCT f.nrID
    INTO #MyNr
    FROM #Flags f
    WHERE
        ISNULL(@KwsId,0) <> 0
    AND (
           f.pzvKwsID = @KwsId
        OR f.IsAssignedToMe = 1
    );

    -----------------------------------------------------------------------
    -- 4) 1-й набор: Назначенные (AssignedToMe + AssignedSameNr_Other)
    --    + добавляем стабильные UI-поля Plan/Fact
    -----------------------------------------------------------------------
    SELECT
        CASE
            WHEN (
                   ((@KwsId IS NOT NULL AND @KwsId <> 0) AND f.pzvKwsID = @KwsId)
                OR ((@KwsId IS NULL OR @KwsId = 0) AND f.IsAssignedToMe = 1)
            )
            THEN N'AssignedToMe'
            ELSE N'AssignedSameNr_Other'
        END AS RowKind,
        f.*,

        -- Стабильные UI-поля по плану/факту
        CASE WHEN ISNULL(f.pzvTab,0) = 0 THEN ISNULL(f.pzvKol,0)     ELSE ISNULL(f.pzvKolNazn,0) END AS PlanKol_UI,
        CASE WHEN ISNULL(f.pzvTab,0) = 0 THEN ISNULL(f.pzvNChasi,0)  ELSE ISNULL(f.pzvChasNazn,0) END AS PlanChas_UI,
        CASE WHEN ISNULL(f.pzvTab,0) = 0 THEN 0                     ELSE ISNULL(f.pzvKol,0) END AS FactKol_UI,
        CASE WHEN ISNULL(f.pzvTab,0) = 0 THEN CAST(0 AS DECIMAL(18,2)) ELSE ISNULL(f.pzvNChasi,0) END AS FactChas_UI

    FROM #Flags f
    LEFT JOIN #MyNr mn ON mn.nrID = f.nrID
    WHERE
    @HasShift = 1
    AND f.pzvKwsID = @KwsId
    AND f.pzvTab = @Tab
    ORDER BY
        CASE WHEN f.data_cd IS NULL THEN 1 ELSE 0 END,
        f.data_cd,
        f.pzvNomZad, f.pzvNom, f.pzvNomN, f.nrN, f.nrN1;

    -----------------------------------------------------------------------
    -- 5) 2-й набор: Кандидаты
    --    - Открытая смена + Tab>0: кандидаты в рамках смены (f.pzvKwsID=@KwsId)
    --    - Закрытая смена ИЛИ Tab=0: неназначенные + лимит по часам (@MaxHours)
    --    + те же UI-поля Plan/Fakt
    -----------------------------------------------------------------------
    IF (ISNULL(@KwsId,0) <> 0 AND ISNULL(@Tab,0) <> 0)
    BEGIN
        -- Открытая смена, конкретный исполнитель: показываем кандидатов из текущей смены
        ;WITH Prioritized AS (
            SELECT
                f.*,
                CASE
                    WHEN f.pach_kod IS NOT NULL AND f.HasAssignedInPack = 1 THEN 1
                    WHEN (f.pach_kod IS NULL OR f.HasAssignedInPack = 0) AND f.HasAssignedInAnn = 1 THEN 2
                    ELSE 3
                END AS PriorityGroup
            FROM #Flags f
            WHERE f.pzvKwsID = @KwsId
        )
        SELECT
            N'Candidate' AS RowKind,
            p.*,

            CASE WHEN ISNULL(p.pzvTab,0) = 0 THEN ISNULL(p.pzvKol,0)     ELSE ISNULL(p.pzvKolNazn,0) END AS PlanKol_UI,
            CASE WHEN ISNULL(p.pzvTab,0) = 0 THEN ISNULL(p.pzvNChasi,0)  ELSE ISNULL(p.pzvChasNazn,0) END AS PlanChas_UI,
            CASE WHEN ISNULL(p.pzvTab,0) = 0 THEN 0                     ELSE ISNULL(p.pzvKol,0) END AS FactKol_UI,
            CASE WHEN ISNULL(p.pzvTab,0) = 0 THEN CAST(0 AS DECIMAL(18,2)) ELSE ISNULL(p.pzvNChasi,0) END AS FactChas_UI

        FROM Prioritized p
        ORDER BY
            p.PriorityGroup,
            CASE WHEN p.data_cd IS NULL THEN 1 ELSE 0 END,
            p.data_cd,
            p.pzvNomZad, p.pzvNom, p.pzvNomN, p.nrN, p.nrN1, p.pzvID;
    END
    ELSE
    BEGIN
        -- Закрытая смена ИЛИ Tab=0 (админ/общий режим): только неназначенные + лимит часов
        ;WITH Prioritized AS (
            SELECT
                f.*,
                CASE
                    WHEN f.pach_kod IS NOT NULL AND f.HasAssignedInPack = 1 THEN 1
                    WHEN (f.pach_kod IS NULL OR f.HasAssignedInPack = 0) AND f.HasAssignedInAnn = 1 THEN 2
                    ELSE 3
                END AS PriorityGroup
            FROM #Flags f
            WHERE
              f.IsAssignedAny = 0
        )

,Ranked AS (
    SELECT
        p.*,
        SUM(p.TaskHours) OVER (
            PARTITION BY p.pzvKmlID
            ORDER BY
                p.PriorityGroup,
                CASE WHEN p.data_cd IS NULL THEN 1 ELSE 0 END,
                p.data_cd,
                p.pzvNomZad, p.pzvNom, p.pzvNomN, p.nrN, p.nrN1, p.pzvID
            ROWS UNBOUNDED PRECEDING
        ) AS RunningHoursByMachine
    FROM Prioritized p
)
SELECT
    N'Candidate' AS RowKind,
    r.*,

    CASE WHEN ISNULL(r.pzvTab,0) = 0 THEN ISNULL(r.pzvKol,0)     ELSE ISNULL(r.pzvKolNazn,0) END AS PlanKol_UI,
    CASE WHEN ISNULL(r.pzvTab,0) = 0 THEN ISNULL(r.pzvNChasi,0)  ELSE ISNULL(r.pzvChasNazn,0) END AS PlanChas_UI,
    CASE WHEN ISNULL(r.pzvTab,0) = 0 THEN 0                     ELSE ISNULL(r.pzvKol,0) END AS FactKol_UI,
    CASE WHEN ISNULL(r.pzvTab,0) = 0 THEN CAST(0 AS DECIMAL(18,2)) ELSE ISNULL(r.pzvNChasi,0) END AS FactChas_UI

FROM Ranked r
WHERE
      r.RunningHoursByMachine <= @MaxHours
   OR (r.RunningHoursByMachine > @MaxHours AND r.RunningHoursByMachine - r.TaskHours < @MaxHours)

ORDER BY
    r.PriorityGroup,
    CASE WHEN r.data_cd IS NULL THEN 1 ELSE 0 END,
    r.data_cd,
    r.pzvNomZad, r.pzvNom, r.pzvNomN, r.nrN, r.nrN1, r.pzvID;

    END

END



GO