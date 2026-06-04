CREATE OR ALTER VIEW dbo.view_seb_vyaz_econom_assort
AS
SELECT
  seb_vyaz_econom_one.nn,
  seb_vyaz_econom_one.nom_zadany,
  MAX(seb_vyaz_econom_one.razm_ryad) AS razm_ryad,
  seb_vyaz_econom_one.pach_min,
  seb_vyaz_econom_one.pach_max,
  seb_vyaz_econom_one.nom,
  MAX(seb_vyaz_econom_one.articul) AS articul,
  MAX(seb_vyaz_econom_one.mod) AS mod,
  MAX(seb_vyaz_econom_one.date_econom) AS date_econom,
  ISNULL(MAX(seb_vyaz_econom_one.last_otm_izm), 0) AS last_otm_izm,
  MAX(vib.grup) AS grup,
  MAX(vib.data_cd_min) AS data_cd_min,
  MAX(vib.koef_zatrat) AS koef_zatrat,
  TRY_CAST(MAX(seb_vyaz_econom_one.id_podr) AS int) AS id_podr,
  MAX(seb_vyaz_econom_one.seb_all) AS seb_all
FROM dbo.seb_vyaz_econom AS seb_vyaz_econom_one
INNER JOIN (
  SELECT
    MIN(raskr_zeh_vyaz.grup) AS grup,
    MIN(raskr_zeh_vyaz.data_cd) AS data_cd_min,
    raskr_zeh_vyaz.zad_pl,
    MAX(vsa.koef_pr) AS koef_zatrat
  FROM dbo.raskr_zeh_vyaz
  INNER JOIN dbo.View_sp_articul AS vsa ON raskr_zeh_vyaz.kod = vsa.kod
  WHERE raskr_zeh_vyaz.data_cd IS NOT NULL
  GROUP BY raskr_zeh_vyaz.zad_pl
) AS vib ON seb_vyaz_econom_one.nom_zadany = vib.zad_pl
WHERE vib.data_cd_min IS NOT NULL
GROUP BY
  seb_vyaz_econom_one.nn,
  seb_vyaz_econom_one.nom_zadany,
  seb_vyaz_econom_one.pach_min,
  seb_vyaz_econom_one.pach_max,
  seb_vyaz_econom_one.nom;
GO
