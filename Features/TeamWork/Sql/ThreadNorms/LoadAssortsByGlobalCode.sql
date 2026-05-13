SELECT
    TAT_GlobalCode AS TAT_ID,
    TAT_Name
FROM global.planeta.dbo.TOVAR_ASSTYPE
WHERE TAT_GlobalCode <= 4
ORDER BY TAT_GlobalCode;
