using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SewingProduction.Features.Sprav;
using SewingProduction.Helpers;

namespace SewingProduction.Features.Sprav
{
    public class EditFioDataService
    {
        private readonly DatabaseHelper _dbHelper;
        public EditFioDataService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        /// <summary>
        /// Функция для отображения значения в комбобоксе
        /// </summary>
        /// <param name="query">текст запроса</param>
        public string SetComboOneTableItems(string query)
        {
            System.Data.DataTable tableList = new System.Data.DataTable();
            tableList = _dbHelper.ExecuteQuery(query);
            return tableList.Rows.Count > 0 ? tableList.Rows[0]["nameColumn"].ToString() : "";
        }
        /// <summary>
        /// Функция для заполнения значений в комбобоксе
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="query"></param>
        public void SetComboAllTableItems(System.Windows.Forms.ComboBox comboBox, string query)
        {
            System.Data.DataTable tableList = new System.Data.DataTable();
            tableList = _dbHelper.ExecuteQuery(query);
            comboBox.Items.Clear();
            //Загрузка в комбобокс:
            foreach (DataRow row in tableList.Rows)
            {
                comboBox.Items.Add(row[0].ToString());
            }
        }
        public string GetNameFromSp_firms(string kod)
        {
            string query = $"SELECT TRIM(name) AS nameColumn FROM sp_firms WHERE sp_firms.kod = {kod}";
            return SetComboOneTableItems(query);
        }
        public string GetFvr_nameFromFio_vid_rabot(int f_fvr_kod)
        {
            string query = $"SELECT TRIM(fvr_name) AS nameColumn FROM fio_vid_rabot WHERE fio_vid_rabot.fvr_kod = {f_fvr_kod}";
            return SetComboOneTableItems(query);
        }
        public string GetNameFromBrig_object(int gr)
        {
            string query = $"SELECT TRIM(name) AS nameColumn FROM brig_object WHERE brig_object.gr = '{gr}'";
            return SetComboOneTableItems(query);
        }
        public string GetNaimenFromTab_n(int ftabn)
        {
            string query = $"SELECT TRIM(naimen) AS nameColumn FROM tab_n WHERE tab_n.tnid =  {ftabn}";
            return SetComboOneTableItems(query);
        }
        public string GetNameFromBrig_ved(int ved)
        {
            string query = $"SELECT TRIM(name) AS nameColumn FROM brig_ved WHERE brig_ved.vdID =  {ved}";
            return SetComboOneTableItems(query);
        }
        public string GetPodrname1cFromSpbrig(string podr_1c_id)
        {
            string query = $"SELECT DISTINCT TRIM(podrname1c) AS nameColumn FROM spbrig WHERE spbrig.podrid1c = '{podr_1c_id}'";
            return SetComboOneTableItems(query);
        }
        public string GetNameFromPodr1C(string podr_pd_uid)
        {
            string query = $"SELECT DISTINCT TRIM(name) AS nameColumn FROM podr1C WHERE podr1C.pd_uid = '{podr_pd_uid}'";
            return SetComboOneTableItems(query);
        }
        public string GetIdFromPodr1C(string podr_pd_uid)
        {
            string query = $"SELECT id AS nameColumn FROM podr1C WHERE podr1C.pd_uid = '{podr_pd_uid}'";
            return SetComboOneTableItems(query);
        }
        public string GetInnFromPodr1C(string podr_pd_uid)
        {
            string query = $"SELECT inn AS nameColumn FROM podr1C WHERE podr1C.pd_uid = '{podr_pd_uid}'";
            return SetComboOneTableItems(query);
        }
        public string GetNameFromDolg1C(string dolg_d_id)
        {
            string query = $"SELECT DISTINCT TRIM(name) AS nameColumn FROM dolg1C WHERE dolg1C.d_id = {dolg_d_id}";
            return SetComboOneTableItems(query);
        }
        public string GetInnFromDolg1C(string dolg_d_id)
        {
            string query = $"SELECT DISTINCT inn AS nameColumn FROM dolg1C WHERE dolg1C.d_id = {dolg_d_id}";
            return SetComboOneTableItems(query);
        }
        public void GetNameFromSp_firms(ComboBox comboBox)
        {
            string query = $"SELECT TRIM(name) FROM sp_firms ORDER BY kod";
            SetComboAllTableItems(comboBox, query);
        }
        public void GetRabFromRab(ComboBox comboBox)
        {
            string query = $"SELECT TRIM(rab) FROM rab ORDER BY rab";
            SetComboAllTableItems(comboBox, query);
        }
        public void GetFvr_nameFromFio_vid_rabot(ComboBox comboBox)
        {
            string query = $"SELECT TRIM(fvr_name) FROM fio_vid_rabot ORDER BY fvr_kod";
            SetComboAllTableItems(comboBox, query);
        }
        public void GetNameFromBrig_object(ComboBox comboBox)
        {
            string query = $"SELECT TRIM(name) FROM brig_object ORDER BY gr";
            SetComboAllTableItems(comboBox, query);
        }
        public void GetNaimenFromTab_n(ComboBox comboBox)
        {
            string query = $"SELECT TRIM(naimen) FROM tab_n  ORDER BY naimen";
            SetComboAllTableItems(comboBox, query);
        }
        public void GetNameFromBrig_ved(ComboBox comboBox)
        {
            string query = $"SELECT TRIM(name) FROM brig_ved ORDER BY name";
            SetComboAllTableItems(comboBox, query);
        }
        public void GetPodrname1cFromSpbrig(ComboBox comboBox)
        {
            string query = $"SELECT DISTINCT TRIM(podrname1c) FROM spbrig";
            SetComboAllTableItems(comboBox, query);
        }
        public void GetNameFromPodr1C(ComboBox comboBox)
        {
            string query = $"SELECT DISTINCT TRIM(name) FROM podr1C";
            SetComboAllTableItems(comboBox, query);
        }
        public void GetNameFromDolg1C(ComboBox comboBox)
        {
            string query = "SELECT DISTINCT TRIM(name) FROM dolg1C";
            SetComboAllTableItems(comboBox, query);
        }
        public string GetTnidFromTab_n(string naimen)
        {
            string query = $"SELECT tnid AS nameColumn FROM tab_n WHERE tab_n.naimen =  '{naimen}'";
            return SetComboOneTableItems(query);
        }
        public string GetKodFromSp_firms(string name)
        {
            string query = $"SELECT sp_firms.kod AS nameColumn FROM sp_firms WHERE sp_firms.name =  '{name}'";
            return SetComboOneTableItems(query);
        }
        public string GetGrFromBrig_object(string name)
        {
            string query = $"SELECT bo.gr AS nameColumn FROM brig_object bo WHERE bo.name = '{name}'";
            return SetComboOneTableItems(query);
        }
        public string GetKodFromFio_vid_rabot(string name)
        {
            string query = $"SELECT fvr.fvr_kod AS nameColumn FROM fio_vid_rabot fvr WHERE fvr.fvr_name = '{name}'";
            return SetComboOneTableItems(query);
        }
        public string GetVdidFromBrig_ved(string name)
        {
            string query = $"SELECT brig_ved.vdID AS nameColumn FROM brig_ved WHERE brig_ved.name =  '{name}'";
            return SetComboOneTableItems(query);
        }
        public string GetPodr_1c_idFromSpbrig(string name)
        {
            string query = $"SELECT sb.podrid1c AS nameColumn FROM spbrig sb WHERE sb.podrname1c = '{name}'";
            return SetComboOneTableItems(query);
        }

        public string GetPd_uidFromPodr1C(string name)
        {
            string query = $"SELECT pd_uid AS nameColumn FROM podr1c WHERE podr1c.name =  '{name}'";
            return SetComboOneTableItems(query);
        }
        public string GetD_idFromDolg1C(string name)
        {
            string query = $"SELECT d_id AS nameColumn FROM dolg1c WHERE dolg1C.name = '{name}'";
            return SetComboOneTableItems(query);
        }
        public DataTable GetFioAndSpFirmsAndSpisok1c(string tab)
        {
            string query = @"SELECT * FROM fio 
                             LEFT JOIN sp_firms ON sp_firms.kod = fio.mast
                             LEFT JOIN spisok1c ON TRY_CAST(REPLACE(spisok1c.tab1c, ' ', '') AS INT) = CAST(fio.tab1c AS INT) AND spisok1c.orgcode = sp_firms.frm_1c_inn
                             WHERE tab = @tab";
            return _dbHelper.ExecuteQuery(query, new Dictionary<string, object> { { "@tab", tab } });
        }
        public void UpdateEditorFromFio(int tab)
        {
            string query = $"UPDATE FIO SET edit_komp = '{Environment.MachineName}', edit_date = GETDATE() WHERE tab = @tab";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@tab", tab } });
        }
        public void ClearEditorFromFio(int tab)
        {
            string query = $"UPDATE fio SET edit_komp = NULL,edit_date = NULL WHERE tab = @tab";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@tab", tab } });
        }
        public void UpdateFioPerson(Person _person)
        {
            string query = "UPDATE fio SET " +
                                "fio = @fio, " +
                                "tel_s = REPLACE(@tel_s,' ',''), " +
                                "bday =  @bday," +
                                "data_p = @data_p, " +
                                "datau = CASE WHEN YEAR(@datau) = 1900 THEN NULL ELSE @datau END," +
                                "rab = @rab, " +
                                "mast =@mast, " +
                                "f_fvr_kod = @f_fvr_kod , " +
                                "gr = @gr, " +
                                "ftabn = @ftabn, " +
                                "podr_1c_id = @podr_1c_id, " +
                                "podr_pd_uid = @podr_pd_uid, " +
                                "dolg_d_id = @dolg_d_id, " +
                                "tab_sovm = @tab_sovm, " +
                                "tab1c = @tab1c, " +
                                "ved = @ved, " +
                                "fgrd = @fgrd, " +
                                "ftabnsort = @ftabnsort, " +
                                "okl = @okl, " +
                                "tab_new = @tab_new, " +
                                "tel_r = @tel_r, " +
                                "tel_d = @tel_d, " +
                                "sovm = @sovm, " +
                                "sdel = @sdel, " +
                                "itr = @itr, " +
                                "dekret = @dekret " +
                                " WHERE tab = @tab";

            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> {  { "@fio", _person.fio } ,                       { "@tel_s", _person.tel_s } ,
                                                                            { "@bday", _person.bday } ,                     { "@data_p", _person.data_p } ,
                                                                            { "@datau", _person.datau } ,                   { "@rab", _person.rab } ,
                                                                            { "@okl", _person.okl } ,                       { "@tab_new", _person.tab_new } ,
                                                                            { "@tel_r", _person.tel_r } ,                   { "@tel_d", _person.tel_d } ,
                                                                            { "@mast", _person.mast } ,                     { "@f_fvr_kod", _person.f_fvr_kod } ,
                                                                            { "@gr", _person.gr } ,                         { "@ftabn", _person.ftabn } ,
                                                                            { "@podr_1c_id", _person.podr_1c_id } ,         { "@podr_pd_uid", _person.podr_pd_uid } ,
                                                                            { "@dolg_d_id", _person.dolg_d_id } ,           { "@tab_sovm", _person.tab_sovm } ,
                                                                            { "@tab1c", _person.tab1c } ,                   { "@ved", _person.ved } ,
                                                                            { "@fgrd", _person.fgrd } ,                     { "@ftabnsort", _person.ftabnsort } ,
                                                                            { "@sovm", _person.sovm } ,                     { "@sdel", _person.sdel } ,
                                                                            { "@itr", _person.itr } ,                       { "@dekret", _person.dekret } ,
                                                                            { "@tab", _person.tab } });
        }
        public void InsertFioPerson(Person _person)
        {
            string query = "INSERT INTO fio (fio, tel_s, bday, data_p, datau, rab, mast, f_fvr_kod, gr, ftabn, podr_1c_id, podr_pd_uid, dolg_d_id," +
                                        " tab_sovm, tab1c, ved, fgrd, ftabnsort, okl, tab_new,  tel_r, tel_d, sovm, sdel, itr, dekret, komp_name, tab) " +
                                        "VALUES (@fio, @tel_s, @bday, @data_p, " +
                                        "CASE WHEN YEAR(@datau) = 1900 THEN NULL ELSE @datau END," +
                                        " @rab, @mast,  @f_fvr_kod, @gr, @ftabn, @podr_1c_id, @podr_pd_uid, @dolg_d_id, " +
                                        "@tab_sovm, @tab1c, @ved, @fgrd, @ftabnsort, @okl, @tab_new,  @tel_r, @tel_d, @sovm, @sdel, @itr, @dekret, @komp_name, " +
                                        "(SELECT MAX(tab)+1 FROM fio))";

            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> {  { "@fio", _person.fio } ,                       { "@tel_s", _person.tel_s } ,
                                                                            { "@bday", _person.bday } ,                     { "@data_p", _person.data_p } ,
                                                                            { "@datau", _person.datau } ,                   { "@rab", _person.rab } ,
                                                                            { "@okl", _person.okl } ,                       { "@tab_new", _person.tab_new } ,
                                                                            { "@tel_r", _person.tel_r } ,                   { "@tel_d", _person.tel_d } ,
                                                                            { "@mast", _person.mast } ,                     { "@f_fvr_kod", _person.f_fvr_kod } ,
                                                                            { "@gr", _person.gr } ,                         { "@ftabn", _person.ftabn } ,
                                                                            { "@podr_1c_id", _person.podr_1c_id } ,         { "@podr_pd_uid", _person.podr_pd_uid } ,
                                                                            { "@dolg_d_id", _person.dolg_d_id } ,           { "@tab_sovm", _person.tab_sovm } ,
                                                                            { "@tab1c", _person.tab1c } ,                   { "@ved", _person.ved } ,
                                                                            { "@fgrd", _person.fgrd } ,                     { "@ftabnsort", _person.ftabnsort } ,
                                                                            { "@sovm", _person.sovm } ,                     { "@sdel", _person.sdel } ,
                                                                            { "@itr", _person.itr } ,                       { "@dekret", _person.dekret } ,
                                                                            { "@komp_name", _person.edit_komp },            { "@tab", _person.tab } });
        }
    }

}
