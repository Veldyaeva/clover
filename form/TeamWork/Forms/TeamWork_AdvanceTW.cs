using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BindingSource = System.Windows.Forms.BindingSource;

namespace SewingProduction.form
{
    public partial class TeamWork_AdvanceTW : CustomForm
    {
        private readonly ArtNormService _artNormService;
        private int _bufferWorkDivision;
        private readonly DatabaseHelper _dbHelper;
        private readonly int _newAnnId = -1;
        private readonly ILogger _logger = new FileLogger();
        private int _mode;

        private readonly BindingList<NormRasz> _normRaszList = new BindingList<NormRasz>();
        private readonly BindingList<NormRask> _normRaskList = new BindingList<NormRask>();
        private readonly BindingList<NormKont> _normKontList = new BindingList<NormKont>();
        private readonly BindingList<NormDopObr> _normDopObrList = new BindingList<NormDopObr>();

        private readonly BindingSource _normRaszBindingSource = new BindingSource();
        private readonly BindingSource _normRaskBindingSource = new BindingSource();
        private readonly BindingSource _normKontBindingSource = new BindingSource();
        private readonly BindingSource _normDopObrBindingSource = new BindingSource();

        public TeamWork_AdvanceTW(int id, int bufferWorkDivision, int mode)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _artNormService = new ArtNormService(_dbHelper);
            ThemeManager.UpdateTheme(this);

            _bufferWorkDivision = bufferWorkDivision;
            _mode = mode;
            _newAnnId = id;
            SetupBindingSources();
            gridView5.OptionsBehavior.EditingMode = GridEditingMode.EditForm;
        }

        private void SetupBindingSources()
        {
            _normRaszBindingSource.DataSource = _normRaszList;
            _normRaskBindingSource.DataSource = _normRaskList;
            _normKontBindingSource.DataSource = _normKontList;
            _normDopObrBindingSource.DataSource = _normDopObrList;

            gridControl5.DataSource = _normRaszBindingSource;
            gridControl2.DataSource = _normRaskBindingSource;
            gridControl3.DataSource = _normKontBindingSource;
            gridControl4.DataSource = _normDopObrBindingSource;
        }

        private async void TeamWork_AdvanceTW_Load(object sender, EventArgs e)
        {
            switch (_mode)
            {
                case (int)Mode.NewWorkDivision:
                    //                  richTextBox1.Text = $"группа: {ANNgridView.GetRowCellValue(_bufferWorkDivision, "grup")}, модель {ANNgridView.GetRowCellValue(_bufferWorkDivision, "mod")}, артикул: {ANNgridView.GetRowCellValue(_bufferWorkDivision, "articul")}";
                    this.Text = "Добавить предварительное";
                    break;
                case (int)Mode.ArchAndCopy:
                    this.Text = "Архив+копия";
                    await bufferLoad();
                    break;
                case (int)Mode.Archive:
                    this.Text = "В архив";
                    await bufferLoad();
                    break;
                case (int)Mode.Edit:
                    this.Text = "Редактировать";
                    await bufferLoad();
                    break;
            }
        }



        #region load
        private async Task bufferLoad()
        {
            if (_bufferWorkDivision <= 0)
            {
                MessageBox.Show("В буфер ничего не скопировано", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                var normRaszTask = _artNormService.GetRelatedNormRasz(_bufferWorkDivision);
                var normRaskTask = _artNormService.GetRelatedNormRask(_bufferWorkDivision);
                var normKontTask = _artNormService.GetRelatedNormKont(_bufferWorkDivision);
                var normDopObrTask = _artNormService.GetRelatedNormDopObr(_bufferWorkDivision);

                // Ждем выполнения всех запросов
                await Task.WhenAll(normRaszTask, normRaskTask, normKontTask, normDopObrTask);

                _normRaszList.Clear();
                _normRaskList.Clear();
                _normKontList.Clear();
                _normDopObrList.Clear();

                RefreshGridData();
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка загрузки данных в буфер");
                MessageBox.Show("Ошибка загрузки данных. Подробности в логе.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RefreshGridData()
        {
            _normRaszBindingSource.ResetBindings(false);
            _normRaskBindingSource.ResetBindings(false);
            _normKontBindingSource.ResetBindings(false);
            _normDopObrBindingSource.ResetBindings(false);
        }

        #endregion
        #region Norm_rasz

        private async void customButton1_Click(object sender, EventArgs e)
        {
            using (var selectionForm = new NormOperNew())
            {
                if (selectionForm.ShowDialog() == DialogResult.OK)
                {
                    var selectedData = selectionForm.SelectedRowData;
                    if (selectedData != null)
                    {
                        Debug.WriteLine("✅ Операция выбрана: " + selectedData.KodO);

                        // **1. Добавляем запись в BindingList**
                        var newNormRasz = new NormRasz
                        {
                            AnnId = _newAnnId,
                            KodO = selectedData.KodO,
                            Text = selectedData.Text,
                            Spec = selectedData.Spec,
                            Razryad = selectedData.Razryad,
                            Obor = selectedData.Obor,
                            KodProizv = selectedData.KodProizv,
                            TextProizv = selectedData.TextProizv,
                            TextOb = selectedData.TextOb,
                            TextVyaz = selectedData.TextVyaz
                        };

                        _normRaszBindingSource.Add(newNormRasz);
                        _normRaszBindingSource.ResetBindings(false);

                        Debug.WriteLine("✅ Данные добавлены в BindingList");

                        // **2. Сохраняем в БД**
                        newNormRasz.nrId = await _artNormService.InsertNormRaszAsync(newNormRasz);

                        Debug.WriteLine("✅ Данные сохранены в БД, nrId = " + newNormRasz.nrId);

                        _normRaszBindingSource.ResetBindings(false);
                        gridControl5.RefreshDataSource();
                        gridView5.RefreshData();

                        // **3. Фокусируемся на новой строке**
                        await Task.Delay(5000); // Даем UI обновиться
                        int newRowHandle = gridView5.LocateByValue("nrId", newNormRasz.nrId);

                        Debug.WriteLine($"🔹 Найденная строка в `GridView`: {newRowHandle}");

                        if (newRowHandle >= 0)
                        {
                            gridView5.FocusedRowHandle = newRowHandle;
                            gridView5.MakeRowVisible(newRowHandle, false);
                            Debug.WriteLine($"✅ Фокус установлен на строку: {gridView5.FocusedRowHandle}");

                            gridView5.ShowPopupEditForm();
                            Debug.WriteLine("📝 EditForm открыт!");
                        }
                        else
                        {
                            Debug.WriteLine("❌ Строка не найдена в `GridView`!");
                        }
                    }
                }
            }
        }
        



        private DataTable _raszDataTable;

        private async void bufferButton_Click(object sender, EventArgs e)
        {
           await bufferLoad();
        }

        private async void gridView5_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            using (NormOperNew selectionForm = new NormOperNew()) // Форма выбора операции
            {
                if (selectionForm.ShowDialog() == DialogResult.OK) // Если выбрана операция
                {
                    var selectedData = selectionForm.SelectedRowData;
                    if (selectedData != null)
                    {
                        Debug.WriteLine("✅ Операция выбрана: " + selectedData.KodO);

                        // **1. Заполняем уже созданную строку**
                        gridView5.SetRowCellValue(e.RowHandle, "AnnId", _newAnnId);
                        gridView5.SetRowCellValue(e.RowHandle, "KodO", selectedData.KodO);
                        gridView5.SetRowCellValue(e.RowHandle, "Text", selectedData.Text);
                        gridView5.SetRowCellValue(e.RowHandle, "Spec", selectedData.Spec);
                        gridView5.SetRowCellValue(e.RowHandle, "Razryad", selectedData.Razryad);
                        gridView5.SetRowCellValue(e.RowHandle, "Obor", selectedData.Obor);
                        gridView5.SetRowCellValue(e.RowHandle, "KodProizv", selectedData.KodProizv);
                        gridView5.SetRowCellValue(e.RowHandle, "TextProizv", selectedData.TextProizv);
                        gridView5.SetRowCellValue(e.RowHandle, "TextOb", selectedData.TextOb);
                        gridView5.SetRowCellValue(e.RowHandle, "TextVyaz", selectedData.TextVyaz);

                        Debug.WriteLine("✅ Данные занесены в новую строку.");

                        // **2. Сохраняем в БД**
                        var newNormRasz = new NormRasz
                        {
                            AnnId = _newAnnId,
                            KodO = selectedData.KodO,
                            Text = selectedData.Text,
                            Spec = selectedData.Spec,
                            Razryad = selectedData.Razryad,
                            Obor = selectedData.Obor,
                            KodProizv = selectedData.KodProizv,
                            TextProizv = selectedData.TextProizv,
                            TextOb = selectedData.TextOb,
                            TextVyaz = selectedData.TextVyaz
                        };

                        newNormRasz.nrId = await _artNormService.InsertNormRaszAsync(newNormRasz);
                        Debug.WriteLine("✅ Данные сохранены в БД, nrId = " + newNormRasz.nrId);

                        var item = _normRaszList.FirstOrDefault(x => x.KodO == selectedData.KodO && x.nrId == 0);
                        if (item != null)
                        {
                            item.nrId = newNormRasz.nrId;
                            Debug.WriteLine($"✅ `nrId` обновлён в `BindingList`: {item.nrId}");
                        }

                        foreach (var it in _normRaszList)
                        {
                            Debug.WriteLine($"🔹 BindingList -> nrId: {it.nrId}, KodO: {it.KodO}");
                        }

                        // **4. Принудительно обновляем `GridView`**
                        _normRaszBindingSource.ResetBindings(false);
                        gridControl5.RefreshDataSource();
                        gridView5.RefreshData();

                        Debug.WriteLine("🔄 Строка обновлена в `GridView`.");

                        //// **3. Обновляем `nrId` в существующей строке**
                        //gridView5.SetRowCellValue(e.RowHandle, "nrId", newNormRasz.nrId);

                        //gridView5.UpdateCurrentRow();
                        //_normRaszBindingSource.ResetBindings(false);
                        //gridView5.RefreshData();
                        //Debug.WriteLine("📢 GridView строк: " + gridView5.DataRowCount);

                        Debug.WriteLine("🔄 Строка обновлена в `GridView`.");
                        foreach (var it in _normRaszList)
                        {
                            Debug.WriteLine($"🔹 nrId: {it.nrId}, KodO: {it.KodO}");
                        }

                        Debug.WriteLine($"📢 gridView5.DataRowCount: {gridView5.DataRowCount}");
                        for (int i = 0; i < gridView5.DataRowCount; i++)
                        {
                            Debug.WriteLine($"🔹 GridView[{i}] -> nrId: {gridView5.GetRowCellValue(i, "nrId")}, KodO: {gridView5.GetRowCellValue(i, "KodO")}");
                        }

                        // **4. Открываем `EditForm`**
                        await Task.Delay(5000);
                        gridView5.FocusedRowHandle = e.RowHandle;
                        gridView5.ShowPopupEditForm();
                    }
                }
                else
                {
                    Debug.WriteLine("❌ Выбор отменён, удаляем пустую строку.");
                    gridView5.DeleteRow(e.RowHandle);
                }
            }
        }

        // private async void gridView5_InitNewRow(object sender, InitNewRowEventArgs e)
        //   {
        //Debug.WriteLine("🔹 InitNewRow вызван! e.RowHandle = " + e.RowHandle);

        //using (NormOperNew selectionForm = new NormOperNew()) // Форма выбора операции
        //{
        //    if (selectionForm.ShowDialog() != DialogResult.OK) // Если выбрана операция
        //    {
        //        Debug.WriteLine("❌ Выбор отменён, удаляем пустую строку.");
        //        if (_normRaszList.Count > 0)
        //        {
        //            _normRaszList.RemoveAt(_normRaszList.Count - 1);
        //            _normRaszBindingSource.ResetBindings(false);
        //        }
        //        return;
        //    }
        //    var selectedData = selectionForm.SelectedRowData;
        //    if (selectedData == null)
        //    {
        //        return;
        //    }
        //    Debug.WriteLine("✅ Операция выбрана: " + selectedData.KodO);

        //    // **1.Добавляем новую запись в `BindingList`**
        //    var newNormRasz = new NormRasz
        //    {
        //        AnnId = _newAnnId,
        //        KodO = selectedData.KodO,
        //        Text = selectedData.Text,
        //        Spec = selectedData.Spec,
        //        Razryad = selectedData.Razryad,
        //        Obor = selectedData.Obor,
        //        KodProizv = selectedData.KodProizv,
        //        TextProizv = selectedData.TextProizv,
        //        TextOb = selectedData.TextOb,
        //        TextVyaz = selectedData.TextVyaz
        //    };

        //    _normRaszBindingSource.Add(newNormRasz);
        //    _normRaszBindingSource.ResetBindings(false);

        //    Debug.WriteLine("✅ Данные занесены в `BindingList`.");

        //    // **3.Сохраняем в БД и обновляем `nrId`**
        //    newNormRasz.nrId = await _artNormService.InsertNormRaszAsync(newNormRasz);

        //    Debug.WriteLine("✅ Данные сохранены в БД, присвоен nrId = " + newNormRasz.nrId);

        //    _normRaszBindingSource.ResetBindings(false);
        //    gridControl5.RefreshDataSource();
        //    gridView5.RefreshData();

        //    //**4.Ждём обновления UI и открываем EditForm**
        //    await Task.Delay(1000);
        //    //           Application.DoEvents();
        //    var item = _normRaszList.FirstOrDefault(x => x.nrId == newNormRasz.nrId);
        //    int newRowHandle = item != null ? _normRaszBindingSource.IndexOf(item) : -1;

        //    // int newRowHandle = gridView5.LocateByValue("nrId", newNormRasz.nrId);
        //    Debug.WriteLine($"🔹 Найденная строка в `GridView`: {newRowHandle}");

        //    if (newRowHandle < 0)
        //    {
        //        Debug.WriteLine("❌ Строка не найдена в `GridView`!");
        //        return;
        //    }
        //    //  **5.Делаем строку видимой и открываем EditForm**
        //    gridView5.MakeRowVisible(newRowHandle, false);
        //    gridView5.FocusedRowHandle = newRowHandle;
        //    Debug.WriteLine($"✅ Фокус установлен на строку: {gridView5.FocusedRowHandle}");

        //    gridView5.ShowPopupEditForm();
        //    Debug.WriteLine("📝 EditForm открыт!");
        //}
        //}


        private void gridView5_EditFormHidden(object sender, EditFormHiddenEventArgs e)
        {
            this.DialogResult = DialogResult.None;
        }


        #endregion

        private async void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK; // Устанавливаем результат
            Close(); // Закрываем окно
        }
    }
}
