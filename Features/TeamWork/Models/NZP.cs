using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Models
{
    public class NZPByKoddRt
    {
        [Column("annId")]
        public int annId { get; set; }
        [Column("kodd_rt")]
        public int kodd_rt { get; set; }
        [Column("kodd")]
        public int kodd { get; set; }
        [Column("grup")]
        public string grup { get; set; }
        [Column("minSize")]
        [NotMapped]
        public string minSize { get; set; }
        [Column("maxSize")]
        [NotMapped]
        public string maxSize { get; set; }
        [Column("articulForRT")]
        public string articulForRT {  get; set; }
        [Column("articul")]
        public string articul { get; set; }
        [Column("mod")]
        public string mod { get; set; }
        [Column("kod_v")]
        public int? kodV { get; set; }
        public int kolRVAll { get; set; }
        public int kolNaklAll { get; set; }
        public int kolGI { get; set; }
        public int kolNZP { get; set; }
        [Column("PztCount")]
        public int PZTCount { get; set; }
        [Column ("data_r")]
        public DateTime? data_r { get; set; }
    }
}
