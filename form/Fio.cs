using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;

namespace SewingProduction.form
{
    public partial class Fio : CustomForm
    {

        // Оснавная БД:
        //string connectionString = Properties.Settings.Default.ACEConnectionString;
        // Для тестов:
        string connectionString = Properties.Settings.Default.ACEtestConnectionString;
        string tableString;
        public Fio(string tableSQL, string rusNameTableSQL)
        {
            InitializeComponent();
            //Таблица fio:
            tableString = tableSQL;
            //Имя формы:
            this.Text = rusNameTableSQL;
        }

        private void Fio_Load(object sender, EventArgs e)
        {

        }

        private void oborudGrid_Load(object sender, EventArgs e)
        {
            string query = $@"SELECT tab,fio,rab,ved,ftabn,ftabnsort,fgrd,data_p,datau,bday,tel_s,f_fvr_kod,tab1c,tab_sovm,
                                tel_r,tel_d,mast,okl,tab_new,po,
                                (SELECT sp_firms.name FROM sp_firms WHERE sp_firms.kod = fio.mast) AS firms_name,
                                (SELECT brig_object.name FROM brig_object WHERE brig_object.gr = fio.gr) AS BRIG_object_name,
                                (SELECT DISTINCT spbrig.podrname1c FROM spbrig WHERE spbrig.podrid1c = fio.podr_1c_id AND podrid1c LIKE '%ЭЙС%') AS podr1cname,
                                sovm,sdel,itr,dekret
                              FROM fio 
                                ORDER BY tab ASC";
            fioList.DataSource = ShowRelatedData("ace_test", query);
            GridView gridView = fioGrid.MainView as GridView;
            if (gridView != null)
            {
                // Создаем экземпляр CheckEdit
                RepositoryItemCheckEdit checkEdit = new RepositoryItemCheckEdit
                {
                    ValueChecked = 1, 
                    ValueUnchecked = 0
                };
                // Назначаем его столбцам
                gridView.Columns["sovm"].ColumnEdit = checkEdit;
                gridView.Columns["sdel"].ColumnEdit = checkEdit;
                gridView.Columns["itr"].ColumnEdit = checkEdit;
                gridView.Columns["dekret"].ColumnEdit = checkEdit;
            }
            gridView.TopRowIndex = gridView.RowCount - 1;
        }

        private void oborudGrid_Click(object sender, EventArgs e)
        {

        }
        private void openSprav(string nameSprav, string columns, string nameSpravRus)
        {
            foreach (Form child in this.MdiParent.MdiChildren)
            {
                if (child is SpravForAll && child.Text.ToString() == nameSpravRus)
                {
                    // Если форма уже открыта, переключаем на нее
                    child.BringToFront();
                    return;
                }
            }
            // Если форма не открыта, создаем новую
            SpravForAll f = new SpravForAll(nameSprav, columns, nameSpravRus);
            f.MdiParent = this.MdiParent;
            f.Show();
        }
        private void customButtonSpDol_Click(object sender, EventArgs e)
        {
            openSprav("rab", "r_id,rab", "Справочник Должностей");
        }

        private void customButtonSpOrg_Click(object sender, EventArgs e)
        {
            openSprav("sp_firms", "kod,name,frm_1c_inn", "Справочник Организаций");
        }

        private void checkButtonShowDel_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
