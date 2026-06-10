SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE dbo.VyazEconomPrint_MarkDateEconom
    @Nn int,
    @DateEconom date
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.seb_vyaz_econom
    SET date_econom = @DateEconom
    WHERE nn = @Nn
      AND date_econom IS NULL;

    SELECT @@ROWCOUNT;
END
GO
