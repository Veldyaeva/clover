using SewingProduction.Features.Articul.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Features.Articul.Service
{
    public class AppendArticulValidator
    {
        private readonly DbService _dbService = new DbService(new DatabaseHelperSQL());
        private readonly FileLogger _logger = new FileLogger();
        private SpArticulPreviewModel _newArt;
        public AppendArticulValidator(SpArticulPreviewModel artToCheck)
        {
            _newArt = artToCheck;
        }
        public async Task<CheckResult> checkBeforPublish()
        {
            if (_newArt.Id_gost == null || _newArt.Id_gost == 0)
            {
                return CheckResult.Fail($"Заполните ГОСТ!");
            }

            if (string.IsNullOrEmpty(_newArt.Mod))
            {
                return CheckResult.Fail($"Заполните Торговую модель!");
            }

            if (string.IsNullOrEmpty(_newArt.Articul))
            {
                return CheckResult.Fail($"Заполните Артикул");
            }



            return CheckResult.Success();
        }
        /// <summary>
        /// проврка на совпадение кодов размеров и вновь созданного кода
        /// </summary>
        public async Task<CheckResult> CheckRazmKod(IEnumerable<PlanRazmSetkaModel> items)
        {

            var chKod = checkKod(items);
            if (!chKod.IsSuccess)
                return chKod;
            var chDoubleKod = checkDoubleKod(items);
            if (!chDoubleKod.IsSuccess)
                return chDoubleKod;
            var chDoubleKodInDB = await checkDoubleKodInDB(items);
            if (!chDoubleKodInDB.IsSuccess)
                return chDoubleKodInDB;

            return CheckResult.Success();

        }
        public CheckResult checkKod(IEnumerable<PlanRazmSetkaModel> items)
        {
            string checkkod = _newArt.Kod.Substring(0, 7);

            bool allStartWithKod = !string.IsNullOrEmpty(checkkod) &&
                        items.All(x =>
                           !string.IsNullOrEmpty(x.Kod) &&
                           x.Kod.StartsWith(checkkod));
            if (!allStartWithKod)
            {
                return CheckResult.Fail($"Внимание! Не совпадают коды артикула в размерах!");
            }

            return CheckResult.Success();
        }
        public CheckResult checkDoubleKod(IEnumerable<PlanRazmSetkaModel> items)
        {
            var doublekods = items.GroupBy(x => x.Kod)
                         .Where(g => g.Count() > 1)
                         .Select(g => g.Key);
            if (doublekods.Any())
            {
                return CheckResult.Fail($"Задвоен код в размерном ряде! код:{string.Join(", ", doublekods)}");
            }
            return CheckResult.Success();
        }
        public async Task<CheckResult> checkDoubleKodInDB(IEnumerable<PlanRazmSetkaModel> items)
        {
            var doublekods = items.Select(x => x.Kod).ToList();
            string query = "SELECT COUNT(*) FROM dbo.sp_articul WHERE kod IN @kods";
            int count = await _dbService.GetFirstOrDefaultAsync<int>(query, new { kods = doublekods });
            if (count > 0)
            {
                return CheckResult.Fail($"В справочнике уже есть артикулы с такими кодами!");
            }
            return CheckResult.Success();
        }
    }
}
