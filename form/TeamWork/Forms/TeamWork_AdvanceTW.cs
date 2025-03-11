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
        private int selectedRowHandle = -1;
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

        private void customButton1_Click(object sender, EventArgs e)
        {
            //gridView5.AddNewRow();
            //int newRowHandle = gridView5.RowCount-1;
            //gridView5.FocusedRowHandle = newRowHandle; 

            //using (NormOperNew selectionForm = new NormOperNew())
            //{
            //    if (selectionForm.ShowDialog() == DialogResult.OK)
            //    {
            //        var selectedData = selectionForm.SelectedRowData;

            //        gridView5.SetRowCellValue(newRowHandle, "kod_o", selectedData.Kod_o);
            //        gridView5.SetRowCellValue(newRowHandle, "text", selectedData.Text);
            //        gridView5.SetRowCellValue(newRowHandle, "spec", selectedData.Spec);
            //        gridView5.SetRowCellValue(newRowHandle, "razryd", selectedData.Razryad);
            //        gridView5.SetRowCellValue(newRowHandle, "obor", selectedData.Obor);
            //        gridView5.SetRowCellValue(newRowHandle, "kod_proizv", selectedData.Kod_proizv);
            //        gridView5.SetRowCellValue(newRowHandle, "text_proizv", selectedData.Text_proizv);
            //        gridView5.SetRowCellValue(newRowHandle, "text_ob", selectedData.Text_ob);
            //        gridView5.SetRowCellValue(newRowHandle, "text_vyaz", selectedData.Text_vyaz);
                            
            //        gridView5.UpdateCurrentRow();
                            
            //       // gridView5.ShowPopupEditForm();
            //    }
            //    else
            //    {
            //        gridView5.DeleteRow(newRowHandle);
            //    }
            //}

        }



        private DataTable _raszDataTable;

        private async void bufferButton_Click(object sender, EventArgs e)
        {
           await bufferLoad();
        }

        //private async void gridView5_InitNewRow(object sender, InitNewRowEventArgs e)
        //{
        //    Debug.WriteLine("🔹 InitNewRow вызван! e.RowHandle = " + e.RowHandle);

        //    using (NormOperNew selectionForm = new NormOperNew()) // Окно выбора операции
        //    {
        //        if (selectionForm.ShowDialog() == DialogResult.OK) // Выбрали операцию?
        //        {
        //            var selectedData = selectionForm.SelectedRowData;
        //            if (selectedData != null)
        //            {
        //                Debug.WriteLine("✅ Операция выбрана: " + selectedData.KodO);

        //                // **1. Создаём новую запись**
        //                var newNormRasz = new NormRasz
        //                {
        //                    nrId = -1, // Временный ID
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

        //                // **2. Добавляем в `BindingList`**
        //                _normRaszList.Add(newNormRasz);
        //                _normRaszBindingSource.ResetBindings(false);
        //                Debug.WriteLine($"📢 В `BindingList` теперь {_normRaszList.Count} записей");

        //                // **3. Обновляем `GridView` перед сохранением**
        //                //gridControl5.RefreshDataSource();
        //                //gridView5.RefreshData();

        //                // **4. Сохраняем в БД**
        //                newNormRasz.nrId = await _artNormService.InsertNormRaszAsync(newNormRasz);
        //                Debug.WriteLine("✅ Данные сохранены в БД, присвоен nrId = " + newNormRasz.nrId);

        //                // **5. Принудительно обновляем `BindingSource` и `GridView`**
        //                _normRaszBindingSource.ResetBindings(false);
        //                gridControl5.RefreshDataSource();
        //                gridView5.RefreshData();

        //                //// **6. Ждём обновления UI**
        //                //await Task.Delay(200);
        //                //Application.DoEvents();

        //                // **7. Повторно ищем строку**
        //                int newRowHandle = -1;

        //                Debug.WriteLine("📢 Полный список строк перед поиском:");
        //                for (int i = 0; i < gridView5.DataRowCount; i++)
        //                {
        //                    Debug.WriteLine($"🔹 Строка {i}: nrId = {gridView5.GetRowCellValue(i, "nrId")}");
        //                }

        //                for (int i = 0; i < 5; i++)
        //                {
        //                    newRowHandle = gridView5.LocateByValue("nrId", newNormRasz.nrId);
        //                    if (newRowHandle >= 0) break;
        //                    await Task.Delay(100);
        //                }

        //                Debug.WriteLine($"🔹 Найденная строка в `GridView`: {newRowHandle}");

        //                if (newRowHandle >= 0)
        //                {
        //                    // **8. Устанавливаем фокус на строку**
        //                    gridView5.MakeRowVisible(newRowHandle, false);
        //                    gridView5.FocusedRowHandle = newRowHandle;
        //                    Debug.WriteLine($"✅ Фокус установлен на строку: {gridView5.FocusedRowHandle}");

        //                    gridView5.UpdateCurrentRow(); // ОБНОВЛЯЕМ текущую строку!
        //                    gridView5.RefreshData(); // ОБНОВЛЯЕМ весь `GridView`!

        //                    gridView5.ShowPopupEditForm();
        //                    Debug.WriteLine("📝 EditForm открыт!");
        //                }
        //                else
        //                {
        //                    Debug.WriteLine("❌ Строка не найдена в `GridView`! Что-то идёт не так...");
        //                    MessageBox.Show("Ошибка: не удалось найти добавленную строку!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        //private async void gridView5_InitNewRow(object sender, InitNewRowEventArgs e)
        //{
        //    Debug.WriteLine("🔹 InitNewRow вызван! e.RowHandle = " + e.RowHandle);

        //    using (NormOperNew selectionForm = new NormOperNew()) // Окно выбора операции
        //    {
        //        if (selectionForm.ShowDialog() == DialogResult.OK) // Если выбрана операция
        //        {
        //            var selectedData = selectionForm.SelectedRowData;
        //            if (selectedData != null)
        //            {
        //                Debug.WriteLine("✅ Операция выбрана: " + selectedData.KodO);

        //                // **1. Создаём новую запись**
        //                var newNormRasz = new NormRasz
        //                {
        //                    nrId = -1, // Временный ID
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

        //                // **2. Добавляем в `BindingList` и сразу обновляем `BindingSource`**
        //                _normRaszList.Add(newNormRasz);
        //                _normRaszBindingSource.ResetBindings(false);

        //                Debug.WriteLine("✅ Новая строка добавлена в `BindingList`.");

        //                // **3. Обновляем `GridView` перед сохранением**
        //                gridControl5.RefreshDataSource();
        //                gridView5.RefreshData();

        //                // **4. Сохраняем в БД**
        //                newNormRasz.nrId = await _artNormService.InsertNormRaszAsync(newNormRasz);
        //                Debug.WriteLine("✅ Данные сохранены в БД, присвоен nrId = " + newNormRasz.nrId);

        //                // **5. Принудительно обновляем `BindingSource` и `GridView`**
        //                _normRaszBindingSource.ResetBindings(false);
        //                gridControl5.RefreshDataSource();
        //                gridView5.RefreshData();

        //                // **6. Ждём обновления UI**
        //                await Task.Delay(200);
        //                Application.DoEvents();

        //                // **7. Повторно ищем строку, если не найдена**
        //                int newRowHandle = -1;
        //                for (int i = 0; i < 5; i++) // Даем 5 попыток
        //                {
        //                    newRowHandle = gridView5.LocateByValue("nrId", newNormRasz.nrId);
        //                    if (newRowHandle >= 0) break; // Нашли строку? Отлично!
        //                    await Task.Delay(100); // Ждём ещё 100мс
        //                }

        //                Debug.WriteLine($"🔹 Найденная строка в `GridView`: {newRowHandle}");

        //                if (newRowHandle >= 0)
        //                {
        //                    // **8. Устанавливаем фокус на строку**
        //                    gridView5.MakeRowVisible(newRowHandle, false);
        //                    gridView5.FocusedRowHandle = newRowHandle;
        //                    Debug.WriteLine($"✅ Фокус установлен на строку: {gridView5.FocusedRowHandle}");

        //                    gridView5.UpdateCurrentRow(); // ОБНОВЛЯЕМ текущую строку!
        //                    gridView5.RefreshData(); // ОБНОВЛЯЕМ весь `GridView`!

        //                    gridView5.ShowPopupEditForm();
        //                    Debug.WriteLine("📝 EditForm открыт!");
        //                }
        //                else
        //                {
        //                    Debug.WriteLine("❌ Строка не найдена в `GridView`! Что-то идёт не так...");
        //                    MessageBox.Show("Ошибка: не удалось найти добавленную строку!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        //private async void gridView5_InitNewRow(object sender, InitNewRowEventArgs e)
        //{
        //    Debug.WriteLine("🔹 InitNewRow вызван! e.RowHandle = " + e.RowHandle);

        //    using (NormOperNew selectionForm = new NormOperNew()) // Форма выбора операции
        //    {
        //        if (selectionForm.ShowDialog() == DialogResult.OK) // Если выбрана операция
        //        {
        //            var selectedData = selectionForm.SelectedRowData;
        //            if (selectedData != null)
        //            {
        //                Debug.WriteLine("✅ Операция выбрана: " + selectedData.KodO);

        //                // **1. Добавляем новую пустую запись**
        //                var newNormRasz = new NormRasz { nrId = -1, AnnId = _newAnnId }; // Временный ID (-1)
        //                _normRaszList.Add(newNormRasz);
        //                _normRaszBindingSource.ResetBindings(false);

        //                Debug.WriteLine("✅ Пустая строка добавлена в `BindingList`.");

        //                // **2. Обновляем строку, чтобы не было пустой**
        //                newNormRasz.KodO = selectedData.KodO;
        //                newNormRasz.Text = selectedData.Text;
        //                newNormRasz.Spec = selectedData.Spec;
        //                newNormRasz.Razryad = selectedData.Razryad;
        //                newNormRasz.Obor = selectedData.Obor;
        //                newNormRasz.KodProizv = selectedData.KodProizv;
        //                newNormRasz.TextProizv = selectedData.TextProizv;
        //                newNormRasz.TextOb = selectedData.TextOb;
        //                newNormRasz.TextVyaz = selectedData.TextVyaz;

        //                Debug.WriteLine("✅ Данные перезаписаны в `BindingList`.");

        //                // **3. Сохраняем в БД**
        //                newNormRasz.nrId = await _artNormService.InsertNormRaszAsync(newNormRasz);
        //                Debug.WriteLine("✅ Данные сохранены в БД, присвоен nrId = " + newNormRasz.nrId);

        //                // **4. Обновляем `BindingSource` и `GridView`**
        //                _normRaszBindingSource.ResetBindings(false);
        //                gridControl5.RefreshDataSource();
        //                gridView5.RefreshData();

        //                // **5. Ждём обновления UI и фокусируем строку**
        //                await Task.Delay(100);
        //                Application.DoEvents();

        //                int newRowHandle = gridView5.LocateByValue("nrId", newNormRasz.nrId);
        //                Debug.WriteLine($"🔹 Найденная строка в `GridView`: {newRowHandle}");

        //                if (newRowHandle >= 0)
        //                {
        //                    // **6. Обновляем UI и открываем EditForm**
        //                    gridView5.MakeRowVisible(newRowHandle, false);
        //                    gridView5.FocusedRowHandle = newRowHandle;
        //                    Debug.WriteLine($"✅ Фокус установлен на строку: {gridView5.FocusedRowHandle}");

        //                    gridView5.UpdateCurrentRow(); // ОБНОВЛЯЕМ текущую строку!
        //                    gridView5.RefreshData(); // ОБНОВЛЯЕМ весь `GridView`!

        //                    gridView5.ShowPopupEditForm();
        //                    Debug.WriteLine("📝 EditForm открыт!");
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


        private async void gridView5_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            Debug.WriteLine("🔹 InitNewRow вызван! e.RowHandle = " + e.RowHandle);

            using (NormOperNew selectionForm = new NormOperNew()) // Форма выбора операции
            {
                if (selectionForm.ShowDialog() != DialogResult.OK) // Если выбрана операция
                {
                    Debug.WriteLine("❌ Выбор отменён, удаляем пустую строку.");
                    if (_normRaszList.Count > 0)
                    {
                        _normRaszList.RemoveAt(_normRaszList.Count - 1);
                        _normRaszBindingSource.ResetBindings(false);
                    }
                    return;
                }
                var selectedData = selectionForm.SelectedRowData;
                if (selectedData == null)
                {
                    return;
                }
                Debug.WriteLine("✅ Операция выбрана: " + selectedData.KodO);

                // **1.Добавляем новую запись в `BindingList`**
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

                Debug.WriteLine("✅ Данные занесены в `BindingList`.");

                // **3.Сохраняем в БД и обновляем `nrId`**
                newNormRasz.nrId = await _artNormService.InsertNormRaszAsync(newNormRasz);

                Debug.WriteLine("✅ Данные сохранены в БД, присвоен nrId = " + newNormRasz.nrId);

                _normRaszBindingSource.ResetBindings(false);
                gridControl5.RefreshDataSource();
                gridView5.RefreshData();

                //**4.Ждём обновления UI и открываем EditForm**
                await Task.Delay(1000);
                //           Application.DoEvents();
                var item = _normRaszList.FirstOrDefault(x => x.nrId == newNormRasz.nrId);
                int newRowHandle = item != null ? _normRaszBindingSource.IndexOf(item) : -1;

                // int newRowHandle = gridView5.LocateByValue("nrId", newNormRasz.nrId);
                Debug.WriteLine($"🔹 Найденная строка в `GridView`: {newRowHandle}");

                if (newRowHandle < 0)
                {
                    Debug.WriteLine("❌ Строка не найдена в `GridView`!");
                    return;
                }
                //  **5.Делаем строку видимой и открываем EditForm**
                gridView5.MakeRowVisible(newRowHandle, false);
                gridView5.FocusedRowHandle = newRowHandle;
                Debug.WriteLine($"✅ Фокус установлен на строку: {gridView5.FocusedRowHandle}");

                gridView5.ShowPopupEditForm();
                Debug.WriteLine("📝 EditForm открыт!");
                //gridControl5.BeginInvoke(new Action(() =>
                //{
                //    gridView5.FocusedRowHandle = newRowHandle;
                //    Debug.WriteLine($"✅ Фокус установлен на строку: {gridView5.FocusedRowHandle}");

                //    // **6. Открываем `EditForm`, если оно не открылось само**
                //    if (!gridView5.IsEditing)
                //    {
                //        gridView5.ShowPopupEditForm();
                //        Debug.WriteLine("📝 EditForm открыт вручную!");
                //    }
                //    else
                //    {
                //        Debug.WriteLine("✅ `EditForm` уже открыт автоматически.");
                //    }
                //}
                //));

            }
        }

        //private  async void gridView5_InitNewRow(object sender, InitNewRowEventArgs e)
        //{
        //    using (NormOperNew selectionForm = new NormOperNew()) // Выбор операции
        //    {
        //        if (selectionForm.ShowDialog() == DialogResult.OK) // Операция выбрана?
        //        {
        //            var selectedData = selectionForm.SelectedRowData;
        //            if (selectedData != null)
        //            {
        //                //// **Создаём новую запись и добавляем её в список**
        //                //var newNormRasz = new NormRasz
        //                //{
        //                //    AnnId = _newAnnId,
        //                //    KodO = selectedData.KodO,
        //                //    Text = selectedData.Text,
        //                //    Spec = selectedData.Spec,
        //                //    Razryad = selectedData.Razryad,
        //                //    Obor = selectedData.Obor,
        //                //    KodProizv = selectedData.KodProizv,
        //                //    TextProizv = selectedData.TextProizv,
        //                //    TextOb = selectedData.TextOb,
        //                //    TextVyaz = selectedData.TextVyaz
        //                //};

        //                //// **Добавляем в `BindingList`, которая сразу обновляет `GridView`**
        //                //_normRaszList.Add(newNormRasz);
        //                // **Находим пустую строку, созданную InitNewRow**
        //                NormRasz newNormRasz = (NormRasz)_normRaszBindingSource[e.RowHandle];

        //                // **Заполняем данные в уже созданной строке**
        //                newNormRasz.AnnId = _newAnnId;
        //                newNormRasz.KodO = selectedData.KodO;
        //                newNormRasz.Text = selectedData.Text;
        //                newNormRasz.Spec = selectedData.Spec;
        //                newNormRasz.Razryad = selectedData.Razryad;
        //                newNormRasz.Obor = selectedData.Obor;
        //                newNormRasz.KodProizv = selectedData.KodProizv;
        //                newNormRasz.TextProizv = selectedData.TextProizv;
        //                newNormRasz.TextOb = selectedData.TextOb;
        //                newNormRasz.TextVyaz = selectedData.TextVyaz;

        //                // **Обновляем `BindingSource` и `GridView`**
        //                _normRaszBindingSource.ResetBindings(false);
        //                gridView5.RefreshData();


        //                await Task.Delay(50);
        //                // **Сохраняем в БД и обновляем `nrId`**
        //                newNormRasz.nrId = await _artNormService.InsertNormRaszAsync(newNormRasz);

        //                // **Обновляем UI**
        //                gridView5.RefreshData();
        //                await Task.Delay(50);

        //                // **Фокусируемся на новой строке и открываем EditForm**
        //                int newRowHandle = gridView5.LocateByValue("nrId", newNormRasz.nrId);
        //                if (newRowHandle >= 0)
        //                {
        //                    gridView5.FocusedRowHandle = newRowHandle;
        //                    gridView5.ShowPopupEditForm();
        //                }
        //            }
        //        }
        //        else
        //        {
        //            // **Если пользователь отменил, удаляем последнюю добавленную строку**
        //            if (_normRaszList.Count > 0)
        //            {
        //                _normRaszList.RemoveAt(_normRaszList.Count - 1);
        //                gridView5.RefreshData();
        //            }
        //        }
        //    }
        //}


        //private async void gridView5_InitNewRow(object sender, InitNewRowEventArgs e)
        //{
        //    using (NormOperNew selectionForm = new NormOperNew())//жмякаем Добавить
        //    {
        //       // int newRowHandle = e.RowHandle;
        //        if (selectionForm.ShowDialog() == DialogResult.OK) //открываем форму с таблицей norm_rasz
        //        {
        //            var selectedData = selectionForm.SelectedRowData;//запоминаем выбранную строку, будем работать с ней
        //            if (selectedData != null)
        //            {
        //                // Создаем новую запись NormRasz
        //                var newNormRasz = new NormRasz
        //                {
        //                    nrId = -1,
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
        //                    _normRaszBindingSource.Add(newNormRasz);
        //                    _normRaszBindingSource.ResetBindings(false);

        //                try
        //                {
        //                   newNormRasz.nrId = await _artNormService.InsertNormRaszAsync(newNormRasz);
        //                // Добавляем в список и UI
        //                //_normRaszList.Add(newNormRasz);

        //                //gridView5.SetRowCellValue(newRowHandle, "annId", newNormRasz.AnnId);
        //                //gridView5.SetRowCellValue(newRowHandle, "kod_o", newNormRasz.KodO);
        //                //gridView5.SetRowCellValue(newRowHandle, "text", newNormRasz.Text);
        //                //gridView5.SetRowCellValue(newRowHandle, "spec", newNormRasz.Spec);
        //                //gridView5.SetRowCellValue(newRowHandle, "razryd", newNormRasz.Razryad);
        //                //gridView5.SetRowCellValue(newRowHandle, "obor", newNormRasz.Obor);
        //                //gridView5.SetRowCellValue(newRowHandle, "kod_proizv", newNormRasz.KodProizv);
        //                //gridView5.SetRowCellValue(newRowHandle, "text_proizv", newNormRasz.TextProizv);
        //                //gridView5.SetRowCellValue(newRowHandle, "text_ob", newNormRasz.TextOb);
        //                //gridView5.SetRowCellValue(newRowHandle, "text_vyaz", newNormRasz.TextVyaz);

        //                //    //gridView5.UpdateCurrentRow();
        //                //_normRaszBindingSource.EndEdit(); // Заканчиваем редактирование                                                       

        //                //    gridControl5.RefreshDataSource();
        //                //    gridView5.RefreshData();

        //                }
        //                catch (Exception ex)
        //                {
        //                    await _logger.LogErrorAsync(ex, "Ошибка при сохранении новой записи в NormRasz");
        //                    MessageBox.Show("Ошибка при сохранении в БД. Подробности в логе.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                }
        //                // Удаляем возможную пустую строку, которая создаётся автоматически при InitNewRow
        //                int emptyRowHandle = gridView5.LocateByValue("nrId", -1);
        //                if (emptyRowHandle >= 0)
        //                {
        //                    gridView5.DeleteRow(emptyRowHandle);
        //                }
        //                //    int newRowHandle = gridView5.LocateByValue("AnnId", newNormRasz.AnnId);
        //                //    gridView5.SetRowCellValue(newRowHandle, "nrId", newNormRasz.nrId);
        //                gridView5.UpdateCurrentRow();
        //                _normRaszBindingSource.EndEdit();
        //                gridControl5.RefreshDataSource();
        //                gridView5.RefreshData();          
        //                //// **Открываем PopupEditForm**
        //                gridView5.FocusedRowHandle = gridView5.LocateByValue("nrId", newNormRasz.nrId);
        //                gridView5.ShowPopupEditForm();

        //            }
        //        }
        //        else
        //        {
        //            await _artNormService.deleteRow("norm_rasz", _newAnnId);
        //            gridView5.CancelUpdateCurrentRow();
        //            gridView5.DeleteRow(gridView5.LocateByValue("annId", _newAnnId));
        //        }
        //    }
        //}

        //private async void gridView5_InitNewRow(object sender, InitNewRowEventArgs e)
        //{
        //    using (NormOperNew selectionForm = new NormOperNew()) // Окно выбора операции
        //    {
        //        if (selectionForm.ShowDialog() == DialogResult.OK) // Выбрали операцию?
        //        {
        //            var selectedData = selectionForm.SelectedRowData;
        //            if (selectedData != null)
        //            {
        //                // **Создаём новый объект и добавляем его в BindingSource**
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

        //                // **Добавляем в `BindingSource` (НЕ В `GridView`!)**
        //                _normRaszBindingSource.Add(newNormRasz);

        //                // **Сохраняем в БД и обновляем `nrId`**
        //                newNormRasz.nrId = await _artNormService.InsertNormRaszAsync(newNormRasz);

        //                // **Обновляем `BindingSource` и `GridView`**
        //                _normRaszBindingSource.ResetBindings(false);
        //                gridControl5.RefreshDataSource();
        //                gridView5.RefreshData();

        //                // **Фокусируемся на новой строке и открываем EditForm**
        //                gridView5.FocusedRowHandle = gridView5.LocateByValue("nrId", newNormRasz.nrId);
        //                gridView5.ShowPopupEditForm();
        //            }
        //        }
        //        else
        //        {
        //            // **Если пользователь закрыл окно — удаляем последнюю добавленную строку**
        //            if (_normRaszBindingSource.Count > 0)
        //            {
        //                _normRaszBindingSource.RemoveAt(_normRaszBindingSource.Count - 1);
        //                _normRaszBindingSource.ResetBindings(false);
        //            }
        //        }
        //    }
        //}


        //private async void gridView5_InitNewRow(object sender, InitNewRowEventArgs e)
        //{
        //    using (NormOperNew selectionForm = new NormOperNew()) // Жмякаем "Добавить", создаём форму, но пока не показываем
        //    {
        //        if (selectionForm.ShowDialog() == DialogResult.OK) // Открываем форму выбора операции
        //        {
        //            var selectedData = selectionForm.SelectedRowData; // Запоминаем выбранную строку

        //            if (selectedData != null)
        //            {
        //                int newRowHandle = gridView5.DataRowCount;
        //                // **Заполняем новую строку, созданную InitNewRow (НЕ ДОБАВЛЯЕМ НОВУЮ!)**
        //                gridView5.SetRowCellValue(newRowHandle, "annId", _newAnnId);
        //                gridView5.SetRowCellValue(newRowHandle, "kod_o", selectedData.KodO);
        //                gridView5.SetRowCellValue(newRowHandle, "text", selectedData.Text);
        //                gridView5.SetRowCellValue(newRowHandle, "spec", selectedData.Spec);
        //                gridView5.SetRowCellValue(newRowHandle, "razryd", selectedData.Razryad);
        //                gridView5.SetRowCellValue(newRowHandle, "obor", selectedData.Obor);
        //                gridView5.SetRowCellValue(newRowHandle, "kod_proizv", selectedData.KodProizv);
        //                gridView5.SetRowCellValue(newRowHandle, "text_proizv", selectedData.TextProizv);
        //                gridView5.SetRowCellValue(newRowHandle, "text_ob", selectedData.TextOb);
        //                gridView5.SetRowCellValue(newRowHandle, "text_vyaz", selectedData.TextVyaz);


        //                // **Сохраняем строку в БД и получаем настоящий nrId**
        //                int nrId = await _artNormService.InsertNormRaszAsync(new NormRasz
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
        //                });

        //                // **Обновляем `nrId` в уже существующей строке**
        //                gridView5.SetRowCellValue(newRowHandle, "nrId", nrId);

        //                // **Фиксируем изменения**
        //                gridView5.UpdateCurrentRow();
        //                _normRaszBindingSource.EndEdit();
        //                gridControl5.RefreshDataSource();
        //                gridView5.RefreshData();

        //                // **Открываем EditForm**
        //                gridView5.FocusedRowHandle = newRowHandle;
        //                gridView5.ShowPopupEditForm();

        //                // Добавляем новую запись в BindingSource (это сразу обновляет GridView)
        //                // _normRaszBindingSource.Add(newNormRasz);
        //                // _normRaszBindingSource.ResetBindings(false);

        //                // // Сохраняем в БД и получаем настоящий nrId
        //                // try
        //                // {
        //                //     newNormRasz.nrId = await _artNormService.InsertNormRaszAsync(newNormRasz);
        //                // }
        //                // catch (Exception ex)
        //                // {
        //                //     await _logger.LogErrorAsync(ex, "Ошибка при сохранении NormRasz");
        //                //     MessageBox.Show("Ошибка при сохранении в БД. Подробности в логе.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                //     return;
        //                // }

        //                // // Удаляем возможную пустую строку, которая создаётся автоматически при InitNewRow
        //                // int emptyRowHandle = gridView5.LocateByValue("nrId", 0);
        //                // if (emptyRowHandle >= 0)
        //                // {
        //                //     gridView5.DeleteRow(emptyRowHandle);
        //                // }

        //                // // Обновляем ID в строке GridView (чтобы сразу отобразился настоящий nrId)
        //                //// int newRowHandle = gridView5.LocateByValue("AnnId", newNormRasz.AnnId);
        //                //// gridView5.SetRowCellValue(newRowHandle, "nrId", newNormRasz.nrId);
        //                // gridView5.UpdateCurrentRow();
        //                // _normRaszBindingSource.EndEdit();
        //                // gridControl5.RefreshDataSource();
        //                // gridView5.RefreshData();

        //                // // Фокусируемся на новой строке и открываем EditForm
        //                // gridView5.FocusedRowHandle = gridView5.LocateByValue("nrId", newNormRasz.nrId);
        //                // gridView5.ShowPopupEditForm();
        //            }
        //        }
        //        else
        //        {
        //            // Если пользователь закрыл окно без выбора - удаляем пустую строку
        //            int emptyRowHandle = gridView5.LocateByValue("AnnId", _newAnnId);
        //            if (emptyRowHandle >= 0)
        //            {
        //                gridView5.DeleteRow(emptyRowHandle);
        //            }
        //        }
        //    }
        //}

        //private async void gridView5_InitNewRow(object sender, InitNewRowEventArgs e)
        //{
        //    // Временно отключаем редактирование
        //    gridView5.OptionsBehavior.Editable = false;

        //    using (NormOperNew selectionForm = new NormOperNew())
        //    {
        //        if (selectionForm.ShowDialog() == DialogResult.OK)
        //        {
        //            var selectedData = selectionForm.SelectedRowData;
        //            if (selectedData != null)
        //            {
        //                // **Создаём новую запись NormRasz**
        //                var newNormRasz = new NormRasz
        //                {
        //                    nrId = -1, // Пока не сохранится в БД
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

        //                // **Добавляем в список**
        //                _normRaszList.Add(newNormRasz);
        //                _normRaszBindingSource.ResetBindings(false);

        //                // **Обновляем UI**
        //                await Task.Delay(50);
        //                gridView5.RefreshData();

        //                // **Получаем индекс новой строки**
        //                int newRowHandle = gridView5.LocateByValue("nrId", -1);
        //                if (newRowHandle < 0) newRowHandle = gridView5.RowCount - 1;

        //                // **Заполняем данные вручную**
        //                gridView5.SetRowCellValue(newRowHandle, "annId", newNormRasz.AnnId);
        //                gridView5.SetRowCellValue(newRowHandle, "kod_o", newNormRasz.KodO);
        //                gridView5.SetRowCellValue(newRowHandle, "text", newNormRasz.Text);
        //                gridView5.SetRowCellValue(newRowHandle, "spec", newNormRasz.Spec);
        //                gridView5.SetRowCellValue(newRowHandle, "razryd", newNormRasz.Razryad);
        //                gridView5.SetRowCellValue(newRowHandle, "obor", newNormRasz.Obor);
        //                gridView5.SetRowCellValue(newRowHandle, "kod_proizv", newNormRasz.KodProizv);
        //                gridView5.SetRowCellValue(newRowHandle, "text_proizv", newNormRasz.TextProizv);
        //                gridView5.SetRowCellValue(newRowHandle, "text_ob", newNormRasz.TextOb);
        //                gridView5.SetRowCellValue(newRowHandle, "text_vyaz", newNormRasz.TextVyaz);

        //                gridView5.UpdateCurrentRow();
        //                _normRaszBindingSource.EndEdit();

        //                try
        //                {
        //                    // **Сохраняем в БД**
        //                    newNormRasz.nrId = await _artNormService.InsertNormRaszAsync(newNormRasz);
        //                    gridView5.SetRowCellValue(newRowHandle, "nrId", newNormRasz.nrId);
        //                    gridView5.UpdateCurrentRow();

        //                    gridControl5.RefreshDataSource();
        //                    gridView5.RefreshData();

        //                    // **Проверяем перед открытием EditForm**
        //                    Debug.WriteLine($"[DEBUG] gridView5.FocusedRowHandle: {gridView5.FocusedRowHandle}");
        //                    Debug.WriteLine($"[DEBUG] nrId: {gridView5.GetRowCellValue(gridView5.FocusedRowHandle, "nrId")}");
        //                    Debug.WriteLine($"[DEBUG] AnnId: {gridView5.GetRowCellValue(gridView5.FocusedRowHandle, "annId")}");
        //                    Debug.WriteLine($"[DEBUG] KodO: {gridView5.GetRowCellValue(gridView5.FocusedRowHandle, "kod_o")}");
        //                    Debug.WriteLine($"[DEBUG] Text: {gridView5.GetRowCellValue(gridView5.FocusedRowHandle, "text")}");

        //                    // **Фокусируемся и открываем EditForm**
        //                    await Task.Delay(50);
        //                    gridView5.FocusedRowHandle = newRowHandle;
        //                    gridView5.ShowPopupEditForm();
        //                }
        //                catch (Exception ex)
        //                {
        //                    await _logger.LogErrorAsync(ex, "Ошибка при сохранении новой записи в NormRasz");
        //                    MessageBox.Show("Ошибка при сохранении в БД. Подробности в логе.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            // **Удаляем строку, если отменили выбор**
        //            await Task.Delay(50);
        //            int rowToDelete = gridView5.LocateByValue("nrId", -1);
        //            if (rowToDelete >= 0)
        //            {
        //                gridView5.DeleteRow(rowToDelete);
        //            }
        //        }
        //    }

        //    // Включаем редактирование обратно
        //    gridView5.OptionsBehavior.Editable = true;
        //}

        //private async void gridView5_InitNewRow(object sender, InitNewRowEventArgs e)
        //{
        //    // Отключаем временно редактирование, чтобы предотвратить лишние вызовы InitNewRow
        //    gridView5.OptionsBehavior.Editable = false;

        //    using (NormOperNew selectionForm = new NormOperNew())//жмякаем Добавить и создаём форму с таблицей norm_rasz. пока не показываем
        //    {
        //       // int newRowHandle = e.RowHandle;
        //        if (selectionForm.ShowDialog() == DialogResult.OK) //открываем форму с таблицей norm_rasz
        //        {
        //            var selectedData = selectionForm.SelectedRowData;//запоминаем выбранную строку, будем работать с ней
        //            if (selectedData != null)
        //            {
        //                // Создаем новую запись NormRasz
        //                var newNormRasz = new NormRasz
        //                {
        //                    nrId = -1,
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

        //                // Добавляем в список и UI
        //                _normRaszList.Add(newNormRasz);
        //                _normRaszBindingSource.ResetBindings(false);
        //                // Ждём, пока данные отобразятся в UI
        //                await Task.Delay(100);

        //                int newRowHandle = gridView5.LocateByValue("nrId", -1);
        //                if (newRowHandle < 0) newRowHandle = gridView5.RowCount - 1;

        //                gridView5.SetRowCellValue(newRowHandle, "annId", newNormRasz.AnnId);
        //                gridView5.SetRowCellValue(newRowHandle, "kod_o", newNormRasz.KodO);
        //                gridView5.SetRowCellValue(newRowHandle, "text", newNormRasz.Text);
        //                gridView5.SetRowCellValue(newRowHandle, "spec", newNormRasz.Spec);
        //                gridView5.SetRowCellValue(newRowHandle, "razryd", newNormRasz.Razryad);
        //                gridView5.SetRowCellValue(newRowHandle, "obor", newNormRasz.Obor);
        //                gridView5.SetRowCellValue(newRowHandle, "kod_proizv", newNormRasz.KodProizv);
        //                gridView5.SetRowCellValue(newRowHandle, "text_proizv", newNormRasz.TextProizv);
        //                gridView5.SetRowCellValue(newRowHandle, "text_ob", newNormRasz.TextOb);
        //                gridView5.SetRowCellValue(newRowHandle, "text_vyaz", newNormRasz.TextVyaz);

        //                gridView5.UpdateCurrentRow();
        //                _normRaszBindingSource.EndEdit(); // Заканчиваем редактирование


        //                // Сохранение в БД
        //                try
        //                {
        //                    newNormRasz.nrId = await _artNormService.InsertNormRaszAsync(newNormRasz);
        //                    gridView5.SetRowCellValue(newRowHandle, "nrId", newNormRasz.nrId);
        //                    gridView5.UpdateCurrentRow();

        //                //    _normRaszBindingSource.EndEdit(); // Заканчиваем редактирование
        //                    gridControl5.RefreshDataSource();
        //                    gridView5.RefreshData();


        //                    // **Открываем EditForm только после того, как данные обновятся**
        //                    await Task.Delay(100);

        //                    // **Открываем PopupEditForm**
        //                    ////gridView5.FocusedRowHandle = newRowHandle;
        //                    gridView5.FocusedRowHandle = newRowHandle;//gridView5.LocateByValue("nrId", newNormRasz.nrId);
        //                    gridView5.ShowPopupEditForm();
        //                    // Открываем PopupEditForm после полной обработки UI
        //                    //gridControl5.BeginInvoke((Action)(() =>
        //                    //{
        //                    //    int realRowHandle = gridView5.LocateByValue("nrId", newNormRasz.nrId);
        //                    //    if (realRowHandle >= 0)
        //                    //    {
        //                    //        gridView5.FocusedRowHandle = realRowHandle;
        //                    //        gridView5.ShowPopupEditForm();
        //                    //    }
        //                    //}));

        //                }
        //                catch (Exception ex)
        //                {
        //                    await _logger.LogErrorAsync(ex, "Ошибка при сохранении новой записи в NormRasz");
        //                    MessageBox.Show("Ошибка при сохранении в БД. Подробности в логе.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            //// Если форма была закрыта без выбора, удаляем пустую строку
        //            //gridView5.CancelUpdateCurrentRow();
        //            //gridView5.DeleteRow(gridView5.LocateByValue("annId", _newAnnId));
        //            // Удаляем строку, если отменили выбор
        //            int rowToDelete = gridView5.RowCount - 1;
        //            if (rowToDelete >= 0)
        //            {
        //                gridView5.DeleteRow(rowToDelete);
        //            }

        //        }
        //    }
        //}


        private void gridView5_EditFormHidden(object sender, EditFormHiddenEventArgs e)
        {
            this.DialogResult = DialogResult.None;
        }


        #endregion

        #region Norm_rask

        private void customButton2_Click(object sender, EventArgs e)
        {
            AddRaskRow();
            int newRowHandle = gridView2.FocusedRowHandle;

            // Открываем форму выбора данных
            using (NormOperNew selectionForm = new NormOperNew())
            {
                if (selectionForm.ShowDialog() == DialogResult.OK)
                {
                    // Получаем выбранные данные
                    var selectedData = selectionForm.SelectedRowData;
                    if (selectedData != null)
                    {
                        // Заполняем `EditForm` значениями
                        gridView2.SetRowCellValue(newRowHandle, "kod_o", selectedData.KodO);
                        gridView2.SetRowCellValue(newRowHandle, "text", selectedData.Text);
                        gridView2.SetRowCellValue(newRowHandle, "spec", selectedData.Spec);
                        gridView2.SetRowCellValue(newRowHandle, "razryd", selectedData.Razryad);
                        gridView2.SetRowCellValue(newRowHandle, "obor", selectedData.Obor);
                        gridView2.SetRowCellValue(newRowHandle, "kod_proizv", selectedData.KodProizv);
                        
                    }
                }
            }

            // Теперь вызываем EditForm для новой строки
            gridView2.ShowEditForm();

        }

        private void gridView2_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            //AddNewRow();
            //int newRowHandle = gridView1.FocusedRowHandle;

            //// Открываем форму выбора данных
            //using (NormOperNew selectionForm = new NormOperNew())
            //{
            //    if (selectionForm.ShowDialog() == DialogResult.OK)
            //    {
            //        // Получаем выбранные данные
            //        var selectedData = selectionForm.SelectedRowData;
            //        if (selectedData != null)
            //        {
            //            // Заполняем `EditForm` значениями
            //            gridView1.SetRowCellValue(newRowHandle, "kod_o", selectedData.Kod_o);
            //            gridView1.SetRowCellValue(newRowHandle, "text", selectedData.Text);
            //            gridView1.SetRowCellValue(newRowHandle, "spec", selectedData.Spec);
            //            gridView1.SetRowCellValue(newRowHandle, "razryd", selectedData.Razryad);
            //            gridView1.SetRowCellValue(newRowHandle, "obor", selectedData.Obor);
            //            gridView1.SetRowCellValue(newRowHandle, "kod_proizv", selectedData.Kod_proizv);

            //        }
            //    }
            //}

            //// Теперь вызываем EditForm для новой строки
            //gridView1.ShowEditForm();

        }


        private DataTable _raskDataTable;

        private void AddRaskRow()
        {
            if (_raszDataTable == null) return;

            DataRow newRow = _raskDataTable.NewRow();
            //newRow["kod_o"] = "";
            //newRow["text"] = "";
            //newRow["spec"] = "";
            //newRow["razryd"] = 0;
            //newRow["obor"] = "";
            //newRow["kod_proizv"] = "";

            _raskDataTable.Rows.Add(newRow);
            normraskBindingSource.ResetBindings(false); // Обновляем привязку

            // Устанавливаем фокус на новую строку
            gridView2.MoveLast(); // Перемещаемся на последнюю строку
        }
        #endregion

        #region Norm_kont

        #endregion

        #region Norm_dop_obr
        private void gridView4_ShowingEditor(object sender, CancelEventArgs e)
        {

            gridView4.AddNewRow();
            if (gridView4.RowCount >= 2)
            {
                // MessageBox.Show("Вы не можете добавить больше двух строк!", "Ограничение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }
        #endregion

        private async void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK; // Устанавливаем результат
            Close(); // Закрываем окно
        }
    }
}
