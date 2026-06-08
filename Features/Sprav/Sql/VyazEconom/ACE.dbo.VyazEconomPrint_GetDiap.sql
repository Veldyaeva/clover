SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE dbo.VyazEconomPrint_GetDiap
    @NomZadany nvarchar(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        SUM(kol) AS kol,
        TRIM(STR(MIN(n_pach))) + ' - ' + TRIM(STR(MAX(n_pach))) AS diapPach,
        TRIM(MIN(razm)) + ' - ' + TRIM(MAX(razm)) AS diapSize
    FROM dbo.raskr_zeh_vyaz WITH (NOLOCK)
    WHERE zad_pl = @NomZadany;
END
GO
