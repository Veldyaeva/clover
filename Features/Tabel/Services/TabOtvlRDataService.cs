using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo.DB.Helpers;
using SewingProduction.Features.Tabel.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.Tabel.Services
{
    public class TabOtvlRDataService
    {
        private readonly DbService _dbService;

        public TabOtvlRDataService()
        {
            _dbService = new DbService(new DatabaseHelperSQL());
        }
        public async Task<List<TabOtvlRModel>> GetByMgGrTabAsync(int gr, int tab)
        {
            const string query = @"
            SELECT
                id, mg, gr, tab, n_r, dat,
                priem_t, priem_t_s, priem_t_po,
                sort_t, sort_t_s, sort_t_po,
                drug_s, drug_s_s, drug_s_po,
                otvl_r, otvl_r_s, otvl_r_po,
                orNew
            FROM tab_otvl_r
            WHERE gr = @gr AND tab = @tab
            ORDER BY dat, n_r, id";

            return await _dbService.GetListAsync<TabOtvlRModel>(query, new {  gr, tab });
        }
        public async Task<List<ZlSpisokMiniModel>> GetZlSpisokAsync(int gr)
        {
            const string query = @"
            SELECT
                zl_spisok.tabno, zl_spisok.firstname, zl_spisok.lastname, zl_spisok.middlename, zl_spisok.gr
            FROM zl_spisok
            inner join spisok1c on s_uin = spisok1c.uin
            WHERE zl_spisok.gr = @gr AND date_u IS NULL
            ORDER BY zl_spisok.firstname";

            return await _dbService.GetListAsync<ZlSpisokMiniModel>(query, new { gr });
        }
        public async Task<String> GetNaimenGroupZlAsync(int gr)
        {
            return await _dbService.SelectOneFieldAsync<string>("zlgr", "naimen", new Dictionary<string, object> { { "gr", gr } });
        }

        public async Task<int> SaveAsync(TabOtvlRModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            return await _dbService.SaveEntityAsync("tab_otvl_r", "Id", model);
        }

        public async Task DeleteAsync(TabOtvlRModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            await _dbService.DeleteEntityAsync("tab_otvl_r", "Id", model);
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (id <= 0) return;

            await _dbService.DeleteEntityAsync("tab_otvl_r", "Id", new TabOtvlRModel { Id = id });
        }
    }
}
