SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE dbo.VyazEconomPrint_LoadData
    @IdPodr int,
    @NomZadany nvarchar(50),
    @Year int
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        RTRIM(nakl) AS nakl,
        kol,
        type_pryz,
        RTRIM(zvet) AS zvet
    FROM dbo.v_spis_pryz WITH (NOLOCK)
    WHERE id_podr = @IdPodr
      AND nom_zadany = @NomZadany;

    SELECT TOP 1
        kod_k,
        RTRIM(articul) AS articul,
        RTRIM(mod) AS mod,
        RTRIM(articul_k) AS articul_k,
        RTRIM(mod_k) AS mod_k,
        zad_pl,
        v,
        p,
        stir,
        pr_printer,
        stra,
        poet,
        p_tamp,
        bus,
        gofp,
        nabiv_all,
        v_seb,
        p_seb
    FROM dbo.raskr_zeh_vyaz WITH (NOLOCK)
    WHERE zad_pl = @NomZadany;

    SELECT
        SUM(kol) AS kol,
        TRIM(STR(MIN(n_pach))) + ' - ' + TRIM(STR(MAX(n_pach))) AS diapPach,
        TRIM(MIN(razm)) + ' - ' + TRIM(MAX(razm)) AS diapSize
    FROM dbo.raskr_zeh_vyaz WITH (NOLOCK)
    WHERE zad_pl = @NomZadany;

    ;WITH NaklFilter AS
    (
        SELECT DISTINCT LTRIM(RTRIM(nakl)) AS nakl
        FROM dbo.v_spis_pryz WITH (NOLOCK)
        WHERE id_podr = @IdPodr
          AND nom_zadany = @NomZadany
          AND NULLIF(LTRIM(RTRIM(nakl)), '') IS NOT NULL
    )
    SELECT
        RTRIM(pp.nakl) AS nakl,
        RTRIM(pp.t_articul) AS t_articul,
        RTRIM(pp.zvet) AS zvet,
        pp.seb_t_m
    FROM dbo.prihod_pryz AS pp WITH (NOLOCK)
    INNER JOIN NaklFilter AS nf ON pp.nakl = nf.nakl;

    DECLARE @q1Start date = DATEFROMPARTS(@Year, 1, 1);
    DECLARE @q1End date = DATEFROMPARTS(@Year, 3, 31);
    DECLARE @q2Start date = DATEFROMPARTS(@Year, 4, 1);
    DECLARE @q2End date = DATEFROMPARTS(@Year, 6, 30);
    DECLARE @q3Start date = DATEFROMPARTS(@Year, 7, 1);
    DECLARE @q3End date = DATEFROMPARTS(@Year, 9, 30);
    DECLARE @q4Start date = DATEFROMPARTS(@Year, 10, 1);
    DECLARE @q4End date = DATEFROMPARTS(@Year, 12, 31);

    ;WITH ZvetFilter AS
    (
        SELECT DISTINCT LTRIM(RTRIM(zvet)) AS zvet
        FROM dbo.v_spis_pryz WITH (NOLOCK)
        WHERE id_podr = @IdPodr
          AND nom_zadany = @NomZadany
          AND NULLIF(LTRIM(RTRIM(zvet)), '') IS NOT NULL
    )
    SELECT
        RTRIM(pp.zvet) AS zvet,
        MAX(CASE WHEN pv.data_sozd >= @q1Start AND pv.data_sozd <= @q1End THEN pp.seb_t_m END) AS Q1,
        MAX(CASE WHEN pv.data_sozd >= @q2Start AND pv.data_sozd <= @q2End THEN pp.seb_t_m END) AS Q2,
        MAX(CASE WHEN pv.data_sozd >= @q3Start AND pv.data_sozd <= @q3End THEN pp.seb_t_m END) AS Q3,
        MAX(CASE WHEN pv.data_sozd >= @q4Start AND pv.data_sozd <= @q4End THEN pp.seb_t_m END) AS Q4
    FROM dbo.prih_v AS pv WITH (NOLOCK)
    INNER JOIN dbo.prihod_v AS prv WITH (NOLOCK) ON pv.np_id = prv.np_id
    INNER JOIN dbo.prihod_pryz AS pp WITH (NOLOCK) ON prv.kod_pr = pp.kod_pr
    INNER JOIN ZvetFilter AS zf ON pp.zvet = zf.zvet
    GROUP BY pp.zvet;
END
GO
