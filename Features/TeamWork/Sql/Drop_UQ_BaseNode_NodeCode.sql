IF EXISTS (
    SELECT 1
    FROM sys.key_constraints
    WHERE name = 'UQ_BaseNode_NodeCode'
      AND parent_object_id = OBJECT_ID('dbo.BaseNode')
)
BEGIN
    ALTER TABLE dbo.BaseNode
        DROP CONSTRAINT UQ_BaseNode_NodeCode;
END
GO
