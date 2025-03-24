using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SewingProduction.Helpers;

namespace SewingProduction.form
{
    public partial class TeamWork_ArchAndCopy : CustomForm
    {
        private readonly Services.ArtNormService _artNormService;
        private int _id;
        public TeamWork_ArchAndCopy(int Id)
        {
            _id = Id;
            InitializeComponent(); 
            var dbHelper = new DatabaseHelper("ace");
            _artNormService = new Services.ArtNormService(dbHelper);
            
            ApplyTheme();
        }

        private void TeamWork_ArchAndCopy_Load(object sender, EventArgs e)
        {
            //LoadGridControlData(gridControl1, bindingSourceRasz, await _artNormService.GetRelatedNormRasz(_id));
            //LoadGridControlData(gridControl3, bindingSourceRask, await _artNormService.GetRelatedNormRask(_id));
            //LoadGridControlData(gridControl4, bindingSourceKont, await _artNormService.GetRelatedNormKont(_id));
            //LoadGridControlData(gridControl5, bindingSourceDop, await _artNormService.GetRelatedNormDopObr(_id));

        }
        private void LoadGridControlData(GridControl grid, BindingSource source, DataTable data)
        {
            // Привязываем данные к BindingSource
            source.DataSource = data;

            // Привязываем BindingSource к GridControl
            grid.DataSource = source;

            // Обновляем визуализацию данных
            grid.RefreshDataSource();
        }
    }
}
