CREATE PROCEDURE dbo.ThreadNorms_CreateDefaultRows
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    INSERT INTO cfn.confection_norm_nitki
    (
        tg_id_n,
        ta_id,
        norm,
        kod_dr,
        kod3,
        kod_art,
        date_change
    )
    SELECT
        cat.TCAT_ID,
        2 AS ta_id,              -- ассортимент по умолчанию
        0 AS norm,
        TRIM(d.kod_dr),
        TRIM(ISNULL(d.kod3, '')),
        TRIM(ISNULL(d.kod_art, '')),
        NULL
    FROM dbo.sewing_thread_categories_view cat
    CROSS JOIN dbo.thread_norms_default d
    WHERE NULLIF(TRIM(ISNULL(d.kod_dr, '')), '') IS NOT NULL
      AND NOT EXISTS
      (
          SELECT 1
          FROM cfn.confection_norm_nitki n
          WHERE n.tg_id_n = cat.TCAT_ID
            AND TRIM(ISNULL(n.kod_dr, '')) = TRIM(d.kod_dr)
      );
END;
GO