using Dapper;
using SewingProduction.Features.TeamWork.Helpers;
using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Helpers;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.TeamWork.Services
{
    public sealed class BaseNodeLibraryService
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly ILogger _logger;

        public BaseNodeLibraryService(DatabaseHelper dbHelper, ILogger logger)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IReadOnlyList<BaseNodeDefinition>> GetAllAsync()
        {
            const string queryTemplate = @"
SELECT
    n.BaseNodeId,
    n.NodeCode,
{1}
{2}
{3}
    n.SourceAnnId,
{0}
    n.SourceRtCode,
    n.SourceImagePath,
    n.NodeName,
    n.NodeGroup,
{4}
    n.NodeType,
    n.ProductKind,
    n.ProductCategory,
    n.Comment,
    n.CreatedAt,
    n.UpdatedAt,
    bo.BaseNodeOperationId,
    bo.OperationRefId,
    bo.SortOrder,
    bo.DefaultN,
    bo.DefaultN1,
    bo.DefaultRazryd,
    bo.DefaultSek,
    bo.DefaultObor,
    bo.DefaultKodOb,
    bo.DefaultSpec,
    bo.DefaultKodProizv,
    bo.DefaultKodPodr,
    bor.OperationCode,
    bor.OperationName,
    bor.DefaultRazryd AS RefDefaultRazryd,
    bor.DefaultSek AS RefDefaultSek,
    bor.DefaultObor AS RefDefaultObor,
    bor.DefaultKodOb AS RefDefaultKodOb,
    bor.DefaultSpec AS RefDefaultSpec,
    bor.DefaultKodProizv AS RefDefaultKodProizv,
    bor.DefaultKodPodr AS RefDefaultKodPodr
FROM dbo.BaseNode n
LEFT JOIN dbo.BaseNodeOperation bo
    ON bo.BaseNodeId = n.BaseNodeId
   AND bo.IsActive = 1
LEFT JOIN dbo.BaseNodeOperationRef bor
    ON bor.OperationRefId = bo.OperationRefId
   AND bor.IsActive = 1
WHERE n.IsActive = 1
ORDER BY n.NodeName, bo.SortOrder, bo.BaseNodeOperationId;";

            try
            {
                using var connection = _dbHelper.GetConnection();
                bool hasSourceArticul = await HasBaseNodeColumnAsync(connection, null, "SourceArticul");
                bool hasNodeTypeId = await HasBaseNodeColumnAsync(connection, null, "NodeTypeId");
                bool hasNodeGroupId = await HasBaseNodeColumnAsync(connection, null, "NodeGroupId");
                bool hasNodeSubgroupId = await HasBaseNodeColumnAsync(connection, null, "NodeSubgroupId");
                bool hasNodeGroupDetail = await HasBaseNodeColumnAsync(connection, null, "NodeGroupDetail");
                string sourceArticulSelect = hasSourceArticul
                    ? "    n.SourceArticul,"
                    : "    CAST(NULL AS NVARCHAR(255)) AS SourceArticul,";
                string nodeTypeIdSelect = hasNodeTypeId
                    ? "    n.NodeTypeId,"
                    : "    CAST(NULL AS INT) AS NodeTypeId,";
                string nodeGroupIdSelect = hasNodeGroupId
                    ? "    n.NodeGroupId,"
                    : "    CAST(NULL AS INT) AS NodeGroupId,";
                string nodeSubgroupIdSelect = hasNodeSubgroupId
                    ? "    n.NodeSubgroupId,"
                    : "    CAST(NULL AS INT) AS NodeSubgroupId,";
                string nodeGroupDetailSelect = hasNodeGroupDetail
                    ? "    n.NodeGroupDetail,"
                    : "    CAST(NULL AS NVARCHAR(255)) AS NodeGroupDetail,";
                string query = string.Format(queryTemplate, sourceArticulSelect, nodeTypeIdSelect, nodeGroupIdSelect, nodeSubgroupIdSelect, nodeGroupDetailSelect);
                var rows = (await connection.QueryAsync<BaseNodeRow>(query)).ToList();

                return rows
                    .GroupBy(x => x.BaseNodeId)
                    .Select(MapNode)
                    .OrderBy(x => x.Name, StringComparer.CurrentCultureIgnoreCase)
                    .ToList();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке библиотеки базовых узлов");
                throw;
            }
        }

        public async Task<IReadOnlyList<BaseNodeMetadataItem>> GetNodeTypesAsync()
        {
            const string query = @"
SELECT
    NodeTypeId AS Id,
    CAST(NULL AS INT) AS ParentId,
    Code,
    Name,
    SortOrder,
    CAST(ISNULL(IsActive, 1) AS bit) AS IsActive
FROM dbo.NodeTypeDictionary
WHERE ISNULL(IsActive, 1) = 1
ORDER BY SortOrder, Name;";

            try
            {
                using var connection = _dbHelper.GetConnection();
                if (!await HasTableAsync(connection, null, "NodeTypeDictionary"))
                {
                    return BaseNodeMetadataOptions.NodeTypes
                        .OrderBy(x => x.SortOrder)
                        .ThenBy(x => x.Name)
                        .ToList();
                }

                var nodeTypes = (await connection.QueryAsync<BaseNodeMetadataItem>(query)).ToList();
                if (nodeTypes.Count > 0)
                {
                    return nodeTypes;
                }

                return BaseNodeMetadataOptions.NodeTypes
                    .OrderBy(x => x.SortOrder)
                    .ThenBy(x => x.Name)
                    .ToList();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке типов базовых узлов");
                return BaseNodeMetadataOptions.NodeTypes
                    .OrderBy(x => x.SortOrder)
                    .ThenBy(x => x.Name)
                    .ToList();
            }
        }

        public async Task<IReadOnlyList<BaseNodeMetadataItem>> GetNodeGroupsAsync()
        {
            const string query = @"
SELECT
    NodeGroupId AS Id,
    CAST(NULL AS INT) AS ParentId,
    Code,
    Name,
    SortOrder,
    CAST(ISNULL(IsActive, 1) AS bit) AS IsActive
FROM dbo.NodeGroupDictionary
WHERE ISNULL(IsActive, 1) = 1
ORDER BY SortOrder, Name;";

            try
            {
                using var connection = _dbHelper.GetConnection();
                if (!await HasTableAsync(connection, null, "NodeGroupDictionary"))
                {
                    return BaseNodeMetadataOptions.NodeGroups
                        .OrderBy(x => x.SortOrder)
                        .ThenBy(x => x.Name)
                        .ToList();
                }

                var nodeGroups = (await connection.QueryAsync<BaseNodeMetadataItem>(query)).ToList();
                if (nodeGroups.Count > 0)
                {
                    return nodeGroups;
                }

                return BaseNodeMetadataOptions.NodeGroups
                    .OrderBy(x => x.SortOrder)
                    .ThenBy(x => x.Name)
                    .ToList();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке групп базовых узлов");
                return BaseNodeMetadataOptions.NodeGroups
                    .OrderBy(x => x.SortOrder)
                    .ThenBy(x => x.Name)
                    .ToList();
            }
        }

        public async Task<IReadOnlyList<BaseNodeMetadataItem>> GetNodeSubgroupsAsync(int? nodeGroupId)
        {
            if (!nodeGroupId.HasValue || nodeGroupId.Value <= 0)
            {
                return new List<BaseNodeMetadataItem>();
            }

            const string query = @"
SELECT
    NodeSubgroupId AS Id,
    NodeGroupId AS ParentId,
    Code,
    Name,
    SortOrder,
    CAST(ISNULL(IsActive, 1) AS bit) AS IsActive
FROM dbo.NodeSubgroupDictionary
WHERE ISNULL(IsActive, 1) = 1
  AND NodeGroupId = @NodeGroupId
ORDER BY SortOrder, Name;";

            try
            {
                using var connection = _dbHelper.GetConnection();
                if (!await HasTableAsync(connection, null, "NodeSubgroupDictionary"))
                {
                    return BaseNodeMetadataOptions.GetNodeSubgroups(nodeGroupId).ToList();
                }

                var nodeSubgroups = (await connection.QueryAsync<BaseNodeMetadataItem>(query, new { NodeGroupId = nodeGroupId.Value })).ToList();
                if (nodeSubgroups.Count > 0)
                {
                    return nodeSubgroups;
                }

                return BaseNodeMetadataOptions.GetNodeSubgroups(nodeGroupId).ToList();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке подгрупп базовых узлов");
                return BaseNodeMetadataOptions.GetNodeSubgroups(nodeGroupId).ToList();
            }
        }

        public async Task<IReadOnlyList<BaseNodeMetadataItem>> GetProductCategoriesAsync()
        {
            const string query = @"
SELECT
    ProductCategoryId AS Id,
    ProductKindId AS ParentId,
    Code,
    Name,
    SortOrder,
    CAST(ISNULL(IsActive, 1) AS bit) AS IsActive
FROM dbo.ProductCategoryDictionary
WHERE ISNULL(IsActive, 1) = 1
ORDER BY SortOrder, Name;";

            try
            {
                using var connection = _dbHelper.GetConnection();
                if (!await HasTableAsync(connection, null, "ProductCategoryDictionary"))
                {
                    return BuildFallbackProductCategories();
                }

                var productCategories = (await connection.QueryAsync<BaseNodeMetadataItem>(query)).ToList();
                if (productCategories.Count > 0)
                {
                    return productCategories;
                }

                return BuildFallbackProductCategories();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке категорий изделий для базовых узлов");
                return BuildFallbackProductCategories();
            }
        }

        public async Task<BaseNodeDefinition> SaveAsync(BaseNodeDefinition node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (string.IsNullOrWhiteSpace(node.Name))
            {
                throw new InvalidOperationException("NodeName не может быть пустым.");
            }

            if (node.Operations == null || node.Operations.Count == 0)
            {
                throw new InvalidOperationException("Базовый узел должен содержать хотя бы одну операцию.");
            }

            using var connection = _dbHelper.GetConnection();
            using var transaction = connection.BeginTransaction();

            try
            {
                var existingNode = await FindExistingNodeAsync(connection, transaction, node);
                bool hasSourceArticul = await HasBaseNodeColumnAsync(connection, transaction, "SourceArticul");
                bool hasNodeTypeId = await HasBaseNodeColumnAsync(connection, transaction, "NodeTypeId");
                bool hasNodeGroupId = await HasBaseNodeColumnAsync(connection, transaction, "NodeGroupId");
                bool hasNodeSubgroupId = await HasBaseNodeColumnAsync(connection, transaction, "NodeSubgroupId");
                bool hasNodeGroupDetail = await HasBaseNodeColumnAsync(connection, transaction, "NodeGroupDetail");
                int baseNodeId;
                string nodeCode;
                var auditUser = BuildAuditUser();

                if (existingNode == null)
                {
                    nodeCode = await NormalizeNodeCodeAsync(connection, transaction, 0, node.NodeCode, node.Name);
                    string insertNodeSql = $@"
INSERT INTO dbo.BaseNode
(
    NodeCode,
    {(hasNodeTypeId ? "NodeTypeId," : string.Empty)}
    {(hasNodeGroupId ? "NodeGroupId," : string.Empty)}
    {(hasNodeSubgroupId ? "NodeSubgroupId," : string.Empty)}
    SourceAnnId,
    {(hasSourceArticul ? "SourceArticul," : string.Empty)}
    SourceRtCode,
    SourceImagePath,
    NodeName,
    NodeGroup,
    {(hasNodeGroupDetail ? "NodeGroupDetail," : string.Empty)}
    NodeType,
    ProductKind,
    ProductCategory,
    IsProductionCore,
    IsActive,
    Comment,
    CreatedBy,
    UpdatedAt,
    UpdatedBy
)
VALUES
(
    @NodeCode,
    {(hasNodeTypeId ? "@NodeTypeId," : string.Empty)}
    {(hasNodeGroupId ? "@NodeGroupId," : string.Empty)}
    {(hasNodeSubgroupId ? "@NodeSubgroupId," : string.Empty)}
    @SourceAnnId,
    {(hasSourceArticul ? "NULLIF(@SourceArticul, N'')," : string.Empty)}
    NULLIF(@SourceRtCode, N''),
    NULLIF(@SourceImagePath, N''),
    @NodeName,
    NULLIF(@NodeGroup, N''),
    {(hasNodeGroupDetail ? "NULLIF(@NodeGroupDetail, N'')," : string.Empty)}
    NULLIF(@NodeType, N''),
    NULLIF(@ProductKind, N''),
    NULLIF(@ProductCategory, N''),
    1,
    1,
    NULLIF(@Comment, N''),
    @AuditUser,
    SYSDATETIME(),
    @AuditUser
);
SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    baseNodeId = await connection.ExecuteScalarAsync<int>(insertNodeSql, new
                    {
                        NodeCode = nodeCode,
                        NodeTypeId = node.NodeTypeId,
                        NodeGroupId = node.NodeGroupId,
                        NodeSubgroupId = node.NodeSubgroupId,
                        SourceAnnId = node.SourceAnnId,
                        SourceArticul = NullIfWhiteSpace(node.SourceArticul),
                        SourceRtCode = NullIfWhiteSpace(node.SourceRtCode),
                        SourceImagePath = NullIfWhiteSpace(node.SourceImagePath),
                        NodeName = StringNormalizer.TrimOrEmpty(node.Name),
                        NodeGroup = NullIfWhiteSpace(node.NodeGroup),
                        NodeGroupDetail = NullIfWhiteSpace(node.NodeGroupDetail),
                        NodeType = NullIfWhiteSpace(node.NodeType),
                        ProductKind = NullIfWhiteSpace(node.ProductKind),
                        ProductCategory = NullIfWhiteSpace(node.ProductCategory),
                        Comment = NullIfWhiteSpace(node.Description),
                        AuditUser = auditUser
                    }, transaction);
                }
                else
                {
                    baseNodeId = existingNode.BaseNodeId;
                    nodeCode = await NormalizeNodeCodeAsync(connection, transaction, baseNodeId, node.NodeCode, node.Name);

                    string updateNodeSql = $@"
UPDATE dbo.BaseNode
SET NodeName = @NodeName,
    NodeCode = @NodeCode,
    {(hasNodeTypeId ? "NodeTypeId = @NodeTypeId," : string.Empty)}
    {(hasNodeGroupId ? "NodeGroupId = @NodeGroupId," : string.Empty)}
    {(hasNodeSubgroupId ? "NodeSubgroupId = @NodeSubgroupId," : string.Empty)}
    SourceAnnId = @SourceAnnId,
    {(hasSourceArticul ? "SourceArticul = NULLIF(@SourceArticul, N'')," : string.Empty)}
    SourceRtCode = NULLIF(@SourceRtCode, N''),
    SourceImagePath = NULLIF(@SourceImagePath, N''),
    NodeGroup = NULLIF(@NodeGroup, N''),
    {(hasNodeGroupDetail ? "NodeGroupDetail = NULLIF(@NodeGroupDetail, N'')," : string.Empty)}
    NodeType = NULLIF(@NodeType, N''),
    ProductKind = NULLIF(@ProductKind, N''),
    ProductCategory = NULLIF(@ProductCategory, N''),
    Comment = NULLIF(@Comment, N''),
    UpdatedAt = SYSDATETIME(),
    UpdatedBy = @AuditUser
WHERE BaseNodeId = @BaseNodeId;";

                    await connection.ExecuteAsync(updateNodeSql, new
                    {
                        BaseNodeId = baseNodeId,
                        NodeCode = nodeCode,
                        NodeTypeId = node.NodeTypeId,
                        NodeGroupId = node.NodeGroupId,
                        NodeSubgroupId = node.NodeSubgroupId,
                        SourceAnnId = node.SourceAnnId,
                        SourceArticul = NullIfWhiteSpace(node.SourceArticul),
                        SourceRtCode = NullIfWhiteSpace(node.SourceRtCode),
                        SourceImagePath = NullIfWhiteSpace(node.SourceImagePath),
                        NodeName = StringNormalizer.TrimOrEmpty(node.Name),
                        NodeGroup = NullIfWhiteSpace(node.NodeGroup),
                        NodeGroupDetail = NullIfWhiteSpace(node.NodeGroupDetail),
                        NodeType = NullIfWhiteSpace(node.NodeType),
                        ProductKind = NullIfWhiteSpace(node.ProductKind),
                        ProductCategory = NullIfWhiteSpace(node.ProductCategory),
                        Comment = NullIfWhiteSpace(node.Description),
                        AuditUser = auditUser
                    }, transaction);

                    await connection.ExecuteAsync("DELETE FROM dbo.BaseNodeOperation WHERE BaseNodeId = @BaseNodeId;", new { BaseNodeId = baseNodeId }, transaction);
                }

                int sortOrder = 1;
                foreach (var operation in node.Operations.OrderBy(x => x.SourceN).ThenBy(x => x.SourceN1))
                {
                    int operationRefId = await ResolveOperationRefIdAsync(connection, transaction, operation, auditUser);

                    const string insertOperationSql = @"
INSERT INTO dbo.BaseNodeOperation
(
    BaseNodeId,
    OperationRefId,
    SortOrder,
    StageNo,
    IsRequired,
    DefaultN,
    DefaultN1,
    DefaultRazryd,
    DefaultSek,
    DefaultObor,
    DefaultKodOb,
    DefaultSpec,
    DefaultKodProizv,
    DefaultKodPodr,
    IsActive,
    Comment,
    CreatedBy,
    UpdatedAt,
    UpdatedBy
)
VALUES
(
    @BaseNodeId,
    @OperationRefId,
    @SortOrder,
    NULL,
    1,
    @DefaultN,
    @DefaultN1,
    @DefaultRazryd,
    @DefaultSek,
    NULLIF(@DefaultObor, N''),
    @DefaultKodOb,
    NULLIF(@DefaultSpec, N''),
    @DefaultKodProizv,
    @DefaultKodPodr,
    1,
    NULL,
    @AuditUser,
    SYSDATETIME(),
    @AuditUser
);";

                    await connection.ExecuteAsync(insertOperationSql, new
                    {
                        BaseNodeId = baseNodeId,
                        OperationRefId = operationRefId,
                        SortOrder = sortOrder++,
                        DefaultN = operation.SourceN,
                        DefaultN1 = operation.SourceN1,
                        DefaultRazryd = NullableFromZero(operation.Razryd),
                        DefaultSek = DecimalFromZero(operation.Sek),
                        DefaultObor = operation.Obor,
                        DefaultKodOb = NullableFromZero(operation.KodOb),
                        DefaultSpec = operation.Spec,
                        DefaultKodProizv = NullableFromZero(operation.KodProizv),
                        DefaultKodPodr = NullableFromZero(operation.KodPodr),
                        AuditUser = auditUser
                    }, transaction);
                }

                transaction.Commit();

                var saved = (await GetAllAsync()).FirstOrDefault(x => x.BaseNodeId == baseNodeId);
                if (saved == null)
                {
                    throw new InvalidOperationException("Узел сохранен, но не был перечитан из базы.");
                }

                return saved;
            }
            catch (Exception ex)
            {
                try { transaction.Rollback(); } catch { }
                await _logger.LogErrorAsync(ex, $"Ошибка при сохранении базового узла \"{node.Name}\"");
                throw;
            }
        }

        public async Task DeleteAsync(int baseNodeId)
        {
            if (baseNodeId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(baseNodeId));
            }

            using var connection = _dbHelper.GetConnection();
            using var transaction = connection.BeginTransaction();

            try
            {
                var auditUser = BuildAuditUser();

                const string deactivateOperationsSql = @"
UPDATE dbo.BaseNodeOperation
SET IsActive = 0,
    UpdatedAt = SYSDATETIME(),
    UpdatedBy = @AuditUser
WHERE BaseNodeId = @BaseNodeId;";

                const string deactivateNodeSql = @"
UPDATE dbo.BaseNode
SET IsActive = 0,
    UpdatedAt = SYSDATETIME(),
    UpdatedBy = @AuditUser
WHERE BaseNodeId = @BaseNodeId;";

                await connection.ExecuteAsync(deactivateOperationsSql, new
                {
                    BaseNodeId = baseNodeId,
                    AuditUser = auditUser
                }, transaction);

                int affected = await connection.ExecuteAsync(deactivateNodeSql, new
                {
                    BaseNodeId = baseNodeId,
                    AuditUser = auditUser
                }, transaction);

                if (affected == 0)
                {
                    throw new InvalidOperationException($"Узел BaseNodeId={baseNodeId} не найден.");
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                try { transaction.Rollback(); } catch { }
                await _logger.LogErrorAsync(ex, $"Ошибка при удалении базового узла BaseNodeId={baseNodeId}");
                throw;
            }
        }

        private async Task<BaseNodeHeaderRow> FindExistingNodeAsync(SqlConnection connection, SqlTransaction transaction, BaseNodeDefinition node)
        {
            const string byIdSql = @"
SELECT TOP (1) BaseNodeId, NodeCode, NodeName
FROM dbo.BaseNode
WHERE BaseNodeId = @BaseNodeId;";

            if (node.BaseNodeId > 0)
            {
                return await connection.QueryFirstOrDefaultAsync<BaseNodeHeaderRow>(byIdSql, new { node.BaseNodeId }, transaction);
            }
            return null;
        }

        private async Task<int> ResolveOperationRefIdAsync(SqlConnection connection, SqlTransaction transaction, BaseNodeOperationDefinition operation, string auditUser)
        {
            const string findSql = @"
SELECT TOP (1) OperationRefId
FROM dbo.BaseNodeOperationRef
WHERE ISNULL(OperationCode, N'') = ISNULL(@OperationCode, N'')
  AND OperationName = @OperationName
  AND ISNULL(DefaultKodOb, 0) = ISNULL(@DefaultKodOb, 0);";

            var existingId = await connection.QueryFirstOrDefaultAsync<int?>(findSql, new
            {
                OperationCode = NullIfWhiteSpace(operation.KodO),
                OperationName = StringNormalizer.TrimOrEmpty(operation.Text),
                DefaultKodOb = NullableFromZero(operation.KodOb)
            }, transaction);

            if (existingId.HasValue && existingId.Value > 0)
            {
                return existingId.Value;
            }

            const string insertSql = @"
INSERT INTO dbo.BaseNodeOperationRef
(
    OperationCode,
    OperationName,
    OperationClass,
    OperationObject,
    DefaultRazryd,
    DefaultSek,
    DefaultObor,
    DefaultKodOb,
    DefaultSpec,
    DefaultKodProizv,
    DefaultKodPodr,
    IsActive,
    Comment,
    CreatedBy,
    UpdatedAt,
    UpdatedBy
)
VALUES
(
    NULLIF(@OperationCode, N''),
    @OperationName,
    NULLIF(@OperationClass, N''),
    NULLIF(@OperationObject, N''),
    @DefaultRazryd,
    @DefaultSek,
    NULLIF(@DefaultObor, N''),
    @DefaultKodOb,
    NULLIF(@DefaultSpec, N''),
    @DefaultKodProizv,
    @DefaultKodPodr,
    1,
    NULL,
    @AuditUser,
    SYSDATETIME(),
    @AuditUser
);
SELECT CAST(SCOPE_IDENTITY() AS INT);";

            var operationClass = OperationSemanticClassifier.DetectClass(operation.Text, operation.Obor, operation.KodOb);
            var operationObject = OperationSemanticClassifier.DetectObject(operation.Text);

            return await connection.ExecuteScalarAsync<int>(insertSql, new
            {
                OperationCode = NullIfWhiteSpace(operation.KodO),
                OperationName = StringNormalizer.TrimOrEmpty(operation.Text),
                OperationClass = operationClass,
                OperationObject = operationObject,
                DefaultRazryd = NullableFromZero(operation.Razryd),
                DefaultSek = DecimalFromZero(operation.Sek),
                DefaultObor = NullIfWhiteSpace(operation.Obor),
                DefaultKodOb = NullableFromZero(operation.KodOb),
                DefaultSpec = NullIfWhiteSpace(operation.Spec),
                DefaultKodProizv = NullableFromZero(operation.KodProizv),
                DefaultKodPodr = NullableFromZero(operation.KodPodr),
                AuditUser = auditUser
            }, transaction);
        }

        private async Task<string> GenerateNodeCodeAsync(SqlConnection connection, SqlTransaction transaction, string nodeName)
        {
            string baseCode = BuildBaseCode(nodeName);
            string code = baseCode;
            int suffix = 1;

            const string existsSql = "SELECT COUNT(1) FROM dbo.BaseNode WHERE NodeCode = @NodeCode;";
            while (await connection.ExecuteScalarAsync<int>(existsSql, new { NodeCode = code }, transaction) > 0)
            {
                suffix++;
                code = $"{baseCode}_{suffix}";
            }

            return code;
        }

        private async Task<string> NormalizeNodeCodeAsync(SqlConnection connection, SqlTransaction transaction, int existingBaseNodeId, string requestedNodeCode, string nodeName)
        {
            string normalizedNodeCode = StringNormalizer.TrimOrEmpty(requestedNodeCode);
            if (string.IsNullOrWhiteSpace(normalizedNodeCode))
            {
                return await GenerateNodeCodeAsync(connection, transaction, nodeName);
            }

            const string existsSql = "SELECT COUNT(1) FROM dbo.BaseNode WHERE NodeCode = @NodeCode AND BaseNodeId <> @BaseNodeId;";
            if (await connection.ExecuteScalarAsync<int>(existsSql, new { NodeCode = normalizedNodeCode, BaseNodeId = existingBaseNodeId }, transaction) == 0)
            {
                return normalizedNodeCode;
            }

            string candidate = normalizedNodeCode;
            int suffix = 1;
            while (await connection.ExecuteScalarAsync<int>(existsSql, new { NodeCode = candidate, BaseNodeId = existingBaseNodeId }, transaction) > 0)
            {
                suffix++;
                candidate = $"{normalizedNodeCode}_{suffix}";
            }

            return candidate;
        }

        private static BaseNodeDefinition MapNode(IGrouping<int, BaseNodeRow> group)
        {
            var first = group.First();
            var node = new BaseNodeDefinition
            {
                BaseNodeId = first.BaseNodeId,
                Id = first.BaseNodeId.ToString(),
                NodeCode = first.NodeCode ?? string.Empty,
                NodeTypeId = first.NodeTypeId,
                NodeGroupId = first.NodeGroupId,
                NodeSubgroupId = first.NodeSubgroupId,
                SourceAnnId = first.SourceAnnId,
                SourceArticul = first.SourceArticul ?? string.Empty,
                SourceRtCode = first.SourceRtCode ?? string.Empty,
                SourceImagePath = first.SourceImagePath ?? string.Empty,
                Name = first.NodeName ?? string.Empty,
                NodeGroup = first.NodeGroup ?? string.Empty,
                NodeGroupDetail = first.NodeGroupDetail ?? string.Empty,
                NodeType = first.NodeType ?? string.Empty,
                ProductKind = first.ProductKind ?? string.Empty,
                ProductCategory = first.ProductCategory ?? string.Empty,
                Description = first.Comment ?? string.Empty,
                CreatedAtUtc = first.CreatedAt ?? DateTime.UtcNow,
                UpdatedAtUtc = first.UpdatedAt ?? first.CreatedAt ?? DateTime.UtcNow
            };

            node.Operations = BaseNodeMapper.DeduplicateOperations(group
                .Where(x => x.BaseNodeOperationId.HasValue && x.OperationRefId.HasValue)
                .OrderBy(x => x.SortOrder ?? int.MaxValue)
                .ThenBy(x => x.DefaultN ?? int.MaxValue)
                .ThenBy(x => x.DefaultN1 ?? int.MaxValue)
                .Select(x => new BaseNodeOperationDefinition
                {
                    BaseNodeOperationId = x.BaseNodeOperationId ?? 0,
                    OperationRefId = x.OperationRefId ?? 0,
                    SortOrder = x.SortOrder ?? 0,
                    SourceN = x.DefaultN ?? 0,
                    SourceN1 = x.DefaultN1 ?? 0,
                    KodO = x.OperationCode ?? string.Empty,
                    Text = x.OperationName ?? string.Empty,
                    Razryd = ToInt(x.DefaultRazryd ?? x.RefDefaultRazryd),
                    Sek = ToInt(x.DefaultSek ?? x.RefDefaultSek),
                    Obor = x.DefaultObor ?? x.RefDefaultObor ?? string.Empty,
                    KodOb = ToInt(x.DefaultKodOb ?? x.RefDefaultKodOb),
                    Spec = x.DefaultSpec ?? x.RefDefaultSpec ?? string.Empty,
                    KodProizv = ToInt(x.DefaultKodProizv ?? x.RefDefaultKodProizv),
                    KodPodr = ToInt(x.DefaultKodPodr ?? x.RefDefaultKodPodr)
                })
                .ToList()).ToList();

            return node;
        }

        private static string BuildAuditUser()
        {
            return $"{Environment.UserName}@{Environment.MachineName}";
        }

        private static string BuildBaseCode(string nodeName)
        {
            return $"BN_{StringNormalizer.NormalizeCodeToken(nodeName, "NODE", 42)}";
        }

        private static int ToInt(decimal? value)
        {
            return value.HasValue ? Convert.ToInt32(Math.Round(value.Value, MidpointRounding.AwayFromZero)) : 0;
        }

        private static decimal? DecimalFromZero(int value)
        {
            return value == 0 ? null : value;
        }

        private static int? NullableFromZero(int value)
        {
            return value == 0 ? null : value;
        }

        private static string NullIfWhiteSpace(string value)
        {
            return StringNormalizer.TrimToNull(value);
        }

        private static IReadOnlyList<BaseNodeMetadataItem> BuildFallbackProductCategories()
        {
            return BaseNodeMetadataOptions.ProductCategories
                .Select((name, index) => new BaseNodeMetadataItem
                {
                    Id = index + 1,
                    Code = $"PC_{index + 1}",
                    Name = name,
                    SortOrder = (index + 1) * 10
                })
                .OrderBy(x => x.SortOrder)
                .ThenBy(x => x.Name)
                .ToList();
        }

        private static async Task<bool> HasBaseNodeColumnAsync(SqlConnection connection, SqlTransaction transaction, string columnName)
        {
            const string query = @"
SELECT CASE
    WHEN EXISTS (
        SELECT 1
        FROM sys.columns
        WHERE object_id = OBJECT_ID('dbo.BaseNode')
          AND name = @ColumnName
    ) THEN CAST(1 AS bit)
    ELSE CAST(0 AS bit)
END;";

            return await connection.ExecuteScalarAsync<bool>(query, new { ColumnName = columnName }, transaction);
        }

        private static async Task<bool> HasTableAsync(SqlConnection connection, SqlTransaction transaction, string tableName)
        {
            const string query = @"
SELECT CASE
    WHEN EXISTS (
        SELECT 1
        FROM sys.tables
        WHERE name = @TableName
          AND schema_id = SCHEMA_ID('dbo')
    ) THEN CAST(1 AS bit)
    ELSE CAST(0 AS bit)
END;";

            return await connection.ExecuteScalarAsync<bool>(query, new { TableName = tableName }, transaction);
        }

        private sealed class BaseNodeHeaderRow
        {
            public int BaseNodeId { get; set; }
            public string NodeCode { get; set; }
            public string NodeName { get; set; }
        }

        private sealed class BaseNodeRow
        {
            public int BaseNodeId { get; set; }
            public string NodeCode { get; set; }
            public int? NodeTypeId { get; set; }
            public int? NodeGroupId { get; set; }
            public int? NodeSubgroupId { get; set; }
            public int? SourceAnnId { get; set; }
            public string SourceArticul { get; set; }
            public string SourceRtCode { get; set; }
            public string SourceImagePath { get; set; }
            public string NodeName { get; set; }
            public string NodeGroup { get; set; }
            public string NodeGroupDetail { get; set; }
            public string NodeType { get; set; }
            public string ProductKind { get; set; }
            public string ProductCategory { get; set; }
            public string Comment { get; set; }
            public DateTime? CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }

            public int? BaseNodeOperationId { get; set; }
            public int? OperationRefId { get; set; }
            public int? SortOrder { get; set; }
            public int? DefaultN { get; set; }
            public int? DefaultN1 { get; set; }
            public decimal? DefaultRazryd { get; set; }
            public decimal? DefaultSek { get; set; }
            public string DefaultObor { get; set; }
            public int? DefaultKodOb { get; set; }
            public string DefaultSpec { get; set; }
            public int? DefaultKodProizv { get; set; }
            public int? DefaultKodPodr { get; set; }

            public string OperationCode { get; set; }
            public string OperationName { get; set; }
            public decimal? RefDefaultRazryd { get; set; }
            public decimal? RefDefaultSek { get; set; }
            public string RefDefaultObor { get; set; }
            public int? RefDefaultKodOb { get; set; }
            public string RefDefaultSpec { get; set; }
            public int? RefDefaultKodProizv { get; set; }
            public int? RefDefaultKodPodr { get; set; }
        }
    }
}
