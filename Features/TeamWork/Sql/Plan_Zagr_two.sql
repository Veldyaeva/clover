USE ACE
GO

IF DB_NAME() <> N'ACE' SET NOEXEC ON
GO

--
-- Create table [dbo].[plan_zagr_two]
--
PRINT (N'Create table [dbo].[plan_zagr_two]')
GO
CREATE TABLE dbo.plan_zagr_two (
  idrecord numeric IDENTITY,
  nom int NULL,
  nom_n int NULL CONSTRAINT DF_plan_zagr_two_nom_n DEFAULT (0),
  articul nvarchar(25) NULL,
  mod nvarchar(50) NULL,
  kod nvarchar(10) NULL,
  no numeric(3) NULL,
  npo numeric(3) NULL,
  operac nvarchar(200) NULL,
  kod_ob nvarchar(10) NULL,
  sek int NULL,
  spec nvarchar(10) NULL,
  kol int NULL,
  n_chasi numeric(6, 2) NULL,
  tip_op int NULL,
  tab int NULL,
  data_rab datetime NULL,
  data_isp datetime NULL,
  delenie int NULL,
  pzIdBrig int NULL,
  brig nvarchar(30) NULL,
  pzIdBrigUt int NULL,
  brigUt nvarchar(30) NULL,
  shtr nvarchar(12) NULL,
  idrecord_osn numeric NULL,
  data_pz datetime NULL,
  komp_add nvarchar(50) NULL CONSTRAINT DF_plan_zagr_two_komp_add DEFAULT (host_name()),
  date_Add datetime NULL CONSTRAINT DF_plan_zagr_two_date_Add DEFAULT (getdate()),
  id_ml_op int NULL,
  dateNazn datetime NULL,
  dateStart datetime NULL,
  dateEnd datetime NULL,
  dateML datetime NULL,
  dateMLUt datetime NULL,
  dateMast datetime NULL,
  rKol int NULL,
  kodOb int NULL,
  sekNazn int NULL,
  chasNazn numeric(6, 2) NULL,
  kolNazn int NULL,
  pztNrID int NULL,
  pbpID int NULL,
  pztUpdDate datetime NULL DEFAULT (getdate()),
  pztVidPr char(2) NULL,
  annID int NULL,
  kodObMain int NULL
)
ON [PRIMARY]
GO

--
-- Create index [ClusteredIndex-plan_zagr_two  idrecord] on table [dbo].[plan_zagr_two]
--
PRINT (N'Create index [ClusteredIndex-plan_zagr_two  idrecord] on table [dbo].[plan_zagr_two]')
GO
CREATE CLUSTERED INDEX [ClusteredIndex-plan_zagr_two  idrecord]
  ON dbo.plan_zagr_two (idrecord)
  ON [PRIMARY]
GO

--
-- Create index [delenieShtrIDX] on table [dbo].[plan_zagr_two]
--
PRINT (N'Create index [delenieShtrIDX] on table [dbo].[plan_zagr_two]')
GO
CREATE INDEX delenieShtrIDX
  ON dbo.plan_zagr_two (delenie, shtr)
  ON [PRIMARY]
GO

--
-- Create index [idRecOsn_IDX] on table [dbo].[plan_zagr_two]
--
PRINT (N'Create index [idRecOsn_IDX] on table [dbo].[plan_zagr_two]')
GO
CREATE INDEX idRecOsn_IDX
  ON dbo.plan_zagr_two (idrecord_osn)
  ON [PRIMARY]
GO

--
-- Create index [IDX_plan_zagr_two] on table [dbo].[plan_zagr_two]
--
PRINT (N'Create index [IDX_plan_zagr_two] on table [dbo].[plan_zagr_two]')
GO
CREATE INDEX IDX_plan_zagr_two
  ON dbo.plan_zagr_two (dateMast, dateEnd)
  ON [PRIMARY]
GO

--
-- Create index [IDX_plan_zagr_two2] on table [dbo].[plan_zagr_two]
--
PRINT (N'Create index [IDX_plan_zagr_two2] on table [dbo].[plan_zagr_two]')
GO
CREATE INDEX IDX_plan_zagr_two2
  ON dbo.plan_zagr_two (pztNrID, tab)
  ON [PRIMARY]
GO

--
-- Create index [nomIDX] on table [dbo].[plan_zagr_two]
--
PRINT (N'Create index [nomIDX] on table [dbo].[plan_zagr_two]')
GO
CREATE INDEX nomIDX
  ON dbo.plan_zagr_two (nom)
  ON [PRIMARY]
GO

--
-- Create index [planZagrTwoIDX1] on table [dbo].[plan_zagr_two]
--
PRINT (N'Create index [planZagrTwoIDX1] on table [dbo].[plan_zagr_two]')
GO
CREATE INDEX planZagrTwoIDX1
  ON dbo.plan_zagr_two (tip_op)
  INCLUDE (kod, no, npo)
  ON [PRIMARY]
GO

--
-- Create index [pzt_DateMastEndIDX] on table [dbo].[plan_zagr_two]
--
PRINT (N'Create index [pzt_DateMastEndIDX] on table [dbo].[plan_zagr_two]')
GO
CREATE INDEX pzt_DateMastEndIDX
  ON dbo.plan_zagr_two (pzIdBrig, dateMast, dateEnd)
  ON [PRIMARY]
GO

--
-- Create index [pztTab_IDX] on table [dbo].[plan_zagr_two]
--
PRINT (N'Create index [pztTab_IDX] on table [dbo].[plan_zagr_two]')
GO
CREATE INDEX pztTab_IDX
  ON dbo.plan_zagr_two (tab)
  ON [PRIMARY]
GO

--
-- Create index [shtrIDX] on table [dbo].[plan_zagr_two]
--
PRINT (N'Create index [shtrIDX] on table [dbo].[plan_zagr_two]')
GO
CREATE INDEX shtrIDX
  ON dbo.plan_zagr_two (shtr)
  ON [PRIMARY]
GO

--
-- Create index [tabBrigShtrIDX] on table [dbo].[plan_zagr_two]
--
PRINT (N'Create index [tabBrigShtrIDX] on table [dbo].[plan_zagr_two]')
GO
CREATE INDEX tabBrigShtrIDX
  ON dbo.plan_zagr_two (tab, brig, shtr)
  ON [PRIMARY]
GO