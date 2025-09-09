using System.ComponentModel.DataAnnotations.Schema;

namespace SewingProduction.Features.KnittingProduction.Models
{
    public class KnitMachineList
    {
        [NotMapped]
        public int kmlID { get; set; }
        [NotMapped]
        public string kmlNumber { get; set; }
        [NotMapped]
        public int kmlKmaID { get; set; }
        [NotMapped]
        public string kmaNumber { get; set; }
        [NotMapped]
        public string machNazn { get; set; }
        [NotMapped]
        public int name_class { get; set; }
        [NotMapped]
        public int mc_id { get; set; }
    }
}
