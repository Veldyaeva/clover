
CREATE PROCEDURE dbo.UpdateKitKnittingTime
AS

DECLARE @annId int;-- = 136453;
DECLARE @kodd varchar(20);
--SELECT ann.sek, ann.sek_shv, ann.sek_vyaz, * FROM art_norm_n ann where annid = @annId
--SELECT ak.sek, ak.sek_shv, ak.sek_v, * FROM art_komplekt ak WHERE ak.child_kodd = '8315471'
--SELECT top 10 psa.sek, psa.sek_shv, psa.sek_vyaz, * FROM dbo.plan_sezon_all psa WHERE psa.nn IN ('2025310567', '2026110123')
    ------------------------------------------------------------------
    -- 0. Нашли kodd в view_sp_articul по annId
    ------------------------------------------------------------------

SELECT TOP (1)
    @kodd = vsak.kodd
FROM dbo.View_sp_articul AS vsak
WHERE vsak.annId = @annId;

IF @kodd IS NULL
BEGIN
    THROW 50001, N'Не найден kodd по annId', 1;
END;
 
BEGIN TRY
    BEGIN TRAN;
    ------------------------------------------------------------------
    -- 1. Обновляем секунды в art_komplekt из art_norm_n
    ------------------------------------------------------------------
    UPDATE ak
    SET
        ak.sek     = ISNULL(ann.sek, ak.sek),
        ak.sek_v   = ISNULL(ann.sek_vyaz, ak.sek_v),
        ak.sek_shv = ISNULL(ann.sek_shv, ak.sek_shv)
    FROM dbo.art_komplekt AS ak
    JOIN dbo.art_norm_n AS ann
        ON ann.annId = @annId
    WHERE ak.child_kodd = @kodd;

    ------------------------------------------------------------------
    -- 2. Пересчитываем суммы по parent_nn 
    ------------------------------------------------------------------
    ;WITH Parents AS
    (
        SELECT DISTINCT
            ak.parent_nn
        FROM dbo.art_komplekt AS ak
        WHERE ak.child_kodd = @kodd
    ),
    Sums AS
    (
        SELECT
            ak.parent_nn,
            SUM(ISNULL(ak.sek, 0))     AS sum_sek,
            SUM(ISNULL(ak.sek_v, 0))   AS sum_sek_vyaz,
            SUM(ISNULL(ak.sek_shv, 0)) AS sum_sek_shv
        FROM dbo.art_komplekt AS ak
        JOIN Parents AS p
            ON p.parent_nn = ak.parent_nn
        GROUP BY ak.parent_nn
    )
    ------------------------------------------------------------------
    -- 3. Обновляем plan_sezon_all
    ------------------------------------------------------------------
    UPDATE psa
    SET
        psa.sek      = s.sum_sek,
        psa.sek_vyaz = s.sum_sek_vyaz,
        psa.sek_shv  = s.sum_sek_shv
    FROM dbo.plan_sezon_all AS psa
    JOIN Sums AS s
        ON s.parent_nn = psa.nn;

COMMIT;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK;

    THROW;
END CATCH;
GO