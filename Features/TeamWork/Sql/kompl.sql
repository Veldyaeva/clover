USE ACE
GO

IF DB_NAME() <> N'ACE' SET NOEXEC ON
GO

--
-- Create table [dbo].[kompl]
--
PRINT (N'Create table [dbo].[kompl]')
GO
CREATE TABLE dbo.kompl (
  kod_k char(8) NULL,
  grup_k varchar(35) NULL,
  articul_k varchar(25) NULL,
  mod_k varchar(50) NULL,
  razm_k varchar(30) NULL,
  sost_k varchar(70) NULL,
  kod1 char(8) NULL,
  kod2 char(8) NULL,
  kod3 char(8) NULL,
  kod4 char(8) NULL,
  kod5 char(8) NULL,
  compName nvarchar(100) NULL CONSTRAINT DF_kompl_compName DEFAULT (host_name()),
  kod6 char(8) NULL,
  kod7 char(8) NULL,
  kod8 char(8) NULL,
  kod9 char(8) NULL,
  kod10 char(8) NULL
)
ON [PRIMARY]
GO

--
-- Create index [NonClusteredIndex-20171025-104608] on table [dbo].[kompl]
--
PRINT (N'Create index [NonClusteredIndex-20171025-104608] on table [dbo].[kompl]')
GO
CREATE UNIQUE INDEX [NonClusteredIndex-20171025-104608]
  ON dbo.kompl (kod_k)
  ON [PRIMARY]
GO