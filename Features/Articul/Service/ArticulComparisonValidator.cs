using DevExpress.CodeParser;
using DevExpress.UIAutomation;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.Articul.Service
{
    public sealed class ArticulComparisonValidator
    {
        private string _kod;
        private string _nn;
        private SpArtPreviewModel _curCompareArticul;
        private CreateArticulMatrModel _curCompareMatr;
        private bool _isRepeat = false;

        //private readonly DbService _dbService = new DbService(new DatabaseHelper());
        //private readonly DatabaseHelper _dbHelper;
        private readonly FileLogger _logger = new FileLogger();
        private CreateArticulMatrService _createArticulMatrService = new CreateArticulMatrService();


        public ArticulComparisonValidator(CreateArticulMatrModel curMatr, SpArtPreviewModel curArt)
        {
            _curCompareArticul = curArt;
            _curCompareMatr = curMatr;
            _kod = _curCompareArticul.Kod;
            _nn = _curCompareMatr.Nn;
        }
        /// <summary>
        /// выполняются проверки для стыковки артикула
        /// </summary>
        /// <returns></returns>
        public async Task<CheckResult> canLinkArticul() 
        {
            try
            {
                //локальные проверки
                //архивность артикула
                var arhArticul = checkArticulArh();
                if (!arhArticul.IsSuccess) 
                    return arhArticul;
                //проверка на повторный артикул
                var repeatArt = checkRepeatArticle();
                if (!repeatArt.IsSuccess)
                    return repeatArt;
                //проверка на найденную модель
                var foundMod = checkFoundMod();
                if (!foundMod.IsSuccess)
                    return foundMod;

                //наличие состава (в kompl) для артикула 
                var checkKomplTask = checkArticulKompls(_nn, _kod);
                var checkGostArhTask = checkGostArh();
                

                var results = await Task.WhenAll(checkKomplTask, checkGostArhTask);

                if (results.All(x => x.IsSuccess))
                    return CheckResult.Success();

                string firstError = results.First(x => !x.IsSuccess).ErrorMessage;
                return CheckResult.Fail(firstError);

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при стыковке артикулов CanLinkArticul");
                return CheckResult.Fail("Исключение в CanLinkArticul");
            }
        }
        public async Task<CheckResult>canChangeArticulSost() {

            if (_isRepeat)
            {
                MessageBox.Show("Внимание! Артикул ранее сдавался с другим составом!");
            }
            var res = await checkNZP();

            if (!res.IsSuccess)
            {
                return CheckResult.Fail("По артикулу есть незавершенное производство! Нельзя заменить состав!");
            }
            else
            {
                return CheckResult.Success();
            }

        }
        public bool isRepeat() { return _isRepeat; }
        private async Task<CheckResult> checkArticulKompls(string nn, string kod) 
        {
            string result = await _createArticulMatrService.GetCheckArticulKomplsCompareAsync(nn, kod);
            
            if (!string.IsNullOrEmpty(result))
                return CheckResult.Fail(result);

            return CheckResult.Success();
        }
        /// <summary>
        /// проверка архивности артикула
        /// </summary>
        /// <returns></returns>
        private CheckResult checkArticulArh()
        {
            if (_curCompareArticul.Arh == 1)
            {
                return CheckResult.Fail("Архивный артикул");
            }
            return CheckResult.Success();
        }
        private async Task<CheckResult> checkGostArh()
        {
            int result = await _createArticulMatrService.GetGostArhAsync(_kod);

            if (result == 1)
                return CheckResult.Fail("Архивный ГОСТ");

            return CheckResult.Success();

        }
        private async Task<CheckResult>checkNZP() {
            bool result = await _createArticulMatrService.HasNzpForArticul(_curCompareArticul.Kodd);
            if (result )
                return CheckResult.Fail("По артикулу есть незавершенное производство! Нельзя заменить состав!");

            return CheckResult.Success();
        }


        /// <summary>
        /// проверка на повторный артикул, если в базе уже есть артикула с таким же кодом, то нужно состыковать новый с существующим, а не создавать новый
        /// </summary>
        /// <returns></returns>
        private CheckResult checkRepeatArticle()
        {
            if (!string.IsNullOrEmpty(_curCompareMatr.RepeatArticle) )
            {
                _isRepeat = true;
                if (!string.Equals(_curCompareMatr.RepeatArticle.Trim(), _curCompareArticul.Articul.Trim()))
                    return CheckResult.Fail($"Повторный артикул, состыкуйте с существующим {_curCompareMatr.RepeatArticle}");
            }
            return CheckResult.Success();
        }
        private CheckResult checkFoundMod() 
        {
            if (!string.IsNullOrEmpty(_curCompareMatr.FoundMod) && !string.Equals(_curCompareMatr.FoundMod.Trim(), _curCompareArticul.Mod.Trim()))
            {
                return CheckResult.Fail($"Швейная модель уже создана в справочнике с торговым артикулом: {_curCompareMatr.FoundMod}");
            }

            return CheckResult.Success();
        }




    }

    public class CheckResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }

        public static CheckResult Success()
        {
            return new CheckResult
            {
                IsSuccess = true,
                ErrorMessage = string.Empty
            };
        }

        public static CheckResult Fail(string errorMessage)
        {
            return new CheckResult
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };
        }
    }


}
