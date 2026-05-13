SELECT DISTINCT
    RTRIM(men) AS Men,
    RTRIM(name) AS Name
FROM dbo.view_grup_men
WHERE ISNULL(RTRIM(men), '') <> ''
ORDER BY Men;
