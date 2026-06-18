CREATE PROCEDURE dbo.ThreadNorms_LoadRows
    @ZeroNormOnly bit = 1
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.ThreadNorms_CreateDefaultRows;
    DECLARE @Today date = CONVERT(date, GETDATE());

    SELECT
        id,
        tg_id_n,
        ta_id,
        norm,
        kod_dr,
        kod3,
        kod_art,
        date_change,
        TCAT_CategoryName,
        TG_ID,
        TG_GroupName,
        TC_ID,
        TC_ClassName,
        TAT_Name,
        ThreadDisplay
    FROM dbo.sewing_thread_norms_view
    WHERE
        @ZeroNormOnly = 0
        OR date_change IS NULL
        OR (
            date_change >= @Today
            AND date_change < DATEADD(day, 1, @Today)
        )
    ORDER BY
        TC_ClassName,
        TG_GroupName,
        TCAT_CategoryName,
        TAT_Name,
        kod_dr;
END;
GO