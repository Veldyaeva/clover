using System.Data;

namespace SewingProduction.Report
{
    public sealed class NormRaszPreparedData
    {
        public int AnnId { get; init; }
        public DataTable Header { get; init; } = new DataTable();
        public DataTable Rows { get; init; } = new DataTable();
        public DataTable ByEquipment { get; init; } = new DataTable();
        public DataTable BySection { get; init; } = new DataTable();
    }
}
