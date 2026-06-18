CREATE OR ALTER VIEW dbo.view_thread_norms_assorts_globalcode
AS
SELECT
    TAT_GlobalCode AS TAT_ID,
    TAT_Name
FROM global.planeta.dbo.TOVAR_ASSTYPE
WHERE TAT_GlobalCode <= 4;
