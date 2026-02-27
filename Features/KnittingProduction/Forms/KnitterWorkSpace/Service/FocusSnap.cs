using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    internal class FocusSnap
    {
        public string MasterTask;
        public string MasterMachine;
        public int? DetailPzvId;
        public int MasterTop;
        public int? DetailTop;
        public string FocusedColumnField;
        public bool WasInDetail;
    }

}