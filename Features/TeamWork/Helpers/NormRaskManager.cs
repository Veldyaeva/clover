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
    // NormRaskManager.cs
    public class NormRaskManager : INormRaskManager
    {
        private readonly ArtNormRepository _service;
        private readonly BindingSource _source;

        public NormRaskManager(ArtNormRepository service, BindingSource source)
        {
            _service = service;
            _source = source;
        }

        public Task LoadNormRask(int annId)
        {
            throw new NotImplementedException();
        }

        public async Task LoadNormRaskAsync(int annId)
        {
            DataTable table = await _service.GetRelatedNormRask(annId);
            _source.DataSource = table;
        }

        public Task<DataTable> LoadRaskroyNormByGroupAsync(int groupId)
        {
            throw new NotImplementedException();
        }
        public async Task<BindingList<NormRask>> LoadByAnnIdAsync(int annId)
        {
            var list = await _service.GetNormRaskByAnnId(annId);
            return new BindingList<NormRask>(list);
        }

    }

}
