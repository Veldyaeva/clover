CREATE VIEW dbo.normRaszView 
AS SELECT
  norm_rasz.kod
 ,norm_rasz.kod_o
 ,norm_rasz.kod_podr
 ,norm_rasz.kod_proizv
 ,norm_rasz.text
 ,norm_rasz.sek
 ,norm_rasz.seb
 ,norm_rasz.n
 ,norm_rasz.n_ch
 ,norm_rasz.n1
 ,norm_rasz.seb_s
 ,norm_rasz.razryd
 ,norm_rasz.spec
 ,norm_rasz.sek12
 ,norm_rasz.sek7
 ,norm_rasz.sek5
 ,norm_rasz.kod_ob
 ,norm_rasz.obor
 ,norm_rasz.sql_pr_add
 ,norm_rasz.date_add
 ,norm_rasz.komp_name
 ,norm_rasz.nrID
 ,norm_rasz.nrDateAdd
 ,norm_rasz.nrCompAdd
 ,norm_rasz.nrDateDel
 ,norm_rasz.nrCompDel
 ,norm_rasz.annId

--,dbo.getConstN(
----      CASE
----          WHEN LOWER(ПОЛЕ_ПРОФЕССИИ) = 'портной' THEN CONCAT('r', norm_rasz.razryd, '_p')
----          ELSE
-- CONCAT('tr_r', norm_rasz.razryd),
----      END,
--      SUBSTRING(CONVERT(nvarchar, GETDATE(), 12), 1, 4)
--  ) AS tarif,
--
--  ISNULL(mc.koefObServ, 1) AS koefObServ,
--
--  -- Само поле зарплаты:
--  ISNULL(norm_rasz.sek,0) / 3600.0
--    * dbo.getConstN(CONCAT('tr_r', norm_rasz.razryd), SUBSTRING(CONVERT(nvarchar, GETDATE(), 12), 1, 4))
--    * ISNULL(mc.koefObServ, 1)
--  AS zarplata

FROM dbo.norm_rasz WITH (NOLOCK)
--LEFT JOIN dbo.matrix_class mc
--    ON TRY_CAST(norm_rasz.kod_ob AS int) = mc.name_class
WHERE norm_rasz.nrDateDel IS NULL
--AND ISNULL(norm_rasz.sek, 0) > 0
GO