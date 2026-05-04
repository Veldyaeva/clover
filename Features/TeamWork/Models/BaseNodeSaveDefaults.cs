namespace SewingProduction.Features.TeamWork.Models
{
    internal sealed class BaseNodeSaveDefaults
    {
        public int? NodeTypeId { get; set; }
        public int? NodeGroupId { get; set; }
        public int? NodeSubgroupId { get; set; }
        public int? SourceAnnId { get; set; }

        public string SourceArticul { get; set; }

        public string SourceRtCode { get; set; }

        public string SourceImagePath { get; set; }

        public string NodeGroup { get; init; }

        public string NodeGroupDetail { get; init; }

        public string NodeType { get; init; }

        public string ProductCategory { get; init; }
    }
}
