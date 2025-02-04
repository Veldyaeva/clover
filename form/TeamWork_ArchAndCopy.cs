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
            var dbHelper = new DatabaseHelper(Properties.Settings.Default.ACEConnectionString);
            _artNormService = new ArtNormService(dbHelper);
            
            UpdateTheme(this);
        }

        private void TeamWork_ArchAndCopy_Load(object sender, EventArgs e)
        {
            //// TODO: данная строка кода позволяет загрузить данные в таблицу "aCE_backupDataSet.norm_dop_obr". При необходимости она может быть перемещена или удалена.
            //this.norm_dop_obrTableAdapter.Fill(this.aCE_backupDataSet.norm_dop_obr);
            //// TODO: данная строка кода позволяет загрузить данные в таблицу "aCE_backupDataSet.norm_kont". При необходимости она может быть перемещена или удалена.
            //this.norm_kontTableAdapter.Fill(this.aCE_backupDataSet.norm_kont);
            //// TODO: данная строка кода позволяет загрузить данные в таблицу "aCE_backupDataSet.Norm_rask". При необходимости она может быть перемещена или удалена.
            //this.norm_raskTableAdapter.Fill(this.aCE_backupDataSet.Norm_rask);
            //// TODO: данная строка кода позволяет загрузить данные в таблицу "aCE_backupDataSet.norm_rasz". При необходимости она может быть перемещена или удалена.
            //this.norm_raszTableAdapter.Fill(this.aCE_backupDataSet.norm_rasz);
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
