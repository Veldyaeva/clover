using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Microsoft.IdentityModel.Tokens;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Features.UserDistribution.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.Sprav
{
    public partial class EditTarif : CustomForm
    {
        private readonly DbService _dbService;
        private readonly TarifDataService _tarifService;
        private TarifModel? currentModel;
        UserClass _user;
        public EditTarif(UserClass user) : base(user)
        {
            InitializeComponent();
            var dbHelper = new DatabaseHelper();
            _dbService = new DbService(dbHelper);
            _tarifService = new TarifDataService(_dbService, dbHelper);
            _user = user;
        }
        public EditTarif()
        {
            InitializeComponent();
        }

        private void EditTarif_Shown(object sender, EventArgs e)
        {
            customGroupBoxAdd.VisibleLogic = false;
            customCheckBoxEco.Checked = customCheckBoxEco.Visible;
            customCheckBoxByh.Checked = customCheckBoxByh.Visible;
            customCheckBoxProg.Checked = customCheckBoxProg.Visible;
            Debug.WriteLine(customCheckBoxEco.Visible);
            Debug.WriteLine(customCheckBoxByh.Visible);
            Debug.WriteLine(customCheckBoxProg.Visible);
        }
        private async void customGridControlZp_Load(object sender, EventArgs e)
        {
            var tableTarif = await _tarifService.LoadTarifList();
            customGridControlZp.DataSource = tableTarif;
            customGridControlZp.RefreshDataSource();
            checkPriznSign();
        }
        void checkPriznSign()
        {
            //if (_user.Roles.Any(r => r.Equals("Экономист", StringComparison.OrdinalIgnoreCase)))
            //    customCheckBoxEco.Checked = true;
            //if (_user.Roles.Any(r => r.Equals("Бухгалтер", StringComparison.OrdinalIgnoreCase)))
            //    customCheckBoxByh.Checked = true;
            //if (_user.Roles.Any(r => r.Equals("Администратор", StringComparison.OrdinalIgnoreCase)))
            //    customCheckBoxProg.Checked = true;
            Filter(null, EventArgs.Empty);
        }
        private async void customGridControlTR_Load(object sender, EventArgs e)
        {
            var tableTarifRabot = await _tarifService.LoadTarifRabotList();
            customGridControlTR.DataSource = tableTarifRabot;
            customGridControlTR.RefreshDataSource();
        }
        private async void gridViewZp_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            customGroupBoxAdd.Visible = false;

            currentModel = gridViewZp.GetRow(gridViewZp.FocusedRowHandle) as TarifModel;

            if (currentModel == null)
            {
                customGridControlHistory.DataSource = null;
                return;
            }

            var tableTarifHisory = await _tarifService.LoadHistoryAsync(currentModel.pc_id);
            customGridControlHistory.DataSource = tableTarifHisory;
            customGridControlHistory.RefreshDataSource();
        }
        public void Filter(object sender, EventArgs e)
        {
            List<string> filters = new List<string>();

            // Фильтрация по организации
            if (customRadioButtonMay.Checked)
                filters.Add("[firm] LIKE 'may'");
            else if (customRadioButtonExp.Checked)
                filters.Add("[firm] LIKE 'exp'");
            else if (customRadioButtonAceKle.Checked)
                filters.Add("[firm] LIKE 'ace/cle'");

            // Фильтрация по признаку
            List<string> priznSigns = new List<string>();
            if (customCheckBoxEco.Checked)
                priznSigns.Add("1");
            if (customCheckBoxByh.Checked)
                priznSigns.Add("2");
            if (customCheckBoxProg.Checked)
                priznSigns.Add("3");

            if (priznSigns.Count == 3)
                priznSigns.Clear();
            else if (priznSigns.Count > 0)
                filters.Add($"[priznSign] IN ({string.Join(",", priznSigns)})");
            else
                filters.Add("([priznSign] IS NULL OR [priznSign] = 0)");

            // Применение фильтра
            gridViewZp.ActiveFilterString = string.Join(" AND ", filters);
            UpdatePriznSignCombo();
            customGroupBoxAdd.Visible = false;
        }

        private async void customButtonSave_Click(object sender, EventArgs e)
        {
            if (!proverka()) return;
            bool isEditMode = customGroupBoxAdd.Text == "Редактирование";

            int pcstId = (int)customComboBoxType.SelectedValue;
            string znach = customTextBoxZnach.Text.Trim();

            TarifModel model;
            if (isEditMode && currentModel != null)
            {
                model = currentModel;
            }
            else
            {
                model = new TarifModel();
            }
            model.constant_name = customTextBoxName.Text.Trim();
            model.dimension = customTextBoxRazm.Text.Trim();
            model.describe = customTextBoxOpis.Text.Trim();
            model.pcstId = pcstId;
            model.begin_dt = customDateTimePickerBegin.Value;
            model.firm = customComboBoxOrg.SelectedValue?.ToString();

            var selectedType = customComboBoxType.SelectedItem as TypeItemModel;
            model.typeConst = selectedType.field_name;
            model.store_name = selectedType.store_name;
            model.name_field_id = selectedType.name_field_id;
            model.priznSign = (int)customComboBoxPriznSign.SelectedItem;
            // Установка значения в нужное поле
            try
            {
                switch (pcstId)
                {
                    case 1: model.value_numeric = decimal.Parse(znach.Replace(',', '.'), CultureInfo.InvariantCulture); break;
                    case 2: model.value_integer = int.Parse(znach); break;
                    case 3:
                        MessageBox.Show("Тип 'float' не поддерживается");
                        return;
                    case 4: model.value_character = znach; break;
                    case 5: model.value_datetime = DateTime.Parse(znach); break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка преобразования значения: {ex.Message}");
                return;
            }

            try
            {
                await _tarifService.SaveTarifAsync(model, isEditMode);
                MessageBox.Show("Сохранено успешно.");
                customGroupBoxAdd.Visible = false;

                var updated = await _tarifService.LoadTarifList();
                customGridControlZp.DataSource = updated;
                customGridControlZp.RefreshDataSource();
                int rowHandle = gridViewZp.LocateByValue("pc_id", model.pc_id);
                gridViewZp.FocusedRowHandle = rowHandle;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении: " + ex.Message);
            }
        }
        public bool proverka()
        {
            var requiredFields = new Dictionary<string, (Func<bool> condition, string message)>
            {
                ["Имя константы"] = (() => string.IsNullOrWhiteSpace(customTextBoxName.Text), "Не заполнено поле Имя константы!"),
                ["Размерность"] = (() => string.IsNullOrWhiteSpace(customTextBoxRazm.Text), "Не заполнена размерность! Если неизвестна, поставьте галочку 'Нет размерности'"),
                ["Описание"] = (() => string.IsNullOrWhiteSpace(customTextBoxOpis.Text), "Не заполнено поле Описание!"),
                ["Тип данных"] = (() => string.IsNullOrWhiteSpace(customComboBoxType.Text), "Не выбран тип данных!"),
                ["Значение"] = (() => string.IsNullOrWhiteSpace(customTextBoxZnach.Text), "Не заполнено поле Значение!")
            };
            foreach (var field in requiredFields.Values)
            {
                if (field.condition())
                {
                    MessageBox.Show(field.message);
                    return false;
                }
            }
            return true;
        }

        private async void customButtonAdd_Click(object sender, EventArgs e)
        {
            loadGroupBoxAdd(false);
        }
        private async void customButtonEdit_Click(object sender, EventArgs e)
        {
            if (gridViewZp.FocusedRowHandle < 0 || currentModel == null) return;

            loadGroupBoxAdd(true);
        }

        async Task loadGroupBoxAdd(bool editMode)
        {
            customGroupBoxAdd.Visible = true;
            customGroupBoxAdd.Text = editMode ? "Редактирование" : "Добавление";
            var typeList = await _tarifService.LoadTypeAsync();
            customComboBoxType.DisplayMember = "field_name";
            customComboBoxType.ValueMember = "pcst_id";
            customComboBoxType.DataSource = typeList;

            customTextBoxName.Text = editMode ? currentModel.constant_name : string.Empty;
            customTextBoxRazm.Text = editMode ? currentModel.dimension : string.Empty;
            customTextBoxOpis.Text = editMode ? currentModel.describe : string.Empty;
            customComboBoxType.SelectedValue = editMode ? currentModel.pcstId : string.Empty;
            customDateTimePickerBegin.Value = editMode ? currentModel.begin_dt : DateTime.Now;
            customComboBoxOrg.DataSource = await _tarifService.LoadFirmAsync();
            customComboBoxOrg.SelectedItem = editMode ? currentModel.firm : string.Empty;

            customTextBoxZnach.Text =
                !editMode ? string.Empty :
                currentModel.pcstId == 1 ? currentModel.value_numeric?.ToString("0.#####") :
                currentModel.pcstId == 2 ? currentModel.value_integer?.ToString() :
                currentModel.pcstId == 3 ? currentModel.value_float?.ToString("0.#####") :
                currentModel.pcstId == 4 ? currentModel.value_character :
                currentModel.pcstId == 5 ? currentModel.value_datetime?.ToString("yyyy-MM-dd") :
                string.Empty;

            customTextBoxName.Enabled = !editMode;
            customTextBoxRazm.Enabled = !editMode;
            customCheckBoxNotRazm.Enabled = !editMode;
            customTextBoxOpis.Enabled = !editMode;
            customComboBoxType.Enabled = !editMode;
            customComboBoxOrg.Enabled = !editMode;
            customComboBoxPriznSign.Enabled = !editMode;
            if (editMode)
            {
                // Обновляем список доступных значений в ComboBox
                UpdatePriznSignCombo();

                string itemText =
                    currentModel.priznSign == 1 ? "1 - Экономист" :
                    currentModel.priznSign == 2 ? "2 - Бухгалтер" :
                    currentModel.priznSign == 3 ? "3 - Программист" :
                    "0 - нет";

                // Добавляем item если вдруг отсутствует
                if (!customComboBoxPriznSign.Items.Contains(itemText))
                    customComboBoxPriznSign.Items.Insert(0, itemText);

                // Устанавливаем выбранный элемент
                customComboBoxPriznSign.SelectedItem = itemText;
            }
            else
            {
                // Обновляем список
                UpdatePriznSignCombo();

                // Выбор по умолчанию
                customComboBoxPriznSign.SelectedIndex = 0;
            }

        }
        private void UpdatePriznSignCombo()
        {
            customComboBoxPriznSign.Items.Clear();
            customComboBoxPriznSign.Items.Add("0 - нет"); // <- обязательно

            if (customCheckBoxEco.Checked)
                customComboBoxPriznSign.Items.Add("1 - Экономист");

            if (customCheckBoxByh.Checked)
                customComboBoxPriznSign.Items.Add("2 - Бухгалтер");

            if (customCheckBoxProg.Checked)
                customComboBoxPriznSign.Items.Add("3 - Программист");
        }

        private void customCheckBoxNotRazm_CheckedChanged(object sender, EventArgs e)
        {
            customTextBoxRazm.Text = customCheckBoxNotRazm.Checked ? customCheckBoxNotRazm.Text : "";
        }

        private void customButtonOtm_Click(object sender, EventArgs e)
        {
            customGroupBoxAdd.Visible = false;
        }
        private async void gridViewTR_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle >= 0 && gridViewTR.GetRow(e.RowHandle) is TarifRabotModel model)
            {
                await _dbService.UpdateFieldAsync("sp_ras_rabot", e.Column.FieldName, e.Value, "id_kod_o", model.id_kod_o);
            }
        }

        private void customGridControlTR_Click(object sender, EventArgs e)
        {

        }
    }
}
