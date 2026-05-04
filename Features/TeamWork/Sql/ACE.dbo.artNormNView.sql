CREATE VIEW dbo.artNormNView 
AS SELECT
  ann.*
FROM dbo.art_norm_n ann
WHERE ann.annDateDel IS NULL
GO