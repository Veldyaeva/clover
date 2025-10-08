using DevExpress.CodeParser;
using DevExpress.DirectX.Common.Direct2D;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class NormOperNew : CustomForm
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly DbService _dbService;
        private readonly ArtNormService _artNormService;
        private readonly ILogger _logger = new FileLogger();
        private readonly TWGridHelper _gridHelper = new TWGridHelper();
        private readonly int _annId;
        private System.Collections.Generic.List<KodProizvModel> kodProizvList;
        private System.Collections.Generic.List<PodrVyazModel> podrVyazList;
        private System.Collections.Generic.List<OborudShvModel> oborudShvList;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)] 
        public NormRasz SelectedRowData { get; private set; }
        public NormOperNew()
        {
            InitializeComponent();
        }
        public NormOperNew(int annId)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper();
            _dbService = new DbService(_dbHelper);
            _artNormService = new ArtNormService(_dbHelper);

            _annId = annId;
            ThemeManager.UpdateTheme(this);

            // Режим редактирования через EditForm и подписки
            gridView1.OptionsBehavior.EditingMode = GridEditingMode.EditForm;
            gridView1.EditFormShowing += gridView1_EditFormShowing;
            gridView1.KeyDown += gridView1_KeyDown;
        }

        private async void NormOperNew_Load(object sender, EventArgs e)
        {
            try
            {
                _gridHelper.LoadGridViewSettings(gridView1, "NormOperGrid.xml");
                LoadData();

                // Загрузка справочников для выпадающих списков (как в TeamWork_AdvanceTW)
                kodProizvList = await _dbService.GetListAsync<KodProizvModel>("SELECT kod_proizv, text_proizv FROM kod_proizv", null);
                podrVyazList = await _dbService.GetListAsync<PodrVyazModel>("SELECT kod_vyaz, text_vyaz, kod_proizv FROM podr_vyaz", null);
                oborudShvList = await _dbService.GetListAsync<OborudShvModel>("SELECT kod_ob, text_ob FROM spOborudShv", null);

                ConfigureLookups();
            }
            catch (ConstraintException ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await _logger.LogErrorAsync(ex, "Ошибка загрузки данных}");
            }
        }
        private void NormOperNew_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Сохраняем настройки грида при закрытии формы
                _gridHelper.SaveGridViewSettings(gridView1, "NormOperGrid.xml");
            }

            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при сохранении настроек грида");
            }

        }

        private void ConfigureLookups()
        {
            try
            {
                // kod_proizv
                var colKodProizv = gridView1.Columns["kod_proizv"];
                if (colKodProizv != null)
                {
                    var repoKodProizv = new RepositoryItemLookUpEdit
                    {
                        DataSource = kodProizvList,
                        DisplayMember = "text_proizv",
                        ValueMember = "kod_proizv",
                        NullText = "[Выберите производство]"
                    };
                    repoKodProizv.EditValueChanged += (s, e) =>
                    {
                        if (gridView1.FocusedRowHandle < 0) return;
                        if (s is LookUpEdit editor && editor.EditValue != null && int.TryParse(editor.EditValue.ToString(), out int kodProizv))
                        {
                            var prodItem = kodProizvList?.FirstOrDefault(x => x.kod_proizv == kodProizv);
                            if (prodItem != null)
                            {
                                gridView1.SetFocusedRowCellValue("text_proizv", prodItem.text_proizv);
                            }
                        }
                    };
                    colKodProizv.ColumnEdit = repoKodProizv;
                }

                // kod_podr (вязальное подразделение)
                var colPodrVyaz = gridView1.Columns["kod_podr"];
                if (colPodrVyaz != null)
                {
                    var repoPodrVyaz = new RepositoryItemLookUpEdit
                    {
                        DataSource = podrVyazList,
                        DisplayMember = "text_vyaz",
                        ValueMember = "kod_vyaz",
                        NullText = "[Выберите подразделение]"
                    };
                    // Фильтруем по текущему kod_proizv при открытии
                    repoPodrVyaz.QueryPopUp += (s, e) =>
                    {
                        if (gridView1.FocusedRowHandle < 0) return;
                        var kodProizvObj = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "kod_proizv");
                        if (kodProizvObj != null && int.TryParse(kodProizvObj.ToString(), out int kodProizv))
                        {
                            var filtered = podrVyazList
                                .Where(x => x.kod_proizv == kodProizv || x.kod_proizv == 9)
                                .ToList();
                            if (s is LookUpEdit editor)
                            {
                                editor.Properties.DataSource = filtered;
                                editor.Properties.ValueMember = "kod_vyaz";
                                editor.Properties.DisplayMember = "text_vyaz";
                                editor.Properties.PopulateColumns();
                            }
                        }
                    };
                    repoPodrVyaz.EditValueChanged += (s, e) =>
                    {
                        if (gridView1.FocusedRowHandle < 0) return;
                        if (s is LookUpEdit editor && editor.EditValue != null && int.TryParse(editor.EditValue.ToString(), out int kodVyaz))
                        {
                            var vyazItem = podrVyazList?.FirstOrDefault(x => x.kod_vyaz == kodVyaz);
                            if (vyazItem != null)
                            {
                                gridView1.SetFocusedRowCellValue("text_vyaz", vyazItem.text_vyaz);
                            }
                        }
                    };
                    colPodrVyaz.ColumnEdit = repoPodrVyaz;
                }

                // kod_ob (оборудование)
                var colOborudShv = gridView1.Columns["kod_ob"];
                if (colOborudShv != null)
                {
                    var repoOborud = new RepositoryItemLookUpEdit
                    {
                        DataSource = oborudShvList,
                        DisplayMember = "text_ob",
                        ValueMember = "kod_ob",
                        NullText = "[Выберите оборудование]"
                    };
                    repoOborud.EditValueChanged += (s, e) =>
                    {
                        if (gridView1.FocusedRowHandle < 0) return;
                        if (s is LookUpEdit editor && editor.EditValue != null && int.TryParse(editor.EditValue.ToString(), out int kodOb))
                        {
                            var obItem = oborudShvList?.FirstOrDefault(x => x.kod_ob == kodOb);
                            if (obItem != null)
                            {
                                gridView1.SetFocusedRowCellValue("text_ob", obItem.text_ob);
                            }
                        }
                    };
                    colOborudShv.ColumnEdit = repoOborud;
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка конфигурации выпадающих списков в NormOperNew");
            }
        }

        /// <summary>
        /// Загружает данные в `GridControl`
        /// </summary>
        private async void LoadData()
        {
            try
            {
                DataTable data = await _artNormService.GetNormOper();

                if (data != null && data.Rows.Count > 0)
                {
                    customGridControl1.DataSource = data;
                }
                else
                {
                    MessageBox.Show("Нет данных для отображения.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка загрузки данных в GridControl");
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridView1_EditFormShowing(object sender, EditFormShowingEventArgs e)
        {
            var view = sender as GridView;
            if (view == null) return;

            // Перехватываем попытку добавления через NewItemRow и открываем EditForm на новой строке
            if (view.IsNewItemRow(e.RowHandle))
            {
                e.Allow = false;
                try { AddNewRowToGrid(); } catch { }
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus)
                {
                    AddNewRowToGrid();
                    e.Handled = true;
                }
            }
            catch { }
        }
        private void customAddButton_Click(object sender, EventArgs e)
        {
            try { AddNewRowToGrid(); }
            catch { }
        }
        private void AddNewRowToGrid()
        {
            try
            {
                var dt = customGridControl1?.DataSource as DataTable;
                if (dt == null) return;

                gridView1.BeginDataUpdate();
                try
                {
                    var newRow = dt.NewRow();
                    // Базовые значения по умолчанию
                    if (dt.Columns.Contains("n1")) newRow["n1"] = 0;
                    if (dt.Columns.Contains("sek")) newRow["sek"] = 0;
                    if (dt.Columns.Contains("razryd")) newRow["razryd"] = 0;

                    dt.Rows.Add(newRow);
                }
                finally
                {
                    gridView1.EndDataUpdate();
                }

                // Фокус на новой строке и открытие EditForm
                int newIndex = dt.Rows.Count - 1;
                int handle = gridView1.GetRowHandle(newIndex);
                if (gridView1.IsValidRowHandle(handle))
                {
                    gridView1.FocusedRowHandle = handle;
                    gridView1.MakeRowVisible(handle);
                    gridView1.ShowEditForm();
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при добавлении новой строки в NormOperNew");
            }
        }

        private async void customOkButton1_Click(object sender, EventArgs e)
        {
            try
            {
                GridView view = gridView1;
                if (view == null || view.FocusedRowHandle < 0) return;

                SelectedRowData = NormalizeDataFromView(view, view.FocusedRowHandle);
                SelectedRowData.annId = _annId; // AnnId устанавливается здесь
                SelectedRowData.IsNew = true;   // IsNew также устанавливается здесь

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка вставки", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                await _logger.LogErrorAsync(ex, "InvalidOperationException при выборе строки в NormOperNew");
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при выборе строки в NormOperNew");
                MessageBox.Show($"Неизвестная ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int GetIntFromView(GridView view, int rowHandle, string fieldName, int defaultValue = 0)
        {
            object value = view.GetRowCellValue(rowHandle, fieldName);
            return value == DBNull.Value ? defaultValue : Convert.ToInt32(value);
        }

        private int? GetNullableIntFromView(GridView view, int rowHandle, string fieldName)
        {
            object value = view.GetRowCellValue(rowHandle, fieldName);
            return value == DBNull.Value ? (int?)null : Convert.ToInt32(value);
        }
        private NormRasz NormalizeDataFromView(GridView view, int rowHandle)
        {
            var normRasz = new NormRasz();

            normRasz.kod_o = view.GetRowCellValue(rowHandle, "kod_o")?.ToString();
            normRasz.Text = Convert.ToString(view.GetRowCellValue(rowHandle, "text"))?.TrimEnd(' ');
            normRasz.Spec = Convert.ToString(view.GetRowCellValue(rowHandle, "spec"))?.TrimEnd(' ');
            // Объединяем источник: проставляем и код, и текст из одной строки norm_oper
            var textOb = Convert.ToString(view.GetRowCellValue(rowHandle, "text_ob"))?.TrimEnd(' ');
            normRasz.Obor = textOb;
            normRasz.razryd = GetIntFromView(view, rowHandle, "razryd");
            normRasz.N1 = GetIntFromView(view, rowHandle, "n1");
            normRasz.Sek = GetIntFromView(view, rowHandle, "sek");
            normRasz.KodOb = GetIntFromView(view, rowHandle, "kod_ob");
            normRasz.KodPodr = GetIntFromView(view, rowHandle, "kod_podr");
            normRasz.KodProizv = GetIntFromView(view, rowHandle, "kod_proizv");
            normRasz.TextProizv = Convert.ToString(view.GetRowCellValue(rowHandle, "text_proizv"))?.TrimEnd(' ');
            normRasz.TextVyaz = Convert.ToString(view.GetRowCellValue(rowHandle, "text_vyaz"))?.TrimEnd(' ');
            normRasz.TextOb = Convert.ToString(view.GetRowCellValue(rowHandle, "text_ob"))?.TrimEnd(' ');

            return normRasz;
        }

        /// <summary>
        /// Фильтрация данных в `GridView` по `radioGroup`
        /// </summary>
        private async void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!(sender is RadioGroup radioGroup)) return;

                string filterString = ""; // Строка фильтра

                switch (radioGroup.SelectedIndex)
                {
                    case 1: // Вязальное производство
                        filterString = "[kod_proizv] = 1";
                        break;
                    case 2: // Швейное производство
                        filterString = "[kod_proizv] = 0";
                        break;
                    case 3: // Носки
                        filterString = "[kod_proizv] = 3";
                        break;
                    default: // Показать все
                        filterString = "";
                        break;
                }

                // Применяем фильтр к GridView
                gridView1.ActiveFilterString = filterString;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при фильтрации данных");
                MessageBox.Show($"Ошибка при фильтрации данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }


}

