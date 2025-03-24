using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using SewingProduction.Interfaces;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace SewingProduction.Helpers
{
    // NormRaszManager.cs
    public class NormRaszManager : INormRaszManager
    {
        private readonly ArtNormService _service;
        private readonly BindingSource _source;

        public NormRaszManager(ArtNormService service, BindingSource source)
        {
            _service = service;
            _source = source;
        }

        public async Task LoadNormRaszAsync(int annId)
        {
            var table = await _service.GetNormRaszByAnnId(annId);
            _source.DataSource = table;
        }

        public Task<int> SaveNormRaszAsync(NormRasz norm)
        {
            throw new NotImplementedException();
        }

        Task<List<NormRasz>> INormRaszManager.LoadNormRaszAsync(int annId)
        {
            throw new NotImplementedException();
        }

        public async Task<BindingList<NormRasz>> LoadByAnnIdAsync(int annId)
        {
            var list = await _service.GetNormRaszByAnnId(annId);
            return new BindingList<NormRasz>(list);
        }
    }

}
