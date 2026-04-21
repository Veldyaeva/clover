USE ACE
GO

IF DB_NAME() <> N'ACE' SET NOEXEC ON
GO

--
-- Create table [dbo].[plan_sezon_all]
--
PRINT (N'Create table [dbo].[plan_sezon_all]')
GO
CREATE TABLE dbo.plan_sezon_all (
  psa_id int IDENTITY,
  n int NULL CONSTRAINT DF_plan_sezon_all_test_n DEFAULT (0),
  nn char(10) NOT NULL CONSTRAINT DF_plan_sezon_all_test_nn DEFAULT (''),
  kodd char(7) NULL CONSTRAINT DF_plan_sezon_all_test_kodd DEFAULT (''),
  grup varchar(30) NULL CONSTRAINT DF_plan_sezon_all_test_grup DEFAULT (''),
  articul varchar(25) NULL CONSTRAINT DF_plan_sezon_all_test_articul DEFAULT (''),
  mod varchar(50) NULL CONSTRAINT DF_plan_sezon_all_test_mod DEFAULT (''),
  mod_v varchar(3) NULL CONSTRAINT DF_plan_sezon_all_test_mod_v DEFAULT (''),
  komb_izd varchar(1) NULL CONSTRAINT DF_plan_sezon_all_test_komb_izd DEFAULT (''),
  rost varchar(15) NULL CONSTRAINT DF_plan_sezon_all_test_rost DEFAULT (''),
  tm int NULL CONSTRAINT DF_plan_sezon_all_test_tm DEFAULT (0),
  kol int NULL CONSTRAINT DF_plan_sezon_all_test_kol DEFAULT (0),
  men char(2) NULL CONSTRAINT DF_plan_sezon_all_test_men DEFAULT (''),
  slogn numeric(1) NULL CONSTRAINT DF_plan_sezon_all_test_slogn DEFAULT (0),
  tema varchar(15) NULL CONSTRAINT DF_plan_sezon_all_test_tema DEFAULT (''),
  tema_kod int NULL CONSTRAINT DF_plan_sezon_all_test_tema_kod DEFAULT (0),
  pict_prn varchar(1) NULL CONSTRAINT DF_plan_sezon_all_test_pict_prn DEFAULT (''),
  kod_tk varchar(3) NULL CONSTRAINT DF_plan_sezon_all_test_kod_tk DEFAULT (''),
  tkan varchar(40) NULL CONSTRAINT DF_plan_sezon_all_test_tkan DEFAULT (''),
  koment varchar(254) NULL CONSTRAINT DF_plan_sezon_all_test_koment DEFAULT (''),
  prim varchar(254) NULL CONSTRAINT DF_plan_sezon_all_test_prim DEFAULT (''),
  seb decimal(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_seb DEFAULT (0),
  seb_rek decimal(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_seb_rek DEFAULT (0),
  seb_mo decimal(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_seb_mo DEFAULT (0),
  seb_rozn decimal(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_seb_rozn DEFAULT (0),
  seb_fa numeric(9, 2) NULL CONSTRAINT DF_plan_sezon_all_test_seb_fa DEFAULT (0),
  seb_rek_fa numeric(9, 2) NULL CONSTRAINT DF_plan_sezon_all_test_seb_rek_fa DEFAULT (0),
  sost varchar(80) NULL CONSTRAINT DF_plan_sezon_all_test_sost DEFAULT (''),
  zvet_all numeric(1) NULL CONSTRAINT DF_plan_sezon_all_test_zvet_all DEFAULT (0),
  edIzm varchar(4) NULL CONSTRAINT DF_plan_sezon_all_test_edIzm DEFAULT (''),
  mod_blok varchar(10) NULL CONSTRAINT DF_plan_sezon_all_test_mod_blok DEFAULT (''),
  komb_osn int NULL CONSTRAINT DF_plan_sezon_all_test_komb_osn DEFAULT (0),
  z_sek int NULL CONSTRAINT DF_plan_sezon_all_test_z_sek DEFAULT (0),
  day_po tinyint NULL CONSTRAINT DF_plan_sezon_all_test_day_po DEFAULT (0),
  datautvv datetime NULL,
  kod1 varchar(1) NULL CONSTRAINT DF_plan_sezon_all_test_kod1 DEFAULT (''),
  text_mo varchar(100) NULL CONSTRAINT DF_plan_sezon_all_test_text_mo DEFAULT (''),
  po varchar(1) NULL CONSTRAINT DF_plan_sezon_all_test_po DEFAULT (''),
  sek int NULL CONSTRAINT DF_plan_sezon_all_test_sek DEFAULT (0),
  sek_inter int NULL CONSTRAINT DF_plan_sezon_all_test_sek_inter DEFAULT (0),
  sek_shv int NULL CONSTRAINT DF_plan_sezon_all_test_sek_shv DEFAULT (0),
  sek_vyaz int NULL CONSTRAINT DF_plan_sezon_all_test_sek_vyaz DEFAULT (0),
  sek_v_km1 int NULL CONSTRAINT DF_plan_sezon_all_test_sek_v_km1 DEFAULT (0),
  sek_v_km2 int NULL CONSTRAINT DF_plan_sezon_all_test_sek_v_km2 DEFAULT (0),
  vyaz_km1 int NULL CONSTRAINT DF_plan_sezon_all_test_vyaz_km1 DEFAULT (0),
  vyaz_km2 int NULL CONSTRAINT DF_plan_sezon_all_test_vyaz_km2 DEFAULT (0),
  norm_t decimal(10, 5) NULL CONSTRAINT DF_plan_sezon_all_test_norm_t DEFAULT (0),
  k_plot decimal(4, 2) NULL CONSTRAINT DF_plan_sezon_all_test_k_plot DEFAULT (0),
  v tinyint NULL CONSTRAINT DF_plan_sezon_all_test_v DEFAULT (0),
  v_steg int NULL CONSTRAINT DF_plan_sezon_all_test_v_steg DEFAULT (0),
  kol_v decimal(4, 1) NULL CONSTRAINT DF_plan_sezon_all_test_kol_v DEFAULT (0),
  stra tinyint NULL CONSTRAINT DF_plan_sezon_all_test_stra DEFAULT (0),
  stir tinyint NULL CONSTRAINT DF_plan_sezon_all_test_stir DEFAULT (0),
  p tinyint NULL CONSTRAINT DF_plan_sezon_all_test_p DEFAULT (0),
  kol_p decimal(4, 1) NULL CONSTRAINT DF_plan_sezon_all_test_kol_p DEFAULT (0),
  up_priv tinyint NULL CONSTRAINT DF_plan_sezon_all_test_UP_PRIV DEFAULT (0),
  p_tamp tinyint NULL CONSTRAINT DF_plan_sezon_all_test_p_tamp DEFAULT (0),
  poet tinyint NULL CONSTRAINT DF_plan_sezon_all_test_poet DEFAULT (0),
  kol_zv decimal(4, 1) NULL CONSTRAINT DF_plan_sezon_all_test_kol_zv DEFAULT (0),
  form_zv varchar(1) NULL CONSTRAINT DF_plan_sezon_all_test_form_zv DEFAULT (''),
  spez_o varchar(2) NULL CONSTRAINT DF_plan_sezon_all_test_spez_o DEFAULT (''),
  nabiv_all tinyint NULL CONSTRAINT DF_plan_sezon_all_test_nabiv_all DEFAULT (0),
  tms_id int NULL CONSTRAINT DF_plan_sezon_all_test_tms_id DEFAULT (0),
  tp_id int NULL CONSTRAINT DF_plan_sezon_all_test_tp_id DEFAULT (0),
  ts_id int NULL CONSTRAINT DF_plan_sezon_all_test_ts_id DEFAULT (0),
  tk_id int NULL CONSTRAINT DF_plan_sezon_all_test_tk_id DEFAULT (0),
  k_p decimal(3, 1) NULL CONSTRAINT DF_plan_sezon_all_test_k_p DEFAULT (0),
  kod_stir int NULL CONSTRAINT DF_plan_sezon_all_test_kod_stir DEFAULT (0),
  data_cd datetime NULL,
  tg_id varchar(3) NULL CONSTRAINT DF_plan_sezon_all_test_tg_id DEFAULT (''),
  tg_id_n int NULL CONSTRAINT DF_plan_sezon_all_test_tg_id_n DEFAULT (0),
  tgm_id_n int NULL CONSTRAINT DF_plan_sezon_all_test_tgm_id_n DEFAULT (0),
  tgm_id varchar(3) NULL CONSTRAINT DF_plan_sezon_all_test_tgm_id DEFAULT (''),
  ta_id int NULL CONSTRAINT DF_plan_sezon_all_test_ta_id DEFAULT (0),
  tb_id varchar(20) NULL CONSTRAINT DF_plan_sezon_all_test_tb_id DEFAULT (''),
  tb_kod int NULL CONSTRAINT DF_plan_sezon_all_test_tb_kod DEFAULT (0),
  prn tinyint NULL CONSTRAINT DF_plan_sezon_all_test_prn DEFAULT (0),
  mes_9 int NULL CONSTRAINT DF_plan_sezon_all_test_mes_9 DEFAULT (0),
  mes_10 int NULL CONSTRAINT DF_plan_sezon_all_test_mes_10 DEFAULT (0),
  mes_11 int NULL CONSTRAINT DF_plan_sezon_all_test_mes_11 DEFAULT (0),
  mes_12 int NULL CONSTRAINT DF_plan_sezon_all_test_mes_12 DEFAULT (0),
  mes1 int NULL CONSTRAINT DF_plan_sezon_all_test_mes1 DEFAULT (0),
  mes2 int NULL CONSTRAINT DF_plan_sezon_all_test_mes2 DEFAULT (0),
  mes3 int NULL CONSTRAINT DF_plan_sezon_all_test_mes3 DEFAULT (0),
  mes4 int NULL CONSTRAINT DF_plan_sezon_all_test_mes4 DEFAULT (0),
  mes5 int NULL CONSTRAINT DF_plan_sezon_all_test_mes5 DEFAULT (0),
  mes6 int NULL CONSTRAINT DF_plan_sezon_all_test_mes6 DEFAULT (0),
  mes7 int NULL CONSTRAINT DF_plan_sezon_all_test_mes7 DEFAULT (0),
  mes8 int NULL CONSTRAINT DF_plan_sezon_all_test_mes8 DEFAULT (0),
  mes9 int NULL CONSTRAINT DF_plan_sezon_all_test_mes9 DEFAULT (0),
  mes10 int NULL CONSTRAINT DF_plan_sezon_all_test_mes10 DEFAULT (0),
  mes11 int NULL CONSTRAINT DF_plan_sezon_all_test_mes11 DEFAULT (0),
  mes12 int NULL CONSTRAINT DF_plan_sezon_all_test_mes12 DEFAULT (0),
  bus int NULL CONSTRAINT DF_plan_sezon_all_bus DEFAULT (0),
  tkb1 varchar(3) NULL CONSTRAINT DF_plan_sezon_all_test_tkb1 DEFAULT (''),
  norm_t1 decimal(10, 5) NULL CONSTRAINT DF_plan_sezon_all_test_norm_t1 DEFAULT (0),
  vid1 tinyint NULL CONSTRAINT DF_plan_sezon_all_test_vid1 DEFAULT (0),
  seb_t1 decimal(7, 2) NULL CONSTRAINT DF_plan_sezon_all_test_seb_t1 DEFAULT (0),
  opis_t1 varchar(30) NULL CONSTRAINT DF_plan_sezon_all_test_opis_t1 DEFAULT (''),
  hh1 int NULL CONSTRAINT DF_plan_sezon_all_test_hh1 DEFAULT (0),
  k_plot1 numeric(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_k_plot1 DEFAULT (0),
  kol_bus int NULL CONSTRAINT DF_plan_sezon_all_kol_bus DEFAULT (0),
  tkb2 varchar(3) NULL CONSTRAINT DF_plan_sezon_all_test_tkb2 DEFAULT (''),
  norm_t2 decimal(10, 5) NULL CONSTRAINT DF_plan_sezon_all_test_norm_t2 DEFAULT (0),
  vid2 tinyint NULL CONSTRAINT DF_plan_sezon_all_test_vid2 DEFAULT (0),
  seb_t2 decimal(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_seb_t2 DEFAULT (0),
  opis_t2 varchar(30) NULL CONSTRAINT DF_plan_sezon_all_test_opis_t2 DEFAULT (''),
  hh2 int NULL CONSTRAINT DF_plan_sezon_all_test_hh2 DEFAULT (0),
  k_plot2 numeric(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_k_plot2 DEFAULT (0),
  gofp int NULL CONSTRAINT DF_plan_sezon_all_gofp DEFAULT (0),
  tkb3 varchar(3) NULL CONSTRAINT DF_plan_sezon_all_test_tkb3 DEFAULT (''),
  norm_t3 decimal(10, 5) NULL CONSTRAINT DF_plan_sezon_all_test_norm_t3 DEFAULT (0),
  vid3 tinyint NULL CONSTRAINT DF_plan_sezon_all_test_vid3 DEFAULT (0),
  seb_t3 decimal(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_seb_t3 DEFAULT (0),
  opis_t3 varchar(30) NULL CONSTRAINT DF_plan_sezon_all_test_opis_t3 DEFAULT (''),
  hh3 int NULL CONSTRAINT DF_plan_sezon_all_test_hh3 DEFAULT (0),
  k_plot3 numeric(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_k_plot3 DEFAULT (0),
  kol_gofp int NULL CONSTRAINT DF_plan_sezon_all_kol_gofp DEFAULT (0),
  tkb4 varchar(3) NULL CONSTRAINT DF_plan_sezon_all_test_tkb4 DEFAULT (''),
  norm_t4 decimal(10, 5) NULL CONSTRAINT DF_plan_sezon_all_test_norm_t4 DEFAULT (0),
  vid4 tinyint NULL CONSTRAINT DF_plan_sezon_all_test_vid4 DEFAULT (0),
  seb_t4 decimal(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_seb_t4 DEFAULT (0),
  opis_t4 varchar(30) NULL CONSTRAINT DF_plan_sezon_all_test_opis_t4 DEFAULT (''),
  hh4 int NULL CONSTRAINT DF_plan_sezon_all_test_hh4 DEFAULT (0),
  k_plot4 numeric(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_k_plot4 DEFAULT (0),
  t_art_prev varchar(150) NULL CONSTRAINT DF_plan_sezon_all_test_kod_t5 DEFAULT (''),
  tkb5 varchar(3) NULL CONSTRAINT DF_plan_sezon_all_test_tkb5 DEFAULT (''),
  norm_t5 decimal(10, 5) NULL CONSTRAINT DF_plan_sezon_all_test_norm_t5 DEFAULT (0),
  vid5 tinyint NULL CONSTRAINT DF_plan_sezon_all_test_vid5 DEFAULT (0),
  seb_t5 decimal(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_seb_t5 DEFAULT (0),
  opis_t5 varchar(30) NULL CONSTRAINT DF_plan_sezon_all_test_opis_t5 DEFAULT (''),
  hh5 int NULL CONSTRAINT DF_plan_sezon_all_test_hh5 DEFAULT (0),
  k_plot5 numeric(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_k_plot5 DEFAULT (0),
  kol_za int NULL CONSTRAINT DF_plan_sezon_all_test_kol_za DEFAULT (0),
  laz tinyint NULL CONSTRAINT DF_plan_sezon_all_test_pryaz_id DEFAULT (0),
  pere_id int NULL CONSTRAINT DF_plan_sezon_all_test_pere_id DEFAULT (0),
  kal_d_stir decimal(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_spvya_id DEFAULT (0),
  klas_id tinyint NULL CONSTRAINT DF_plan_sezon_all_test_klas_id DEFAULT (0),
  sek_n int NULL CONSTRAINT DF_plan_sezon_all_test_sek_n DEFAULT (0),
  ves_n decimal(9, 3) NULL CONSTRAINT DF_plan_sezon_all_test_ves_n DEFAULT (0),
  kf1 decimal(5, 2) NULL CONSTRAINT DF_plan_sezon_all_test_kf1 DEFAULT (0),
  kf2 decimal(5, 2) NULL CONSTRAINT DF_plan_sezon_all_test_kf2 DEFAULT (0),
  kf3 decimal(5, 2) NULL CONSTRAINT DF_plan_sezon_all_test_kf3 DEFAULT (0),
  kf4 decimal(5, 2) NULL CONSTRAINT DF_plan_sezon_all_test_kf4 DEFAULT (0),
  kf5 decimal(5, 2) NULL CONSTRAINT DF_plan_sezon_all_test_kf5 DEFAULT (0),
  p_priv tinyint NULL CONSTRAINT DF_plan_sezon_all_test_zv1 DEFAULT (0),
  v_priv tinyint NULL CONSTRAINT DF_plan_sezon_all_test_zv2 DEFAULT (0),
  id_decor int NULL CONSTRAINT DF_plan_sezon_all_test_zv3 DEFAULT (''),
  is_blocked bit NULL CONSTRAINT DF_plan_sezon_all_test_zv4 DEFAULT (0),
  pokraska int NULL CONSTRAINT DF_plan_sezon_all_test_zv5 DEFAULT (0),
  p_pres tinyint NULL CONSTRAINT DF_plan_sezon_all_p_pres DEFAULT (0),
  vid6 tinyint NULL CONSTRAINT DF_plan_sezon_all_test_vid6 DEFAULT (0),
  kf6 decimal(5, 2) NULL CONSTRAINT DF_plan_sezon_all_test_kf6 DEFAULT (0),
  norm_t6 decimal(10, 5) NULL CONSTRAINT DF_plan_sezon_all_test_norm_t6 DEFAULT (0),
  nom_lekal int NULL CONSTRAINT DF_plan_sezon_all_test_zv6 DEFAULT (''),
  tkb6 varchar(3) NULL CONSTRAINT DF_plan_sezon_all_test_tkb6 DEFAULT (''),
  seb_t6 decimal(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_seb_t6 DEFAULT (0),
  opis_t6 varchar(30) NULL CONSTRAINT DF_plan_sezon_all_test_opis_t6 DEFAULT (''),
  hh6 int NULL CONSTRAINT DF_plan_sezon_all_test_hh6 DEFAULT (0),
  k_plot6 numeric(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_k_plot6 DEFAULT (0),
  kod_t7 varchar(3) NULL CONSTRAINT DF_plan_sezon_all_test_kod_t7 DEFAULT (''),
  vid7 tinyint NULL CONSTRAINT DF_plan_sezon_all_test_vid7 DEFAULT (0),
  kf7 decimal(5, 2) NULL CONSTRAINT DF_plan_sezon_all_test_kf7 DEFAULT (0),
  norm_t7 decimal(10, 5) NULL CONSTRAINT DF_plan_sezon_all_test_norm_t7 DEFAULT (0),
  kod_pokras int NULL CONSTRAINT DF_plan_sezon_all_test_zv7 DEFAULT (''),
  tkb7 varchar(3) NULL CONSTRAINT DF_plan_sezon_all_test_tkb7 DEFAULT (''),
  seb_t7 decimal(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_seb_t7 DEFAULT (0),
  opis_t7 varchar(30) NULL CONSTRAINT DF_plan_sezon_all_test_opis_t7 DEFAULT (''),
  hh7 int NULL CONSTRAINT DF_plan_sezon_all_test_hh7 DEFAULT (0),
  k_plot7 numeric(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_k_plot7 DEFAULT (0),
  kol_raskr int NULL CONSTRAINT DF_plan_sezon_all_test_kol_raskr DEFAULT (0),
  kol_cd int NULL CONSTRAINT DF_plan_sezon_all_test_kol_cd DEFAULT (0),
  kol_opt int NULL CONSTRAINT DF_plan_sezon_all_test_kol_opt DEFAULT (0),
  kol_fl int NULL CONSTRAINT DF_plan_sezon_all_test_kol_fl DEFAULT (0),
  kol_rozn int NULL CONSTRAINT DF_plan_sezon_all_test_kol_rozn DEFAULT (0),
  kal_tk decimal(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_kal_tk DEFAULT (0),
  kal_z_rk bit NULL CONSTRAINT DF_plan_sezon_all_test_kal_z_rk DEFAULT (0),
  kal_z decimal(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_kal_z DEFAULT (0),
  kal_f decimal(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_kal_f DEFAULT (0),
  kal_d_poet decimal(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_kal_d_poet DEFAULT (0),
  kal_d_v decimal(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_kal_d_v DEFAULT (0),
  kal_d_tamp decimal(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_kal_d_tamp DEFAULT (0),
  kal_d_p decimal(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_kal_d_p DEFAULT (0),
  kal_d_stra decimal(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_kal_d_stra DEFAULT (0),
  kal_d decimal(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_kal_d DEFAULT (0),
  kal_koef decimal(4, 2) NULL CONSTRAINT DF_plan_sezon_all_test_kal_koef DEFAULT (0),
  kal_koef_p decimal(7, 3) NULL CONSTRAINT DF_plan_sezon_all_test_kal_koef_p DEFAULT (0),
  kal_all decimal(7, 2) NULL CONSTRAINT DF_plan_sezon_all_test_kal_all DEFAULT (0),
  data_utv datetime NULL,
  p_kol_zv1 tinyint NULL CONSTRAINT DF_plan_sezon_all_test_p_kol_zv1 DEFAULT (0),
  p_kol_zv2 tinyint NULL CONSTRAINT DF_plan_sezon_all_test_p_kol_zv2 DEFAULT (0),
  p_kol_zv3 tinyint NULL CONSTRAINT DF_plan_sezon_all_test_p_kol_zv3 DEFAULT (0),
  p_kol_zv4 tinyint NULL CONSTRAINT DF_plan_sezon_all_test_p_kol_zv4 DEFAULT (0),
  p_form_zv1 varchar(1) NULL CONSTRAINT DF_plan_sezon_all_test_p_form_zv1 DEFAULT (''),
  p_form_zv2 varchar(1) NULL CONSTRAINT DF_plan_sezon_all_test_p_form_zv2 DEFAULT (''),
  p_form_zv3 varchar(1) NULL CONSTRAINT DF_plan_sezon_all_test_p_form_zv3 DEFAULT (''),
  p_form_zv4 varchar(1) NULL CONSTRAINT DF_plan_sezon_all_test_p_form_zv4 DEFAULT (''),
  p_spe_zv1 varchar(1) NULL CONSTRAINT DF_plan_sezon_all_test_p_spe_zv1 DEFAULT (''),
  p_spe_zv2 varchar(1) NULL CONSTRAINT DF_plan_sezon_all_test_p_spe_zv2 DEFAULT (''),
  p_spe_zv3 varchar(1) NULL CONSTRAINT DF_plan_sezon_all_test_p_spe_zv3 DEFAULT (''),
  p_spe_zv4 varchar(1) NULL CONSTRAINT DF_plan_sezon_all_test_p_spe_zv4 DEFAULT (''),
  p_sum1 decimal(5, 2) NULL CONSTRAINT DF_plan_sezon_all_test_p_sum1 DEFAULT (0),
  p_sum2 decimal(5, 2) NULL CONSTRAINT DF_plan_sezon_all_test_p_sum2 DEFAULT (0),
  p_sum3 decimal(5, 2) NULL CONSTRAINT DF_plan_sezon_all_test_p_sum3 DEFAULT (0),
  p_sum4 decimal(5, 2) NULL CONSTRAINT DF_plan_sezon_all_test_p_sum4 DEFAULT (0),
  graf tinyint NULL CONSTRAINT DF_plan_sezon_all_test_graf DEFAULT (0),
  spec_p varchar(1) NULL CONSTRAINT DF_plan_sezon_all_test_spec_p DEFAULT (''),
  n_zak_priv int NULL CONSTRAINT DF_plan_sezon_all_test_n_zak_priv DEFAULT (0),
  strana int NULL CONSTRAINT DF_plan_sezon_all_test_strana DEFAULT (0),
  data_lek datetime NULL,
  utv_konfek datetime NULL,
  ob_izd decimal(10, 5) NULL CONSTRAINT DF_plan_sezon_all_test_ob_izd DEFAULT (0),
  datef_edit datetime NULL,
  datef_app datetime NULL,
  slogn_vish decimal(4, 1) NULL CONSTRAINT DF_plan_sezon_all_test_slogn_vish DEFAULT (0),
  sposob_up int NULL CONSTRAINT DF_plan_sezon_all_test_sposob_up DEFAULT (0),
  dateutvpr datetime NULL,
  ob_noski int NULL CONSTRAINT DF_plan_sezon_all_test_ob_noski DEFAULT (0),
  sek_noski int NULL CONSTRAINT DF_plan_sezon_all_test_sek_noski DEFAULT (0),
  kal_f_u numeric(8, 2) NULL CONSTRAINT DF_plan_sezon_all_test_kal_f_u DEFAULT (0),
  kal_f_pror numeric(8, 2) NULL CONSTRAINT DF_plan_sezon_all_test_kal_f_pror DEFAULT (0),
  kal_ff int NULL CONSTRAINT DF_plan_sezon_all_test_kal_ff DEFAULT (0),
  sek_seb_z numeric(8, 2) NULL CONSTRAINT DF_plan_sezon_all_test_sek_seb_z DEFAULT (0),
  prim_im nvarchar(254) NULL CONSTRAINT DF_plan_sezon_all_test_prim_im DEFAULT (''),
  kol_paet numeric(4, 1) NULL CONSTRAINT DF_plan_sezon_all_test_kol_paet_1 DEFAULT (0),
  slogn_paet numeric(5, 1) NULL CONSTRAINT DF_plan_sezon_all_test_slogn_paet_1 DEFAULT (0),
  kmp1 tinyint NULL CONSTRAINT DF_plan_sezon_all_test_kmp1 DEFAULT (0),
  kmp2 tinyint NULL CONSTRAINT DF_plan_sezon_all_test_kmp2 DEFAULT (0),
  kmp3 tinyint NULL CONSTRAINT DF_plan_sezon_all_test_kmp3 DEFAULT (0),
  kmp4 tinyint NULL CONSTRAINT DF_plan_sezon_all_test_kmp4 DEFAULT (0),
  kmp5 tinyint NULL CONSTRAINT DF_plan_sezon_all_test_kmp5 DEFAULT (0),
  kmp6 tinyint NULL CONSTRAINT DF_plan_sezon_all_test_kmp6 DEFAULT (0),
  kr_pryz tinyint NULL CONSTRAINT DF_plan_sezon_all_test_kr_pryz DEFAULT (0),
  krp_zvet int NULL CONSTRAINT DF_plan_sezon_all_test_krp_zvet DEFAULT (0),
  krp_slog int NULL CONSTRAINT DF_plan_sezon_all_test_krp_slog DEFAULT (0),
  t_sizer int NULL CONSTRAINT DF_plan_sezon_all_test_t_sizer DEFAULT (0),
  t_age int NULL CONSTRAINT DF_plan_sezon_all_test_t_age DEFAULT (0),
  t_typeup int NULL CONSTRAINT DF_plan_sezon_all_test_t_typeup DEFAULT (0),
  t_canvas int NULL CONSTRAINT DF_plan_sezon_all_test_t_canvas DEFAULT (0),
  t_typev int NULL CONSTRAINT DF_plan_sezon_all_test_t_typev DEFAULT (0),
  t_lining int NULL CONSTRAINT DF_plan_sezon_all_test_t_lining DEFAULT (0),
  printer tinyint NULL CONSTRAINT DF_plan_sezon_all_test_printer DEFAULT (0),
  pf_printer varchar(1) NULL CONSTRAINT DF_plan_sezon_all_test_pf_printer DEFAULT (''),
  printerSum numeric(8, 2) NULL CONSTRAINT DF_plan_sezon_all_test_printerSum DEFAULT (0),
  cal_koef_p numeric(5, 2) NULL CONSTRAINT DF_plan_sezon_all_test_cal_koef_p DEFAULT (0),
  kal_koef_d numeric(7, 4) NULL CONSTRAINT DF_plan_sezon_all_test_kal_koef_d DEFAULT (0),
  mes_6 int NULL CONSTRAINT DF_plan_sezon_all_test_mes_6 DEFAULT (0),
  mes_7 int NULL CONSTRAINT DF_plan_sezon_all_test_mes_7 DEFAULT (0),
  mes_8 int NULL CONSTRAINT DF_plan_sezon_all_test_mes_8 DEFAULT (0),
  sek_kr int NULL CONSTRAINT DF_plan_sezon_all_test_sek_kr DEFAULT (0),
  kol_brak int NULL CONSTRAINT DF_plan_sezon_all_test_kol_brak DEFAULT (0),
  t_art nvarchar(120) NULL CONSTRAINT DF_plan_sezon_all_test_t_art DEFAULT (''),
  t_art_poln nvarchar(120) NULL CONSTRAINT DF_plan_sezon_all_test_t_art_poln DEFAULT (''),
  last_zad int NULL CONSTRAINT DF_plan_sezon_all_test_last_zad DEFAULT (0),
  data_add datetime NULL,
  komp_add nvarchar(50) NULL,
  data_del datetime NULL,
  komp_del nvarchar(50) NULL,
  data_edit datetime NULL,
  komp_edit nvarchar(50) NULL,
  psa_id_osn int NULL,
  refreshoms bit NULL,
  spvya_id tinyint NULL,
  article_sp nvarchar(20) NULL,
  kind_decoration int NULL,
  id_word int NULL,
  article_number int NULL CONSTRAINT DF_plan_sezon_all_article_number DEFAULT (0),
  utoch_konfek datetime NULL,
  kol_raskr_new int NULL,
  kol_cd_new int NULL,
  kol_brak_new int NULL,
  purpose1 int NULL,
  purpose2 int NULL,
  purpose3 int NULL,
  purpose4 int NULL,
  purpose5 int NULL,
  purpose6 int NULL,
  purpose7 int NULL,
  sost2 varchar(80) NULL,
  sost3 varchar(80) NULL,
  sost4 varchar(80) NULL,
  sost5 varchar(80) NULL,
  sost6 varchar(80) NULL,
  sost7 varchar(80) NULL,
  sost1 varchar(80) NULL,
  id_fit int NULL,
  id_length int NULL,
  id_pattern int NULL,
  mat_id int NULL,
  kruj smallint NULL,
  rez_kruj smallint NULL,
  p_priv_nam smallint NULL CONSTRAINT DF_plan_sezon_all_p_priv_nam DEFAULT (0),
  v_priv_nam smallint NULL CONSTRAINT DF_plan_sezon_all_v_priv_nam DEFAULT (0),
  tcd_id smallint NULL,
  koef_mat_pror smallint NULL,
  parent_nn_pror varchar(10) NULL,
  nosumkol smallint NULL CONSTRAINT DF_plan_sezon_all_nosumkol DEFAULT (0),
  mtc_id int NULL,
  mtp_id int NULL CONSTRAINT DF__plan_sezo__mtp_i__6526EE3D DEFAULT (3),
  shrink int NULL,
  tkid_set int NULL,
  dupl_block int NULL CONSTRAINT DF__plan_sezo__dupl___2359F637 DEFAULT (0),
  notPrintVsh int NULL CONSTRAINT DF__plan_sezo__notPr__2BDA2D9F DEFAULT (0),
  print_noski int NULL CONSTRAINT DF__plan_sezo__print__248E1192 DEFAULT (0),
  stk_id int NULL DEFAULT (1),
  dtf_print int NULL CONSTRAINT DF_plan_sezon_all_dtf_print DEFAULT (0),
  sostguid1 uniqueidentifier NULL,
  sostguid2 uniqueidentifier NULL,
  sostguid3 uniqueidentifier NULL,
  sostguid4 uniqueidentifier NULL,
  sostguid5 uniqueidentifier NULL,
  sostguid6 uniqueidentifier NULL,
  sostguid7 uniqueidentifier NULL,
  sostguid uniqueidentifier NULL,
  dopr_id int NULL CONSTRAINT DF__plan_sezo__dopr___0AA4225D DEFAULT (0),
  sum_zarpl decimal(12, 2) NULL CONSTRAINT DF__plan_sezo__sum_z__6D9DCF5B DEFAULT (0),
  sum_dopopl decimal(12, 2) NULL CONSTRAINT DF__plan_sezo__sum_d__6E91F394 DEFAULT (0),
  sum_strvznos decimal(12, 2) NULL CONSTRAINT DF__plan_sezo__sum_s__6F8617CD DEFAULT (0),
  sum_sebraskr decimal(12, 2) NULL CONSTRAINT DF__plan_sezo__sum_s__707A3C06 DEFAULT (0),
  sum_komplnum decimal(12, 2) NULL CONSTRAINT DF__plan_sezo__sum_k__716E603F DEFAULT (0),
  utv_date_noski datetime NULL,
  clr_time_noski decimal(12, 2) NULL,
  sost8 varchar(80) NULL,
  sost9 varchar(80) NULL,
  sost10 varchar(80) NULL,
  purpose8 int NULL,
  purpose9 int NULL,
  purpose10 int NULL,
  sostguid8 uniqueidentifier NULL,
  sostguid9 uniqueidentifier NULL,
  sostguid10 uniqueidentifier NULL,
  seb_t8 decimal(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_seb_t8 DEFAULT (0),
  seb_t9 decimal(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_seb_t9 DEFAULT (0),
  seb_t10 decimal(6, 2) NULL CONSTRAINT DF_plan_sezon_all_test_seb_t10 DEFAULT (0),
  hh8 int NULL CONSTRAINT DF_plan_sezon_all_test_hh8 DEFAULT (0),
  hh9 int NULL CONSTRAINT DF_plan_sezon_all_test_hh9 DEFAULT (0),
  hh10 int NULL CONSTRAINT DF_plan_sezon_all_test_hh10 DEFAULT (0),
  CONSTRAINT PK_plan_sezon_all_test PRIMARY KEY CLUSTERED (nn)
)
ON [PRIMARY]
GO

--
-- Create index [index_articul] on table [dbo].[plan_sezon_all]
--
PRINT (N'Create index [index_articul] on table [dbo].[plan_sezon_all]')
GO
CREATE INDEX index_articul
  ON dbo.plan_sezon_all (data_del, articul)
  INCLUDE (psa_id, nn, kodd)
  ON [PRIMARY]
GO

--
-- Create index [index_mod] on table [dbo].[plan_sezon_all]
--
PRINT (N'Create index [index_mod] on table [dbo].[plan_sezon_all]')
GO
CREATE INDEX index_mod
  ON dbo.plan_sezon_all (data_del, mod)
  INCLUDE (psa_id, nn)
  ON [PRIMARY]
GO

--
-- Create index [IX_plan_sezon_all_test] on table [dbo].[plan_sezon_all]
--
PRINT (N'Create index [IX_plan_sezon_all_test] on table [dbo].[plan_sezon_all]')
GO
CREATE INDEX IX_plan_sezon_all_test
  ON dbo.plan_sezon_all (nn, men, tp_id, tgm_id_n, ta_id, tb_kod)
  ON [PRIMARY]
GO

--
-- Create index [KEY_plan_sezon_all] on table [dbo].[plan_sezon_all]
--
PRINT (N'Create index [KEY_plan_sezon_all] on table [dbo].[plan_sezon_all]')
GO
CREATE UNIQUE INDEX KEY_plan_sezon_all
  ON dbo.plan_sezon_all (psa_id, nn)
  ON [PRIMARY]
GO

--
-- Create index [KEY_plan_sezon_all2] on table [dbo].[plan_sezon_all]
--
PRINT (N'Create index [KEY_plan_sezon_all2] on table [dbo].[plan_sezon_all]')
GO
CREATE UNIQUE INDEX KEY_plan_sezon_all2
  ON dbo.plan_sezon_all (nn, psa_id)
  ON [PRIMARY]
GO

--
-- Create index [nonclast_ind_data_del_n] on table [dbo].[plan_sezon_all]
--
PRINT (N'Create index [nonclast_ind_data_del_n] on table [dbo].[plan_sezon_all]')
GO
CREATE INDEX nonclast_ind_data_del_n
  ON dbo.plan_sezon_all (data_del, n)
  ON [PRIMARY]
GO

--
-- Create index [NonClusteredIndex-20180209-093722] on table [dbo].[plan_sezon_all]
--
PRINT (N'Create index [NonClusteredIndex-20180209-093722] on table [dbo].[plan_sezon_all]')
GO
CREATE INDEX [NonClusteredIndex-20180209-093722]
  ON dbo.plan_sezon_all (komb_osn, psa_id_osn, articul, mod)
  ON [PRIMARY]
GO

--
-- Create index [NonClusteredIndex-20180209-094045] on table [dbo].[plan_sezon_all]
--
PRINT (N'Create index [NonClusteredIndex-20180209-094045] on table [dbo].[plan_sezon_all]')
GO
CREATE INDEX [NonClusteredIndex-20180209-094045]
  ON dbo.plan_sezon_all (psa_id_osn, articul, mod, komb_izd)
  ON [PRIMARY]
GO

--
-- Create index [NonClusteredIndex-20180728-151905] on table [dbo].[plan_sezon_all]
--
PRINT (N'Create index [NonClusteredIndex-20180728-151905] on table [dbo].[plan_sezon_all]')
GO
CREATE INDEX [NonClusteredIndex-20180728-151905]
  ON dbo.plan_sezon_all (tg_id_n, ta_id, psa_id, men)
  ON [PRIMARY]
GO

--
-- Create index [NonClusteredIndex-20181017-103857] on table [dbo].[plan_sezon_all]
--
PRINT (N'Create index [NonClusteredIndex-20181017-103857] on table [dbo].[plan_sezon_all]')
GO
CREATE INDEX [NonClusteredIndex-20181017-103857]
  ON dbo.plan_sezon_all (tb_kod, nn, men, ta_id)
  ON [PRIMARY]
GO

--
-- Create index [NonClusteredIndex-20181017-104115] on table [dbo].[plan_sezon_all]
--
PRINT (N'Create index [NonClusteredIndex-20181017-104115] on table [dbo].[plan_sezon_all]')
GO
CREATE INDEX [NonClusteredIndex-20181017-104115]
  ON dbo.plan_sezon_all (ta_id, tb_kod, nn, men)
  ON [PRIMARY]
GO

--
-- Create index [NonClusteredIndex-20181017-113254] on table [dbo].[plan_sezon_all]
--
PRINT (N'Create index [NonClusteredIndex-20181017-113254] on table [dbo].[plan_sezon_all]')
GO
CREATE INDEX [NonClusteredIndex-20181017-113254]
  ON dbo.plan_sezon_all (n, mod, komb_izd, tp_id, ta_id, tb_kod, men, tb_id)
  ON [PRIMARY]
GO

--
-- Create index [NonClusteredIndex-nn,data_del] on table [dbo].[plan_sezon_all]
--
PRINT (N'Create index [NonClusteredIndex-nn,data_del] on table [dbo].[plan_sezon_all]')
GO
CREATE INDEX [NonClusteredIndex-nn,data_del]
  ON dbo.plan_sezon_all (nn, data_del)
  ON [PRIMARY]
GO

--
-- Create index [NonClusteredIndex-numberOrderOms] on table [dbo].[plan_sezon_all]
--
PRINT (N'Create index [NonClusteredIndex-numberOrderOms] on table [dbo].[plan_sezon_all]')
GO
CREATE INDEX [NonClusteredIndex-numberOrderOms]
  ON dbo.plan_sezon_all (n_zak_priv)
  ON [PRIMARY]
GO

--
-- Create index [NonClusteredIndex-tema_kod] on table [dbo].[plan_sezon_all]
--
PRINT (N'Create index [NonClusteredIndex-tema_kod] on table [dbo].[plan_sezon_all]')
GO
CREATE INDEX [NonClusteredIndex-tema_kod]
  ON dbo.plan_sezon_all (tema_kod)
  ON [PRIMARY]
GO

--
-- Create foreign key [FK_plan_sezon_all_dopr_id] on table [dbo].[plan_sezon_all]
--
PRINT (N'Create foreign key [FK_plan_sezon_all_dopr_id] on table [dbo].[plan_sezon_all]')
GO
ALTER TABLE dbo.plan_sezon_all
  ADD CONSTRAINT FK_plan_sezon_all_dopr_id FOREIGN KEY (dopr_id) REFERENCES mtx.dopObrabPryz (dopr_id)
GO

--
-- Create foreign key [FK_plan_sezon_all_mtp_id] on table [dbo].[plan_sezon_all]
--
PRINT (N'Create foreign key [FK_plan_sezon_all_mtp_id] on table [dbo].[plan_sezon_all]')
GO
ALTER TABLE dbo.plan_sezon_all
  ADD CONSTRAINT FK_plan_sezon_all_mtp_id FOREIGN KEY (mtp_id) REFERENCES spr.matrixtypepotok (mtp_id)
GO

--
-- Create foreign key [FK_plan_sezon_all_stk_id] on table [dbo].[plan_sezon_all]
--
PRINT (N'Create foreign key [FK_plan_sezon_all_stk_id] on table [dbo].[plan_sezon_all]')
GO
ALTER TABLE dbo.plan_sezon_all
  ADD CONSTRAINT FK_plan_sezon_all_stk_id FOREIGN KEY (stk_id) REFERENCES dbo.spr_TypeKompls (stk_id)
GO

--
-- Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[v]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[v]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'вышивка', 'SCHEMA', N'dbo', 'TABLE', N'plan_sezon_all', 'COLUMN', N'v'
GO

--
-- Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[kol_v]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[kol_v]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'кол-во вышивок', 'SCHEMA', N'dbo', 'TABLE', N'plan_sezon_all', 'COLUMN', N'kol_v'
GO

--
-- Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[stra]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[stra]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'стразы', 'SCHEMA', N'dbo', 'TABLE', N'plan_sezon_all', 'COLUMN', N'stra'
GO

--
-- Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[stir]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[stir]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'стирка', 'SCHEMA', N'dbo', 'TABLE', N'plan_sezon_all', 'COLUMN', N'stir'
GO

--
-- Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[p_tamp]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[p_tamp]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'тампонная печать', 'SCHEMA', N'dbo', 'TABLE', N'plan_sezon_all', 'COLUMN', N'p_tamp'
GO

--
-- Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[nabiv_all]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[nabiv_all]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'набивка полностью', 'SCHEMA', N'dbo', 'TABLE', N'plan_sezon_all', 'COLUMN', N'nabiv_all'
GO

--
-- Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[tp_id]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[tp_id]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'направление(собств\привл)', 'SCHEMA', N'dbo', 'TABLE', N'plan_sezon_all', 'COLUMN', N'tp_id'
GO

--
-- Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[tk_id]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[tk_id]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'верх\низ', 'SCHEMA', N'dbo', 'TABLE', N'plan_sezon_all', 'COLUMN', N'tk_id'
GO

--
-- Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[tg_id_n]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[tg_id_n]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'категория', 'SCHEMA', N'dbo', 'TABLE', N'plan_sezon_all', 'COLUMN', N'tg_id_n'
GO

--
-- Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[tgm_id_n]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[tgm_id_n]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'динамич признак', 'SCHEMA', N'dbo', 'TABLE', N'plan_sezon_all', 'COLUMN', N'tgm_id_n'
GO

--
-- Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[ta_id]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[ta_id]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'ассортимент (вяз\кроен)', 'SCHEMA', N'dbo', 'TABLE', N'plan_sezon_all', 'COLUMN', N'ta_id'
GO

--
-- Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[tb_id]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[tb_id]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'имя блока', 'SCHEMA', N'dbo', 'TABLE', N'plan_sezon_all', 'COLUMN', N'tb_id'
GO

--
-- Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[tb_kod]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[tb_kod]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'id блока ', 'SCHEMA', N'dbo', 'TABLE', N'plan_sezon_all', 'COLUMN', N'tb_kod'
GO

--
-- Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[prn]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[prn]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'принт', 'SCHEMA', N'dbo', 'TABLE', N'plan_sezon_all', 'COLUMN', N'prn'
GO

--
-- Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[bus]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[bus]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'бусины/жемчуг', 'SCHEMA', N'dbo', 'TABLE', N'plan_sezon_all', 'COLUMN', N'bus'
GO

--
-- Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[t_age]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[t_age]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'возраст', 'SCHEMA', N'dbo', 'TABLE', N'plan_sezon_all', 'COLUMN', N't_age'
GO

--
-- Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[t_typeup]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[t_typeup]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'тип упаковки', 'SCHEMA', N'dbo', 'TABLE', N'plan_sezon_all', 'COLUMN', N't_typeup'
GO

--
-- Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[printer]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[printer]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'принтер', 'SCHEMA', N'dbo', 'TABLE', N'plan_sezon_all', 'COLUMN', N'printer'
GO

--
-- Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[mat_id]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[mat_id]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'идентификатор ассортиментной матрицы', 'SCHEMA', N'dbo', 'TABLE', N'plan_sezon_all', 'COLUMN', N'mat_id'
GO

--
-- Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[kruj]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[kruj]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'признак кружевного', 'SCHEMA', N'dbo', 'TABLE', N'plan_sezon_all', 'COLUMN', N'kruj'
GO

--
-- Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[rez_kruj]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[rez_kruj]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'где кроится кружево', 'SCHEMA', N'dbo', 'TABLE', N'plan_sezon_all', 'COLUMN', N'rez_kruj'
GO

--
-- Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[tcd_id]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[tcd_id]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'тип шнура', 'SCHEMA', N'dbo', 'TABLE', N'plan_sezon_all', 'COLUMN', N'tcd_id'
GO

--
-- Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[nosumkol]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[nosumkol]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'признак вхождения в отчеты', 'SCHEMA', N'dbo', 'TABLE', N'plan_sezon_all', 'COLUMN', N'nosumkol'
GO

--
-- Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[shrink]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[shrink]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'усадка', 'SCHEMA', N'dbo', 'TABLE', N'plan_sezon_all', 'COLUMN', N'shrink'
GO

--
-- Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[tkid_set]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[tkid_set]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'идентификатор раскомплекта', 'SCHEMA', N'dbo', 'TABLE', N'plan_sezon_all', 'COLUMN', N'tkid_set'
GO

--
-- Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[dupl_block]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[dupl_block]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'дублировочный блок', 'SCHEMA', N'dbo', 'TABLE', N'plan_sezon_all', 'COLUMN', N'dupl_block'
GO

--
-- Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[stk_id]
--
PRINT (N'Add extended property [MS_Description] on column [dbo].[plan_sezon_all].[stk_id]')
GO
EXEC sys.sp_addextendedproperty N'MS_Description', N'тип изделия(набор, комлпект общий или одиночное изделие)', 'SCHEMA', N'dbo', 'TABLE', N'plan_sezon_all', 'COLUMN', N'stk_id'
GO