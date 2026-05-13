SELECT
    n.id,
    n.men,
    menView.name AS men_name,
    n.tg_id_n,
    n.ta_id,
    n.norm,
    n.kod_dr,
    n.kod3,
    n.kod_art,
    n.date_change,
    cat.TCAT_CategoryName,
    grp.TG_ID,
    grp.TG_GroupName,
    cls.TC_ID,
    cls.TC_ClassName,
    assort.TAT_Name,
    LTRIM(RTRIM(ISNULL(n.kod3, ''))) +
        CASE
            WHEN NULLIF(LTRIM(RTRIM(ISNULL(n.kod_art, ''))), '') IS NULL THEN ''
            ELSE ' / ' + LTRIM(RTRIM(n.kod_art))
        END AS ThreadDisplay
FROM cfn.confection_norm_nitki n
LEFT JOIN dbo.view_grup_men menView
    ON menView.men = n.men
LEFT JOIN global.planeta.dbo.TOVAR_CATEGORY cat
    ON cat.TCAT_ID = n.tg_id_n
LEFT JOIN global.planeta.dbo.TOVAR_GROUP grp
    ON grp.TG_ID = cat.TCAT_TG_ID
LEFT JOIN global.planeta.dbo.TOVAR_CLASS cls
    ON cls.TC_ID = grp.TG_TC_ID
LEFT JOIN global.planeta.dbo.TOVAR_ASSTYPE assort
    ON assort.TAT_ID = n.ta_id
{whereClause}
ORDER BY n.men, cls.TC_ClassName, grp.TG_GroupName, cat.TCAT_CategoryName, assort.TAT_Name, n.kod_dr;
