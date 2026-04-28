using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SewingProduction.Features.Articul.Models;

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
            
            if (string.IsNullOrEmpty(_newArt.Mod) )
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
        public CheckResult checkKod(IReadOnlyList<PlanRazmSetkaModel> items)
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

    }
}
