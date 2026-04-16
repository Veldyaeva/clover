using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SewingProduction.Helpers;

namespace SewingProduction.Features.Sprav.DataService
{
    public class SpravOborudDataService
    {
        private readonly DatabaseHelperSQL _dbHelper;
        public SpravOborudDataService(DatabaseHelperSQL dbHelper)
        {
            _dbHelper = dbHelper;
        }
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
        public System.Data.DataTable GetSpOborudShv(bool arhiv)
        {
            string query = $@"SELECT kod_ob ,spOborudShv.text_ob,text_ob_s,
                                            oborud_shv_ob.text_ob AS text_ob_tip,spec_ob,
                                            spOborudMachine.name AS vidm,pokaz_sp,matrix_class.caption AS idClass,show_for_plan,
                                            (CASE arhiv WHEN 1 THEN 1 ELSE 0 END) AS arhiv,
                                            (CASE nastav WHEN 1 THEN 'оверлок' WHEN 2 THEN 'плоскошовка' WHEN 3 THEN 'универсалка' ELSE NULL END) AS nastav,
                                            (CASE vid_shp WHEN 1 THEN 'основное' WHEN 2 THEN 'дополнительное' ELSE NULL END) AS vid_shp,
                                            (CASE vid_vzp WHEN 1 THEN 'основное' WHEN 2 THEN 'дополнительное' ELSE NULL END) AS vid_vzp,
                                            (CASE vid_np WHEN 1 THEN 'основное' WHEN 2 THEN 'дополнительное' ELSE NULL END) AS vid_np,
                                            (CASE vid_rz WHEN 1 THEN 'основное' WHEN 2 THEN 'дополнительное' ELSE NULL END) AS vid_rz
                                         FROM spOborudShv 
                                             LEFT JOIN oborud_shv_ob ON oborud_shv_ob.ko_ob_all = spOborudShv.ko_ob_all 
                                             LEFT JOIN spOborudMachine ON spOborudMachine.miniName = spOborudShv.no_spec 
                                             LEFT JOIN matrix_class ON matrix_class.id_class = spOborudShv.id_class
                                            {(arhiv ? "" : "WHERE arhiv IS NULL OR arhiv = 0")} ";
            return _dbHelper.ExecuteQuery(query);
        }
        public void GetOborudShvOb(ComboBox comboBox)
        {
            string query = $"SELECT text_ob FROM oborud_shv_ob ORDER by ko_ob_all ASC";
            SetComboAllTableItems(comboBox, query);
        }
        public void GetSpOborudMachine(ComboBox comboBox)
        {
            string query = $"SELECT name FROM spOborudMachine";
            SetComboAllTableItems(comboBox, query);
        }
        public void GetMatrix_class(ComboBox comboBox)
        {
            string query = $"SELECT caption FROM matrix_class";
            SetComboAllTableItems(comboBox, query);
        }
        public void UpdateSpOborudShv(string SOStext_ob, string text_ob_s, string id_class,
                    object vid_shp, object vid_vzp, object vid_np, object vid_rz,
                    object nastav, bool show_for_plan, bool spec_ob, bool arhiv,
                    string kod_ob, string OSOtext_ob, string name)
        {
            string query = $"UPDATE spOborudShv " +
                            $"SET text_ob = @SOStext_ob, " +
                                             $"text_ob_s = @text_ob_s, " +
                                             $"spOborudShv.ko_ob_all = oborud_shv_ob.ko_ob_all, " +
                                             $"no_spec = spOborudMachine.miniName, " +
                                             $"spOborudShv.id_class = (SELECT id_class FROM matrix_class WHERE caption = @id_class)," +
                                             $"vid_shp = @vid_shp, " +
                                             $"vid_vzp = @vid_vzp, " +
                                             $"vid_np = @vid_np, " +
                                             $"vid_rz = @vid_rz, " +
                                             $"nastav = @nastav, " +
                                             $"show_for_plan = @show_for_plan, " +
                                             $"spec_ob = @spec_ob, " +
                                             $"arhiv = @arhiv " +
                                         $" FROM spOborudShv,spOborudMachine,oborud_shv_ob " +
                                         $" WHERE kod_ob = @kod_ob" +
                                         $" AND oborud_shv_ob.text_ob = @OSOtext_ob " +
                                         $" AND spOborudMachine.name = @name ";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@SOStext_ob", SOStext_ob } , { "@text_ob_s", text_ob_s } , { "@id_class", id_class } ,
                                                    { "@vid_shp", vid_shp }, { "@vid_vzp", vid_vzp } ,{ "@vid_np", vid_np } ,{ "@vid_rz", vid_rz } ,
                                                    { "@nastav", nastav } ,{ "@show_for_plan", show_for_plan }, { "@spec_ob", spec_ob } ,{ "@arhiv", arhiv } ,
                                                    { "@kod_ob", kod_ob } ,{ "@OSOtext_ob", OSOtext_ob } ,{ "@name", name } });
        }
        public void InsertSpOborudShv(string SOStext_ob, string text_ob_s, string id_class,
                    object vid_shp, object vid_vzp, object vid_np, object vid_rz,
                    object nastav, bool show_for_plan, bool spec_ob, bool arhiv,
                    string kod_ob, string OSOtext_ob, string name,
                    string AddClass, string AddGrup, string AddVidm, string AddKod)
        {
            string klass;
            if (AddClass == "")
                klass = "NULL";
            else
                klass = "(SELECT id_class FROM matrix_class WHERE caption = @AddClass)";

            string query = $"INSERT INTO spOborudShv (text_ob, text_ob_s, ko_ob_all, no_spec, id_class, vid_shp," +
                                        $" vid_vzp, vid_np, vid_rz, nastav, show_for_plan, spec_ob,arhiv) " +
                                    $"VALUES (" +
                                        $"@SOStext_ob, " +
                                        $"@text_ob_s, " +
                                        $"(SELECT ko_ob_all FROM oborud_shv_ob WHERE text_ob = @AddGrup), " +
                                        $"(SELECT miniName FROM spOborudMachine WHERE name = @AddVidm), " +
                                        $"{klass} ," +
                                        $"@vid_shp," +
                                        $"@vid_vzp," +
                                        $"@vid_np," +
                                        $"@vid_rz," +
                                        $"@nastav, " +
                                        $"@show_for_plan, " +
                                        $"@spec_ob, " +
                                        $"@arhiv); " +
                                        $"EXEC dbo.add_columns_plan_proz_mg @obor_n = {AddKod};";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@SOStext_ob", SOStext_ob } , { "@text_ob_s", text_ob_s } , { "@id_class", id_class } ,
                                                    { "@vid_shp", vid_shp }, { "@vid_vzp", vid_vzp } ,{ "@vid_np", vid_np } ,{ "@vid_rz", vid_rz } ,
                                                    { "@nastav", nastav } ,{ "@show_for_plan", show_for_plan }, { "@spec_ob", spec_ob } ,{ "@arhiv", arhiv } ,
                                                    { "@kod_ob", kod_ob } ,{ "@OSOtext_ob", OSOtext_ob } ,{ "@name", name } ,
                                                    { "@AddClass", AddClass }  ,{ "@AddGrup", AddGrup }  ,{ "@AddVidm", AddVidm } });
        }
        public int GetLastId()
        {
            string query = "SELECT TOP 1 kod_ob FROM spOborudShv ORDER BY kod_ob DESC";
            System.Data.DataTable tableList = _dbHelper.ExecuteQuery(query);
            return (int)tableList.Rows[0]["kod_ob"];
        }
        public void SetArhiv(int kodObArh)
        {
            string query = $"UPDATE spOborudShv SET arhiv = 1 WHERE kod_ob = @kodObArh";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@kodObArh", kodObArh } });
        }
    }

}
