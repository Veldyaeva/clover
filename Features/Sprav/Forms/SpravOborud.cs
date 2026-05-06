
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using Microsoft.Extensions.DependencyInjection;
using SewingProduction.Core;
using SewingProduction.Core.interfaces;
using SewingProduction.Core.services;
using SewingProduction.Features.Sprav.DataService;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;


namespace SewingProduction.form
{
    public partial class SpravOborud : CustomForm, IDataUpdatableForm
    {
        private readonly SpravOborudDataService _spravOborudDataService;
        private readonly IAppServiceBrokerHub _sbHub;
        private readonly string _sbHubOwnerId = $"SpravOborud:{Guid.NewGuid():N}";
        private CancellationTokenSource? _sbLifetimeCts;
        int currentRowIndex = 0;//текущий индекс
        int topRowIndex = 0;//верхний индекс 
        //если добавили поле в таблицу:
        bool flagAddDown = false;
        bool flagStartListening = false;
        public SpravOborud(UserClass user) : base(user)
        {
            InitializeComponent();
            DatabaseHelperSQL dbHelper = new DatabaseHelperSQL();
            _spravOborudDataService = new SpravOborudDataService(dbHelper);
            _sbHub = AppServices.Services?.GetService<IAppServiceBrokerHub>() ?? new AppServiceBrokerHub();
           // ThemeManager.UpdateTheme(this);
        }

        private void SpravOborud_Load(object sender, EventArgs e)
        {
            label4.Text = "Группа оборуд-я (для учета \n в цехе, компетенций)";
            label7.Text = "Группа оборуд-я (для учета \n в цехе, компетенций)";
            label21.Text = "Спец. оборудование \n для оказания услуг";
            label22.Text = "Спец. оборудование \n для оказания услуг";
        }

        #region service broker
        // Интерфейс доступный сервис брокеру:
        public interface IDataUpdatableForm
        {
            void UpdateDataInForm();
        }
        // Процедура, которая вызывается из брокера при поступлении обновления?
        public void UpdateDataInForm(string _table)
        {
            LoadData();
        }

        #endregion

        // Загрузка / обновление данных:
        private void LoadData()
        {
            oborudList.DataSource = _spravOborudDataService.GetSpOborudShv(checkEditArhiv.Checked);
            GridView gridView = oborudGrid.MainView as GridView;
            //Запрет на редактирование
            gridView.OptionsBehavior.Editable = false;
            // Если он открыт
            if (gridView != null)
            {
                // Создаем экземпляр CheckEdit
                RepositoryItemCheckEdit checkEdit = new RepositoryItemCheckEdit
                {
                    ValueChecked = 1,   // Значение для отмеченной галочки
                    ValueUnchecked = 0   // Значение для неотмеченной галочки
                };
                // Назначаем его столбцам
                gridView.Columns["show_for_plan"].ColumnEdit = checkEdit;
                gridView.Columns["spec_ob"].ColumnEdit = checkEdit;
                gridView.Columns["arhiv"].ColumnEdit = checkEdit;
                //gridView.Columns["pokaz"].OptionsColumn.AllowEdit = false; // Запрещаем редактирование

                gridView.OptionsView.ShowGroupPanel = false; // Панель группировки отображается
                gridView.GroupPanelText = ""; // Текст
                gridView.OptionsFind.AlwaysVisible = true; // Всегда показывать панель поиска
            }
        }

        // Загрузка таблицы:
        private async void oborudGrid_Load(object sender, EventArgs e)
        {
            LoadData();
            if (!flagStartListening)
            {
                var tableFields = new Dictionary<string, IReadOnlyCollection<string>>(StringComparer.OrdinalIgnoreCase)
                {
                    ["dbo.spoborudshv"] = new[]
                    {
                        "kod_ob","text_ob","text_ob_s","ko_ob_all","spec_ob","nastav","arhiv","no_spec",
                        "pokaz_sp","id_class","show_for_plan","vid_shp","vid_vzp","vid_np","vid_rz"
                    }
                };
                await _sbHub.SubscribeAsync(
                    ownerId: _sbHubOwnerId,
                    ownerName: GetType().Name,
                    tableFields: tableFields,
                    onTableChangedAsync: async (table, changed) =>
                    {
                        if (IsDisposed || Disposing)
                            return;
                        if (InvokeRequired)
                        {
                            BeginInvoke(new Action(() => UpdateDataInForm(table)));
                            return;
                        }
                        UpdateDataInForm(table);
                        await Task.CompletedTask;
                    },
                    ct: GetServiceBrokerLifetimeToken());
                flagStartListening = true;
            }
        }



        private bool GetCheckBoxValue(string columnName, GridView gridViewGet)
        {
            // Получаем текущее выделенное значение в указанной ячейке столбца
            object value = gridViewGet.GetFocusedRowCellValue(columnName);
            // Проверяем, если значение равно 1 (отмеченная галочка)
            return value != null && value.Equals(1); // Вернуть true, если галочка отмечена, иначе false
        }

        // Кнопка Редактировать
        private void simpleButtonRed_Click(object sender, EventArgs e)
        {
            // Переключаем видимость вкладки
            AddTab.TabPages[0].PageVisible = false;
            AddTab.TabPages[1].PageVisible = true;
            // Получаем доступ к GridView
            GridView gridView = oborudGrid.MainView as GridView;
            // Получаем текущую выделенную строку в текстбокси и др
            textBoxRedKod.Text = gridView.GetFocusedRowCellValue("kod_ob").ToString();
            textBoxRedName.Text = gridView.GetFocusedRowCellValue("text_ob").ToString().Trim();
            textBoxRedSokrName.Text = gridView.GetFocusedRowCellValue("text_ob_s").ToString().Trim();
            // Заполняем комбобоксы:
            _spravOborudDataService.GetOborudShvOb(comboBoxRedGrup);
            _spravOborudDataService.GetSpOborudMachine(comboBoxRedVidm);
            _spravOborudDataService.GetMatrix_class(comboBoxRedClass);
            comboBoxRedGrup.Text = gridView.GetFocusedRowCellValue("text_ob_tip") != DBNull.Value ? gridView.GetFocusedRowCellValue("text_ob_tip").ToString() : "";
            comboBoxRedVidm.Text = gridView.GetFocusedRowCellValue("vidm") != DBNull.Value ? gridView.GetFocusedRowCellValue("vidm").ToString() : "";
            comboBoxRedClass.Text = gridView.GetFocusedRowCellValue("idClass") != DBNull.Value ? gridView.GetFocusedRowCellValue("idClass").ToString() : "";
            //comboBoxRedNastav.Text = gridView.GetFocusedRowCellValue("nastav") != DBNull.Value ? gridView.GetFocusedRowCellValue("nastav").ToString() : "";
            if (gridView.GetFocusedRowCellValue("nastav") != DBNull.Value)
                comboBoxRedNastav.Text = gridView.GetFocusedRowCellValue("nastav").ToString();
            else comboBoxRedNastav.SelectedIndex = -1;
            // comboBox group for proizv:
            comboBoxRedShp.Text = gridView.GetFocusedRowCellValue("vid_shp") != DBNull.Value ? gridView.GetFocusedRowCellValue("vid_shp").ToString() : "нет";
            comboBoxRedVzp.Text = gridView.GetFocusedRowCellValue("vid_vzp") != DBNull.Value ? gridView.GetFocusedRowCellValue("vid_vzp").ToString() : "нет";
            comboBoxRedNp.Text = gridView.GetFocusedRowCellValue("vid_np") != DBNull.Value ? gridView.GetFocusedRowCellValue("vid_np").ToString() : "нет";
            comboBoxRedRz.Text = gridView.GetFocusedRowCellValue("vid_rz") != DBNull.Value ? gridView.GetFocusedRowCellValue("vid_rz").ToString() : "нет";
            //checkBox:
            checkBoxRedShow.Checked = GetCheckBoxValue("show_for_plan", gridView);
            checkBoxRedSpec.Checked = GetCheckBoxValue("spec_ob", gridView);
            checkBoxRedArhiv.Checked = GetCheckBoxValue("arhiv", gridView);
        }

        // Кнопка Добавить
        private void simpleButtonAdd_Click(object sender, EventArgs e)
        {
            AddTab.TabPages[0].PageVisible = true;
            AddTab.TabPages[1].PageVisible = false;
            textBoxAddKod.Text = (_spravOborudDataService.GetLastId() + 1).ToString();
            textBoxAddName.Text = "";
            textBoxAddSokrName.Text = "";
            // Заполняем комбобоксы:
            _spravOborudDataService.GetOborudShvOb(comboBoxAddGrup);
            _spravOborudDataService.GetSpOborudMachine(comboBoxAddVidm);
            _spravOborudDataService.GetMatrix_class(comboBoxAddClass);
            comboBoxAddGrup.Text = "прочее";
            comboBoxAddVidm.Text = "Другое";
            comboBoxAddClass.Text = "";
            comboBoxAddNastav.Text = "";
            //Вид произв:
            comboBoxAddShp.Text = "нет";
            comboBoxAddVzp.Text = "нет";
            comboBoxAddNp.Text = "нет";
            comboBoxAddRz.Text = "нет";
            //скрыть и тд:
            checkBoxAddShow.Checked = false;
            checkBoxAddSpec.Checked = false;
            checkBoxAddArhiv.Checked = false;
        }

        // Кнопка Отменить на вкладке Редактировать
        private void simpleButtonRedOtm_Click(object sender, EventArgs e)
        {
            // Переключаем видимость вкладки
            AddTab.TabPages[1].PageVisible = false;
            textBoxRedKod.Text = "";
            textBoxRedName.Text = "";
            textBoxRedSokrName.Text = "";
            comboBoxRedGrup.Text = "";
            comboBoxRedVidm.Text = "";
            comboBoxRedClass.Text = "";
            comboBoxRedNastav.Text = "";
            checkBoxRedShow.Checked = false;
            checkBoxRedSpec.Checked = false;
            checkBoxRedArhiv.Checked = false;
        }

        // Кнопка Отменить на вкладке Добавить
        private void simpleButtonAddOtm_Click(object sender, EventArgs e)
        {
            // Переключаем видимость вкладки
            AddTab.TabPages[0].PageVisible = false;
        }
        // Проверка заполения полей
        string proverkaZap(TextBox proverkaKod, TextBox proverkaName, TextBox proverkaSokrName, ComboBox proverkaGrup,
                        ComboBox proverkaShp, ComboBox proverkaVzp, ComboBox proverkaNp, ComboBox proverkaRz)
        {
            if (proverkaName.Text == "")
                return "Заполните поле 'Вид оборудования'!";
            if (proverkaSokrName.Text == "")
                return "Заполните поле 'Cокращенное наименование'!";
            if (proverkaGrup.Text == "")
                return "Заполните поле 'Группа оборудования'!";
            if (proverkaShp.Text == "нет" && proverkaVzp.Text == "нет" && proverkaNp.Text == "нет" && proverkaRz.Text == "нет")
                return "Выберите вид производства!";
            if (proverkaKod.Text == "0" || proverkaKod.Text == "")
                return "Ошибка, связанная с кодом записи, перезапустите программу и попробуйте снова.";
            return "OK";
        }
        // Кнопка Сохранить на вкладке Редактировать
        private void simpleButtonRedSave_Click(object sender, EventArgs e)
        {
            string proverka = proverkaZap(textBoxRedKod, textBoxRedName, textBoxRedSokrName, comboBoxRedGrup,
                comboBoxRedShp, comboBoxRedVzp, comboBoxRedNp, comboBoxRedRz);
            if (proverka == "OK")
            {
                currentRowIndex = gridView1.FocusedRowHandle;
                _spravOborudDataService.UpdateSpOborudShv(textBoxRedName.Text, textBoxRedSokrName.Text, comboBoxRedClass.Text,
                    comboBoxRedShp.SelectedIndex, comboBoxRedVzp.SelectedIndex, comboBoxRedNp.SelectedIndex, comboBoxRedRz.SelectedIndex,
                    comboBoxRedNastav.SelectedIndex, checkBoxRedShow.Checked, checkBoxRedSpec.Checked, checkBoxRedArhiv.Checked,
                    textBoxRedKod.Text, comboBoxRedGrup.Text, comboBoxRedVidm.Text);
                AddTab.TabPages[1].PageVisible = false;
            }
            else MessageBox.Show(proverka, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Кнопка Сохранить на вкладке Добавить
        private void simpleButtonAddSave_Click(object sender, EventArgs e)
        {
            string proverka = proverkaZap(textBoxAddName, textBoxAddName, textBoxAddSokrName, comboBoxAddGrup,
                comboBoxAddShp, comboBoxAddVzp, comboBoxAddNp, comboBoxAddRz);
            if (proverka == "OK")
            {
                _spravOborudDataService.InsertSpOborudShv(textBoxAddName.Text, textBoxAddSokrName.Text, comboBoxAddClass.Text,
                    comboBoxAddShp.SelectedIndex, comboBoxAddVzp.SelectedIndex, comboBoxAddNp.SelectedIndex, comboBoxAddRz.SelectedIndex,
                    comboBoxAddNastav.SelectedIndex, checkBoxAddShow.Checked, checkBoxAddSpec.Checked, checkBoxAddArhiv.Checked,
                    textBoxAddKod.Text, comboBoxAddGrup.Text, comboBoxAddVidm.Text,
                    comboBoxAddClass.Text, comboBoxAddGrup.Text, comboBoxAddVidm.Text, textBoxAddKod.Text);
                // Флаг для перехода вниз
                flagAddDown = true;
                // Закрыть вкладку
                AddTab.TabPages[0].PageVisible = false;
            }
            else MessageBox.Show(proverka, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Кнопка Архив
        private void simpleButtonArhiv_Click(object sender, EventArgs e)
        {
            GridView gridView = oborudGrid.MainView as GridView;
            // Сохраняем индекс строки
            currentRowIndex = gridView.FocusedRowHandle;
            // Получаем данные из ячейки
            string textObArh = gridView.GetFocusedRowCellValue("text_ob").ToString();
            int kodObArh = Convert.ToInt32(gridView.GetFocusedRowCellValue("kod_ob"));
            string message = "Вы уверены что хотите занести '" + textObArh + "' в архив?";
            var result = MessageBox.Show(message, "В архив?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                _spravOborudDataService.SetArhiv(kodObArh);
            }
        }
        private void oborudGrid_Click(object sender, EventArgs e)
        {
            GridView gridView = oborudGrid.MainView as GridView;
            // Сохраняем индекс строки
            currentRowIndex = gridView.FocusedRowHandle;
            // Отменяем если редакитруем:
            simpleButtonRedOtm_Click(sender, e);
        }

        private void checkEditArhiv_CheckedChanged(object sender, EventArgs e)
        {
            LoadData();
            GridView gridView = oborudGrid.MainView as GridView;
            //gridView.Columns["arhiv"].Visible = !gridView.Columns["arhiv"].Visible;
            //перенос столбца архив в конец:
            gridView.Columns["arhiv"].VisibleIndex = -(gridView.Columns["arhiv"].VisibleIndex - (gridView.Columns.Count - 2));
        }
        private void SpravOborud_FormClosing(object sender, FormClosingEventArgs e)
        {
            ShutdownServiceBroker();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            try
            {
                ShutdownServiceBroker();
            }
            finally
            {
                base.OnFormClosed(e);
            }
        }

        private void ShutdownServiceBroker()
        {
            try { _sbLifetimeCts?.Cancel(); } catch { }
            try { _sbHub.UnsubscribeAsync(_sbHubOwnerId).GetAwaiter().GetResult(); } catch { }
            try { _sbLifetimeCts?.Dispose(); } catch { }
            _sbLifetimeCts = null;
            flagStartListening = false;
        }

        private CancellationToken GetServiceBrokerLifetimeToken()
        {
            _sbLifetimeCts ??= new CancellationTokenSource();
            return _sbLifetimeCts.Token;
        }

    }
}
