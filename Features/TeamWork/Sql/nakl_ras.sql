USE ACE
GO

IF DB_NAME() <> N'ACE' SET NOEXEC ON
GO

--
-- Create table [dbo].[nakl_ras]
--
PRINT (N'Create table [dbo].[nakl_ras]')
GO
CREATE TABLE dbo.nakl_ras (
  kod_pr char(10) NULL,
  iz_nakl decimal(5) NULL,
  iz nvarchar(9) NULL CONSTRAINT DF__nakl_ras__iz__55465786 DEFAULT (202435074),
  pf_data datetime NULL,
  iz_data datetime NULL,
  kod char(8) NULL,
  grup varchar(35) NULL,
  articul varchar(25) NULL,
  mod varchar(50) NULL,
  razm varchar(30) NULL,
  kol decimal(6) NULL,
  t_seb decimal(10, 2) NULL,
  t_seb1 decimal(10, 2) NULL,
  t_seb_t decimal(10, 2) NULL,
  p_seb decimal(7, 2) NULL,
  stra_seb decimal(7, 2) NULL,
  v_seb decimal(7, 2) NULL,
  sum decimal(11, 2) NULL,
  seb_rekom decimal(9, 2) NULL,
  kod_ra nvarchar(12) NULL,
  norm_t decimal(7, 3) NULL,
  koef decimal(6, 2) NULL,
  kod_k nvarchar(8) NULL,
  grup_k varchar(35) NULL,
  articul_k varchar(25) NULL,
  mod_k varchar(50) NULL,
  razm_k varchar(30) NULL,
  sost_k nvarchar(20) NULL,
  seb_z decimal(7, 2) NULL,
  sf decimal(4) NULL,
  data_sf datetime NULL,
  dost_fio char(30) NULL,
  nakl_nom decimal(6) NULL,
  kod_sh nvarchar(15) NULL,
  kod_sh_1c nvarchar(15) NULL,
  n_pach nvarchar(11) NULL,
  pach_kod nvarchar(19) NULL,
  sd_data datetime NULL,
  tr nvarchar(8) NULL,
  sez char(2) NULL,
  nr_norm_t decimal(7, 3) NULL,
  nr_seb_z decimal(7, 2) NULL,
  nr_seb_dop decimal(7, 2) NULL,
  nr_koef decimal(7, 2) NULL,
  koef_pr decimal(7, 2) NULL,
  idpos int NULL,
  koef_d decimal(8, 4) NULL,
  id1c nvarchar(32) NULL,
  kol_uvs2 numeric(5) NULL,
  nrNaklYear AS (CONVERT([int],substring(replace([iz],' ','0'),(1),(4)),(0))),
  packageid int NULL,
  bus_seb numeric(10, 2) NULL,
  seb_rek_k decimal(9, 2) NULL,
  t_seb_k decimal(10, 2) NULL,
  skzak nvarchar(20) NULL,
  t_seb_s decimal(10, 2) NULL,
  p_seb_pl decimal(7, 2) NULL,
  v_seb_pl decimal(7, 2) NULL,
  bus_seb_pl numeric(10, 2) NULL,
  stra_seb_p decimal(7, 2) NULL,
  term_seb numeric(10, 2) NULL,
  term_seb_p decimal(7, 2) NULL,
  kod_sh1 nvarchar(15) NULL,
  kod_sh2 nvarchar(15) NULL,
  kod_sh_1c1 nvarchar(15) NULL,
  kod_sh_1c2 nvarchar(15) NULL,
  nr_id int IDENTITY
)
ON [PRIMARY]
GO

--
-- Create index [for_gtin_views-20170511-093956] on table [dbo].[nakl_ras]
--
PRINT (N'Create index [for_gtin_views-20170511-093956] on table [dbo].[nakl_ras]')
GO
CREATE INDEX [for_gtin_views-20170511-093956]
  ON dbo.nakl_ras (iz_nakl, nrNaklYear, kod, grup, articul, razm, iz, pach_kod)
  ON [PRIMARY]
GO

--
-- Create index [for_gtin_views-NonClusteredIndex-20170616-162021] on table [dbo].[nakl_ras]
--
PRINT (N'Create index [for_gtin_views-NonClusteredIndex-20170616-162021] on table [dbo].[nakl_ras]')
GO
CREATE INDEX [for_gtin_views-NonClusteredIndex-20170616-162021]
  ON dbo.nakl_ras (kod_sh_1c, kod_sh, iz)
  ON [PRIMARY]
GO

--
-- Create index [for_gtin_views-NonClusteredIndex-20171025-151104] on table [dbo].[nakl_ras]
--
PRINT (N'Create index [for_gtin_views-NonClusteredIndex-20171025-151104] on table [dbo].[nakl_ras]')
GO
CREATE INDEX [for_gtin_views-NonClusteredIndex-20171025-151104]
  ON dbo.nakl_ras (kod_sh, kod_sh_1c, kod, mod, razm, kod_k, mod_k, razm_k, iz)
  ON [PRIMARY]
GO

--
-- Create index [IDX_nr_id] on table [dbo].[nakl_ras]
--
PRINT (N'Create index [IDX_nr_id] on table [dbo].[nakl_ras]')
GO
CREATE INDEX IDX_nr_id
  ON dbo.nakl_ras (nr_id)
  ON [PRIMARY]
GO

--
-- Create index [IX_nakl_ras] on table [dbo].[nakl_ras]
--
PRINT (N'Create index [IX_nakl_ras] on table [dbo].[nakl_ras]')
GO
CREATE INDEX IX_nakl_ras
  ON dbo.nakl_ras (kod_pr)
  ON [PRIMARY]
GO

--
-- Create index [IX_nakl_ras_iz_year] on table [dbo].[nakl_ras]
--
PRINT (N'Create index [IX_nakl_ras_iz_year] on table [dbo].[nakl_ras]')
GO
CREATE INDEX IX_nakl_ras_iz_year
  ON dbo.nakl_ras (iz, nrNaklYear)
  INCLUDE (kod_pr, kol, grup, articul, mod, razm, t_seb_t, p_seb, stra_seb, v_seb, seb_rekom, norm_t, kod_k, grup_k, articul_k, mod_k, seb_z, tr, pach_kod, nr_seb_dop, koef_pr, koef_d, t_seb, t_seb1, kod)
  ON [PRIMARY]
GO

--
-- Create index [IX_nakl_ras_kod_kod_k] on table [dbo].[nakl_ras]
--
PRINT (N'Create index [IX_nakl_ras_kod_kod_k] on table [dbo].[nakl_ras]')
GO
CREATE INDEX IX_nakl_ras_kod_kod_k
  ON dbo.nakl_ras (kod, kod_k)
  INCLUDE (kod_sh)
  ON [PRIMARY]
GO

--
-- Create index [iz] on table [dbo].[nakl_ras]
--
PRINT (N'Create index [iz] on table [dbo].[nakl_ras]')
GO
CREATE CLUSTERED INDEX iz
  ON dbo.nakl_ras (iz)
  ON [PRIMARY]
GO

--
-- Create index [kod_k] on table [dbo].[nakl_ras]
--
PRINT (N'Create index [kod_k] on table [dbo].[nakl_ras]')
GO
CREATE INDEX kod_k
  ON dbo.nakl_ras (kod_k)
  ON [PRIMARY]
GO

--
-- Create index [kod_pr] on table [dbo].[nakl_ras]
--
PRINT (N'Create index [kod_pr] on table [dbo].[nakl_ras]')
GO
CREATE INDEX kod_pr
  ON dbo.nakl_ras (kod_pr)
  ON [PRIMARY]
GO

--
-- Create index [Mod] on table [dbo].[nakl_ras]
--
PRINT (N'Create index [Mod] on table [dbo].[nakl_ras]')
GO
CREATE INDEX Mod
  ON dbo.nakl_ras (mod)
  ON [PRIMARY]
GO

--
-- Create index [NonClusteredIndex-20221123-090243] on table [dbo].[nakl_ras]
--
PRINT (N'Create index [NonClusteredIndex-20221123-090243] on table [dbo].[nakl_ras]')
GO
CREATE INDEX [NonClusteredIndex-20221123-090243]
  ON dbo.nakl_ras (iz)
  INCLUDE (kol)
  ON [PRIMARY]
GO

--
-- Create index [nr_grup] on table [dbo].[nakl_ras]
--
PRINT (N'Create index [nr_grup] on table [dbo].[nakl_ras]')
GO
CREATE INDEX nr_grup
  ON dbo.nakl_ras (grup)
  INCLUDE (t_seb, iz)
  ON [PRIMARY]
GO

--
-- Create index [nr_NaklYear] on table [dbo].[nakl_ras]
--
PRINT (N'Create index [nr_NaklYear] on table [dbo].[nakl_ras]')
GO
CREATE INDEX nr_NaklYear
  ON dbo.nakl_ras (nrNaklYear)
  ON [PRIMARY]
GO

--
-- Create index [pach_kod] on table [dbo].[nakl_ras]
--
PRINT (N'Create index [pach_kod] on table [dbo].[nakl_ras]')
GO
CREATE INDEX pach_kod
  ON dbo.nakl_ras (pach_kod)
  INCLUDE (kol)
  ON [PRIMARY]
GO

--
-- Create index [pf_data - iz - kod_sh] on table [dbo].[nakl_ras]
--
PRINT (N'Create index [pf_data - iz - kod_sh] on table [dbo].[nakl_ras]')
GO
CREATE INDEX [pf_data - iz - kod_sh]
  ON dbo.nakl_ras (pf_data)
  INCLUDE (iz, kod_sh)
  ON [PRIMARY]
GO