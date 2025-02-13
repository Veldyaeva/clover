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
    public partial class TeamWork : Form
    {
        public TeamWork()
        {
            InitializeComponent();
        }

        private void xtraTabPage1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tablePanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TeamWork_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "aCEDataSet.sp_articul". При необходимости она может быть перемещена или удалена.
            this.sp_articulTableAdapter.Fill(this.aCEDataSet.sp_articul);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "aCEDataSet.norm_dop_obr". При необходимости она может быть перемещена или удалена.
            this.norm_dop_obrTableAdapter.Fill(this.aCEDataSet.norm_dop_obr);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "aCEDataSet.norm_kont". При необходимости она может быть перемещена или удалена.
            this.norm_kontTableAdapter.Fill(this.aCEDataSet.norm_kont);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "aCEDataSet.Norm_rask". При необходимости она может быть перемещена или удалена.
            this.norm_raskTableAdapter.Fill(this.aCEDataSet.Norm_rask);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "aCEDataSet.norm_rasz". При необходимости она может быть перемещена или удалена.
            this.norm_raszTableAdapter.Fill(this.aCEDataSet.norm_rasz);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "aCEDataSet.art_norm_n". При необходимости она может быть перемещена или удалена.
            this.art_norm_nTableAdapter.Fill(this.aCEDataSet.art_norm_n);

        }
    }
}
