CREATE OR ALTER VIEW dbo.view_thread_norms_assorts_id
AS
SELECT
    TAT_ID,
    TAT_Name
FROM global.planeta.dbo.TOVAR_ASSTYPE
WHERE TAT_ID <= 4;
