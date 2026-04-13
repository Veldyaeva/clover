


CREATE PROCEDURE dbo.GetPztCountsByKoddRT
    @xAnnID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT  
       -- LEFT(nr.kod, 7) AS kod,
       nr.annId, 
        COUNT_BIG(*) AS PztCount  -- COUNT_BIG на случай большого объема
    FROM plan_zagr_two pzt
    INNER JOIN norm_rasz nr ON pzt.pztNrID = nr.nrID
    WHERE nr.annId = @xAnnID
      AND pzt.tab > 0
    GROUP BY nr.annId--LEFT(nr.kod, 7);
END
GO