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

namespace SewingProduction.Helpers
{
    public class AnnManager : IAnnManager
    {
        private readonly ArtNormService _service;
        private readonly ILogger _logger;
        private readonly BindingSource _bindingSource;
        private readonly GridControl _grid;
        private readonly GridView _view;

        public AnnManager(ArtNormService service, ILogger logger,
                          BindingSource bindingSource, GridControl grid, GridView view)
        {
            _service = service;
            _logger = logger;
            _bindingSource = bindingSource;
            _grid = grid;
            _view = view;
        }

        //public async Task<BindingList<MyDataANN>> LoadAnnByArtAsync(int kod, string articul)
        //{
        //    var list = await _service.GetAnnByKodAndArtAsync(kod, articul);
        //    _bindingSource.DataSource = list;
        //    return list;
        //}

        //public void RefreshAnnGrid()
        //{
        //    _bindingSource.ResetBindings(false);
        //    _grid.RefreshDataSource();
        //    _view.RefreshData();
        //}

        //public async Task SaveAnnToDatabaseAsync(MyDataANN ann)
        //{
        //    await _service.SaveAnnAsync(ann);
        //}

        public async Task<BindingList<MyDataANN>> LoadByArtAsync(int kod, string articul, bool loadAll)
        {
            var result = new List<ArtNormN>();

            if (loadAll)
            {
                result = await _service.GetArtNormDataCurrent(kod, true);
            }
            else
            {
                result = await _service.GetArtNormDataCurrent(kod, false);
                var dashIndex = articul.IndexOf("-");
                if (dashIndex > 0)
                {
                    var partial = await _service.GetArtNormDataCurrent(articul.Substring(0, dashIndex));
                    result.AddRange(partial);
                }
            }

            return new BindingList<MyDataANN>(result.Select(r => new MyDataANN
            {
                AnnId = r.AnnID,
                Kod = r.Kod,
                Articul = r.Articul,
                Group = r.Group,
                Model = r.Mod,
                Status = r.Status,
                IsChecked = false
            }).ToList());
        }

        public AnnManager(ArtNormService service, BindingSource source)
        {
            _service = service;
            _bindingSource = source;
        }

        public async Task RefreshANN()
        {
            var data = await _service.GetArtNormDataCurrent(0, true);
            _bindingSource.DataSource = new BindingList<ArtNormN>(data);
        }

        public Task<BindingList<MyDataANN>> LoadAnnByArtAsync(int kod, string articul)
        {
            throw new NotImplementedException();
        }

        public void RefreshAnnGrid()
        {
            throw new NotImplementedException();
        }

        public Task SaveAnnToDatabaseAsync(MyDataANN ann)
        {
            throw new NotImplementedException();
        }
    }

}
