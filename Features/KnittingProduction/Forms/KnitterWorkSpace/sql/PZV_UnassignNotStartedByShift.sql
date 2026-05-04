USE ACE
GO

IF DB_NAME() <> N'ACE' SET NOEXEC ON
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

--
-- Create procedure [dbo].[PZV_UnassignNotStartedByShift]
--
GO
PRINT (N'Create procedure [dbo].[PZV_UnassignNotStartedByShift]')
GO
CREATE PROCEDURE dbo.PZV_UnassignNotStartedByShift
    @KwsId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE p
    SET
        p.pzvTab = 0,
        p.pzvKwsID = 0,
        p.pzvDateNaznTab = NULL,
        p.pzvUpdDate = GETDATE()
    FROM dbo.planZagrVyaz p
    WHERE
        p.pzvKwsID = @KwsId
        AND p.pzvDateStart IS NULL
        AND p.pzvDateEnd IS NULL;
END
GO