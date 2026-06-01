CREATE OR ALTER VIEW dbo.view_thread_norms_managers
AS
SELECT DISTINCT
    RTRIM(men) AS Men,
    RTRIM(name) AS Name
FROM dbo.view_grup_men
WHERE ISNULL(RTRIM(men), '') <> '';
