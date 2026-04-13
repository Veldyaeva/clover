USE ACE
GO

IF DB_NAME() <> N'ACE' SET NOEXEC ON
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

--
-- Create or alter view [dbo].[View_rzu_rzv_nom_zad]
--
GO
PRINT (N'Create or alter view [dbo].[View_rzu_rzv_nom_zad]')
GO
CREATE OR ALTER VIEW dbo.View_rzu_rzv_nom_zad 
AS SELECT dost_zeh,data_zeh, n_zeh , nom_zad           ,mod, articul,up.kol as kol_pach
, case when kod_k <>'' then kod_k  else kod end kod_izd, kod as kod_pach, up.nom nom_pach, up.nom_n nom_n_pach
, 1 as SOURCE
, 0 AS proizvType
, data_r
, razm razm_pach, n_pach n_pach_nz, pach_kod, iif( kod_k <> '',articul_k, articul) articul_izd,iif( kod_k <> '',grup_k, grup) grup_izd,iif( kod_k <> '',mod_k, mod) mod_izd, grup as grup_pach
, kod_k as kod_k_pach, grup_k as grup_k_pach ,articul_k as articul_k_pach,mod_k as mod_k_pach,razm_k as razm_k_pach, iz_nakl as iz_nakl_pach, up.data_cd,data_plan, h_reestr,mest_zeh,tab_r1
, psz.id_sbit
, up.mg_zakr
, up.id_brig
, LEFT(up.kod, 7) AS kod7
, LEFT(up.kod_k, 7) AS kodK7
, IIF(LEN(TRIM(up.kod_k)) <> 0, up.kod_k, up.kod) AS kodIzd7
, YEAR(up.data_r) AS yearPach
, up.n_zvet
, up.shtrZp1 AS RzuShtrStart
, iif( kod_k <> '',up.razm_k, up.razm) razm_izd
FROM raskr_zeh_up up 
LEFT join plan_sezon_zad psz on up.nom_zad = psz.nom
UNION
SELECT rzv.dost_zeh, rzv.data_zeh, rzv.n_zeh, rzv.zad_pl as nom_zad, rzv.mod, rzv.articul, rzv.kol as kol_pach
, case when rzv.kod_k <>'' then rzv.kod_k else rzv.kod end kod_izd, rzv.kod as kod_pach, rzv.nom nom_pach, rzv.nom_n nom_n_pach
, 2 as SOURCE
, CASE WHEN vsa.grupp NOT IN (10,30,33) THEN 1 WHEN vsa.grupp IN (10) THEN 2 WHEN vsa.grupp IN (30, 33) THEN 3 ELSE -1 END AS proizvType
, rzv.data_r
, rzv.razm razm_pach, rzv.n_pach n_pach_nz, rzv.pach_kod, iif(rzv.kod_k <> '', rzv.articul_k, rzv.articul) articul_izd, iif(rzv.kod_k <> '', rzv.grup_k, rzv.grup) grup_izd, iif(rzv.kod_k <> '', rzv.mod_k, rzv.mod) mod_izd, rzv.grup as grup_pach
, rzv.kod_k as kod_k_pach, rzv.grup_k as grup_k_pach, rzv.articul_k as articul_k_pach, rzv.mod_k as mod_k_pach, rzv.razm_k as razm_k_pach, rzv.iz_nakl as iz_nakl_pach, rzv.data_got as data_cd, rzv.data_plan, rzv.h_reestr, rzv.mest_zeh, 0 as tab_r1
, rzv.id_sbit
, '' AS mg_zakr
, rzv.id_brig
, LEFT(rzv.kod, 7) AS kod7
, LEFT(rzv.kod_k, 7) AS kodK7
, IIF(LEN(TRIM(rzv.kod_k)) <> 0, rzv.kod_k, rzv.kod) AS kodIzd7
, YEAR(rzv.data_r) AS yearPach
, rzv.n_zvet
, LEFT(rzv.shtrZp1,9) AS RzuShtrStart
, iif( kod_k <> '',rzv.razm_k, rzv.razm) razm_izd
FROM raskr_zeh_vyaz rzv
  LEFT JOIN View_sp_articul vsa ON rzv.kod = vsa.kod
GO

--
-- Add extended property [MS_DiagramPane1] on view [dbo].[View_rzu_rzv_nom_zad]
--
PRINT (N'Add extended property [MS_DiagramPane1] on view [dbo].[View_rzu_rzv_nom_zad]')
GO
EXEC sys.sp_addextendedproperty N'MS_DiagramPane1', N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', 'SCHEMA', N'dbo', 'VIEW', N'View_rzu_rzv_nom_zad'
GO

--
-- Add extended property [MS_DiagramPaneCount] on view [dbo].[View_rzu_rzv_nom_zad]
--
PRINT (N'Add extended property [MS_DiagramPaneCount] on view [dbo].[View_rzu_rzv_nom_zad]')
GO
EXEC sys.sp_addextendedproperty N'MS_DiagramPaneCount', 1, 'SCHEMA', N'dbo', 'VIEW', N'View_rzu_rzv_nom_zad'
GO