USE ACE
GO

IF DB_NAME() <> N'ACE' SET NOEXEC ON
GO

--
-- Create table [dbo].[razm]
--
PRINT (N'Create table [dbo].[razm]')
GO
CREATE TABLE dbo.razm (
  razm nvarchar(30) NULL,
  razm1 nchar(5) NULL,
  razm_all nchar(12) NULL,
  rost int NULL,
  r_glo int NULL,
  id_r int NULL,
  razm_dop nchar(12) NULL,
  vid_f int NULL,
  r_id int NULL,
  c_id int NULL,
  arhiv int NULL,
  idrazm int NULL,
  razm_sokr nchar(12) NULL,
  razm_naklei varchar(30) NULL
)
ON [PRIMARY]
GO

--
-- Add extended property [MS_Description] on column [dbo].[razm].[idrazm]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[razm].[idrazm]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'идентификатор', 'SCHEMA', N'dbo', 'TABLE', N'razm', 'COLUMN', N'idrazm'
GO