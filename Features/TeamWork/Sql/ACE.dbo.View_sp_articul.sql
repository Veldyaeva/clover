CREATE VIEW dbo.View_sp_articul 
AS SELECT
  SUBSTRING(sa.kod,1,7)           AS ko,
  SUBSTRING(sa.kod,1,6) + CASE WHEN sa.po=' ' THEN SUBSTRING(sa.kod,7,1) ELSE isnull(sa.po,'') END AS kodd,
  SUBSTRING(sa.kod,1,6) + CASE WHEN isnull(TRIM(sa.po),'')='' THEN '0' ELSE isnull(sa.po,'') END          AS kodd_rt,
  CASE WHEN sa.GRUPP = 8 OR sa.baza NOT IN(3,6,9) THEN SUBSTRING(CAST(YEAR(GETDATE()) AS CHAR(4)),4,1) ELSE '0' END AS baz,
  ISNULL(sa.seb_rekom,0)          AS seb_rekom,
  sa.annId,
  sa.kod, sa.po, sa.grup, sa.articul, sa.mod, sa.razm, sa.sost, sa.sost2_old,
  sa.gost, sa.norm_t, sa.norm_r, sa.sek_shv, sa.sh_r, sa.seb_r, sa.norm_n, sa.kat_n,
  sa.seb_n, sa.seb_z, sa.koef, sa.seb_rekom AS expr1, sa.seb_proizv, sa.seb_z_s, sa.sek,
  sa.sek_vyaz5, sa.sek_vyaz7, sa.sek_vyaz12, sa.sek_vyaz, sa.pict, sa.kod_shtr, sa.kod_shtr_k,
  sa.gr, x, sa.text, sa.kle, sa.grupp, sa.p, sa.v, sa.s, sa.kod_tov, sa.gruppa, sa.p_gruppa,
  sa.text_m, sa.tkb, sa.kod_t1, sa.tkb1, sa.norm_t1, sa.opis_t1, sa.kod_t2, sa.tkb2,
  sa.norm_t2, sa.opis_t2, sa.kod_t3, sa.tkb3, sa.norm_t3, sa.opis_t3, sa.kod_t4, sa.tkb4,
  sa.norm_t4, sa.opis_t4, sa.baza, sa.kod_v, sa.kod_t5, sa.tkb5, sa.norm_t5, sa.opis_t5,
  sa.kod_t6, sa.tkb6, sa.norm_t6, sa.opis_t6, sa.seb_dop, sa.seb_t1, sa.seb_t3, sa.seb_t2,
  sa.seb_t4, sa.seb_t5, sa.seb_t6, sa.sek_vyaz6, sa.sek_vyaz10, sa.sek_vyazo, sa.normaPryz,
  sa.id_gost, sa.id_svyaz, sa.koef_pr, sa.ob_izd, sa.stavka_nds, sa.kod_t7, sa.tkb7,
  sa.norm_t7, sa.seb_t7, sa.opis_t7, sa.sposob_up, sa.id_country, sa.old_prch, sa.sek_vyaz70,
  sa.sek_vyaz3, sa.koef_d, sa.st_nds, sa.upd_razm, sa.gl_rekom, sa.sek_kr, sa.arh, sa.nds,
  sa.ed_izm, sa.brak_t1, sa.brak_t2, sa.brak_t3, sa.brak_t4, sa.brak_t5, sa.brak_t6,
  sa.brak_t7, sa.brak_avg, sa.cena_prdc, sa.komb_det, sa.komb_izd, sa.kod_t,
  sa.k_kg_m1, sa.k_kg_m2, sa.k_kg_m3, sa.k_kg_m4, sa.k_kg_m5, sa.k_kg_m6, sa.k_kg_m7,
  sa.sost2, sa.is_furnit, sa.is_upak, sa.sek_vyaz14, sa.gcg_id, sa.gbm_id, sa.gcgp_id,
  sa.gbt_id, sa.bus, sa.stra, sa.p_pres, sa.seb_usl, sa.art_segm, sa.art_family, sa.art_class,
  sa.art_block, sa.scid_n, sa.kod_tnved, sa.dateOpis, sa.mtrl_up, sa.mtrl_pdkl, sa.vid_obuv,
  sa.mtrl_down, sa.sql_pr_add, sa.sql_pr_upd, sa.komp_name, sa.date_add, sa.sek_vyaz62,
  sa.sek_vyaz71, sa.sek_vyaz72, sa.sost3, sa.kruj, sa.ag_id, sa.sek_cord, sa.kod_lv3,
  sa.norm_cord, sa.tgm_id_n, sa.dateutvkk, sa.sum_zarpl, sa.sum_dopopl, sa.sum_strvznos,
  sa.sum_sebraskr, sa.sum_komplnum,
  IIF(sa.kod_v=1,gm.frm_v,gm.frm_s) AS frmIzgID,
  f.frm_naimen                AS frmIzgName
,isnull(sa.sum_zarpl,0)+isnull(sa.sum_dopopl,0)+isnull(sa.sum_strvznos,0)+isnull(sa.sum_sebraskr,0)+isnull(sa.sum_komplnum,0) as cost_cut_sew_rub
FROM dbo.sp_articul sa
LEFT JOIN grup_men gm ON sa.grupp = gm.men_int
LEFT JOIN global.planeta.dbo.firms f ON IIF(sa.kod_v=1,gm.frm_v,gm.frm_s) = f.frm_id
WHERE ISNULL(sa.arh,0) = 0 
GO

EXEC sys.sp_addextendedproperty N'MS_DiagramPane1', N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[59] 4[3] 2[21] 3) )"
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
         Begin Table = "sp_articul"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 317
               Right = 205
            End
            DisplayFlags = 280
            TopColumn = 146
         End
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
', 'SCHEMA', N'dbo', 'VIEW', N'View_sp_articul'
GO

EXEC sys.sp_addextendedproperty N'MS_DiagramPaneCount', 1, 'SCHEMA', N'dbo', 'VIEW', N'View_sp_articul'
GO