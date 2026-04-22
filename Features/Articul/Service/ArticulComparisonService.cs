using DevExpress.UIAutomation;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SewingProduction.Features.Articul.Models;

namespace SewingProduction.Features.Articul.Service
{
    public sealed class ArticulComparisonService
    {
        private string _kod;
        private string _nn;
        private SpArtPreviewModel _curCompareArticul;
        private CreateArticulMatrModel _curCompareMatr;


        //private readonly DbService _dbService = new DbService(new DatabaseHelper());
        //private readonly DatabaseHelper _dbHelper;
        private readonly FileLogger _logger = new FileLogger();
        private CreateArticulMatrService _createArticulMatrService = new CreateArticulMatrService();


        public ArticulComparisonService(string nn, SpArtPreviewModel curArt)
        {
            _nn = nn;
            _curCompareArticul = curArt;
            _kod = _curCompareArticul.Kod;
        }
        /// <summary>
        /// выполняются проверки для стыковки артикула
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CanLinkArticul() 
        {
            try
            {
                if (_curCompareArticul.Arh == 1)
                {
                    MessageBox.Show("Архивный артикул", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                var checkKomplTask = _createArticulMatrService.GetCheckArticulKomplsCompareAsync(_nn, _kod);
                var checkArhGost = _createArticulMatrService.GetGostArhAsync(_kod);

                await Task.WhenAll(checkKomplTask, checkArhGost);

                if (!string.IsNullOrEmpty(checkKomplTask.Result))
                {
                    MessageBox.Show(checkKomplTask.Result, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if (checkArhGost.Result == 1)
                {
                    MessageBox.Show("Архивный ГОСТ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при стыковке артикулов CanLinkArticul");
                return false;
            }

        }

    }
}
