CREATE VIEW dbo.articulListGroupBySizeLabel 
AS WITH PlanTb
AS
(SELECT
    kodd
   ,MAX(nn) AS nn
   ,MAX(tb_Id) AS tb_Id
  FROM plan_sezon_all
  GROUP BY kodd),
RankedData
AS
(SELECT
    MAX(vsa.grup) AS grup
   ,vsa.articul
   ,vsa.mod
   ,vsa.kodd_rt
   ,vsa.ko AS kodd
   ,MIN(vsa.razm) AS minSize
   ,MAX(vsa.razm) AS maxSize
   ,ROW_NUMBER() OVER (PARTITION BY vsa.articul ORDER BY vsa.kodd_rt) AS row_num
   ,COUNT(*) OVER (PARTITION BY vsa.articul) AS total_count
   ,MIN(r.razm_all) AS minSizeAll
   ,MAX(r.razm_all) AS maxSizeAll
   ,vsa.annId
   ,p.tb_Id               -- ← добавили
  FROM View_sp_articul vsa
  LEFT JOIN kompl k
    ON vsa.kod = k.kod_k
  LEFT JOIN razm r
    ON vsa.razm = r.razm
  LEFT JOIN PlanTb p
    ON p.kodd = vsa.ko     -- ← здесь тот самый "getTbByKodd"
  WHERE k.kod_k IS NULL
  GROUP BY vsa.articul
          ,vsa.mod
          ,vsa.kodd_rt
          ,vsa.ko
          ,vsa.annID
          ,p.tb_Id                -- ← добавили в GROUP BY, так как он не агрегат
)
SELECT
  annID
 ,trim(grup) grup
 ,trim(articul) articul
 ,trim(mod) mod
 ,kodd_rt
 ,kodd
 ,minSize
 ,maxSize
 ,row_num
 ,minSizeAll
 ,maxSizeAll
 ,CASE
    WHEN total_count = 1 THEN ''
    WHEN row_num = 1 THEN 'min'
    WHEN row_num = total_count THEN 'max'
    ELSE 'mid'
  END AS size_label
 ,TRIM(articul) + ' ' +
  CASE
    WHEN total_count = 1 THEN ''
    WHEN row_num = 1 THEN 'min'
    WHEN row_num = total_count THEN 'max'
    ELSE 'mid'
  END AS articulForRT
 ,tb_Id                       -- ← новое поле в итоговом SELECT
FROM RankedData
GO