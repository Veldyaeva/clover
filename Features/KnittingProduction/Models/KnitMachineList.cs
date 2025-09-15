using System;
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
public class KnitMachineAreaListView
{
    public int kmaID { get; set; }
    public string kmaNumber { get; set; }
    public DateTime? kmaDateAdd { get; set; }
    public string kmaCompAdd { get; set; }
    public int kmaDel { get; set; }
    public DateTime? kmaDateDel { get; set; }
    public string kmaCompDel { get; set; }
    public int kmaIDNazn { get; set; }
    public string nazn { get; set; }
    public string kmaZdID { get; set; }
    //public string object { get; set; }
}
public class KnitMachineClassList
{
    public int id_class { get; set; }
    public int name_class { get; set; }
    public int mc_id { get; set; }
    public string caption { get; set; }
    public decimal koef { get; set; }
}