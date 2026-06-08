SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE dbo.VyazEconomPrint_GetQuarterMaxPricesByZvet
    @ZvetsJson nvarchar(max),
    @Year int
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @q1Start date = DATEFROMPARTS(@Year, 1, 1);
    DECLARE @q1End date = DATEFROMPARTS(@Year, 3, 31);
    DECLARE @q2Start date = DATEFROMPARTS(@Year, 4, 1);
    DECLARE @q2End date = DATEFROMPARTS(@Year, 6, 30);
    DECLARE @q3Start date = DATEFROMPARTS(@Year, 7, 1);
    DECLARE @q3End date = DATEFROMPARTS(@Year, 9, 30);
    DECLARE @q4Start date = DATEFROMPARTS(@Year, 10, 1);
    DECLARE @q4End date = DATEFROMPARTS(@Year, 12, 31);

    WITH ZvetFilter AS
    (
        SELECT DISTINCT LTRIM(RTRIM(CONVERT(nvarchar(100), [value]))) AS zvet
        FROM OPENJSON(@ZvetsJson)
        WHERE NULLIF(LTRIM(RTRIM(CONVERT(nvarchar(100), [value]))), '') IS NOT NULL
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
