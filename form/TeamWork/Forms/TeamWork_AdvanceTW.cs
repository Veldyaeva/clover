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
        //private readonly BindingList<NormRask> _normRaskList = new BindingList<NormRask>();
        //private readonly BindingList<NormKont> _normKontList = new BindingList<NormKont>();
        //private readonly BindingList<NormDopObr> _normDopObrList = new BindingList<NormDopObr>();

        private readonly BindingSource _normRaszBindingSource = new BindingSource();
        //private readonly BindingSource _normRaskBindingSource = new BindingSource();
        //private readonly BindingSource _normKontBindingSource = new BindingSource();
        //private readonly BindingSource _normDopObrBindingSource = new BindingSource();
       // private DataTable _normRaszTable;

        private void InitializeDataTable()
        {
            //_normRaszTable = new DataTable();
            //_normRaszTable.Columns.Add("nrId", typeof(int));
            //_normRaszTable.Columns.Add("AnnId", typeof(int));
            //_normRaszTable.Columns.Add("KodO", typeof(int));
            //_normRaszTable.Columns.Add("Text", typeof(string));
            //_normRaszTable.Columns.Add("Spec", typeof(string));
            //_normRaszTable.Columns.Add("Razryad", typeof(int));
            //_normRaszTable.Columns.Add("Obor", typeof(string));
            //_normRaszTable.Columns.Add("KodProizv", typeof(string));
            //_normRaszTable.Columns.Add("TextProizv", typeof(string));
            //_normRaszTable.Columns.Add("TextOb", typeof(string));
            //_normRaszTable.Columns.Add("TextVyaz", typeof(string));

            //_normRaszBindingSource.DataSource = _normRaszTable;
            //gridControl5.DataSource = _normRaszBindingSource;
        }

        public TeamWork_AdvanceTW(int id, int bufferWorkDivision, int mode)
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _artNormService = new ArtNormService(_dbHelper);
            ThemeManager.UpdateTheme(this);

            _bufferWorkDivision = bufferWorkDivision;
            _mode = mode;
            _newAnnId = id;
           // SetupBindingSources();
            _normRaszBindingSource.DataSource = _normRaszList;
            gridControl5.DataSource = _normRaszBindingSource;
          //  gridView5.OptionsBehavior.EditingMode = GridEditingMode.EditForm;
          //  InitializeDataTable();
        }

        private void SetupBindingSources()
        {
         //   _normRaszBindingSource.DataSource = _normRaszList;
         //   _normRaskBindingSource.DataSource = _normRaskList;
         //   _normKontBindingSource.DataSource = _normKontList;
         //   _normDopObrBindingSource.DataSource = _normDopObrList;

         ////   gridControl5.DataSource = _normRaszBindingSource;
         //   gridControl2.DataSource = _normRaskBindingSource;
         //   gridControl3.DataSource = _normKontBindingSource;
         //   gridControl4.DataSource = _normDopObrBindingSource;
            //_normRaszBindingSource.DataSource = _normRaszTable;
            //gridControl5.DataSource = _normRaszBindingSource;

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
        //    if (_bufferWorkDivision <= 0)
        //    {
        //        MessageBox.Show("В буфер ничего не скопировано", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }
        //    try
        //    {
        //        var normRaszTask = _artNormService.GetRelatedNormRasz(_bufferWorkDivision);
        //        var normRaskTask = _artNormService.GetRelatedNormRask(_bufferWorkDivision);
        //        var normKontTask = _artNormService.GetRelatedNormKont(_bufferWorkDivision);
        //        var normDopObrTask = _artNormService.GetRelatedNormDopObr(_bufferWorkDivision);

        //        // Ждем выполнения всех запросов
        //        await Task.WhenAll(normRaszTask, normRaskTask, normKontTask, normDopObrTask);

        //        _normRaszList.Clear();
        //        _normRaskList.Clear();
        //        _normKontList.Clear();
        //        _normDopObrList.Clear();

        //       // RefreshGridData();
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, "Ошибка загрузки данных в буфер");
        //        MessageBox.Show("Ошибка загрузки данных. Подробности в логе.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        //private void RefreshGridData()
        //{
        //    _normRaszBindingSource.ResetBindings(false);
        //    _normRaskBindingSource.ResetBindings(false);
        //    _normKontBindingSource.ResetBindings(false);
        //    _normDopObrBindingSource.ResetBindings(false);
        }

        #endregion
        #region Norm_rasz

        private DataTable _raszDataTable;

        private async void bufferButton_Click(object sender, EventArgs e)
        {
           await bufferLoad();
        }
        private void customButton1_Click(object sender, EventArgs e)
        { }

        private async void gridView5_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            Debug.WriteLine($"🔹 InitNewRow вызван! e.RowHandle = {e.RowHandle}");

            using (NormOperNew selectionForm = new NormOperNew())
            {
                if (selectionForm.ShowDialog() == DialogResult.OK)
                {
                    var selectedData = selectionForm.SelectedRowData;
                    if (selectedData != null)
                    {
                        Debug.WriteLine($"✅ Операция выбрана: {selectedData.KodO}");

                        // **1. Используем уже созданную строку (e.RowHandle)**
                        NormRasz newNormRasz = new NormRasz
                        {
                            AnnId = _newAnnId,
                            KodO = selectedData.KodO,
                            Text = selectedData.Text,
                            Spec = selectedData.Spec,
                            Razryad = selectedData.Razryad,
                            Obor = selectedData.Obor,
                            KodProizv = selectedData.KodProizv,
                        };

                        // **2. Записываем данные в уже добавленную строку**
                        gridView5.SetRowCellValue(e.RowHandle, "AnnId", newNormRasz.AnnId);
                        gridView5.SetRowCellValue(e.RowHandle, "KodO", newNormRasz.KodO);
                        gridView5.SetRowCellValue(e.RowHandle, "Text", newNormRasz.Text);
                        gridView5.SetRowCellValue(e.RowHandle, "Spec", newNormRasz.Spec);
                        gridView5.SetRowCellValue(e.RowHandle, "Razryad", newNormRasz.Razryad);
                        gridView5.SetRowCellValue(e.RowHandle, "Obor", newNormRasz.Obor);
                        gridView5.SetRowCellValue(e.RowHandle, "KodProizv", newNormRasz.KodProizv);

                        Debug.WriteLine("✅ Данные записаны в GridView.");

                        // **3. Сохраняем в БД**
                        newNormRasz.nrId = await _artNormService.InsertNormRaszAsync(newNormRasz);
                        gridView5.SetRowCellValue(e.RowHandle, "nrId", newNormRasz.nrId);
                        Debug.WriteLine($"✅ Данные сохранены в БД, nrId = {newNormRasz.nrId}");

                        // **4. Обновляем UI**
                        _normRaszBindingSource.ResetBindings(false);
                        gridControl5.RefreshDataSource();
                        gridView5.RefreshData();

                        // **5. Открываем EditForm**
                        int newRowHandle = gridView5.LocateByValue("nrId", newNormRasz.nrId);
                        if (newRowHandle >= 0)
                        {
                            gridView5.FocusedRowHandle = newRowHandle;
                            gridView5.ShowPopupEditForm();
                            Debug.WriteLine($"✅ Фокус на строке {newRowHandle}, EditForm открыт!");
                        }
                        else
                        {
                            Debug.WriteLine("❌ Строка не найдена в GridView!");
                        }
                    }
                }
                else
                {
                    Debug.WriteLine("❌ Выбор отменён, удаляем строку.");
                    gridView5.DeleteRow(e.RowHandle);
                }
            }
        }
    
        //private async void gridView5_InitNewRow(object sender, InitNewRowEventArgs e)
        //{
        //    Debug.WriteLine($"🔹 InitNewRow вызван! e.RowHandle = {e.RowHandle}");

        //    using (NormOperNew selectionForm = new NormOperNew())
        //    {
        //        if (selectionForm.ShowDialog() == DialogResult.OK)
        //        {
        //            var selectedData = selectionForm.SelectedRowData;
        //            if (selectedData != null)
        //            {
        //                Debug.WriteLine($"✅ Операция выбрана: {selectedData.KodO}");

        //                // **1. Вывод структуры `GridView`**
        //                Debug.WriteLine("📢 Структура `GridView5`:");
        //                foreach (var column in gridView5.Columns)
        //                {
        //                    Debug.WriteLine($"🔹 Column: {column.ToString()}");
        //                }

        //                // **2. Создаём объект и заполняем строку**
        //                NormRasz newNormRasz = new NormRasz
        //                {
        //                    nrId = -1, // Пока не знаем реальный ID
        //                    AnnId = _newAnnId,
        //                    KodO = selectedData.KodO,
        //                    Text = selectedData.Text,
        //                    Spec = selectedData.Spec,
        //                    Razryad = selectedData.Razryad,
        //                    Obor = selectedData.Obor,
        //                    KodProizv = selectedData.KodProizv,
        //                    Kod = selectedData.Kod,
        //                    N1 = selectedData.N1,
        //                    Sek = selectedData.Sek,
        //                    KodPodr = selectedData.KodPodr,
        //                    KodOb = selectedData.KodOb
        //                };

        //                Debug.WriteLine("✅ Данные для вставки:");
        //                Debug.WriteLine($"🔹 AnnId: {newNormRasz.AnnId}, KodO: {newNormRasz.KodO}, Text: {newNormRasz.Text}");

        //                // **3. Заполняем существующую строку**
        //                gridView5.SetRowCellValue(e.RowHandle, "AnnId", _newAnnId);
        //                gridView5.SetRowCellValue(e.RowHandle, "KodO", selectedData.KodO);
        //                gridView5.SetRowCellValue(e.RowHandle, "Text", selectedData.Text);
        //                gridView5.SetRowCellValue(e.RowHandle, "Spec", selectedData.Spec);
        //                gridView5.SetRowCellValue(e.RowHandle, "Razryad", selectedData.Razryad);
        //                gridView5.SetRowCellValue(e.RowHandle, "Obor", selectedData.Obor);
        //                gridView5.SetRowCellValue(e.RowHandle, "KodProizv", selectedData.KodProizv);
        //                gridView5.SetRowCellValue(e.RowHandle, "Kod", selectedData.Kod);
        //                gridView5.SetRowCellValue(e.RowHandle, "N1", selectedData.N1);
        //                gridView5.SetRowCellValue(e.RowHandle, "Sek", selectedData.Sek);
        //                gridView5.SetRowCellValue(e.RowHandle, "KodPodr", selectedData.KodPodr);
        //                gridView5.SetRowCellValue(e.RowHandle, "KodOb", selectedData.KodOb);

        //                Debug.WriteLine("✅ Данные занесены в `GridView5`.");

        //                // **4. Логируем все строки перед сохранением**
        //                Debug.WriteLine("📢 Все строки в `GridView5` перед сохранением:");
        //                for (int i = 0; i < gridView5.DataRowCount; i++)
        //                {
        //                    Debug.WriteLine($"🔹 [{i}] AnnId: {gridView5.GetRowCellValue(i, "AnnId")}, KodO: {gridView5.GetRowCellValue(i, "KodO")}, Text: {gridView5.GetRowCellValue(i, "Text")}");
        //                }

        //                // **5. Сохраняем в БД**
        //                newNormRasz.nrId = await _artNormService.InsertNormRaszAsync(newNormRasz);
        //                gridView5.SetRowCellValue(e.RowHandle, "nrId", newNormRasz.nrId);
        //                Debug.WriteLine($"✅ Данные сохранены в БД, nrId = {newNormRasz.nrId}");

        //                // **6. Логируем обновлённую строку**
        //                Debug.WriteLine($"📢 Обновлённая строка [{e.RowHandle}]:");
        //                foreach (var column in gridView5.Columns)
        //                {
        //                    var value = gridView5.GetRowCellValue(e.RowHandle, column.ToString());
        //                    Debug.WriteLine($"🔹 {column.ToString()}: {value}");
        //                }

        //                // **7. Обновляем UI**
        //                _normRaszBindingSource.RemoveCurrent();
        //                _normRaszBindingSource.ResetBindings(false);
        //                gridControl5.RefreshDataSource();
        //                gridView5.RefreshData();

        //                // **8. Открываем EditForm**
        //                int newRowHandle = gridView5.LocateByValue("nrId", newNormRasz.nrId);
        //                if (newRowHandle >= 0)
        //                {
        //                    gridView5.FocusedRowHandle = newRowHandle;
        //                    gridView5.ShowPopupEditForm();
        //                    Debug.WriteLine($"✅ Фокус на строке {newRowHandle}, EditForm открыт!");
        //                }
        //                else
        //                {
        //                    Debug.WriteLine("❌ Строка не найдена в GridView!");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            Debug.WriteLine("❌ Выбор отменён, удаляем строку.");
        //            gridView5.DeleteRow(e.RowHandle);
        //        }
        //    }
        //}

        //private async void gridView5_InitNewRow(object sender, InitNewRowEventArgs e)
        //{
        //    Debug.WriteLine($"🔹 InitNewRow вызван! e.RowHandle = {e.RowHandle}");

        //    using (var selectionForm = new NormOperNew())
        //    {
        //        if (selectionForm.ShowDialog() == DialogResult.OK)
        //        {
        //            var selectedData = selectionForm.SelectedRowData;
        //            if (selectedData != null)
        //            {
        //                Debug.WriteLine($"✅ Операция выбрана: {selectedData.KodO}");

        //                // **1. Используем существующую строку (e.RowHandle)**
        //                gridView5.SetRowCellValue(e.RowHandle, "AnnId", _newAnnId);
        //                gridView5.SetRowCellValue(e.RowHandle, "KodO", selectedData.KodO);
        //                gridView5.SetRowCellValue(e.RowHandle, "Text", selectedData.Text);
        //                gridView5.SetRowCellValue(e.RowHandle, "Spec", selectedData.Spec);
        //                gridView5.SetRowCellValue(e.RowHandle, "Razryad", selectedData.Razryad);
        //                gridView5.SetRowCellValue(e.RowHandle, "Obor", selectedData.Obor);
        //                gridView5.SetRowCellValue(e.RowHandle, "KodProizv", selectedData.KodProizv);
        //                gridView5.SetRowCellValue(e.RowHandle, "Kod", selectedData.Kod);
        //                gridView5.SetRowCellValue(e.RowHandle, "N1", selectedData.N1);
        //                gridView5.SetRowCellValue(e.RowHandle, "Sek", selectedData.Sek);
        //                gridView5.SetRowCellValue(e.RowHandle, "KodPodr", selectedData.KodPodr);
        //                gridView5.SetRowCellValue(e.RowHandle, "KodOb", selectedData.KodOb);

        //                Debug.WriteLine("✅ Данные записаны в `GridView` (авто-строка)");

        //                // **2. Находим существующую строку в `BindingList`**
        //                var newNormRasz = _normRaszList.FirstOrDefault(x => x.nrId == 0);
        //                if (newNormRasz == null)
        //                {
        //                    Debug.WriteLine("⚠️ Не найдена существующая строка в `BindingList`, создаём новую.");
        //                    newNormRasz = new NormRasz();
        //                    _normRaszList.Add(newNormRasz);
        //                }

        //                // **3. Заполняем объект `NormRasz`**
        //                newNormRasz.AnnId = _newAnnId;
        //                newNormRasz.KodO = selectedData.KodO;
        //                newNormRasz.Text = selectedData.Text;
        //                newNormRasz.Spec = selectedData.Spec;
        //                newNormRasz.Razryad = selectedData.Razryad;
        //                newNormRasz.Obor = selectedData.Obor;
        //                newNormRasz.KodProizv = selectedData.KodProizv;
        //                newNormRasz.Kod = selectedData.Kod;
        //                newNormRasz.N1 = selectedData.N1;
        //                newNormRasz.Sek = selectedData.Sek;
        //                newNormRasz.KodPodr = selectedData.KodPodr;
        //                newNormRasz.KodOb = selectedData.KodOb;

        //                Debug.WriteLine($"📢 `BindingList` теперь содержит {_normRaszList.Count} записей");

        //                // **4. Принудительное обновление `BindingSource`**
        //                _normRaszBindingSource.ResetBindings(false);
        //                await Task.Delay(200); // Даем `GridView` время обновить данные

        //                // **5. Сохранение в БД**
        //                newNormRasz.nrId = await _artNormService.InsertNormRaszAsync(newNormRasz);
        //                Debug.WriteLine($"✅ Данные сохранены в БД, nrId = {newNormRasz.nrId}");

        //                // **6. Обновляем `nrId` в `GridView`**
        //                gridView5.SetRowCellValue(e.RowHandle, "nrId", newNormRasz.nrId);

        //                // **7. Обновляем UI**
        //                _normRaszBindingSource.ResetBindings(false);
        //                gridControl5.RefreshDataSource();
        //                gridView5.RefreshData();

        //                Debug.WriteLine("📢 gridView5.DataRowCount: " + gridView5.DataRowCount);
        //                for (int i = 0; i < gridView5.DataRowCount; i++)
        //                {
        //                    Debug.WriteLine($"🔹 GridView[{i}] -> nrId: {gridView5.GetRowCellValue(i, "nrId")}, KodO: {gridView5.GetRowCellValue(i, "KodO")}");
        //                }

        //                await Task.Delay(300);

        //                // **8. Открываем EditForm**
        //                int newRowHandle = gridView5.LocateByValue("nrId", newNormRasz.nrId);
        //                if (newRowHandle >= 0)
        //                {
        //                    gridView5.FocusedRowHandle = newRowHandle;
        //                    gridView5.ShowPopupEditForm();
        //                    Debug.WriteLine($"✅ EditForm открыт для строки {newRowHandle}!");
        //                }
        //                else
        //                {
        //                    Debug.WriteLine("❌ Строка не найдена в `GridView`!");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            Debug.WriteLine("❌ Выбор отменён, удаляем последнюю строку.");
        //            if (_normRaszList.Count > 0)
        //            {
        //                _normRaszList.RemoveAt(_normRaszList.Count - 1);
        //                _normRaszBindingSource.ResetBindings(false);
        //            }
        //        }
        //    }
        //}


        //private async void gridView5_InitNewRow(object sender, InitNewRowEventArgs e)
        //{
        //    Debug.WriteLine($"🔹 InitNewRow вызван! e.RowHandle = {e.RowHandle}");

        //    using (var selectionForm = new NormOperNew())
        //    {
        //        if (selectionForm.ShowDialog() == DialogResult.OK)
        //        {
        //            var selectedData = selectionForm.SelectedRowData;
        //            if (selectedData != null)
        //            {
        //                Debug.WriteLine($"✅ Операция выбрана: {selectedData.KodO}");

        //                // **1. Используем автоматически созданную строку (e.RowHandle)**
        //                gridView5.SetRowCellValue(e.RowHandle, "AnnId", _newAnnId);
        //                gridView5.SetRowCellValue(e.RowHandle, "KodO", selectedData.KodO);
        //                gridView5.SetRowCellValue(e.RowHandle, "Text", selectedData.Text);
        //                gridView5.SetRowCellValue(e.RowHandle, "Spec", selectedData.Spec);
        //                gridView5.SetRowCellValue(e.RowHandle, "Razryad", selectedData.Razryad);
        //                gridView5.SetRowCellValue(e.RowHandle, "Obor", selectedData.Obor);
        //                gridView5.SetRowCellValue(e.RowHandle, "KodProizv", selectedData.KodProizv);
        //                gridView5.SetRowCellValue(e.RowHandle, "Kod", selectedData.Kod);
        //                gridView5.SetRowCellValue(e.RowHandle, "N1", selectedData.N1);
        //                gridView5.SetRowCellValue(e.RowHandle, "Sek", selectedData.Sek);
        //                gridView5.SetRowCellValue(e.RowHandle, "KodPodr", selectedData.KodPodr);
        //                gridView5.SetRowCellValue(e.RowHandle, "KodOb", selectedData.KodOb);
        //                //gridView5.SetRowCellValue(e.RowHandle, "TextProizv", selectedData.TextProizv);
        //                //gridView5.SetRowCellValue(e.RowHandle, "TextOb", selectedData.TextOb);
        //                //gridView5.SetRowCellValue(e.RowHandle, "TextVyaz", selectedData.TextVyaz);

        //                Debug.WriteLine("✅ Данные записаны в `GridView` (авто-строка)");
        //                // **1. Вывод структуры `GridView`**
        //                Debug.WriteLine("📢 Структура `GridView5`:");
        //                foreach (var column in gridView5.Columns)
        //                {
        //                    Debug.WriteLine($"🔹 Column: {column.ToString()}");
        //                }
        //                // **2. Создаём объект для `BindingList`**
        //                var newNormRasz = new NormRasz
        //                {
        //                    nrId = -1, // Пока не знаем реальный ID
        //                    AnnId = _newAnnId,
        //                    KodO = selectedData.KodO,
        //                    Text = selectedData.Text,
        //                    Spec = selectedData.Spec,
        //                    Razryad = selectedData.Razryad,
        //                    Obor = selectedData.Obor,
        //                    KodProizv = selectedData.KodProizv,
        //                    Kod = selectedData.Kod,
        //                    N1 = selectedData.N1,
        //                    Sek = selectedData.Sek,
        //                    KodPodr = selectedData.KodPodr,
        //                    KodOb = selectedData.KodOb

        //                    //TextProizv = selectedData.TextProizv,
        //                    //TextOb = selectedData.TextOb,
        //                    //TextVyaz = selectedData.TextVyaz
        //                };

        //                // **3. Добавляем в `BindingList` (обновляем UI)**
        //                _normRaszList.Add(newNormRasz);
        //                _normRaszBindingSource.ResetBindings(false);
        //                _normRaszBindingSource.RemoveCurrent();
        //                Debug.WriteLine($"📢 `BindingList` теперь содержит {_normRaszList.Count} записей");

        //                // **4. Логируем все строки перед сохранением**
        //                Debug.WriteLine("📢 Все строки в `GridView5` перед сохранением:");
        //                for (int i = 0; i < gridView5.DataRowCount; i++)
        //                {
        //                    Debug.WriteLine($"🔹 [{i}] AnnId: {gridView5.GetRowCellValue(i, "AnnId")}, KodO: {gridView5.GetRowCellValue(i, "KodO")}, Text: {gridView5.GetRowCellValue(i, "Text")}");
        //                }


        //                // **4. Сохраняем в БД и получаем `nrId`**
        //                newNormRasz.nrId = await _artNormService.InsertNormRaszAsync(newNormRasz);
        //                Debug.WriteLine($"✅ Данные сохранены в БД, nrId = {newNormRasz.nrId}");

        //                // **5. Обновляем `nrId` в `BindingList`**
        //                var lastAddedItem = _normRaszList.LastOrDefault();
        //                if (lastAddedItem != null)
        //                {
        //                    lastAddedItem.nrId = newNormRasz.nrId;
        //                }


        //                // **6. Логируем обновлённую строку**
        //                Debug.WriteLine($"📢 Обновлённая строка [{e.RowHandle}]:");
        //                foreach (var column in gridView5.Columns)
        //                {
        //                    var value = gridView5.GetRowCellValue(e.RowHandle, column.ToString());
        //                    Debug.WriteLine($"🔹 {column.ToString()}: {value}");
        //                }


        //                // **6. Обновляем UI**
        //                _normRaszBindingSource.ResetBindings(false);
        //                gridControl5.RefreshDataSource();
        //                gridView5.RefreshData();
        //                Debug.WriteLine("📢 gridControl5.DataSource: " + gridControl5.DataSource?.GetType());
        //                Debug.WriteLine("📢 gridView5.DataRowCount: " + gridView5.DataRowCount);
        //                for (int i = 0; i < gridView5.DataRowCount; i++)
        //                {
        //                    Debug.WriteLine($"🔹 GridView[{i}] -> nrId: {gridView5.GetRowCellValue(i, "nrId")}, KodO: {gridView5.GetRowCellValue(i, "KodO")}");
        //                }


        //                await Task.Delay(1000);

        //                // **7. Открываем EditForm**
        //                int newRowHandle = gridView5.LocateByValue("nrId", newNormRasz.nrId);
        //                if (newRowHandle >= 0)
        //                {
        //                    gridView5.FocusedRowHandle = newRowHandle;
        //                    gridView5.ShowPopupEditForm();
        //                    Debug.WriteLine($"✅ EditForm открыт для строки {newRowHandle}!");
        //                }
        //                else
        //                {
        //                    Debug.WriteLine("❌ Строка не найдена в `GridView`!");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            Debug.WriteLine("❌ Выбор отменён, удаляем последнюю строку.");
        //            if (_normRaszList.Count > 0)
        //            {
        //                _normRaszList.RemoveAt(_normRaszList.Count - 1);
        //                _normRaszBindingSource.ResetBindings(false);
        //            }
        //        }
        //    }
        //}


        ////private async void gridView5_InitNewRow(object sender, InitNewRowEventArgs e)
        ////{
        ////    Debug.WriteLine($"🔹 InitNewRow вызван! e.RowHandle = {e.RowHandle}");

        ////    using (var selectionForm = new NormOperNew()) // Открываем форму выбора
        ////    {
        ////        if (selectionForm.ShowDialog() == DialogResult.OK) // Если выбрана операция
        ////        {
        ////            var selectedData = selectionForm.SelectedRowData;
        ////            if (selectedData != null)
        ////            {
        ////                Debug.WriteLine($"✅ Операция выбрана: {selectedData.KodO}");

        ////                int rowHandle = e.RowHandle;
        ////                // **1️⃣ Заполняем автоматически созданную строку**
        ////                gridView5.SetRowCellValue(rowHandle, "AnnId", _newAnnId);
        ////                gridView5.SetRowCellValue(rowHandle, "KodO", selectedData.KodO);
        ////                gridView5.SetRowCellValue(rowHandle, "Text", selectedData.Text);
        ////                gridView5.SetRowCellValue(rowHandle, "Spec", selectedData.Spec);
        ////                gridView5.SetRowCellValue(rowHandle, "Razryad", selectedData.Razryad);
        ////                gridView5.SetRowCellValue(rowHandle, "Obor", selectedData.Obor);
        ////                gridView5.SetRowCellValue(rowHandle, "KodProizv", selectedData.KodProizv);
        ////                gridView5.SetRowCellValue(rowHandle, "TextProizv", selectedData.TextProizv);
        ////                gridView5.SetRowCellValue(rowHandle, "TextOb", selectedData.TextOb);
        ////                gridView5.SetRowCellValue(rowHandle, "TextVyaz", selectedData.TextVyaz);

        ////                Debug.WriteLine("✅ Данные записаны в `GridView` (авто-строка)");

        ////                // **2️⃣ Создаём объект для БД**
        ////                var newNormRasz = new NormRasz
        ////                {
        ////                    AnnId = _newAnnId,
        ////                    KodO = selectedData.KodO,
        ////                    Text = selectedData.Text,
        ////                    Spec = selectedData.Spec,
        ////                    Razryad = selectedData.Razryad,
        ////                    Obor = selectedData.Obor,
        ////                    KodProizv = selectedData.KodProizv,
        ////                    TextProizv = selectedData.TextProizv,
        ////                    TextOb = selectedData.TextOb,
        ////                    TextVyaz = selectedData.TextVyaz
        ////                };

        ////                // **3️⃣ Сохраняем в БД и получаем реальный `nrId`**
        ////                newNormRasz.nrId = await _artNormService.InsertNormRaszAsync(newNormRasz);
        ////                Debug.WriteLine($"✅ Данные сохранены в БД, присвоен nrId = {newNormRasz.nrId}");

        ////                // **4️⃣ Обновляем `nrId` в той же строке**
        ////                gridView5.SetRowCellValue(rowHandle, "nrId", newNormRasz.nrId);
        ////                gridView5.UpdateCurrentRow();

        ////                // **5️⃣ Обновляем UI**
        ////                _normRaszBindingSource.ResetBindings(false);
        ////                gridControl5.RefreshDataSource();
        ////                gridView5.RefreshData();

        ////                // **6️⃣ Ждём обновления UI и находим строку по `nrId`**
        ////                await Task.Delay(100);
        ////                int newRowHandle = gridView5.LocateByValue("nrId", newNormRasz.nrId);
        ////                Debug.WriteLine($"🔹 Найденная строка: {newRowHandle}");

        ////                // **7️⃣ Открываем EditForm**
        ////                if (newRowHandle >= 0)
        ////                {
        ////                    gridView5.FocusedRowHandle = newRowHandle;
        ////                    gridView5.ShowPopupEditForm();
        ////                    Debug.WriteLine("✅ EditForm открыт!");
        ////                }
        ////                else
        ////                {
        ////                    Debug.WriteLine("❌ Строка не найдена в `GridView`!");
        ////                }
        ////            }
        ////        }
        ////        else
        ////        {
        ////            Debug.WriteLine("❌ Выбор отменён, удаляем строку.");
        ////            gridView5.DeleteRow(e.RowHandle);
        ////        }
        ////    }
        ////}


        //////private async void gridView5_InitNewRow(object sender, InitNewRowEventArgs e)
        //////{
        //////    Debug.WriteLine($"🔹 InitNewRow вызван! e.RowHandle = {e.RowHandle}");

        //////    using (NormOperNew selectionForm = new NormOperNew())
        //////    {
        //////        if (selectionForm.ShowDialog() == DialogResult.OK)
        //////        {
        //////            var selectedData = selectionForm.SelectedRowData;
        //////            if (selectedData != null)
        //////            {
        //////                Debug.WriteLine($"✅ Операция выбрана: {selectedData.KodO}");

        //////                // **1. Добавляем строку в `DataTable`**
        //////                DataRow newRow = _normRaszTable.NewRow();
        //////                newRow["nrId"] = 0; // Пока не знаем реальный ID
        //////                newRow["AnnId"] = _newAnnId;
        //////                newRow["KodO"] = selectedData.KodO;
        //////                newRow["Text"] = selectedData.Text;
        //////                newRow["Spec"] = selectedData.Spec;
        //////                newRow["Razryad"] = selectedData.Razryad;
        //////                newRow["Obor"] = selectedData.Obor;
        //////                newRow["KodProizv"] = selectedData.KodProizv;
        //////                newRow["TextProizv"] = selectedData.TextProizv;
        //////                newRow["TextOb"] = selectedData.TextOb;
        //////                newRow["TextVyaz"] = selectedData.TextVyaz;

        //////                _normRaszTable.Rows.Add(newRow);
        //////                Debug.WriteLine("✅ Данные занесены в `DataTable`.");

        //////                Debug.WriteLine($"🔍 gridControl5.DataSource: {gridControl5.DataSource?.GetType()}");
        //////                Debug.WriteLine($"🔍 gridView5.DataSource: {gridView5.DataSource}"); // Обычно null!
        //////                Debug.WriteLine($"🔍 normRaszBindingSource.DataSource: {_normRaszBindingSource.DataSource?.GetType()}");


        //////                // **2. Принудительное обновление UI**
        //////                _normRaszBindingSource.ResetBindings(false);
        //////                await Task.Delay(100);

        //////                // **3. Сохранение в БД**
        //////                int newNrId = await _artNormService.InsertNormRaszAsync(new NormRasz
        //////                {
        //////                    AnnId = _newAnnId,
        //////                    KodO = selectedData.KodO,
        //////                    Text = selectedData.Text,
        //////                    Spec = selectedData.Spec,
        //////                    Razryad = selectedData.Razryad,
        //////                    Obor = selectedData.Obor,
        //////                    KodProizv = selectedData.KodProizv,
        //////                    TextProizv = selectedData.TextProizv,
        //////                    TextOb = selectedData.TextOb,
        //////                    TextVyaz = selectedData.TextVyaz
        //////                });

        //////                Debug.WriteLine($"✅ Данные сохранены в БД, nrId = {newNrId}");

        //////                // **4. Обновляем `nrId` в `DataTable`**
        //////                newRow["nrId"] = newNrId;



        //////                Debug.WriteLine($"📢 DataTable строк: {_normRaszTable.Rows.Count}");
        //////                foreach (DataRow row in _normRaszTable.Rows)
        //////                {
        //////                    Debug.WriteLine($"🔹 nrId: {row["nrId"]}, KodO: {row["KodO"]}");
        //////                }


        //////                Debug.WriteLine($"📢 gridView5.DataRowCount после обновления: {gridView5.DataRowCount}");
        //////                for (int i = 0; i < gridView5.DataRowCount; i++)
        //////                {
        //////                    Debug.WriteLine($"🔹 GridView[{i}] -> nrId: {gridView5.GetRowCellValue(i, "nrId")}, KodO: {gridView5.GetRowCellValue(i, "KodO")}");
        //////                }


        //////                // **5. Обновляем UI**
        //////                gridControl5.DataSource = null;
        //////                gridControl5.DataSource = _normRaszBindingSource;
        //////                gridControl5.RefreshDataSource();
        //////                gridView5.RefreshData();
        //////                await Task.Delay(100);


        //////                Debug.WriteLine($"📢 gridView5.DataRowCount: {gridView5.DataRowCount}");
        //////                for (int i = 0; i < gridView5.DataRowCount; i++)
        //////                {
        //////                    Debug.WriteLine($"🔹 GridView[{i}] -> nrId: {gridView5.GetRowCellValue(i, "nrId")}, KodO: {gridView5.GetRowCellValue(i, "KodO")}");
        //////                }

        //////                // **6. Открываем EditForm**
        //////                int newRowHandle = gridView5.LocateByValue("nrId", newNrId);
        //////                if (newRowHandle >= 0)
        //////                {
        //////                    gridView5.FocusedRowHandle = newRowHandle;
        //////                    gridView5.ShowPopupEditForm();
        //////                    Debug.WriteLine($"✅ Фокус на строке {newRowHandle}, EditForm открыт!");
        //////                }
        //////                else
        //////                {
        //////                    Debug.WriteLine("❌ Строка не найдена в `GridView`!");
        //////                }
        //////            }
        //////        }
        //////        else
        //////        {
        //////            Debug.WriteLine("❌ Выбор отменён, удаляем последнюю строку.");
        //////            if (_normRaszTable.Rows.Count > 0)
        //////            {
        //////                _normRaszTable.Rows[_normRaszTable.Rows.Count - 1].Delete();
        //////                _normRaszBindingSource.ResetBindings(false);
        //////            }
        //////        }
        //////    }
        //////}


        //private async void gridView5_InitNewRow(object sender, InitNewRowEventArgs e)
        //{
        //    Debug.WriteLine($"🔹 InitNewRow вызван! e.RowHandle = {e.RowHandle}");
        //    gridView5.DeleteRow(e.RowHandle);
        //    using (NormOperNew selectionForm = new NormOperNew())
        //    {
        //        if (selectionForm.ShowDialog() == DialogResult.OK)
        //        {
        //            var selectedData = selectionForm.SelectedRowData;
        //            if (selectedData != null)
        //            {
        //                Debug.WriteLine($"✅ Операция выбрана: {selectedData.KodO}");

        //                // **1. Добавляем в `_normRaszList`, не через `GridView`!**
        //                var newNormRasz = new NormRasz
        //                {
        //                    AnnId = _newAnnId,
        //                    KodO = selectedData.KodO,
        //                    Text = selectedData.Text,
        //                    Spec = selectedData.Spec,
        //                    Razryad = selectedData.Razryad,
        //                    Obor = selectedData.Obor,
        //                    KodProizv = selectedData.KodProizv,
        //                    TextProizv = selectedData.TextProizv,
        //                    TextOb = selectedData.TextOb,
        //                    TextVyaz = selectedData.TextVyaz
        //                };

        //                _normRaszList.Add(newNormRasz);
        //                Debug.WriteLine("✅ Данные занесены в `_normRaszList`.");

        //                // **2. Принудительно обновляем `GridView`**
        //                _normRaszBindingSource.ResetBindings(false);

        //                // **3. Ждём обновления UI**
        //                await Task.Delay(100);

        //                // **4. Сохраняем в БД**
        //                newNormRasz.nrId = await _artNormService.InsertNormRaszAsync(newNormRasz);
        //                Debug.WriteLine($"✅ Данные сохранены в БД, nrId = {newNormRasz.nrId}");

        //                // **5. Обновляем `nrId` в BindingList**
        //                var item = _normRaszList.LastOrDefault();
        //                if (item != null) item.nrId = newNormRasz.nrId;

        //                // **6. Принудительное обновление**
        //                _normRaszBindingSource.ResetBindings(false);
        //                gridControl5.RefreshDataSource();
        //                gridView5.RefreshData();
        //                await Task.Delay(100);

        //                // **7. Фокусируем строку и открываем EditForm**
        //                int newRowHandle = gridView5.LocateByValue("nrId", newNormRasz.nrId);
        //                if (newRowHandle >= 0)
        //                {
        //                    gridView5.FocusedRowHandle = newRowHandle;
        //                    gridView5.ShowPopupEditForm();
        //                    Debug.WriteLine($"✅ Фокус установлен на строку {newRowHandle} и EditForm открыт!");
        //                }
        //                else
        //                {
        //                    Debug.WriteLine("❌ Строка не найдена в `GridView`!");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            Debug.WriteLine("❌ Выбор отменён, удаляем пустую строку.");
        //            if (_normRaszList.Count > 0)
        //            {
        //                _normRaszList.RemoveAt(_normRaszList.Count - 1);
        //                _normRaszBindingSource.ResetBindings(false);
        //            }
        //        }
        //    }
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
