using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.Sprav;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Articul.Service
{
    public class LightweightTableArtPreview : BindingList<LightRow>
    {
        public List<LightColumn> Columns = new();
    }
    
    public class LightColumn
    {
        public string Name { get; set; }
        public Func<SpArtPreviewModel, object> Getter { get; set; }
        public Action<SpArtPreviewModel, object> Setter { get; set; }
    }
    public class LightRow
    {
        public SpArtPreviewModel Ref;

        public string Kod => Ref.Kod;
        public string Articul => Ref.Articul;
        public string Mod => Ref.Mod;
        public string Razm => Ref.Razm;
        public string Sost => Ref.Sost;
        public string Kle => Ref.Kle;


    }

}
