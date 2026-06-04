CREATE VIEW dbo.sewing_thread_categories_view
AS
SELECT
    cat.TCAT_ID,
    cat.TCAT_CategoryName,
    grp.TG_ID,
    grp.TG_GroupName,
    cls.TC_ID,
    cls.TC_ClassName
FROM global.planeta.dbo.TOVAR_CATEGORY cat
LEFT JOIN global.planeta.dbo.TOVAR_GROUP grp
    ON grp.TG_ID = cat.TCAT_TG_ID
LEFT JOIN global.planeta.dbo.TOVAR_CLASS cls
    ON cls.TC_ID = grp.TG_TC_ID;
GO