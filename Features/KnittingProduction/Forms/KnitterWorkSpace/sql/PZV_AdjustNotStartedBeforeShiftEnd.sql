CREATE OR ALTER PROCEDURE dbo.PZV_AdjustNotStartedBeforeShiftEnd
    @KwsId INT,
    @MinHours DECIMAL(18,2) = 12.0
AS
BEGIN
    SET NOCOUNT ON;

    /*
      Правила:
      - факт по машине = SUM(pzvNChasi) по строкам текущей смены, где операция начата (pzvDateStart IS NOT NULL)
      - добор = из неначатых (pzvDateStart IS NULL AND pzvDateEnd IS NULL) по "времени назначено"
              берем pzvChasNazn, а если он 0/null — fallback на pzvNChasi (на всякий)
      - оставляем столько неначатых, чтобы факт+добор >= @MinHours (с включением "перешагнувшей" строки)
      - все остальные неначатые в смене разназначаем: tab=0, kwsId=0, dateNaznTab=NULL
    */

    IF @KwsId IS NULL OR @KwsId = 0
        THROW 50001, 'KwsId must be non-zero for shift end adjustment.', 1;

    ;WITH ShiftRows AS (
        SELECT
            p.pzvID,
            p.pzvKmlID,
            p.pzvTab,
            p.pzvKwsID,
            p.pzvDateStart,
            p.pzvDateEnd,
            CAST(ISNULL(p.pzvNChasi, 0) AS DECIMAL(18,2)) AS FactHours, -- факт
            CAST(
                CASE
                    WHEN ISNULL(p.pzvChasNazn,0) <> 0 THEN ISNULL(p.pzvChasNazn,0)
                    ELSE ISNULL(p.pzvNChasi,0)  -- fallback
                END
            AS DECIMAL(18,2)) AS AssignedHours,  -- "время назн" для добора
            -- порядок отбора неначатых: можно менять под приоритеты
            p.pzvNomZad, p.pzvNom, p.pzvNomN, p.pzvNrID
        FROM dbo.planZagrVyaz p
        WHERE p.pzvKwsID = @KwsId
          AND p.pzvKmlID IS NOT NULL
    ),
    FactByMachine AS (
        SELECT
            pzvKmlID,
            SUM(FactHours) AS FactSum
        FROM ShiftRows
        WHERE pzvDateStart IS NOT NULL
        GROUP BY pzvKmlID
    ),
    NotStarted AS (
        SELECT
            s.*,
            ISNULL(fbm.FactSum, 0) AS FactSum,
            CASE
                WHEN @MinHours - ISNULL(fbm.FactSum,0) > 0 THEN @MinHours - ISNULL(fbm.FactSum,0)
                ELSE CAST(0 AS DECIMAL(18,2))
            END AS NeedHours
        FROM ShiftRows s
        LEFT JOIN FactByMachine fbm ON fbm.pzvKmlID = s.pzvKmlID
        WHERE s.pzvDateStart IS NULL
          AND s.pzvDateEnd IS NULL
    ),
    Ranked AS (
        SELECT
            n.*,
            SUM(n.AssignedHours) OVER (
                PARTITION BY n.pzvKmlID
                ORDER BY
                    -- ВАЖНО: порядок, в каком "добираем". Сейчас — по назначенным часам (больше сначала),
                    -- потом по номерам. Можешь заменить на свой приоритет.
                    n.AssignedHours DESC,
                    n.pzvNomZad, n.pzvNom, n.pzvNomN, n.pzvNrID, n.pzvID
                ROWS UNBOUNDED PRECEDING
            ) AS RunningAssigned
        FROM NotStarted n
    ),
    Keep AS (
        SELECT r.pzvID
        FROM Ranked r
        WHERE r.NeedHours > 0
          AND (
                 r.RunningAssigned <= r.NeedHours
              OR (r.RunningAssigned > r.NeedHours AND r.RunningAssigned - r.AssignedHours < r.NeedHours)
          )
    )
    UPDATE p
    SET
        p.pzvTab = 0,
        p.pzvKwsID = 0,
        p.pzvDateNaznTab = NULL,
        p.pzvUpdDate = GETDATE()
    FROM dbo.planZagrVyaz p
    JOIN NotStarted ns ON ns.pzvID = p.pzvID
    LEFT JOIN Keep k ON k.pzvID = p.pzvID
    WHERE k.pzvID IS NULL;  -- разназначаем ВСЕ неначатые, которые не нужны для добора

    -------------------------------------------------------------------
    -- Отчет для UI: сколько факта и сколько добрали/осталось
    -------------------------------------------------------------------
    ;WITH KeptByMachine AS (
        SELECT
            r.pzvKmlID,
            SUM(r.AssignedHours) AS KeptAssigned
        FROM Ranked r
        JOIN Keep k ON k.pzvID = r.pzvID
        GROUP BY r.pzvKmlID
    )
    SELECT
        m.pzvKmlID,
        FactHours = ISNULL(f.FactSum,0),
        KeptAssignedHours = ISNULL(k.KeptAssigned,0),
        TotalForCheck = ISNULL(f.FactSum,0) + ISNULL(k.KeptAssigned,0),
        StillLessThanMin = CASE WHEN ISNULL(f.FactSum,0) + ISNULL(k.KeptAssigned,0) < @MinHours THEN 1 ELSE 0 END
    FROM (SELECT DISTINCT pzvKmlID FROM ShiftRows) m
    LEFT JOIN FactByMachine f ON f.pzvKmlID = m.pzvKmlID
    LEFT JOIN KeptByMachine k ON k.pzvKmlID = m.pzvKmlID
    ORDER BY m.pzvKmlID;
END
GO


--SELECT top 100 * FROM planZagrVyaz zva WHERE zva.pzvID = 159

EXEC PZV_AdjustNotStartedBeforeShiftEnd(@KwsId = 159, @minHours = 12)


