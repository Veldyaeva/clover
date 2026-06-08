SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE dbo.VyazEconomPrint_GetWoolLines
    @IdPodr int,
    @NomZadany nvarchar(50)
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
END
GO
