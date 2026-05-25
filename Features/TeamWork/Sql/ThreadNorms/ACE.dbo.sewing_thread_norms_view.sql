CREATE VIEW dbo.sewing_thread_norms_view 
AS SELECT
    n.id,
    n.tg_id_n,

    -- если ta_id пустой, считаем его ассортиментом 2
    ISNULL(n.ta_id, 2) AS ta_id,

    ISNULL(n.norm, 0) AS norm,

    TRIM(ISNULL(n.kod_dr, '')) AS kod_dr,

    kod3 =
        CASE
            WHEN NULLIF(TRIM(ISNULL(n.kod3, '')), '') IS NULL
                THEN TRIM(ISNULL(d.kod3, ''))
            ELSE TRIM(n.kod3)
        END,

    kod_art =
        CASE
            WHEN NULLIF(TRIM(ISNULL(n.kod_art, '')), '') IS NULL
                THEN TRIM(ISNULL(d.kod_art, ''))
            ELSE TRIM(n.kod_art)
        END,

    n.date_change,

    cat.TCAT_CategoryName,
    cat.TG_ID,
    cat.TG_GroupName,
    cat.TC_ID,
    cat.TC_ClassName,

    assort.TAT_Name,

    d.gr,

    ThreadDisplay = TRIM(ISNULL(d.articul, ''))
FROM cfn.confection_norm_nitki n
LEFT JOIN dbo.sewing_thread_categories_view cat
    ON cat.TCAT_ID = n.tg_id_n
LEFT JOIN dbo.sewing_thread_assorts_view assort
    ON assort.TAT_ID = ISNULL(n.ta_id, 2)
LEFT JOIN dbo.thread_norms_default d
    ON TRIM(d.kod_dr) = TRIM(ISNULL(n.kod_dr, ''))
GO