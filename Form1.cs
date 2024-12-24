using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SewingProduction
{
    public partial class Form1 : CustomForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "aCEDataSet.art_norm_n". При необходимости она может быть перемещена или удалена.
            this.art_norm_nTableAdapter.Fill(this.aCEDataSet.art_norm_n);

        }
    }
}
