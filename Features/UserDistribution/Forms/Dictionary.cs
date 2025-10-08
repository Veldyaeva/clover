using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Features.UserDistribution.Models;
using SewingProduction.form;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.UserDistribution.Forms
{
    public partial class Dictionary : CustomForm
    {
        private readonly AllTableNameDataService _tableService;
        private readonly AllColumnNameDataService _columnService;
        private List<AllTableNameModel> _tables;
        private List<AllColumnNameModel> _columns;
        public Dictionary(UserClass user) : base(user)
        {
            InitializeComponent();
            gridViewTable.FocusedRowChanged += gridViewTable_FocusedRowChanged;
            var dbHelper = new DatabaseHelper();
            var dbService = new DbService(dbHelper);
            _tableService = new AllTableNameDataService(dbService, dbHelper);
            _columnService = new AllColumnNameDataService(dbService, dbHelper);

            customGridControlTable.DataSource = bindingSourceTable;
            customGridControlColumn.DataSource = bindingSourceColumn;
        }

        private async void customGridControlTable_Load(object sender, EventArgs e)
        {
            await LoadTablesAsync();
        }

        private async Task LoadTablesAsync()
        {
            _tables = await _tableService.GetListTableAsync();
            bindingSourceTable.DataSource = _tables;
        }

        private async Task LoadColumnsFromSelectedTableAsync()
        {
            if (bindingSourceTable.Current is AllTableNameModel selectedTable)
            {
                _columns = await _columnService.GetListColumnFromTable(selectedTable.id_atn);
                bindingSourceColumn.DataSource = _columns;
            }
        }
        private async void gridViewTable_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            await LoadColumnsFromSelectedTableAsync();
        }

        private async void customGridControlTable_Click(object sender, EventArgs e)
        {
            await LoadColumnsFromSelectedTableAsync();
        }

        private async void customButtonAddTable_Click(object sender, EventArgs e)
        {
            string tableName = customTextBoxAddTable.Text.Trim();
            if (string.IsNullOrWhiteSpace(tableName))
            {
                MessageBox.Show("Введите название таблицы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int count = await _tableService.CheckAsync(tableName);

            if (count == 0)
            {
                MessageBox.Show($"Таблица '{tableName}' не найдена в базе данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int id_atn;
                var existing = _tables.FirstOrDefault(t => t.name.Equals(tableName, StringComparison.OrdinalIgnoreCase));

                if (existing != null)
                {
                    // ничего не меняем в name_rus
                    id_atn = existing.id_atn;

                    MessageBox.Show("Такая таблица уже существует. Добавим только недостающие столбцы.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var newTable = new AllTableNameModel
                    {
                        name = tableName,
                        name_rus = tableName
                    };
                    id_atn = await _tableService.SaveAsync(newTable);
                }

                await _columnService.InsertOnlyNewColumnsFromInformationSchema(tableName, id_atn);

                await LoadTablesAsync();
                bindingSourceTable.Position = _tables.FindIndex(t => t.id_atn == id_atn);
                await LoadColumnsFromSelectedTableAsync();

                MessageBox.Show("Таблица обработана успешно.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении таблицы: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void customButtonOpen_Click(object sender, EventArgs e)
        {
            if (bindingSourceTable.Current is not AllTableNameModel selectedTable)
            {
                MessageBox.Show("Сначала выберите таблицу из списка.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.MdiParent is SpMainForm mainForm)
            {
                var form = new SpravForAll(selectedTable.name, rusNameTableSQL: selectedTable.name_rus);
                mainForm.OpenForm(form, sender);
            }
        }


        private async void customButtonDeleteTable_Click(object sender, EventArgs e)
        {
            if (bindingSourceTable.Current is not AllTableNameModel selectedTable)
            {
                MessageBox.Show("Выберите таблицу для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Удалить таблицу '{selectedTable.name}' и все её столбцы?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                // Удалить все колонки таблицы
                var columns = await _columnService.GetListColumnFromTable(selectedTable.id_atn);
                foreach (var column in columns)
                    await _columnService.DeleteAsync(column);

                // Удалить таблицу
                await _tableService.DeleteAsync(selectedTable);

                await LoadTablesAsync();
                bindingSourceColumn.DataSource = null;
                MessageBox.Show("Таблица и её столбцы удалены.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void customButtonDeleteColumn_Click(object sender, EventArgs e)
        {
            if (bindingSourceColumn.Current is not AllColumnNameModel selectedColumn)
            {
                MessageBox.Show("Выберите столбец для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Удалить столбец '{selectedColumn.name}'?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                await _columnService.DeleteAsync(selectedColumn);
                await LoadColumnsFromSelectedTableAsync();
                MessageBox.Show("Столбец удалён.", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при удалении столбца: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void gridViewTable_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            if (e.Row is AllTableNameModel updatedTable)
            {
                await _tableService.SaveAsync(updatedTable);
                await LoadTablesAsync();
            }
        }

        private async void gridViewColumn_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            if (e.Row is AllColumnNameModel updatedColumn)
            {
                await _columnService.SaveAsync(updatedColumn);
                await LoadColumnsFromSelectedTableAsync();
            }
        }

        private async void customButtonAddButton_Click(object sender, EventArgs e)
        {
            if (bindingSourceTable.Current is not AllTableNameModel selectedTable)
                return;
            await _columnService.InsertButtonForSprav(selectedTable.id_atn);
            await LoadColumnsFromSelectedTableAsync();
        }
    }

}
