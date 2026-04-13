USE ACE
GO

IF DB_NAME() <> N'ACE' SET NOEXEC ON
GO

--
-- Create table [dbo].[art_norm_n]
--
PRINT (N'Create table [dbo].[art_norm_n]')
GO
CREATE TABLE dbo.art_norm_n (
  kod nchar(7) NULL,
  grup nvarchar(35) NULL,
  articul nvarchar(25) NULL,
  mod nvarchar(50) NULL,
  size_label nvarchar(3) NULL,
  po nchar(1) NULL,
  sek_shv int NULL,
  sek_vyaz3 int NULL,
  sek_vyaz5 int NULL,
  sek_vyaz6 int NULL,
  sek_vyaz7 int NULL,
  sek_vyaz10 int NULL,
  sek_vyaz12 int NULL,
  sek_vyaz62 int NULL,
  sek_vyaz71 int NULL,
  sek_vyaz72 int NULL,
  sek_vyazo int NULL,
  sek_vyaz int NULL,
  sek int NULL,
  seb numeric(13, 5) NULL,
  st int NULL,
  po1 nchar(1) NULL,
  komment nvarchar(max) NULL,
  data_sozd datetime NULL,
  diz int NULL,
  constr int NULL,
  data_obn datetime NULL,
  sek_vyaz70 int NULL,
  sek_kr int NULL,
  slogn int NULL,
  sek_vyaz14 int NULL,
  arh int NULL,
  sql_pr_add int NULL,
  date_add datetime NULL,
  komp_name nchar(50) NULL,
  annDateDel datetime NULL,
  annCompDel nvarchar(50) NULL,
  annID int IDENTITY,
  annDateAdd datetime NULL CONSTRAINT DF_art_norm_n_annDateAdd DEFAULT (getdate()),
  annCompAdd nvarchar(50) NULL CONSTRAINT DF_art_norm_n_annCompAdd DEFAULT (host_name()),
  annRecommendation nvarchar(150) NULL,
  sek_vyaz57 int NULL DEFAULT (0),
  sek_vyaz18 int NULL DEFAULT (0),
  status int NULL,
  parentId int NULL
)
ON [PRIMARY]
TEXTIMAGE_ON [PRIMARY]
GO

--
-- Create index [IDX_art_norm_n] on table [dbo].[art_norm_n]
--
PRINT (N'Create index [IDX_art_norm_n] on table [dbo].[art_norm_n]')
GO
CREATE INDEX IDX_art_norm_n
  ON dbo.art_norm_n (kod, annDateDel)
  ON [PRIMARY]
GO

--
-- Create index [IX_ArtNormNView_Articul] on table [dbo].[art_norm_n]
--
PRINT (N'Create index [IX_ArtNormNView_Articul] on table [dbo].[art_norm_n]')
GO
CREATE INDEX IX_ArtNormNView_Articul
  ON dbo.art_norm_n (articul)
  ON [PRIMARY]
GO

--
-- Create index [IX_ArtNormNView_Status] on table [dbo].[art_norm_n]
--
PRINT (N'Create index [IX_ArtNormNView_Status] on table [dbo].[art_norm_n]')
GO
CREATE INDEX IX_ArtNormNView_Status
  ON dbo.art_norm_n (status)
  ON [PRIMARY]
GO

--
-- Create index [UK_art_norm_n_annID] on table [dbo].[art_norm_n]
--
PRINT (N'Create index [UK_art_norm_n_annID] on table [dbo].[art_norm_n]')
GO
CREATE UNIQUE INDEX UK_art_norm_n_annID
  ON dbo.art_norm_n (annID)
  ON [PRIMARY]
GO