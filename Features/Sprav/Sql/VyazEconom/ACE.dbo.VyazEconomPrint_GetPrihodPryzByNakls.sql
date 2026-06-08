SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE dbo.VyazEconomPrint_GetPrihodPryzByNakls
    @NaklsJson nvarchar(max)
AS
BEGIN
    SET NOCOUNT ON;

    WITH NaklFilter AS
    (
        SELECT DISTINCT LTRIM(RTRIM(CONVERT(nvarchar(50), [value]))) AS nakl
        FROM OPENJSON(@NaklsJson)
        WHERE NULLIF(LTRIM(RTRIM(CONVERT(nvarchar(50), [value]))), '') IS NOT NULL
    )
    SELECT
        RTRIM(pp.nakl) AS nakl,
        RTRIM(pp.t_articul) AS t_articul,
        RTRIM(pp.zvet) AS zvet,
        pp.seb_t_m
    FROM dbo.prihod_pryz AS pp WITH (NOLOCK)
    INNER JOIN NaklFilter AS nf ON pp.nakl = nf.nakl;
END
GO
