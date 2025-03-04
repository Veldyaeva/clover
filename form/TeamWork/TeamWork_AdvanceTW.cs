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
    public partial class TeamWork_AdvanceTW : CustomForm
    {
        private readonly ArtNormService _artNormService;

        public TeamWork_AdvanceTW()
        {
            InitializeComponent();
            var dbHelper = new DatabaseHelper(Properties.Settings.Default.ACEConnectionString);
            _artNormService = new ArtNormService(dbHelper);
            UpdateTheme(this);

        }

        private void TeamWork_AdvanceTW_Load(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK; // Устанавливаем результат
            this.Close(); // Закрываем окно
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
        }

        private void gridView5_ShowingEditor(object sender, CancelEventArgs e)
        {
            gridView5.AddNewRow();
            if (gridView5.RowCount >= 2)
            {
               // MessageBox.Show("Вы не можете добавить больше двух строк!", "Ограничение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }
    }
}
