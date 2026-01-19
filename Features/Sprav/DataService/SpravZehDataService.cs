using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SewingProduction.Helpers;

namespace SewingProduction.Features.Sprav.DataService
{
    public class SpravZehDataService
    {
        private readonly DatabaseHelper _dbHelper;
        public SpravZehDataService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public DataTable GetZehList()
        {
            string query = $@"SELECT idZeh,nameZeh AS 'Название',nameProizv AS 'Производство' ,address AS 'Адрес'
                            FROM ZehList
                            LEFT JOIN spVidProizv ON spVidProizv.idProizv = ZehList.idProizv";
            return _dbHelper.ExecuteQuery(query);
        }
        public void InsertZehList(string name, string adres, string VidProizv)
        {
            string query = $@"INSERT INTO zehList (nameZeh,address,idProizv)
                                              VALUES (@name,@adres,
                                               (SELECT idProizv FROM spVidProizv WHERE nameProizv = @VidProizv))";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@name", name }, { "@adres", adres }, { "@VidProizv", VidProizv } });
        }
        public void UpdateZehList(string name, string adres, string VidProizv, string kod)
        {
            string query = $@"UPDATE zehList
                                   SET nameZeh = @name,
                                       address = @adres,
                                       idProizv = (SELECT idProizv FROM spVidProizv WHERE nameProizv = @VidProizv)
                                   WHERE idZeh =  @kod";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@name", name }, { "@adres", adres }, { "@VidProizv", VidProizv }, { "@kod", kod } });
        }
        public void UpdateRowZehList(string eFieldName, object eValue, object ekod)
        {
            string query = $@"UPDATE zehList
                              SET {eFieldName} = '{eValue}'
                              WHERE idZeh =  {ekod}";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@eValue", eValue }, { "@ekod", ekod } });
        }
        public void DeleteZehList(string kodCol)
        {
            string query = $@" DELETE FROM OborudBrig WHERE idZeh = @kodCol
                               DELETE FROM spBrig WHERE idZeh = @kodCol
                               DELETE FROM ZehList WHERE idZeh = @kodCol";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@kodCol", kodCol } });
        }
        public DataTable GetNameProizvFromSpVidProizv()
        {
            string query = $"SELECT nameProizv FROM spVidProizv";
            return _dbHelper.ExecuteQuery(query);
        }
    }
}
