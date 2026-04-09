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
            const string query = @"
SELECT
    n.BaseNodeId,
    n.NodeCode,
    n.SourceAnnId,
    n.SourceRtCode,
    n.SourceImagePath,
    n.NodeName,
    n.NodeGroup,
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

        public async Task<IReadOnlyList<string>> GetNodeGroupsAsync()
        {
            const string query = @"
SELECT caption AS Value
FROM ACE.dbo.proizv_defect_details
WHERE ISNULL(arh, 0) = 0
  AND caption IS NOT NULL
ORDER BY caption;";

            try
            {
                using var connection = _dbHelper.GetConnection();
                var values = await connection.QueryAsync<string>(query);

                // Пустое значение оставляем первым, чтобы группу узла можно было не фиксировать жестко.
                return new[] { string.Empty }
                    .Concat(values
                        .Where(value => !string.IsNullOrWhiteSpace(value))
                        .Select(StringNormalizer.NormalizeWhitespace)
                        .Distinct(StringComparer.CurrentCultureIgnoreCase))
                    .ToList();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при загрузке списка групп базовых узлов");
                throw;
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
                int baseNodeId;
                string nodeCode;
                var auditUser = BuildAuditUser();

                if (existingNode == null)
                {
                    nodeCode = await NormalizeNodeCodeAsync(connection, transaction, node.NodeCode, node.Name);
                    const string insertNodeSql = @"
INSERT INTO dbo.BaseNode
(
    NodeCode,
    SourceAnnId,
    SourceRtCode,
    SourceImagePath,
    NodeName,
    NodeGroup,
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
    @SourceAnnId,
    NULLIF(@SourceRtCode, N''),
    NULLIF(@SourceImagePath, N''),
    @NodeName,
    NULLIF(@NodeGroup, N''),
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
                        SourceAnnId = node.SourceAnnId,
                        SourceRtCode = NullIfWhiteSpace(node.SourceRtCode),
                        SourceImagePath = NullIfWhiteSpace(node.SourceImagePath),
                        NodeName = StringNormalizer.TrimOrEmpty(node.Name),
                        NodeGroup = NullIfWhiteSpace(node.NodeGroup),
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
                    nodeCode = await NormalizeNodeCodeAsync(connection, transaction, node.NodeCode, node.Name);

                    const string updateNodeSql = @"
UPDATE dbo.BaseNode
SET NodeName = @NodeName,
    NodeCode = @NodeCode,
    SourceAnnId = @SourceAnnId,
    SourceRtCode = NULLIF(@SourceRtCode, N''),
    SourceImagePath = NULLIF(@SourceImagePath, N''),
    NodeGroup = NULLIF(@NodeGroup, N''),
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
                        SourceAnnId = node.SourceAnnId,
                        SourceRtCode = NullIfWhiteSpace(node.SourceRtCode),
                        SourceImagePath = NullIfWhiteSpace(node.SourceImagePath),
                        NodeName = StringNormalizer.TrimOrEmpty(node.Name),
                        NodeGroup = NullIfWhiteSpace(node.NodeGroup),
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

            const string byNameSql = @"
SELECT TOP (1) BaseNodeId, NodeCode, NodeName
FROM dbo.BaseNode
WHERE NodeName = @NodeName;";

            return await connection.QueryFirstOrDefaultAsync<BaseNodeHeaderRow>(byNameSql, new { NodeName = StringNormalizer.TrimOrEmpty(node.Name) }, transaction);
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

        private async Task<string> NormalizeNodeCodeAsync(SqlConnection connection, SqlTransaction transaction, string requestedNodeCode, string nodeName)
        {
            string normalizedNodeCode = StringNormalizer.TrimOrEmpty(requestedNodeCode);
            if (string.IsNullOrWhiteSpace(normalizedNodeCode))
            {
                return await GenerateNodeCodeAsync(connection, transaction, nodeName);
            }

            return normalizedNodeCode;
        }

        private static BaseNodeDefinition MapNode(IGrouping<int, BaseNodeRow> group)
        {
            var first = group.First();
            var node = new BaseNodeDefinition
            {
                BaseNodeId = first.BaseNodeId,
                Id = first.BaseNodeId.ToString(),
                NodeCode = first.NodeCode ?? string.Empty,
                SourceAnnId = first.SourceAnnId,
                SourceRtCode = first.SourceRtCode ?? string.Empty,
                SourceImagePath = first.SourceImagePath ?? string.Empty,
                Name = first.NodeName ?? string.Empty,
                NodeGroup = first.NodeGroup ?? string.Empty,
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
            public int? SourceAnnId { get; set; }
            public string SourceRtCode { get; set; }
            public string SourceImagePath { get; set; }
            public string NodeName { get; set; }
            public string NodeGroup { get; set; }
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
