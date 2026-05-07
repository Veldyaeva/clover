using DevExpress.CodeParser;
using DevExpress.DirectX.Common.Direct2D;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Features.TeamWork.Services;
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
        private readonly DatabaseHelperSQL _dbHelper;
        private readonly DbService _dbService;
        private readonly ArtNormRepository _artNormService;
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
			var databaseServices = TeamWorkDependencyFactory.CreateDatabaseServices();
            _dbHelper = new DatabaseHelperSQL();
            _dbService = new DbService(_dbHelper);
            _artNormService = new ArtNormRepository(_dbHelper);

            _annId = annId;
           // ThemeManager.UpdateTheme(this);

            //// Режим редактирования через EditForm и подписки
            //gridView1.OptionsBehavior.EditingMode = GridEditingMode.EditForm;
            //gridView1.EditFormShowing += gridViewEditAdvRazm_EditFormShowing;
        }

        private async void NormOperNew_Load(object sender, EventArgs e)
        {
            try
            {
                _gridHelper.LoadGridViewSettings(gridView1, "NormOperGrid.xml");
                LoadData();

                //// Загрузка справочников для выпадающих списков (как в TeamWork_AdvanceTW)
                //kodProizvList = await _dbService.GetListAsync<KodProizvModel>("SELECT kod_proizv, text_proizv FROM kod_proizv", null);
                //podrVyazList = await _dbService.GetListAsync<PodrVyazModel>("SELECT kod_vyaz, text_vyaz, kod_proizv FROM podr_vyaz", null);
                //oborudShvList = await _dbService.GetListAsync<OborudShvModel>("SELECT kod_ob, text_ob FROM spOborudShv", null);

                //ConfigureLookups();

                //// Разрешаем редактирование через EditForm и сохраняем изменения
                //gridView1.OptionsBehavior.EditingMode = GridEditingMode.EditForm;
                //gridView1.RowUpdated += gridView1_RowUpdated;
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

        //private void gridViewEditAdvRazm_EditFormShowing(object sender, EditFormShowingEventArgs e)
        //{
        //    var view = sender as GridView;
        //    if (view == null) return;

        //    // Перехватываем попытку добавления через NewItemRow и открываем EditForm на новой строке
        //    if (view.IsNewItemRow(e.RowHandle))
        //    {
        //        e.Allow = false;
        //        try { AddNewRowToGrid(); } catch { }
        //    }
        //}

        //private void gridView1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus)
        //        {
        //            AddNewRowToGrid();
        //            e.Handled = true;
        //        }
        //    }
        //    catch { }
        //}
        //private void customAddButton_Click(object sender, EventArgs e)
        //{
        //    try { AddNewRowToGrid(); }
        //    catch { }
        //}
        //private void AddNewRowToGrid()
        //{
        //    try
        //    {
        //        var dt = customGridControl1?.DataSource as DataTable;
        //        if (dt == null) return;

        //        gridView1.BeginDataUpdate();
        //        try
        //        {
        //            var newRow = dt.NewRow();
        //            Базовые значения по умолчанию
        //            if (dt.Columns.Contains("n1")) newRow["n1"] = 0;
        //            if (dt.Columns.Contains("sek")) newRow["sek"] = 0;
        //            if (dt.Columns.Contains("razryd")) newRow["razryd"] = 0;

        //            dt.Rows.Add(newRow);
        //        }
        //        finally
        //        {
        //            gridView1.EndDataUpdate();
        //        }

        //        Фокус на новой строке и открытие EditForm
        //        int newIndex = dt.Rows.Count - 1;
        //        int handle = gridView1.GetRowHandle(newIndex);
        //        if (gridView1.IsValidRowHandle(handle))
        //        {
        //            gridView1.FocusedRowHandle = handle;
        //            gridView1.MakeRowVisible(handle);
        //            gridView1.ShowEditForm();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogErrorAsync(ex, "Ошибка при добавлении новой строки в NormOperNew");
        //    }
        //}

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
            normRasz.Text = StringNormalizer.TrimEndOrNull(Convert.ToString(view.GetRowCellValue(rowHandle, "text")), ' ');
            normRasz.Spec = StringNormalizer.TrimEndOrNull(Convert.ToString(view.GetRowCellValue(rowHandle, "spec")), ' ');
            // Объединяем источник: проставляем и код, и текст из одной строки norm_oper
            var textOb = StringNormalizer.TrimEndOrNull(Convert.ToString(view.GetRowCellValue(rowHandle, "text_ob")), ' ');
            normRasz.Obor = textOb;
            normRasz.razryd = GetIntFromView(view, rowHandle, "razryd");
            normRasz.N1 = GetIntFromView(view, rowHandle, "n1");
            normRasz.Sek = GetIntFromView(view, rowHandle, "sek");
            normRasz.KodOb = GetIntFromView(view, rowHandle, "kod_ob");
            normRasz.KodPodr = GetIntFromView(view, rowHandle, "kod_podr");
            normRasz.KodProizv = GetIntFromView(view, rowHandle, "kod_proizv");
            normRasz.TextProizv = StringNormalizer.TrimEndOrNull(Convert.ToString(view.GetRowCellValue(rowHandle, "text_proizv")), ' ');
            normRasz.TextVyaz = StringNormalizer.TrimEndOrNull(Convert.ToString(view.GetRowCellValue(rowHandle, "text_vyaz")), ' ');
            normRasz.TextOb = StringNormalizer.TrimEndOrNull(Convert.ToString(view.GetRowCellValue(rowHandle, "text_ob")), ' ');

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

        private void customAddButton_Click(object sender, EventArgs e)
        {
            try
            {
                var dt = customGridControl1?.DataSource as DataTable;
                if (dt == null) return;

                gridView1.BeginDataUpdate();
                try
                {
                    var newRow = dt.NewRow();
                    if (dt.Columns.Contains("kod_o")) newRow["kod_o"] = string.Empty;
                    if (dt.Columns.Contains("text")) newRow["text"] = string.Empty;
                    if (dt.Columns.Contains("po")) newRow["po"] = DBNull.Value;
                    if (dt.Columns.Contains("n")) newRow["n"] = DBNull.Value;
                    if (dt.Columns.Contains("n1")) newRow["n1"] = 0;
                    if (dt.Columns.Contains("sek")) newRow["sek"] = 0;
                    if (dt.Columns.Contains("new")) newRow["new"] = DBNull.Value;
                    if (dt.Columns.Contains("razryd")) newRow["razryd"] = 0;
                    if (dt.Columns.Contains("spec")) newRow["spec"] = string.Empty;
                    if (dt.Columns.Contains("obor")) newRow["obor"] = string.Empty;
                    if (dt.Columns.Contains("kod_ob")) newRow["kod_ob"] = DBNull.Value;
                    if (dt.Columns.Contains("kod_proizv")) newRow["kod_proizv"] = DBNull.Value;
                    if (dt.Columns.Contains("text_proizv")) newRow["text_proizv"] = string.Empty;
                    if (dt.Columns.Contains("text_vyaz")) newRow["text_vyaz"] = string.Empty;
                    if (dt.Columns.Contains("text_ob")) newRow["text_ob"] = string.Empty;

                    dt.Rows.Add(newRow);
                }
                finally
                {
                    gridView1.EndDataUpdate();
                }

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

        private async void gridView1_RowUpdated(object sender, RowObjectEventArgs e)
        {
            try
            {
                var view = sender as GridView;
                if (view == null) return;
                var rowHandle = e.RowHandle;
                var row = view.GetDataRow(rowHandle);
                if (row == null) return;

                // Подготовка значений
                string kod_o = row.Table.Columns.Contains("kod_o") ? Convert.ToString(row["kod_o"]) : null;
                string text = row.Table.Columns.Contains("text") ? Convert.ToString(row["text"]) : null;
                string po = row.Table.Columns.Contains("po") ? Convert.ToString(row["po"]) : null;
                int? n = TryToNullableInt(row, "n");
                int? n1 = TryToNullableInt(row, "n1");
                int? sek = TryToNullableInt(row, "sek");
                string @new = row.Table.Columns.Contains("new") ? Convert.ToString(row["new"]) : null;
                int? razryd = TryToNullableInt(row, "razryd");
                string spec = row.Table.Columns.Contains("spec") ? Convert.ToString(row["spec"]) : null;
                string obor = row.Table.Columns.Contains("obor") ? Convert.ToString(row["obor"]) : null;
                int? kod_ob = TryToNullableInt(row, "kod_ob");
                int? kod_proizv = TryToNullableInt(row, "kod_proizv");

                // Вставка/обновление
                if (!row.Table.Columns.Contains("Id"))
                {
                    // Если в выборке нет столбца Id — добавить для отслеживания
                    row.Table.Columns.Add("Id", typeof(int));
                }

                int id = row["Id"] == DBNull.Value ? 0 : Convert.ToInt32(row["Id"]);
                if (id == 0)
                {
                    string insertSql = @"INSERT INTO dbo.norm_oper (kod_o, [text], po, n, n1, sek, [new], razryd, spec, obor, kod_ob, kod_proizv)
                                        VALUES (@kod_o, @text, @po, @n, @n1, @sek, @new, @razryd, @spec, @obor, @kod_ob, @kod_proizv);
                                        SELECT CAST(SCOPE_IDENTITY() as int);";
                    var newId = await _dbHelper.ExecuteScalarAsync<int>(insertSql, new System.Collections.Generic.Dictionary<string, object>
                    {
                        {"@kod_o", (object)kod_o ?? DBNull.Value},
                        {"@text", (object)text ?? DBNull.Value},
                        {"@po", (object)po ?? DBNull.Value},
                        {"@n", (object)n ?? DBNull.Value},
                        {"@n1", (object)n1 ?? DBNull.Value},
                        {"@sek", (object)sek ?? DBNull.Value},
                        {"@new", (object)@new ?? DBNull.Value},
                        {"@razryd", (object)razryd ?? DBNull.Value},
                        {"@spec", (object)spec ?? DBNull.Value},
                        {"@obor", (object)obor ?? DBNull.Value},
                        {"@kod_ob", (object)kod_ob ?? DBNull.Value},
                        {"@kod_proizv", (object)kod_proizv ?? DBNull.Value},
                    });
                    row["Id"] = newId;
                }
                else
                {
                    string updateSql = @"UPDATE dbo.norm_oper SET
                                            kod_o = @kod_o,
                                            [text] = @text,
                                            po = @po,
                                            n = @n,
                                            n1 = @n1,
                                            sek = @sek,
                                            [new] = @new,
                                            razryd = @razryd,
                                            spec = @spec,
                                            obor = @obor,
                                            kod_ob = @kod_ob,
                                            kod_proizv = @kod_proizv
                                          WHERE Id = @Id";
                    await _dbHelper.ExecuteNonQueryAsync(updateSql, new System.Collections.Generic.Dictionary<string, object>
                    {
                        {"@Id", id},
                        {"@kod_o", (object)kod_o ?? DBNull.Value},
                        {"@text", (object)text ?? DBNull.Value},
                        {"@po", (object)po ?? DBNull.Value},
                        {"@n", (object)n ?? DBNull.Value},
                        {"@n1", (object)n1 ?? DBNull.Value},
                        {"@sek", (object)sek ?? DBNull.Value},
                        {"@new", (object)@new ?? DBNull.Value},
                        {"@razryd", (object)razryd ?? DBNull.Value},
                        {"@spec", (object)spec ?? DBNull.Value},
                        {"@obor", (object)obor ?? DBNull.Value},
                        {"@kod_ob", (object)kod_ob ?? DBNull.Value},
                        {"@kod_proizv", (object)kod_proizv ?? DBNull.Value},
                    });
                }

                // Точечное обновление строки без полного ResetBindings
                if (view.IsValidRowHandle(rowHandle))
                {
                    view.RefreshRow(rowHandle);
                    // Включать только если нужно немедленно триггернуть ValidateRow:
                    // view.UpdateCurrentRow();
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка сохранения записи norm_oper");
                MessageBox.Show($"Ошибка при сохранении записи: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int? TryToNullableInt(DataRow row, string column)
        {
            if (!row.Table.Columns.Contains(column)) return null;
            var v = row[column];
            if (v == null || v == DBNull.Value) return null;
            if (int.TryParse(v.ToString(), out int parsed)) return parsed;
            return null;
        }
    }


}

