using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Xpo.DB.Helpers;

namespace SewingProduction.form
{
    public partial class editFio : CustomForm
    {
        public editFio(string idFIO, string openType)
        {
            InitializeComponent();
            //Имя формы:
            this.Text = openType;
            customTextBoxTab.Text = idFIO;
        }

        private void editFio_Load(object sender, EventArgs e)
        {
            comboBox_editFio_Load(sender, e);
        }
        private void comboBox_editFio_Load(object sender, EventArgs e)
        {
            /*
            fioList.DataSource = ShowRelatedData("ace_test", query);
            using (SqlConnection connectionCombo = new SqlConnection(connectionString))
            {
                //Текст запроса:
                string queryZehAllList = $"SELECT nameZeh FROM ZehList";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(queryZehAllList, connectionCombo);
                //Создаем в памяти таблицу:
                System.Data.DataTable tableVidProizv = new System.Data.DataTable();
                //Добавляем ответ сервера в таблицу:
                dataAdapter.Fill(tableVidProizv);
                comboBoxZeh.Items.Clear();
                //Загрузка в комбобокс:
                foreach (DataRow row in tableVidProizv.Rows)
                {
                    comboBoxZeh.Items.Add(row[0].ToString());
                }
            }
            */
        }

        private void customOkButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
