USE ACE
GO

IF DB_NAME() <> N'ACE' SET NOEXEC ON
GO

--
-- Create table [dbo].[norm_rasz]
--
PRINT (N'Create table [dbo].[norm_rasz]')
GO
CREATE TABLE dbo.norm_rasz (
  kod char(7) NULL,
  kod_o char(3) NULL,
  kod_podr int NULL,
  kod_proizv int NULL,
  text char(200) NULL,
  sek int NULL,
  seb numeric(21, 5) NULL,
  n int NULL,
  n_ch int NULL,
  n1 int NULL,
  seb_s numeric(21, 5) NULL,
  razryd int NULL,
  spec char(3) NULL,
  obor char(35) NULL,
  sek12 int NULL,
  sek7 int NULL,
  sek5 int NULL,
  kod_ob int NULL,
  sql_pr_add int NULL,
  date_add datetime NULL,
  komp_name nchar(50) NULL,
  nrID int IDENTITY,
  nrDateDel datetime NULL,
  nrCompDel nvarchar(50) NULL,
  nrDateAdd datetime NULL CONSTRAINT DF_norm_rasz_nrDateAdd DEFAULT (getdate()),
  nrCompAdd nvarchar(50) NULL CONSTRAINT DF_norm_rasz_nrCompAdd DEFAULT (host_name()),
  annId int NULL
)
ON [PRIMARY]
GO

--
-- Create index [IX_norm_rasz_annId_nrID_kod] on table [dbo].[norm_rasz]
--
PRINT (N'Create index [IX_norm_rasz_annId_nrID_kod] on table [dbo].[norm_rasz]')
GO
CREATE INDEX IX_norm_rasz_annId_nrID_kod
  ON dbo.norm_rasz (annId, nrID, kod)
  ON [PRIMARY]
GO

--
-- Create index [kodIDX] on table [dbo].[norm_rasz]
--
PRINT (N'Create index [kodIDX] on table [dbo].[norm_rasz]')
GO
CREATE INDEX kodIDX
  ON dbo.norm_rasz (kod)
  INCLUDE (n)
  ON [PRIMARY]
GO

--
-- Create index [kodNN1_IDX] on table [dbo].[norm_rasz]
--
PRINT (N'Create index [kodNN1_IDX] on table [dbo].[norm_rasz]')
GO
CREATE INDEX kodNN1_IDX
  ON dbo.norm_rasz (kod, n, n1)
  ON [PRIMARY]
GO

--
-- Create index [kodObIDX] on table [dbo].[norm_rasz]
--
PRINT (N'Create index [kodObIDX] on table [dbo].[norm_rasz]')
GO
CREATE INDEX kodObIDX
  ON dbo.norm_rasz (kod_ob)
  ON [PRIMARY]
GO

--
-- Create index [nIDX] on table [dbo].[norm_rasz]
--
PRINT (N'Create index [nIDX] on table [dbo].[norm_rasz]')
GO
CREATE INDEX nIDX
  ON dbo.norm_rasz (n)
  ON [PRIMARY]
GO

--
-- Create index [nr_IDX] on table [dbo].[norm_rasz]
--
PRINT (N'Create index [nr_IDX] on table [dbo].[norm_rasz]')
GO
CREATE INDEX nr_IDX
  ON dbo.norm_rasz (nrDateDel)
  INCLUDE (kod, kod_o, kod_podr, kod_proizv, text, sek, seb, n, n_ch, n1, seb_s, razryd, spec, obor, sek12, sek7, sek5, kod_ob, sql_pr_add, date_add, komp_name, nrID, nrCompDel)
  ON [PRIMARY]
GO

--
-- Create index [nrID] on table [dbo].[norm_rasz]
--
PRINT (N'Create index [nrID] on table [dbo].[norm_rasz]')
GO
CREATE INDEX nrID
  ON dbo.norm_rasz (nrID)
  ON [PRIMARY]
GO