SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE dbo.VyazEconomPrint_GetRaskrHeader
    @NomZadany nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;

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
END
GO
