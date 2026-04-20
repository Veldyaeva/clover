CREATE TABLE ACE.dbo.knitWorkingShiftNew (
  kwsID INT IDENTITY
 ,kwsDateAdd DATETIME NULL CONSTRAINT DF_knitWorkingShiftNew_kwsDateAdd DEFAULT (GETDATE())
 ,kwsCompAdd NVARCHAR(100) NULL CONSTRAINT DF_knitWorkingShiftNew_kwsCompAdd DEFAULT (HOST_NAME())
 ,kwsTabStart INT NULL
 ,kwsTabEnd INT NULL
 ,kwsKmaID INT NULL
 ,kwsKmsID INT NULL
 ,kwsDateStart DATETIME NULL
 ,kwsDateEnd DATETIME NULL
 ,kwsDel INT NULL CONSTRAINT DF_knitWorkingShiftNew_kwsDel DEFAULT (0)
 ,kwsDateDel DATETIME NULL
 ,kwsCompDel NVARCHAR(100) NULL
) ON [PRIMARY]
GO

CREATE UNIQUE INDEX UK_knitWorkingShiftNew
ON ACE.dbo.knitWorkingShiftNew (kwsTabStart, kwsDateStart)
ON [PRIMARY]
GO