USE ACE
GO

IF DB_NAME() <> N'ACE' SET NOEXEC ON
GO

--
-- Create table [dbo].[nakl]
--
PRINT (N'Create table [dbo].[nakl]')
GO
CREATE TABLE dbo.nakl (
  iz_nakl decimal(5) NULL,
  iz_data datetime NULL,
  sum decimal(13, 2) NULL,
  pf_data datetime NULL,
  kod char(5) NULL,
  k_p char(1) NULL,
  zvet char(60) NULL,
  kod_fc decimal(7) NULL,
  zvet_fc char(50) NULL,
  kod2 char(5) NULL,
  k_p2 char(1) NULL,
  zvet2 char(60) NULL,
  kod_fc2 decimal(7) NULL,
  zvet_fc2 char(50) NULL,
  br char(30) NULL,
  sd_data datetime NULL,
  rma decimal(1) NULL,
  iz char(9) NOT NULL,
  text char(150) NULL,
  po char(1) NULL,
  n_r char(19) NULL,
  tr char(5) NULL,
  dost_data datetime NULL,
  dost_fio char(12) NULL,
  dost_mest decimal(4) NULL,
  dost_n decimal(6) NULL,
  frm_st decimal(3) NULL,
  n_sch decimal(3) NULL,
  dat_sch datetime NULL,
  frm decimal(3) NULL,
  nom_zad char(9) NULL,
  skl_otgr int NULL,
  otm datetime NULL CONSTRAINT DF_nakl_otm DEFAULT (NULL),
  men char(2) NULL,
  ot_men decimal(1) NULL,
  zvet_korob char(50) NULL,
  data_cd_pl datetime NULL,
  tab_up int NULL,
  gl_id int NULL,
  gl_nomer char(10) NULL,
  nsid int NULL,
  nzakid int NULL,
  nn char(10) NULL,
  id_imp int NULL,
  id_izg int NULL,
  zv_tkan_za char(35) NULL,
  up_otgr int NULL,
  date_print datetime NULL,
  mtrz_h numeric(2) NULL,
  mtrz_sost numeric(1) NULL,
  mest_pr int NULL,
  prn_priv decimal(1) NULL,
  kolprint int NULL,
  kolprzv int NULL,
  idpos int NULL,
  idcar int NULL,
  psa_id int NULL,
  zakaz decimal(1) NULL,
  iz_ob nchar(9) NULL,
  iz_ob_prch int NULL,
  col_gl_id int NULL,
  col_gl_txt char(50) NULL,
  isfurn char(12) NULL,
  id_gl_vn int NULL,
  nom_gl_vn nchar(10) NULL,
  up_otgrstr char(2) NULL,
  n_1c_id char(36) NULL,
  n_1c_nomer char(20) NULL,
  frm_id int NULL,
  data_up datetime NULL,
  nNaklYear AS (CONVERT([int],substring(replace([iz],' ','0'),(1),(4)),(0))),
  idata_cdpl datetime NULL,
  r_nom char(15) NULL,
  ved_nom nchar(10) NULL,
  privl numeric(1) NULL,
  upak int NULL CONSTRAINT DF_nakl_upak DEFAULT (0),
  skl_id_1c int NULL,
  crpt_ut datetime NULL,
  id_brig int NULL CONSTRAINT DF_nakl_id_brig DEFAULT (0),
  id_divis int NULL,
  gddcID int NULL,
  CONSTRAINT PK_nakl PRIMARY KEY CLUSTERED (iz)
)
ON [PRIMARY]
GO

--
-- Create index [for_gtin_views-NonClusteredIndex-20171025-151356] on table [dbo].[nakl]
--
PRINT (N'Create index [for_gtin_views-NonClusteredIndex-20171025-151356] on table [dbo].[nakl]')
GO
CREATE INDEX [for_gtin_views-NonClusteredIndex-20171025-151356]
  ON dbo.nakl (dost_n, nNaklYear, iz, zv_tkan_za)
  ON [PRIMARY]
GO

--
-- Create index [idx_iz_data] on table [dbo].[nakl]
--
PRINT (N'Create index [idx_iz_data] on table [dbo].[nakl]')
GO
CREATE INDEX idx_iz_data
  ON dbo.nakl (iz_data)
  ON [PRIMARY]
GO

--
-- Create index [IX_nakl_iz_year] on table [dbo].[nakl]
--
PRINT (N'Create index [IX_nakl_iz_year] on table [dbo].[nakl]')
GO
CREATE INDEX IX_nakl_iz_year
  ON dbo.nakl (iz, nNaklYear)
  INCLUDE (iz_nakl, iz_data, pf_data, kod, zvet, text, nn, dost_data, dost_n, gl_id, gl_nomer, zvet_korob, kod_fc, zvet_fc, men, iz_ob, br)
  ON [PRIMARY]
GO

--
-- Create index [iz_ob_frm] on table [dbo].[nakl]
--
PRINT (N'Create index [iz_ob_frm] on table [dbo].[nakl]')
GO
CREATE INDEX iz_ob_frm
  ON dbo.nakl (iz_ob_prch, frm_id)
  ON [PRIMARY]
GO

--
-- Create index [izObIDX] on table [dbo].[nakl]
--
PRINT (N'Create index [izObIDX] on table [dbo].[nakl]')
GO
CREATE INDEX izObIDX
  ON dbo.nakl (iz_ob)
  ON [PRIMARY]
GO

--
-- Create index [izObPrchIDX] on table [dbo].[nakl]
--
PRINT (N'Create index [izObPrchIDX] on table [dbo].[nakl]')
GO
CREATE INDEX izObPrchIDX
  ON dbo.nakl (iz_ob_prch)
  ON [PRIMARY]
GO

--
-- Create index [n_br] on table [dbo].[nakl]
--
PRINT (N'Create index [n_br] on table [dbo].[nakl]')
GO
CREATE INDEX n_br
  ON dbo.nakl (br)
  ON [PRIMARY]
GO

--
-- Create index [n_NaklYear] on table [dbo].[nakl]
--
PRINT (N'Create index [n_NaklYear] on table [dbo].[nakl]')
GO
CREATE INDEX n_NaklYear
  ON dbo.nakl (nNaklYear)
  ON [PRIMARY]
GO

--
-- Create index [n_nn] on table [dbo].[nakl]
--
PRINT (N'Create index [n_nn] on table [dbo].[nakl]')
GO
CREATE INDEX n_nn
  ON dbo.nakl (nn)
  ON [PRIMARY]
GO

--
-- Create index [NonClusteredIndex-20221123-085443] on table [dbo].[nakl]
--
PRINT (N'Create index [NonClusteredIndex-20221123-085443] on table [dbo].[nakl]')
GO
CREATE INDEX [NonClusteredIndex-20221123-085443]
  ON dbo.nakl (iz)
  INCLUDE (dost_data, dost_n, nom_zad)
  ON [PRIMARY]
GO

--
-- Create index [Индекс для производительности запроса] on table [dbo].[nakl]
--
PRINT (N'Create index [Индекс для производительности запроса] on table [dbo].[nakl]')
GO
CREATE INDEX [Индекс для производительности запроса]
  ON dbo.nakl (pf_data, iz)
  INCLUDE (gl_id, gl_nomer)
  ON [PRIMARY]
GO