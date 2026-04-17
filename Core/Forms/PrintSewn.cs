using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SewingProduction.Core.Models;
using SewingProduction.Core.services;
using SewingProduction.Features.UserDistribution.Helpers;

namespace SewingProduction.Core.Forms
{
    public partial class PrintSewn : CustomForm
    {
        private string _kod;
        private string _model;
        private List<PrintSewnRazmKolRow> _printSewnRazmKolRow;
        private readonly PrintSewnDataService _printSewnDataService = new PrintSewnDataService();
        private List<PrintSewnBlVshRow> _blVshRows = new List<PrintSewnBlVshRow>();
        public PrintSewn(UserClass user) : base(user)
        {
            InitializeComponent();
        }
        public PrintSewn(string kod, string model, List<PrintSewnRazmKolRow> printSewnRazmKolRow)
        {
            _kod = kod;
            _model = model;
            _printSewnRazmKolRow = printSewnRazmKolRow;
            InitializeComponent();
        }
        private async void PrintSewn_Load(object sender, EventArgs e)
        {
            try
            {
                gridControlRazmKol.DataSource = null;
                gridControlBlVsh.DataSource = null;

                gridControlRazmKol.DataSource = _printSewnRazmKolRow;

                bool ok = await PrnVshivAllAsync();
                if (!ok)
                    return;

                gridControlBlVsh.DataSource = _blVshRows;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки формы PrintSewn: {ex.Message}");
            }
        }
        private async Task<bool> PrnVshivAllAsync()
        {
            try
            {
                if (_printSewnRazmKolRow == null || _printSewnRazmKolRow.Count == 0)
                {
                    MessageBox.Show("Нет данных razm_kol для печати.");
                    return false;
                }

                string kod = _printSewnRazmKolRow[0].Kod ?? string.Empty;
                if (string.IsNullOrWhiteSpace(kod))
                {
                    MessageBox.Show("Не определен код артикула.");
                    return false;
                }

                var existKompl = await _printSewnDataService.GetExistKompl(kod);
                var kombProv = await _printSewnDataService.GetKompProv(kod);
                var ras2List = await _printSewnDataService.GetRas2ByKod(kod);

                if (ras2List == null || ras2List.Count == 0)
                {
                    MessageBox.Show("Не найдены данные артикула в view_sp_articul.");
                    return false;
                }

                ArticulModel ras2 = ras2List[0];

                int st = 2;
                string mod = ras2.Mod ?? string.Empty;
                string gost = ras2.Gost ?? string.Empty;
                string sost = ras2.Sost ?? string.Empty;

                if (mod.Contains("в") || mod.Contains("ю") || mod.Contains("х") || mod.Contains("э") ||
                    mod.Contains("е") || mod.Contains("л") || mod.Contains("п") || mod.Contains("ж") ||
                    gost == "ГОСТ 25294-2003" || gost == "ГОСТ 25295-2003")
                    st = 1;

                if (gost == "ГОСТ Р 53142-2008" || gost == "ГОСТ Р 53147-2008" || gost == "ГОСТ Р 53146-2008" ||
                    (mod.Length >= 3 && mod.Substring(2, 1) == "п" && sost == "100%х"))
                    st = 2;

                if (gost == "ГОСТ Р 53145-2008" || gost == "ГОСТ 12694-90" || gost == "ГОСТ Р 53144-2008")
                    st = 3;

                if ((mod.Contains("в") || mod.Contains("ю") || mod.Contains("х") || mod.Contains("э") ||
                     mod.Contains("е") || mod.Contains("л") || mod.Contains("п") || mod.Contains("ж") || mod.Contains("я")) &&
                    (gost == "ГОСТ Р 53142-2008" || gost == "ГОСТ Р 53147-2008" || gost == "ГОСТ Р 53146-2008" ||
                     (mod.Length >= 3 && mod.Substring(2, 1) == "п" && sost != "100%х")))
                    st = 4;

                _blVshRows = new List<PrintSewnBlVshRow>();

                int kol_it7 = 0;

                foreach (var razmRow in _printSewnRazmKolRow)
                {
                    kol_it7 += razmRow.Kol ?? 0;
                    int? kol_t = razmRow.Kol;

                    while (kol_t > 0)
                    {
                        var row = new PrintSewnBlVshRow
                        {
                            Grup = ras2.Grup ?? string.Empty,
                            Kod = razmRow.Kod ?? string.Empty,
                            Articul = razmRow.Articul ?? string.Empty,
                            Mod = razmRow.Mod ?? string.Empty,
                            Razm = FormatRazm(razmRow.Razm),
                            Razm1 = GetRazm1(razmRow.Razm),
                            Razm2 = GetRazm2(razmRow.Razm),
                            Kle = ras2.Kle ?? string.Empty,
                            Sost = kombProv != null && kombProv.Count > 0 ? kombProv[0].sost ?? string.Empty : ras2.Sost ?? string.Empty,
                            Sost2 = kombProv != null && kombProv.Count > 0 ? kombProv[0].sost2 ?? string.Empty : ras2.Sost2 ?? string.Empty,
                            Sost3 = kombProv != null && kombProv.Count > 0 ? kombProv[0].sost3 ?? string.Empty : ras2.Sost3 ?? string.Empty,
                            IdGost = ras2.Id_gost,
                            Gost = ras2.Gost ?? string.Empty,
                            IdSvyaz = ras2.Id_svyaz,
                            Kruj = ras2.Kruj.ToString()
                        };

                        _blVshRows.Add(row);

                        if (!string.IsNullOrWhiteSpace(ras2.Grup) &&
                            ras2.Grup.ToLower().Contains("пижама"))
                        {
                            _blVshRows.Add(new PrintSewnBlVshRow
                            {
                                Grup = row.Grup,
                                Kod = row.Kod,
                                Articul = row.Articul,
                                Mod = row.Mod,
                                Razm = row.Razm,
                                Razm1 = row.Razm1,
                                Razm2 = row.Razm2,
                                Kle = row.Kle,
                                Sost = row.Sost,
                                Sost2 = row.Sost2,
                                Sost3 = row.Sost3,
                                IdGost = row.IdGost,
                                Gost = row.Gost,
                                IdSvyaz = row.IdSvyaz,
                                Kruj = row.Kruj
                            });
                        }

                        kol_t--;
                    }
                }

                foreach (var row in _blVshRows)
                {
                    int? idGost = row.IdGost;
                    var svinf = await _printSewnDataService.GetGostSvPictByIdGost(idGost);
                    var svinf2 = await _printSewnDataService.GetGostById(idGost);

                    if (svinf2 == null || svinf2.Count == 0)
                    {
                        row.Symbol = string.Empty;
                        continue;
                    }

                    row.Symbol = Convert.ToString(svinf2[0].Symbol) ?? string.Empty;

                    if (svinf != null && svinf.Count > 0)
                    {
                        int ggost = svinf[0].Id_gost;
                        int ggroup = svinf[0].Id_vidchel;

                        var pred = await _printSewnDataService.GetGostPred(ggost, ggroup);
                    }
                }

                bool hasZeroSymbol = _blVshRows.Any(x => string.IsNullOrWhiteSpace(x.Symbol) || x.Symbol.Trim() == "0");
                if (hasZeroSymbol)
                {
                    MessageBox.Show("Нет символов по уходу");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подготовки печати вшивок: {ex.Message}");
                return false;
            }
        }
        private string FormatRazm(string razm)
        {
            if (string.IsNullOrWhiteSpace(razm))
                return string.Empty;

            string value = razm.Trim();

            if (value.Length < 12)
                return value;

            int index = value.IndexOf("(");
            if (index < 0)
                return value;

            return value.Replace("(", Environment.NewLine + "(");
        }
        private string GetRazm1(string razm)
        {
            if (string.IsNullOrWhiteSpace(razm))
                return string.Empty;

            int index = razm.IndexOf("(");
            if (index < 0)
                return razm;

            return razm.Substring(0, index);
        }
        private string GetRazm2(string razm)
        {
            if (string.IsNullOrWhiteSpace(razm))
                return string.Empty;

            int index = razm.IndexOf("(");
            if (index < 0)
                return string.Empty;

            return razm.Substring(index);
        }
        #region широкие ЭЙС, Клевер (новый)

        private int _torgMarkaPer = 1;
        private string _typeVsh = string.Empty;
        private string _razmSymbol = "v";
        private string _symbolImage = string.Empty;
        private string _nameVsh = string.Empty;
        private string _nomVyazAndRask = string.Empty;
        private async void customSimpleButtonACE_Click(object sender, EventArgs e)
        {
            bool ok = await PrnVshivAllAsync();
            if (!ok) return;

            if (_blVshRows == null || _blVshRows.Count == 0)
            {
                MessageBox.Show("Нет данных для печати.");
                return;
            }

            var distinctSost = _blVshRows
                .Select(x => new
                {
                    Sost = x.Sost?.Trim() ?? string.Empty,
                    Sost2 = x.Sost2?.Trim() ?? string.Empty,
                    Sost3 = x.Sost3?.Trim() ?? string.Empty
                })
                .Distinct()
                .ToList();

            if (distinctSost.Count > 1)
            {
                MessageBox.Show("Внимание! Разный состав для одной модели! Печать невозможна!");
                return;
            }

            var pechCheck = _blVshRows
                .Select(x => GetKod7(x.Kod))
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct()
                .ToList();

            if (pechCheck.Count > 1)
            {
                int? psaIdOsn = await _printSewnDataService.GetPsaIdOsnByNomAsync(_nomVyazAndRask);
                if (psaIdOsn.HasValue && psaIdOsn.Value != 0)
                {
                    MessageBox.Show("Печатать по кнопке 'Комплекты'");
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(_nomVyazAndRask) && _nomVyazAndRask.StartsWith("32"))
            {
                MessageBox.Show("Печать по кнопке Вшивки для текстиля ЭЙС (130x50)!");
                return;
            }

            string firstKod7 = pechCheck.FirstOrDefault() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(firstKod7))
            {
                bool isNabor = await _printSewnDataService.IsNaborOdezhdyAsync(firstKod7);
                if (isNabor)
                {
                    MessageBox.Show("Это набор одежды! Печать по другой кнопке");
                    return;
                }
            }

            var firstRow = _blVshRows[0];

            if (firstRow.IdGost == 216)
            {
                MessageBox.Show("Спецодежда! Печать по кнопке Спецодежда!");
                return;
            }

            _torgMarkaPer = await _printSewnDataService.GetOsUseTmAsync(_nomVyazAndRask);

            _typeVsh = "_double2";
            _razmSymbol = "v";
            _symbolImage = BuildSymbolImage(_razmSymbol, firstRow.Symbol);

            if (firstRow.Articul == "1Ф1686" ||
                firstRow.Articul == "1Ф1673" ||
                firstRow.Articul == "1В576" ||
                firstRow.Articul == "1Ф1798" ||
                firstRow.Articul == "1В298" ||
                firstRow.Articul == "1Ф1808" ||
                firstRow.Articul == "1М448")
            {
                _razmSymbol = "w";
                _nameVsh = "vshivka_big1_17" + _typeVsh + ".frx";

                await PrintCurrentTemplateAsync(_nameVsh);
                return;
            }

            if (firstRow.Articul == "1Ю176" || firstRow.Articul == "1Р112")
            {
                _razmSymbol = "w";
                _nameVsh = "vshivka_big_zam1_17" + _typeVsh + ".frx";

                await PrintCurrentTemplateAsync(_nameVsh);
                return;
            }

            if (firstRow.Articul == "1А68")
            {
                _nameVsh = "vshivka_big_zam2_17" + _typeVsh + ".frx";

                await PrintCurrentTemplateAsync(_nameVsh);
                return;
            }

            if (firstRow.Articul == "1Ф1780" || firstRow.Articul == "1Ф1781")
            {
                _nameVsh = "vshivka_big2_17" + _typeVsh + ".frx";

                await PrintCurrentTemplateAsync(_nameVsh);
                return;
            }

            _razmSymbol = "w";
            _symbolImage = BuildSymbolImage(_razmSymbol, firstRow.Symbol);

            if (HasMstrFields())
                _nameVsh = "vshivka_big3_17" + _typeVsh + "_1.frx";
            else
                _nameVsh = "vshivka_big3_17" + _typeVsh + ".frx";

            await PrintCurrentTemplateAsync(_nameVsh);

            int? nn = await _printSewnDataService.GetPlanSezonNnByNomAsync(_nomVyazAndRask);
            if (!nn.HasValue)
                return;

            bool hasKomplJoin = await _printSewnDataService.HasKomplJoinVshivkiAsync(nn.Value);
            if (hasKomplJoin)
            {
                MessageBox.Show(
                    "Напечатан " +
                    (firstRow.Grup ?? string.Empty).Split(' ').FirstOrDefault()?.ToUpper() +
                    Environment.NewLine + Environment.NewLine +
                    "Нажмите ОК для печати 2-го экземпляра вшивок для Комплекта/Пижамы");

                await PrintCurrentTemplateAsync(_nameVsh);
            }
        }
        private string GetKod7(string kod)
        {
            if (string.IsNullOrWhiteSpace(kod))
                return string.Empty;

            string value = kod.Trim();
            return value.Length <= 7 ? value : value.Substring(0, 7);
        }
        private string BuildSymbolImage(string razmSymbol, string symbol)
        {
            string pathZnak = @"\\dbfsv\ace\prog\proizv_set\znak\";
            return System.IO.Path.Combine(pathZnak, $"{razmSymbol}{symbol}.JPG");
        }
        private bool HasMstrFields()
        {
            return _blVshRows.Any();
        }
        private Task PrintCurrentTemplateAsync(string reportName)
        {
            MessageBox.Show($"Выбран шаблон печати: {reportName}");
            return Task.CompletedTask;
        }
        #endregion
        #region Комплекты

        #endregion
        #region Набор одежды

        #endregion
        private void customSimpleButtonKompl_Click(object sender, EventArgs e)
        {

        }

        private void customSimpleButtonNabor_Click(object sender, EventArgs e)
        {

        }

    }
}
