using Dapper;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraDiagram.Bars;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSpreadsheet.UI;
using SewingProduction.Core.helpers;
using SewingProduction.Extensions;
using SewingProduction.Features.CuttingProduction.Models;
using SewingProduction.Features.CuttingProduction.Services;
using SewingProduction.Features.Tabel.Models;
using SewingProduction.Features.Tabel.Services;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.Utils.Drawing.Helpers.NativeMethods;
using static SewingProduction.Core.helpers.BindingSourceHelper;

namespace SewingProduction.Features.Tabel.Forms
{
    public partial class SpisokConnect : CustomForm
    {
        private static DatabaseHelper _dbHelper;
        private static DbService _dbService;
        private readonly ILogger _logger = new FileLogger();
        private BindingSource _spisokBindingSource;
        private List<ListForLinking> _currentSpisokData = new List<ListForLinking>();
        private BindingList<ListForLinking> _spisokBindingList;
        private static TabelDataService _tabelDataService;
        public BindingSource _scheduleOfWork;
        public BindingSource _zlPodr;
        public BindingSource _spPodr;
        public BindingSource _spisokNewBindingSource;
        public SpisokConnect()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper();
            _dbService = new DbService(_dbHelper);
            _tabelDataService = new TabelDataService(_dbHelper);
            _spisokNewBindingSource = new BindingSource();
        }
        private async Task InitializeBindingsAsync()
        {
            try
            {
                var spisokViewTask = Task.Run(() =>
                {
                    _spisokBindingList = new BindingList<ListForLinking>();
                    _spisokBindingSource = new BindingSource { DataSource = _spisokBindingList };
                });
                await Task.WhenAll(spisokViewTask);
                #region увязка grid со ListForLinking
                customGridSpisokForLinking.DataSource = _spisokBindingSource;
                gridSpisokInn.FieldName = "inn";
                gridSpisokLastName.FieldName = "firstname";
                gridSpisokMiddleName.FieldName = "lastname";
                gridSpisokFirstName.FieldName = "middlename";
                gridSpisokDateP.FieldName = "date_p";
                gridSpisokDateU.FieldName = "date_u";
                gridSpisokUin.FieldName = "s_uin";
                gridSpisokOrgName.FieldName = "orgName";
                gridSpisokPodr1c.FieldName = "podrName";
                gridSpisokPodr.FieldName = "gr";
                gridColumnTabno.FieldName = "tabno";
                gridSpisokVerif1c.FieldName = "verif1c";
                gridSpisokVerifParsec.FieldName = "verifParsec";
                gridColumnGroupName.FieldName = "nameGroup";
                //var items = await _tabelDataService.GetScheduleOfWorkAsync();
                //_scheduleOfWork = new BindingSource { DataSource = items.ToList() };
                //repositoryItemLookUpEdit1.DataSource = _scheduleOfWork;
                //repositoryItemLookUpEdit1.DisplayMember = "scheduleName";
                //repositoryItemLookUpEdit1.ValueMember = "id";
                var zlPodr = await _tabelDataService.GetZlComboPodrAsync();
                _zlPodr = new BindingSource { DataSource = zlPodr.ToList() };
                repositoryItemLookUpEdit2.DataSource = _zlPodr;
                repositoryItemLookUpEdit2.DisplayMember = "naimen";
                repositoryItemLookUpEdit2.ValueMember = "gr";
                var spPodr = await _tabelDataService.GetSpComboPodrAsync();
                _spPodr = new BindingSource { DataSource = spPodr.ToList() };
                repositoryItemLookUpEdit1.DataSource = _spPodr;
                repositoryItemLookUpEdit1.DisplayMember = "naimen";
                repositoryItemLookUpEdit1.ValueMember = "gr";
                // 
                //repositoryItemComboBox1.Items
                #endregion

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка при инициализации привязок");
                throw;
            }
        }
        private async void SpisokConnect_Load(object sender, EventArgs e)
        {

            Task bindingsTask = InitializeBindingsAsync();
            await Task.WhenAll(bindingsTask);
            try
            {
                await LoadAsyncSpisok();
            }
            catch { }
        }
        private async Task LoadAsyncSpisok()
        {
            _spisokBindingSource.Clear();
            _spisokBindingSource.ResetBindings(false);
            var spisokData = await _tabelDataService.GetListForLinkingsAsync();
            if (spisokData != null)
            {
                await _logger.LogEventAsync($"Получены данные ListForLinking", "LoadCuttingForm");

                await this.InvokeAsync(() =>
                {
                    _currentSpisokData = spisokData;                // Обновляем текущую модель
                    _spisokBindingSource.DataSource = _currentSpisokData; // Привязываем данные к форме
                });

                await _logger.LogEventAsync($"Данные ListForLinking успешно загружены", "LoadVyazPlanDataAsync");
                //LoadList(vyazPlanViewData, _vyazPlanViewBindingList, nameof(NormRasz.nrId));
                //_spisokBindingList.Add(spisokData[0]);
                _spisokBindingSource.ResetBindings(false);

            }
            else
            {
                await _logger.LogEventAsync($"Не удалось найти данные spisok", "GetListForLinkingsAsync");
            }
        }
        private async Task LoadAsyncSpisokNew()
        {
            _spisokNewBindingSource.Clear();
            _spisokNewBindingSource.ResetBindings(false);
            var spisokData = await _tabelDataService.GetListForLinkingsAsync();
            if (spisokData != null)
            {
                await _logger.LogEventAsync($"Получены данные ListForLinking", "LoadCuttingForm");

                await this.InvokeAsync(() =>
                {
                    _currentSpisokData = spisokData;                // Обновляем текущую модель
                    _spisokNewBindingSource.DataSource = _currentSpisokData; // Привязываем данные к форме
                });

                await _logger.LogEventAsync($"Данные ListForLinking успешно загружены", "LoadVyazPlanDataAsync");
                //LoadList(vyazPlanViewData, _vyazPlanViewBindingList, nameof(NormRasz.nrId));
                //_spisokBindingList.Add(spisokData[0]);
                _spisokBindingSource.ResetBindings(false);

            }
            else
            {
                await _logger.LogEventAsync($"Не удалось найти данные spisok", "GetListForLinkingsAsync");
            }
        }

        private void customGridSpisokForLinking_Click(object sender, EventArgs e)
        {

        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            using (var connection = _dbHelper.GetConnection())
            {
                int rowHandle = gridView1.FocusedRowHandle;
                string columnName = e.Column.FieldName;
                if (columnName == "gr")
                {
                    int tabno = (int)gridView1.GetRowCellValue(rowHandle, "tabno");
                    int value = (int)gridView1.GetRowCellValue(rowHandle, columnName);
                    string sql;
                    sql = $"UPDATE zl_spisok SET {columnName} = {value} WHERE tabno = {tabno}";
                    _dbHelper.ExecuteNonQuery(sql, new Dictionary<string, object> { });
                }
            }
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.Clicks == 1)
            {
                OpenChoouseUin(e.RowHandle, e.Column);
            }
        }
        private async void OpenChoouseUin(int rowHandle, GridColumn column)
        {
            try
            {
                if (rowHandle < 0 || column == null) return;
                string lastName = gridView1.GetRowCellValue(rowHandle, "firstname").ToString();
                string firstName = gridView1.GetRowCellValue(rowHandle, "middlename").ToString();
                string middleName = gridView1.GetRowCellValue(rowHandle, "lastname").ToString();
                int valueVerif1c = (int)gridView1.GetRowCellValue(rowHandle, "verif1c");
                int tabno = (int)gridView1.GetRowCellValue(rowHandle, "tabno");
                string naimenPodr = gridView1.GetRowCellValue(rowHandle, "naimen").ToString();
                string nameGroup = gridView1.GetRowCellValue(rowHandle, "nameGroup").ToString();
                if (valueVerif1c == 1)
                {
                    MessageBox.Show("Увязка с 1с не требуется!");
                    //return;
                }
                using (var chooseForm = new ChooseUin(_user,lastName, firstName, middleName, tabno, naimenPodr, nameGroup))
                {
                    //Point mousePosition = Control.MousePosition;
                    //calculator.StartPosition = FormStartPosition.Manual;
                    //calculator.Location = mousePosition;
                    Point cursorPos = Cursor.Position;
                    Point safePosition = CalculateSafePosition(
                        cursorPos,
                        chooseForm.Size);

                    chooseForm.StartPosition = FormStartPosition.Manual;
                    chooseForm.Location = safePosition;
                    if (chooseForm.ShowDialog() == DialogResult.OK)
                    {
                        gridView1.BeginUpdate();
                        _spisokNewBindingSource.Clear();
                        _spisokNewBindingSource = new BindingSource { DataSource = await _tabelDataService.GetListForLinkingsAsync() };
                        var changes = BindingSourceHelper.GetChanges<ListForLinking>(
                          _spisokBindingSource,
                          _spisokNewBindingSource,
                          HashMode.All,
                          keyProperties: new[] { "tabno", "nameGroup" });
                        int indexGrid = gridView1.TopRowIndex;
                        BindingSourceHelper.ApplyChanges<ListForLinking>(
                           _spisokBindingSource,
                           changes,
                           UpdateFieldsMode.All,
                           keyProperties: new[] { "tabno", "nameGroup" },
                           gridView1);
                        gridView1.TopRowIndex = indexGrid;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка!");
                return;
            }
        }
        public static Point CalculateSafePosition(Point desiredLocation, Size formSize)
        {
            // Получаем экран, на котором находится курсор
            Screen screen = Screen.FromPoint(desiredLocation);
            Rectangle workingArea = screen.WorkingArea;

            int x = desiredLocation.X;
            int y = desiredLocation.Y;

            // Проверяем правую границу
            if (x + formSize.Width > workingArea.Right)
            {
                // Не помещается справа - показываем слева от курсора
                x = desiredLocation.X - formSize.Width - 10;

                // Если и слева не помещается, прижимаем к левому краю
                if (x < workingArea.Left)
                {
                    x = workingArea.Left;
                }
            }

            // Проверяем нижнюю границу
            if (y + formSize.Height > workingArea.Bottom)
            {
                // Не помещается снизу - показываем сверху от курсора
                y = desiredLocation.Y - formSize.Height - 10;

                // Если и сверху не помещается, прижимаем к верхнему краю
                if (y < workingArea.Top)
                {
                    y = workingArea.Top;
                }
            }

            // Дополнительная проверка левой и верхней границ
            x = Math.Max(workingArea.Left, x);
            y = Math.Max(workingArea.Top, y);

            return new Point(x, y);
        }

        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "gr")
            {
                GridView view = sender as GridView;
                string condition = view.GetRowCellValue(e.RowHandle, "nameGroup")?.ToString();
                if (condition != null)
                {
                    if (condition == "shp")
                    {
                        e.RepositoryItem = repositoryItemLookUpEdit1;

                    }
                    else if (condition == "zl")
                    {
                        e.RepositoryItem = repositoryItemLookUpEdit2;
                    }
                }
            }
        }

        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {

        }

        private void gridView1_EditFormShowing(object sender, EditFormShowingEventArgs e)
        {
            GridView view = sender as GridView;

            // Получаем текущую строку
            int rowHandle = view.FocusedRowHandle;

            // Проверяем условие для конкретной строки
            if (rowHandle == -1) // Запрещаем редактирование первой строки
            {
                e.Allow = false; // Отменяем открытие редактора
            }

            // Или по значению в строке
            string status = view.GetRowCellValue(rowHandle, "nameGroup")?.ToString();
            if (status == "shp")
            {
                e.Allow = false; // Запрещаем редактирование для закрытых записей
            }
        }
    }
}
