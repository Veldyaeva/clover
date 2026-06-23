using System;

namespace SewingProduction.Features.Yarn.Models
{
    public sealed class YarnArticulHistoryRow
    {
        public string komp_user { get; set; }
        public DateTime? data_smena { get; set; }
        public string kod { get; set; }
        public string field_name { get; set; }
        public string field_n_k { get; set; }
        public string old_value { get; set; }
        public string new_value { get; set; }
    }
}
