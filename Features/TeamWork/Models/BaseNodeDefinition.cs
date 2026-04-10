using System;
using System.Collections.Generic;

namespace SewingProduction.Features.TeamWork.Models
{
    public sealed class BaseNodeDefinition
    {
        public int BaseNodeId { get; set; }
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public string NodeCode { get; set; } = string.Empty;
        public int? NodeTypeId { get; set; }
        public int? NodeGroupId { get; set; }
        public int? NodeSubgroupId { get; set; }
        public int? SourceAnnId { get; set; }
        public string SourceArticul { get; set; } = string.Empty;
        public string SourceRtCode { get; set; } = string.Empty;
        public string SourceImagePath { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string NodeGroup { get; set; } = string.Empty;
        public string NodeGroupDetail { get; set; } = string.Empty;
        public string NodeType { get; set; } = string.Empty;
        public string ProductKind { get; set; } = string.Empty;
        public string ProductCategory { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;
        public List<BaseNodeOperationDefinition> Operations { get; set; } = new List<BaseNodeOperationDefinition>();

        public string DisplayName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(SourceRtCode))
                    return Name;

                if (string.IsNullOrWhiteSpace(Name))
                    return $"[{SourceRtCode}]";

                return Name.Contains(SourceRtCode, StringComparison.CurrentCultureIgnoreCase)
                    ? Name
                    : $"[{SourceRtCode}] {Name}";
            }
        }

        public override string ToString() => DisplayName;
    }

    public sealed class BaseNodeMetadataItem
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int SortOrder { get; set; }
        public bool IsActive { get; set; } = true;

        public override string ToString() => Name;
    }

    public sealed class BaseNodeOperationDefinition
    {
        public int BaseNodeOperationId { get; set; }
        public int OperationRefId { get; set; }
        public int SortOrder { get; set; }
        public int SourceN { get; set; }
        public int SourceN1 { get; set; }
        public string Kod { get; set; } = string.Empty;
        public string KodO { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int Razryd { get; set; }
        public int Sek { get; set; }
        public decimal Seb { get; set; }
        public string Obor { get; set; } = string.Empty;
        public string Spec { get; set; } = string.Empty;
        public int KodProizv { get; set; }
        public int KodPodr { get; set; }
        public int KodOb { get; set; }
        public string TextOb { get; set; } = string.Empty;
        public string TextVyaz { get; set; } = string.Empty;
        public string TextProizv { get; set; } = string.Empty;
    }

    public sealed class BaseNodeOperationPreviewRow
    {
        public string Number { get; set; } = string.Empty;
        public string Operation { get; set; } = string.Empty;
        public int Razryd { get; set; }
        public int Sek { get; set; }
        public string Equipment { get; set; } = string.Empty;
    }

    public sealed class BaseNodeInsertionPoint
    {
        public string Key { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public int? AfterN { get; set; }
        public bool AppendToEnd { get; set; }

        public override string ToString() => Label;
    }
}
