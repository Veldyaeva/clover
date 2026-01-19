using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.Spreadsheet.Export;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Utilities;
using SewingProduction.Features.Articul;
using SewingProduction.form;
using SewingProduction.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace SewingProduction.Features.KnittingProduction.Models
{
    public class PodrVyaz
    {
        [NotMapped] public int kod_vyaz { get; set; }
        [NotMapped] public string text_vyaz { get; set; }
    }
}
