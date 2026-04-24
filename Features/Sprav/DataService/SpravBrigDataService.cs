using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SewingProduction.Helpers;

namespace SewingProduction.Features.Sprav.DataService
{

    public class SpravBrigDataService
    {
        private readonly DatabaseHelperSQL _dbHelper;
        public SpravBrigDataService(DatabaseHelperSQL dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public DataTable GetSpBrig()
        {
            string query = $@" SELECT id_brig,n_brig,brig,nameZeh
                            FROM spBrig
                            LEFT JOIN ZehList ON ZehList.idZeh = spBrig.idZeh ";
            return _dbHelper.ExecuteQuery(query);
        }
        public void InsertSpBrig(string brig, string nBrig, string zeh)
        {
            string query = $@"INSERT INTO spBrig (brig,n_brig,idZeh)
                              VALUES (@brig,@nBrig,
                              (SELECT idZeh FROM ZehList WHERE nameZeh = @zeh))";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@brig", brig }, { "@nBrig", nBrig }, { "@zeh", zeh } });
        }
        public void UpdateSpBrig(string brig, string nBrig, string zeh, string kod)
        {
            string query = $@"UPDATE spBrig
                              SET brig = @brig,
                                n_brig = @nBrig,
                                idZeh = (SELECT idZeh FROM ZehList WHERE nameZeh = @zeh)
                              WHERE id_brig =  @kod";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@brig", brig }, { "@nBrig", nBrig }, { "@zeh", zeh }, { "@kod", kod } });
        }
        public void UpdateRowSpBrig(string eFieldName, object eValue, object ekod)
        {
            string query = $@"UPDATE spBrig
                              SET {eFieldName} = @eValue
                              WHERE id_brig =  @ekod";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@eValue", eValue }, { "@ekod", ekod } });
        }
        public void DeleteSpBrig(string kodCol)
        {
            string query = $"DELETE FROM spBrig WHERE id_brig = @kodCol";
            _dbHelper.ExecuteNonQuery(query, new Dictionary<string, object> { { "@kodCol", kodCol } });
        }
        public DataTable GetNameZehFromZehList()
        {
            string query = $"SELECT nameZeh FROM ZehList";
            return _dbHelper.ExecuteQuery(query);
        }
    }
}
