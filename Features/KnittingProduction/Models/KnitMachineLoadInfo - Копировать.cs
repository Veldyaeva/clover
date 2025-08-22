using SewingProduction.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

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
