IF COL_LENGTH('dbo.BaseNode', 'SourceAnnId') IS NULL
BEGIN
    ALTER TABLE dbo.BaseNode
        ADD SourceAnnId INT NULL;
END
GO

IF COL_LENGTH('dbo.BaseNode', 'SourceRtCode') IS NULL
BEGIN
    ALTER TABLE dbo.BaseNode
        ADD SourceRtCode NVARCHAR(64) NULL;
END
GO

IF COL_LENGTH('dbo.BaseNode', 'SourceImagePath') IS NULL
BEGIN
    ALTER TABLE dbo.BaseNode
        ADD SourceImagePath NVARCHAR(1024) NULL;
END
GO
