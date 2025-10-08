using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Extensions;
using SewingProduction.Features.CuttingProduction.Models;
using SewingProduction.Features.CuttingProduction.Services;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.CuttingProduction.Forms
{
    public partial class CuttingForm : CustomForm
    {
        private static DatabaseHelper _dbHelper;
        private static DbService _dbService;
        private readonly ILogger _logger = new FileLogger();
        private readonly CuttingService _cuttingService;
        private List<raskrZehUpView> _currentRzuData = new List<raskrZehUpView>();
        private BindingList<raskrZehUpView> _rzuBindingList;
        private BindingSource _rzuBindingSource;
        private Panel overlay;
        private PictureBox spinnerPb;
        public CuttingForm()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper();
            _dbService = new DbService(_dbHelper);
            _cuttingService = new CuttingService(_dbHelper);
            CreateOverlaySpinner();

        }
        private async void CuttingForm_Load(object sender, EventArgs e)
        {
            Task bindingsTask = InitializeBindingsAsync();
            await Task.WhenAll(bindingsTask);
            ShowOverlay();
            try
            {
                await LoadRzuAsync();
            }
            finally { HideOverlay(); }
        }
        private async Task InitializeBindingsAsync()
        {
            try
            {
                var rzuViewTask = Task.Run(() =>
                {
                    _rzuBindingList = new BindingList<raskrZehUpView>();
                    _rzuBindingSource = new BindingSource { DataSource = _rzuBindingList };
                });
                await Task.WhenAll(rzuViewTask);
                #region увязка customGridRzu с данными модели

                customGridRzu.DataSource = _rzuBindingSource;
                gridColumnRzuChipStatus.FieldName = "PR_CRPT";
                gridColumnRzuNom.FieldName = "nom";
                gridColumnRzuNomN.FieldName = "nom_n";
                gridColumnRzuNPach.FieldName = "n_pach";
                gridColumnRzuDataR.FieldName = "data_r";
                gridColumnRzuArticul.FieldName = "articul";
                gridColumnRzuMod.FieldName = "mod";
                gridColumnRzuRazm.FieldName = "razm";
                gridColumnRzuKol.FieldName = "kol";
                gridColumnRzuDataPr.FieldName = "HasDatePr";
                gridColumnRzuDataPt.FieldName = "HadDatePt";
                gridColumnRzuDataV.FieldName = "HasDateV";
                gridColumnRzuBus.FieldName = "bus";
                gridColumnRzuStra.FieldName = "stra";
                gridColumnRzuGofp.FieldName = "gofp";
                gridColumnRzuPPress.FieldName = "p_pres";
                gridColumnRzuDtfPrint.FieldName = "dtf_print";
                gridColumnRzuBd.FieldName = "bd";
                gridColumnRzuTabP.FieldName = "tab_p";
                gridColumnRzuTabKlad.FieldName = "tab_klad";
                gridColumnRzuTabBuh.FieldName = "tab_buh";
                gridColumnRzuTabM.FieldName = "tab_m";
                gridColumnRzuTabK.FieldName = "tab_k";
                gridColumnRzuTabKpv.FieldName = "tab_kpv";
                gridColumnRzuVsh.FieldName = "vsh";
                gridColumnRzuKle.FieldName = "kle";
                gridColumnRzuDataZeh.FieldName = "data_zeh";
                gridColumnRzuDostZeh.FieldName = "dost_zeh";
                gridColumnRzuPomBezKompl.FieldName = "pombezkompl";
                gridColumnRzuHReestr.FieldName = "h_reestr";
                gridColumnRzuDataDubcd.FieldName = "data_dubcd";
                gridColumnRzuDataSkZp.FieldName = "data_sk_zp";
                gridColumnRzuIz.FieldName = "iz";
                gridColumnRzuDataRst.FieldName = "data_rst";
                gridColumnRzuVipad.FieldName = "vipad";
                gridColumnRzuKodK.FieldName = "kod_k";
                gridColumnRzuDataRasp.FieldName = "data_rasp";
                gridColumnRzuDataRasv.FieldName = "data_rasv";
                gridColumnRzuDataCd.FieldName = "data_cd";
                gridColumnRzuMgZakr.FieldName = "mg_zakr";
                gridColumnRzuNomZad.FieldName = "nom_zad";
                gridColumnRzuDataP.FieldName = "data_p";
                gridColumnRzuDataKlad.FieldName = "data_klad";
                gridColumnRzuDataSozd.FieldName = "data_sozd";
                gridColumnRzuPachYear.FieldName = "PachYear";
                gridColumnRzuPachYear.Visible = false;
                gridViewRzu.OptionsView.EnableAppearanceEvenRow = false;
                gridViewRzu.OptionsView.EnableAppearanceOddRow = false;
                #endregion
                #region увязка текстбоксов с данными из грида
                TabR1TextBox.DataBindings.Add("Text", _rzuBindingSource, nameof(raskrZehUpView.tab_r1), true, DataSourceUpdateMode.Never);
                Fio1Label.DataBindings.Add("Text", _rzuBindingSource, nameof(raskrZehUpView.fio1), true, DataSourceUpdateMode.Never);
                TabR2TextBox.DataBindings.Add("Text", _rzuBindingSource, nameof(raskrZehUpView.tab_r2), true, DataSourceUpdateMode.Never);
                Fio2Label.DataBindings.Add("Text", _rzuBindingSource, nameof(raskrZehUpView.fio2), true, DataSourceUpdateMode.Never);
                TabOrkTextBox.DataBindings.Add("Text", _rzuBindingSource, nameof(raskrZehUpView.tab_ork), true, DataSourceUpdateMode.Never);
                FioOrkLabel.DataBindings.Add("Text", _rzuBindingSource, nameof(raskrZehUpView.fio_ork), true, DataSourceUpdateMode.Never);
                DataPrTextBox.DataBindings.Add("Text", _rzuBindingSource, nameof(raskrZehUpView.data_pr), true, DataSourceUpdateMode.Never);
                DataVTextBox.DataBindings.Add("Text", _rzuBindingSource, nameof(raskrZehUpView.data_v), true, DataSourceUpdateMode.Never);
                VipadTextBox.DataBindings.Add("Text", _rzuBindingSource, nameof(raskrZehUpView.vipad), true, DataSourceUpdateMode.Never);
                pachTextBox.DataBindings.Add("Text", _rzuBindingSource, nameof(raskrZehUpView.n_pach), true, DataSourceUpdateMode.Never);
                DataRTextBox.DataBindings.Add("Text", _rzuBindingSource, nameof(raskrZehUpView.data_r), true, DataSourceUpdateMode.Never);
                ArticulKTextBox.DataBindings.Add("Text", _rzuBindingSource, nameof(raskrZehUpView.articul_k), true, DataSourceUpdateMode.Never);
                ModKTextBox.DataBindings.Add("Text", _rzuBindingSource, nameof(raskrZehUpView.mod_k), true, DataSourceUpdateMode.Never);
                FioKLabel.DataBindings.Add("Text", _rzuBindingSource, nameof(raskrZehUpView.fio_k), true, DataSourceUpdateMode.Never);
                dateZehTextBox.DataBindings.Add("Text", _rzuBindingSource, nameof(raskrZehUpView.data_zeh), true, DataSourceUpdateMode.Never);
                dostZehTextBox.DataBindings.Add("Text", _rzuBindingSource, nameof(raskrZehUpView.dost_zeh), true, DataSourceUpdateMode.Never);
                datePlanTextBox.DataBindings.Add("Text", _rzuBindingSource, nameof(raskrZehUpView.data_plan), true, DataSourceUpdateMode.Never);
                nomZadTextBox.DataBindings.Add("Text", _rzuBindingSource, nameof(raskrZehUpView.nom_zad), true, DataSourceUpdateMode.Never);
                rzIdTextBox.DataBindings.Add("Text", _rzuBindingSource, nameof(raskrZehUpView.rz_id), true, DataSourceUpdateMode.Never);
                nZvetTextBox.DataBindings.Add("Text", _rzuBindingSource, nameof(raskrZehUpView.n_zvet), true, DataSourceUpdateMode.Never);
                YearSearchTextBox.Text = DateTime.Now.Year.ToString();

                #endregion


            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок");
                throw;
            }
        }
        private async Task LoadRzuAsync()
        {
            try
            {
                _rzuBindingSource.Clear();
                _rzuBindingSource.ResetBindings(false);
                //List<VyazPlanView> vyazPlanViewData = await _vyazService.GetVyazPlanView();
                //var vyazPlanViewData = await _vyazService.GetVyazPlanView();
                var RzuData = await _cuttingService.GetRaskrZehUpViewAsync();
                //_vyazPlanViewBindingList = vyazPlanViewData;
                //_vyazPlanViewBindingList = await _vyazService.GetVyazPlanView();
                if (RzuData != null)
                {
                    await _logger.LogEventAsync($"Получены данные RaskrZehUpView", "LoadCuttingForm");

                    await this.InvokeAsync(() =>
                    {
                        _currentRzuData = RzuData;                // Обновляем текущую модель
                        _rzuBindingSource.DataSource = _currentRzuData; // Привязываем данные к форме
                    });

                    await _logger.LogEventAsync($"Данные VyazPlanView успешно загружены", "LoadVyazPlanDataAsync");
                    //LoadList(vyazPlanViewData, _vyazPlanViewBindingList, nameof(NormRasz.nrId));
                    _rzuBindingList.Add(RzuData[0]);
                    _rzuBindingSource.ResetBindings(false);
                    var ordered = RzuData.OrderBy(x => x.nom).ToList();

                    int currentGroup = 0;
                    decimal? lastNom = null;

                    foreach (var row in ordered)
                    {
                        if (lastNom != row.nom)
                        {
                            currentGroup++;
                            lastNom = row.nom;
                        }

                        row.groupIndex = currentGroup % 2; // 0 или 1
                    }
                }
                else
                {
                    await _logger.LogEventAsync($"Не удалось найти данные rzu", "LoadVyazPlanDataAsync");
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка загрузки данных VyazPlanView");
            }
        }
        private void CreateOverlaySpinner()
        {
            // Панель-оверлей, закрывающая форму (приобр. полупрозрачный фон)
            overlay = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(120, Color.Gray), // полупрозрачный серый
                Visible = false
            };

            // PictureBox по центру для GIF
            spinnerPb = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.CenterImage,
                Size = new Size(128, 128),
                Anchor = AnchorStyles.None
            };

            // Путь к GIF-файлу; положите файл рядом с .exe или укажите абсолютный путь.
            string gifPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "loading_dude.gif");
            if (File.Exists(gifPath))
            {
                spinnerPb.Image = Image.FromFile(gifPath); // сохраняет анимацию
            }
            else
            {
                MessageBox.Show("GIF не найден: " + gifPath);
            }

            // Центрируем PictureBox внутри оверлея
            overlay.Controls.Add(spinnerPb);
            overlay.ControlAdded += (s, e) => CenterSpinner();

            this.Controls.Add(overlay);
            overlay.BringToFront();

            // при изменении размера формы — заново центрировать
            this.Resize += (s, e) => CenterSpinner();
        }
        private void CenterSpinner()
        {
            if (overlay == null || spinnerPb == null) return;
            spinnerPb.Left = (overlay.ClientSize.Width - spinnerPb.Width) / 2;
            spinnerPb.Top = (overlay.ClientSize.Height - spinnerPb.Height) / 2;
        }
        private void ShowOverlay() => overlay.Visible = true;
        private void HideOverlay() => overlay.Visible = false;
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void xtraTabPage1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void customLabel13_Click(object sender, EventArgs e)
        {

        }

        private void customLabel16_Click(object sender, EventArgs e)
        {

        }

        private void customGridRzu_Click(object sender, EventArgs e)
        {

        }

        private void YearSearchTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void customLabel19_Click(object sender, EventArgs e)
        {

        }

        private void gridViewRzu_RowStyle(object sender, RowStyleEventArgs e)
        {

            var row = gridViewRzu.GetRow(e.RowHandle) as raskrZehUpView;
            if (row == null) return;

            decimal? nom = row.nom;
            int groupIndex = row.groupIndex;

            e.Appearance.BackColor = groupIndex == 0 ? Color.White : Color.LightGray;
        }

        private void gridViewRzu_DoubleClick(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            var pt = view.GridControl.PointToClient(Control.MousePosition);
            var hitInfo = view.CalcHitInfo(pt);
            if (hitInfo.InRow || hitInfo.InRowCell)
            {
                string fieldName = hitInfo.Column.FieldName;
                decimal? clickedNom = Convert.ToDecimal(view.GetRowCellValue(hitInfo.RowHandle, "nom").ToString());
                var recordsToUpdate = _rzuBindingSource.Cast<raskrZehUpView>().Where(record => record.nom == clickedNom).ToList();
                bool exitForEach = false;
                bool updDataTag = false;
                bool tagUpdateData = false;
                object? valueUpd = null;
                foreach (var record in recordsToUpdate)
                {

                    switch (fieldName)
                    {
                        case "tab_p":
                            if ((record.tab_p != null && !string.IsNullOrEmpty(TabPTextBox.Text)) || record.tab_p == null)
                            {
                                if (int.TryParse(TabPTextBox.Text.Trim(), out int tabNum))
                                {
                                    record.tab_p = tabNum;
                                    valueUpd = tabNum;
                                    tagUpdateData = true;
                                }
                                else
                                {
                                    MessageBox.Show("Введите корректный табельный номер (число).");
                                    exitForEach = true;
                                }
                            }
                            else
                            {
                                record.tab_p = null;
                                valueUpd = null;
                                tagUpdateData = true;
                            }
                            break;
                        case "tab_m":
                            if ((record.tab_m != null && !string.IsNullOrEmpty(TabMTextBox.Text)) || record.tab_m == null)
                            {
                                if (int.TryParse(TabMTextBox.Text.Trim(), out int tabNum))
                                {
                                    record.tab_m = tabNum;
                                    valueUpd = tabNum;
                                    tagUpdateData = true;
                                }
                                else
                                {
                                    MessageBox.Show("Введите корректный табельный номер (число).");
                                    exitForEach = true;
                                }
                            }
                            else
                            {
                                record.tab_m = null;
                                valueUpd = null;
                                tagUpdateData = true;
                            }
                            break;
                        case "tab_kpv":
                            if ((record.tab_kpv != null && !string.IsNullOrEmpty(TabKpvTextBox.Text)) || record.tab_kpv == null)
                            {
                                if (int.TryParse(TabKpvTextBox.Text.Trim(), out int tabNum))
                                {
                                    record.tab_kpv = tabNum;
                                    valueUpd = tabNum;
                                    tagUpdateData = true;
                                }
                                else
                                {
                                    MessageBox.Show("Введите корректный табельный номер (число).");
                                    exitForEach = true;
                                }
                            }
                            else
                            {
                                record.tab_kpv = null;
                                valueUpd = null;
                                tagUpdateData = true;
                            }
                            break;
                        // Добавьте другие поля по необходимости
                        case "data_zeh":
                            DataTable dt = _cuttingService.GetNewDataRzu(record.nom);
                            DataRow dr = dt.Rows[0];
                            if (dt != null)
                            {
                                if (int.Parse(dr["tab_k"].ToString()) == 0)
                                {
                                    MessageBox.Show("Не заполнен табельный комплектовщика!");
                                    exitForEach = true;
                                }
                                if (string.IsNullOrEmpty(dr["data_kk_cd"].ToString()))
                                {
                                    MessageBox.Show("Нет даты готово контролера кроя");
                                    exitForEach = true;
                                }
                                if (int.Parse(dr["n_zeh"].ToString()) != 0)
                                {
                                    MessageBox.Show("Заполнен номер отгрузки в бригаду!");
                                    exitForEach = true;
                                }
                                if (!string.IsNullOrEmpty(dr["dost_zeh"].ToString().Trim()))
                                {
                                    MessageBox.Show("Заполнена бригада!");
                                    exitForEach = true;
                                }
                                if (record.data_zeh != null)
                                {
                                    MessageBox.Show("Дата в цех уже стоит!");
                                    exitForEach = true;
                                }
                                if (!exitForEach)
                                {
                                    valueUpd = DateTime.Now.Date;
                                    tagUpdateData = true;
                                    record.data_zeh = DateTime.Now;
                                }


                            }
                            break;
                    }
                    if (exitForEach)
                        break;
                }
                if ((!exitForEach && tagUpdateData))
                {
                    _cuttingService.SetValue(fieldName, valueUpd, clickedNom);
                }
                _rzuBindingSource.ResetBindings(false);
                gridViewRzu.RefreshData();
            }


        }

        private void gridViewRzu_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Clicks == 2) // проверяем двойной клик
            //{
            //    GridView view = sender as GridView;
            //    var pt = view.GridControl.PointToClient(MousePosition);
            //    var hitInfo = view.CalcHitInfo(pt);

            //    if (hitInfo.InRow || hitInfo.InRowCell)
            //    {
            //        int rowHandle = hitInfo.RowHandle;
            //        string newValue = TabPTextBox.Text.Trim();

            //        if (int.TryParse(newValue, out int tabNum))
            //        {
            //            // отключаем редактор, чтобы можно было вставить значение
            //            view.CloseEditor();
            //            view.SetRowCellValue(rowHandle, "tab_p", tabNum);
            //        }
            //        else
            //        {
            //            MessageBox.Show("Введите корректный табельный номер (число).");
            //        }
            //    }
            //}
        }

        private void customGridRzu_DoubleClick(object sender, EventArgs e)
        {

        }

        private void gridViewRzu_ShowingEditor(object sender, CancelEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName == "tab_p" || view.FocusedColumn.FieldName == "tab_m" || view.FocusedColumn.FieldName == "tab_kpv" || view.FocusedColumn.FieldName == "data_zeh")
            {
                e.Cancel = true; // блокируем редактирование
            }
        }

        private void tabKTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void SearchPachСustomButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(YearSearchTextBox.Text) && !string.IsNullOrEmpty(PachSearchTextBox.Text))
            {
                GridColumn column = gridViewRzu.Columns["PachYear"];
                int rowHandle = gridViewRzu.LocateByValue(0, column, $"{PachSearchTextBox.Text}{YearSearchTextBox.Text}");
                if (rowHandle != GridControl.InvalidRowHandle)
                {
                    gridViewRzu.FocusedRowHandle = gridViewRzu.LocateByValue(0, column, $"{PachSearchTextBox.Text}{YearSearchTextBox.Text}");
                    gridViewRzu.ShowEditor();
                }
                else
                {
                    MessageBox.Show("Запись не найдена!");
                }


            }
        }

        private void TabPTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TabPTextBox.Text))
            {
                TabPLabel.Text = _cuttingService.GetFio(int.Parse(TabPTextBox.Text));
                TabPLabel.Refresh();
            }
            else
            {
                TabPLabel.Text = "ФИО";
                TabPLabel.Refresh();
            }
        }

        private void TabMTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TabMTextBox.Text))
            {
                TabMLabel.Text = _cuttingService.GetFio(int.Parse(TabMTextBox.Text));
                TabMLabel.Refresh();
            }
            else
            {
                TabMLabel.Text = "ФИО";
                TabMLabel.Refresh();
            }
        }

        private void TabKpvTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TabKpvTextBox.Text))
            {
                TabKpvLabel.Text = _cuttingService.GetFio(int.Parse(TabKpvTextBox.Text));
                TabKpvLabel.Refresh();
            }
            else
            {
                TabKpvLabel.Text = "ФИО";
                TabKpvLabel.Refresh();
            }
        }

        private void customTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void nZvetTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void nZvetTextBox_Validated(object sender, EventArgs e)
        {
            string message = "Вы уверены что хотите сохранить изменения?";
            DialogResult result = MessageBox.Show(
           message,
           "Подтверждение сохранения",
           MessageBoxButtons.YesNo,
           MessageBoxIcon.Question,
           MessageBoxDefaultButton.Button2);

            if (result == DialogResult.Yes)
            {

                var view = gridViewRzu;
                int rowHandle = view.FocusedRowHandle;

                TextBox textBox = (TextBox)sender;
                var rowRzu = view.GetRow(rowHandle) as raskrZehUpView;
                decimal? clickedNom = rowRzu.nom;
                var recordsToUpdate = _rzuBindingSource.Cast<raskrZehUpView>().Where(record => record.nom == clickedNom).ToList();
                foreach (var record in recordsToUpdate)
                {
                    record.n_zvet = textBox.Text.ToString();
                }
                _cuttingService.SetValue("n_zvet", textBox.Text.ToString(), clickedNom);
                MessageBox.Show("Сохранено!");
                gridViewRzu.RefreshData();

            }
            else
            {
                _rzuBindingSource.ResetBindings(false);
            }
        }

        private void customGridRzu_DoubleClick_1(object sender, EventArgs e)
        {

        }

        private void gridViewRzu_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void gridViewRzu_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var view = gridViewRzu;
            int rowHandle = view.FocusedRowHandle;
            var rowRzu = view.GetRow(rowHandle) as raskrZehUpView;
            string? clickedNomZad = rowRzu.nom_zad;
            decimal? clickedNom = rowRzu.nom;
            recNomZadTextBox.Text = _cuttingService.getRecForNomZad(clickedNomZad);
            recNomTextBox.Text = _cuttingService.getRecForNom(clickedNom);
            recNomZadTextBox.Refresh();
            recNomTextBox.Refresh();
        }

        private void customButton1_Click(object sender, EventArgs e)
        {
            CuttingFormAdding FDI = new CuttingFormAdding();

            DialogResult result = FDI.ShowDialog();
        }
    }
}
