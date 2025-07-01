using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SewingProduction.form;
using SewingProduction.Helpers;
using System.Windows.Forms;

namespace SewingProduction.Features.Sprav
{
    public class Person
    {
        private EditFioDataService _editFioDataService;
        #region столбцы
        private int f_id { get; set; }
        public int tab { get; set; }
        public string fio { get; set; }
        public string rab { get; set; }
        public string mast { get; set; }
        public string datau { get; set; }
        public int okl { get; set; }
        public int tab_new { get; set; }
        public string tel_r { get; set; }
        public string tel_d { get; set; }
        public string tel_s { get; set; }
        public string bday { get; set; }
        public string data_p { get; set; }
        public int gr { get; set; }
        public int dekret { get; set; }
        public int f_fvr_kod { get; set; }
        public string tab1c { get; set; }
        public int sdel { get; set; }
        public int itr { get; set; }
        public string podr_1c_id { get; set; }
        public int sovm { get; set; }
        public int tab_sovm { get; set; }
        public int ftabn { get; set; }
        public int fgrd { get; set; }
        public int ftabnsort { get; set; }
        public int ved { get; set; }
        public string edit_komp { get; set; }
        public string edit_date { get; set; }
        public string inn { get; set; }
        //Получаемые по ИД:
        public string mast_sp_firms { get; set; }
        public string f_fvr_kod_fio_vid_rabot { get; set; }
        public string gr_brig_object { get; set; }
        public string ftabn_tab_n { get; set; }
        public string ved_brig_ved { get; set; }
        public string podr_1c_id_spbrig { get; set; }
        //Должности 1С:
        public string dolg_d_id { get; set; }
        public string dolg_name_dolg1C { get; set; }
        public string dolg_inn_dolg1C { get; set; }
        //Подразделения 1С:
        public string podr_pd_uid { get; set; }
        public string podr_id_podr1C { get; set; }
        public string podr_name_podr1C { get; set; }
        public string podr_inn_podr1C { get; set; }
        #endregion

        /// <summary>
        /// Загрузка данных сотрудника
        /// </summary>
        /// <param name="xTab">табельный</param>
        /// <param name="dbHelper"></param>
        public bool LoadData(string xTab, DatabaseHelper dbHelper)
        {
            _editFioDataService = new EditFioDataService(dbHelper);
            System.Data.DataTable tableList = _editFioDataService.GetFioAndSpFirmsAndSpisok1c(xTab);
            DataRow row = tableList.Rows[0];
            // Данные о редакторе:
            DateTime? editDate = row["edit_date"] == DBNull.Value ? null : (DateTime?)row["edit_date"];
            edit_date = editDate?.ToString() ?? "";
            edit_komp = row["edit_komp"]?.ToString().Trim() ?? "";
            tab = (int)row["tab"];
            // если никто не редактирует или редактируем мы((дата пустая ИЛИ комп тот же) И табельный не 0)
            if ((!editDate.HasValue || string.IsNullOrEmpty(edit_komp) || edit_komp == Environment.MachineName) && tab != 0)
            {
                _editFioDataService.UpdateEditorFromFio(tab);
                // ФИО:
                fio = row["fio"]?.ToString() ?? "";
                // Телефон и Даты:
                tel_s = row["tel_s"]?.ToString() ?? "";
                bday = row["bday"].ToString();
                data_p = row["data_p"]?.ToString() ?? "";
                datau = row["datau"]?.ToString() ?? "";
                // Чекбоксы:
                sovm = (int)row["sovm"];
                sdel = (int)row["sdel"];
                itr = (int)row["itr"];
                dekret = (int)row["dekret"];
                // Остальное:
                tab_sovm = (int)row["tab_sovm"];
                tab1c = row["tab1c"]?.ToString() ?? "";
                ved = (int)row["ved"];
                ftabn = (int)row["ftabn"];
                fgrd = (int)row["fgrd"];
                ftabnsort = (int)row["ftabnsort"];
                okl = row["okl"] == DBNull.Value ? 0 : Convert.ToInt32(row["okl"]);
                tab_new = (int)row["tab_new"];
                tel_r = row["tel_r"]?.ToString() ?? "";
                tel_d = row["tel_d"]?.ToString() ?? "";
                inn = row["inn"]?.ToString() ?? "";
                // для комбобоксов:
                rab = row["rab"]?.ToString().Trim() ?? "";
                mast = row["mast"].ToString();
                f_fvr_kod = DBNull.Value.Equals(row["f_fvr_kod"]) ? 0 : (int)row["f_fvr_kod"];
                gr = row["gr"] == null ? 0 : (int)row["gr"];
                ftabn = row["ftabn"] == null ? 0 : (int)row["ftabn"];
                ved = row["ved"] == null ? 0 : (int)row["ved"];
                podr_1c_id = row["podr_1c_id"].ToString();
                // комбобоксы:
                mast_sp_firms = _editFioDataService.GetNameFromSp_firms(mast);
                f_fvr_kod_fio_vid_rabot = _editFioDataService.GetFvr_nameFromFio_vid_rabot(f_fvr_kod);
                gr_brig_object = _editFioDataService.GetNameFromBrig_object(gr);
                ftabn_tab_n = _editFioDataService.GetNaimenFromTab_n(ftabn);
                ved_brig_ved = _editFioDataService.GetNameFromBrig_ved(ved);
                podr_1c_id_spbrig = _editFioDataService.GetPodrname1cFromSpbrig(podr_1c_id);
                // 1C
                dolg_d_id = row["dolg_d_id"].ToString();
                dolg_name_dolg1C = _editFioDataService.GetNameFromDolg1C(dolg_d_id);
                dolg_inn_dolg1C = _editFioDataService.GetInnFromDolg1C(dolg_d_id);
                podr_pd_uid = row["podr_pd_uid"].ToString();
                podr_id_podr1C = _editFioDataService.GetIdFromPodr1C(podr_pd_uid);
                podr_name_podr1C = _editFioDataService.GetNameFromPodr1C(podr_pd_uid); ;
                podr_inn_podr1C = _editFioDataService.GetInnFromPodr1C(podr_pd_uid); ;

                return true;
            }
            // Если кто то уже радактирует:
            else
            {
                string eMessageTitle = "Занято другим компьютером";
                string eMessageText = $"Пользователь {edit_komp} уже редактирует этот профиль с {editDate}!";
                DialogResult nAnswer = MessageBox.Show(eMessageText, eMessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
        /// <summary>
        /// Обновление редактора у "персоны" по табельному
        /// </summary>
        public void UpdateEditor()
        {
            _editFioDataService.UpdateEditorFromFio(tab);
        }
        public void ClearEditor()
        {
            _editFioDataService.ClearEditorFromFio(tab);
        }
        public void UpdatePerson(Person _p)
        {
            _editFioDataService.UpdateFioPerson(_p);
        }
        public void InsertPerson(Person _p, DatabaseHelper dbHelper)
        {
            _editFioDataService = new EditFioDataService(dbHelper);
            _editFioDataService.InsertFioPerson(_p);
        }
    }
}
