using Org.BouncyCastle.Asn1.X509;
using SewingProduction.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace SewingProduction.Features.KnittingProduction.Models
{
    public class KnitMachineLoadInfo
    {
        [NotMapped]
        public string yearMonth { get; set; }
        [NotMapped]
        public string kmlNumber { get; set; }
        [NotMapped]
        public string combinedPszNom { get; set; }
        [NotMapped]
        public int monthNumber { get; set; }
        [NotMapped]
        public int yearNumber { get; set; }
    }
}
