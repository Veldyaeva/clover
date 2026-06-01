CREATE OR ALTER VIEW dbo.view_thread_norms_materials
AS
SELECT
    RTRIM(dr.kod_dr) AS kod_dr,
    ISNULL((
        SELECT TOP (1) RTRIM(drm.kod)
        FROM dbo.dop_ras_mat drm
        WHERE dr.kod_dr = LEFT(drm.kod, 4)
          AND ISNULL(drm.kod_art, '') <> ''
        ORDER BY drm.kod
    ), '') AS kod3,
    ISNULL((
        SELECT TOP (1) RTRIM(drm.kod_art)
        FROM dbo.dop_ras_mat drm
        WHERE dr.kod_dr = LEFT(drm.kod, 4)
          AND ISNULL(drm.kod_art, '') <> ''
        ORDER BY drm.kod_art
    ), '') AS kod_art,
    RTRIM(dr.kod_dr) + ' | ' + RTRIM(dr.gr) + ' | ' + RTRIM(dr.articul) AS displayText,
    RTRIM(dr.gr) AS SortGroup,
    RTRIM(dr.articul) AS SortArticul
FROM dbo.dop_ras dr
WHERE dr.kod_gr = '25';
