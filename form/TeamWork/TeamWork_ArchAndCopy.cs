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

namespace SewingProduction.form
{
    public partial class TeamWork_ArchAndCopy : CustomForm
    {
        private readonly ArtNormService _artNormService;
        private int _id;
        public TeamWork_ArchAndCopy(int Id)
        {
            _id = Id;
            InitializeComponent(); 
            var dbHelper = new DatabaseHelper("ace");
            _artNormService = new ArtNormService(dbHelper);
            
            UpdateTheme(this);
        }

        private void TeamWork_ArchAndCopy_Load(object sender, EventArgs e)
        {
            LoadGridControlData(gridControl1, bindingSourceRasz, _artNormService.GetRelatedNormRasz(_id));
            LoadGridControlData(gridControl3, bindingSourceRask, _artNormService.GetRelatedNormRask(_id));
            LoadGridControlData(gridControl4, bindingSourceKont, _artNormService.GetRelatedNormKont(_id));
            LoadGridControlData(gridControl5, bindingSourceDop, _artNormService.GetRelatedNormDopObr(_id));

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
