using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Xpo.DB.Helpers;
using SewingProduction.Features.Sprav.DataService;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.Sprav.Forms
{
    public partial class EditTarif : CustomForm
    {
        private readonly DbService _dbService;
        private readonly TarifDataService _tarifService;
        private TarifModel? currentModel;
        UserClass _user;
        bool _addMode; // режим добавления
        public EditTarif(UserClass user) : base(user)
        {
            InitializeComponent();
            var dbHelper = new DatabaseHelperSQL();
            _dbService = new DbService(dbHelper);
            _tarifService = new TarifDataService(_dbService, dbHelper);
            _user = user;
        }
        public EditTarif()
        {
            InitializeComponent();
        }

        #region раздел: Гриды
        private void EditTarif_Shown(object sender, EventArgs e)
        {
            customGroupBoxAdd.VisibleLogic = false;
            customCheckBoxEco.Checked = customCheckBoxEco.Visible;
            customCheckBoxByh.Checked = customCheckBoxByh.Visible;
            customCheckBoxProg.Checked = customCheckBoxProg.Visible;
            customButtonEdit.Enabled = false;
        }
        private void customGridControlZp_Load(object sender, EventArgs e)
        {
            tarifLoad();
            customGridControlZp.InitializeAccess(_user, this.Name, new List<string> { "ViewProizvConstants" });
            checkPriznSign();
        }
        async void tarifLoad(int pcId = 0)
        {
            var tableTarif = await _tarifService.LoadTarifList();
            customGridControlZp.DataSource = tableTarif;
            customGridControlZp.RefreshDataSource();
            FocusRowById(pcId);
        }
        private void FocusRowById(int pcId)
        {
            if (pcId <= 0) return;
            int rowHandle = gridViewZp.LocateByValue("pc_id", pcId);
            if (rowHandle >= 0)
            {
                gridViewZp.FocusedRowHandle = rowHandle;
                gridViewZp.MakeRowVisible(rowHandle);
            }
        }

        void checkPriznSign()
        {
            Filter(null, EventArgs.Empty);
        }
        private async void gridViewZp_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (!_addMode)
                    customGroupBoxAdd.Visible = false;

                currentModel = gridViewZp.GetRow(gridViewZp.FocusedRowHandle) as TarifModel;

                if (currentModel == null)
                {
                    customButtonEdit.Enabled = false;
                    customGridControlHistory.DataSource = null;
                    return;
                }

                int byh = currentModel.priznSign / 10;
                int eco = currentModel.priznSign % 10;

                bool isProg = customCheckBoxProg.Checked;
                bool canEdit =
                    isProg ||
                    (customCheckBoxByh.Checked && byh == 2) ||
                    (customCheckBoxEco.Checked && eco == 2);

                customButtonEdit.Enabled = canEdit;

                if (currentModel.pc_id > 0)
                {
                    var tableTarifHistory = await _tarifService.LoadHistoryAsync(currentModel.pc_id);
                    customGridControlHistory.DataSource = tableTarifHistory;
                    customGridControlHistory.RefreshDataSource();
                }
                else
                {
                    customGridControlHistory.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке истории: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Filter(object sender, EventArgs e)
        {
            List<string> filters = new List<string>();
            /*
            * Доступ для бухгалтеров - десятки
            * Для экономистов - еденицы
            * Администратору доступно все
            priznSign:
            0 - нет доступа
            1 - просмотр
            2 - редактор
            */
            bool isByh = customCheckBoxByh.Checked;
            bool isEco = customCheckBoxEco.Checked;
            if (isByh && !isEco)
            {
                // Только бухгалтер
                filters.Add("(([priznSign] / 10) % 10 > 0)");
            }
            else if (isEco && !isByh)
            {
                // Только экономист
                filters.Add("([priznSign] % 10 > 0)");
            }
            else if (isByh && isEco)
            {
                // И бухгалтер, и экономист
                filters.Add("([priznSign] > 0)");
            }
            else
            {
                // Ни один чекбокс не выбран → ничего не показываем
                filters.Add("([priznSign] IS NULL OR [priznSign] = 0)");
            }
            if (customCheckBoxProg.Checked)
                filters.Clear();

            // Фильтрация по организации
            if (customRadioButtonMay.Checked)
                filters.Add("[firm] LIKE 'may'");
            else if (customRadioButtonExp.Checked)
                filters.Add("[firm] LIKE 'exp'");
            else if (customRadioButtonAceKle.Checked)
                filters.Add("[firm] LIKE 'ace/cle'");

            // Применение фильтра
            gridViewZp.ActiveFilterString = string.Join(" AND ", filters);

            if (!_addMode)
                customGroupBoxAdd.Visible = false;
        }
        #endregion
        #region раздел: Редактировать / Сохранить
        private async void customButtonAdd_Click(object sender, EventArgs e)
        {
            _addMode = true;
            await loadGroupBoxAdd(false);
        }
        private async void customButtonEdit_Click(object sender, EventArgs e)
        {
            _addMode = false;
            if (gridViewZp.FocusedRowHandle < 0 || currentModel == null) return;
            await loadGroupBoxAdd(true);
        }
        private async void customButtonCopy_Click(object sender, EventArgs e)
        {
            _addMode = true;
            if (gridViewZp.FocusedRowHandle < 0 || currentModel == null) return;
            await loadGroupBoxAdd(true);
        }
        async Task loadGroupBoxAdd(bool editMode)
        {
            customGroupBoxAdd.Visible = true;
            customGroupBoxAdd.Text = _addMode ? "Добавление" : "Редактирование";

            var typeList = await _tarifService.LoadTypeAsync();
            customComboBoxType.DisplayMember = "field_name";
            customComboBoxType.ValueMember = "pcst_id";
            customComboBoxType.DataSource = typeList;

            customTextBoxName.Text = _addMode ? string.Empty : currentModel.constant_name;
            customTextBoxRazm.Text = editMode ? currentModel.dimension : string.Empty;
            customTextBoxOpis.Text = editMode ? currentModel.describe : string.Empty;
            customComboBoxType.SelectedValue = editMode ? currentModel.pcstId : string.Empty;
            customDateTimePickerBegin.Value = editMode ? currentModel.begin_dt : DateTime.Now;
            customComboBoxOrg.DataSource = await _tarifService.LoadFirmAsync();
            customComboBoxOrg.SelectedItem = editMode ? currentModel.firm : string.Empty;
            customTextBoxWhereUses.Text = editMode ? currentModel.whereUses : string.Empty;

            customCheckBoxNotRazm.Checked = !editMode ? false : // если yt добавляем
                currentModel.dimension.ToLower() == customCheckBoxNotRazm.Text ? true : false; // если "нет размерности"

            customTextBoxZnach.Text =
                !editMode ? string.Empty :
                currentModel.value;

            customCheckBoxArhiv.Checked =
                !editMode ? false :
                currentModel.arhiv;

            LoadPriznSignCombo(editMode);

            if (customCheckBoxProg.Checked)
            {
                editMode = false;
                customTextBoxName.Enabled = true;
            }
            else
                customTextBoxName.Enabled = false;

            if (_addMode)
                editMode = false;

            customTextBoxRazm.Enabled = !editMode;
            customCheckBoxNotRazm.Enabled = !editMode;
            //customTextBoxOpis.Enabled = !editMode;
            customComboBoxType.Enabled = !editMode;
            customComboBoxOrg.Enabled = !editMode;
            customComboBoxPriznEco.Enabled = !editMode;
            customComboBoxPriznByh.Enabled = !editMode;
            //customTextBoxWhereUses.Enabled = !editMode;
        }
        private void LoadPriznSignCombo(bool editMode)
        {
            if (editMode && currentModel != null)
            {
                int byh = currentModel.priznSign / 10;
                int eco = currentModel.priznSign % 10;
                customComboBoxPriznByh.SelectedIndex = byh;
                customComboBoxPriznEco.SelectedIndex = eco;
            }
            else
            {
                if (customCheckBoxByh.Checked)
                    customComboBoxPriznByh.SelectedIndex = 2;
                else
                    customComboBoxPriznByh.SelectedIndex = 0;
                if (customCheckBoxEco.Checked)
                    customComboBoxPriznEco.SelectedIndex = 2;
                else
                    customComboBoxPriznEco.SelectedIndex = 0;
            }

        }

        private void customCheckBoxNotRazm_CheckedChanged(object sender, EventArgs e)
        {
            customTextBoxRazm.Text = customCheckBoxNotRazm.Checked ? customCheckBoxNotRazm.Text : "";
        }

        private void customButtonOtm_Click(object sender, EventArgs e)
        {
            customGroupBoxAdd.Visible = false;
            _addMode = false;
        }

        private async void customButtonArhiv_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите добавить в архив " + currentModel.describe,
                                                   "Подтверждение",
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int targetId = currentModel.pc_id;
                await _tarifService.ArhivTarifAsync(targetId);
                tarifLoad(targetId);
            }
        }

        private void customButtonExcel_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog { Filter = "Excel (*.xlsx)|*.xlsx", FileName = "Константы.xlsx" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                customGridControlZp.ExportToXlsx(sfd.FileName);
                Process.Start(new ProcessStartInfo { FileName = sfd.FileName, UseShellExecute = true });
            }
        }
        #endregion
        #region Сохранение тарифа
        private void customButtonSave_Click(object sender, EventArgs e)
        {
            if (!proverka()) return;
            bool isEditMode = customGroupBoxAdd.Text == "Редактирование";
            int pcstId = (int)customComboBoxType.SelectedValue;

            TarifModel model;
            if (isEditMode && currentModel != null)
                model = currentModel;
            else
                model = new TarifModel();

            model.constant_name = customTextBoxName.Text.Trim();
            model.dimension = customTextBoxRazm.Text.Trim();
            model.describe = customTextBoxOpis.Text.Trim();
            model.pcstId = pcstId;
            model.begin_dt = customDateTimePickerBegin.Value;
            model.firm = customComboBoxOrg.SelectedValue?.ToString();

            var selectedType = customComboBoxType.SelectedItem as TypeItemModel;
            model.typeConst = selectedType.type_n;
            model.store_name = selectedType.store_name;
            model.name_field_id = selectedType.name_field_id;

            int eco = int.Parse(customComboBoxPriznEco.SelectedItem.ToString().Substring(0, 1));
            int byh = int.Parse(customComboBoxPriznByh.SelectedItem.ToString().Substring(0, 1));
            model.priznSign = byh * 10 + eco;

            model.whereUses = customTextBoxWhereUses.Text.Trim();
            model.arhiv = customCheckBoxArhiv.Checked;

            // Установка значения в нужное поле
            selectZnach(model);
            saveModel(model, isEditMode);

        }
        public void selectZnach(TarifModel model)
        {
            string znach = customTextBoxZnach.Text.Trim();
            try
            {
                switch (model.pcstId)
                {
                    case 1: model.value_numeric = decimal.Parse(znach.Replace(',', '.'), CultureInfo.InvariantCulture); break;
                    case 2: model.value_integer = int.Parse(znach); break;
                    case 3:
                        MessageBox.Show("Тип 'float' не поддерживается");
                        return;
                    case 4: model.value_character = znach; break;
                    case 5:
                        {
                            var formats = new[] { "dd.MM.yyyy", "dd/MM/yyyy" };
                            model.value_datetime = DateTime.Parse(znach);
                            if (DateTime.TryParseExact(
                            znach,
                            formats,
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out DateTime dt))
                            {
                                model.value_datetime = dt.Date;
                            }
                            else
                            {
                                MessageBox.Show("Неверный формат даты. Введите: dd.MM.yyyy");
                                return;
                            }
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка преобразования значения: {ex.Message}");
                return;
            }
        }
        public void saveModel(TarifModel model, bool isEditMode)
        {
            try
            {
                // _tarifService.SaveTarif(model, isEditMode, _user.UserId);
                _tarifService.SaveTarifJson(model, isEditMode, _user.UserId);
                MessageBox.Show("Сохранено успешно.");
                customGroupBoxAdd.Visible = false;

                tarifLoad(model.pc_id);
                System.Windows.Forms.Application.DoEvents(); //задержка
                int rowHandle = gridViewZp.LocateByValue("constant_name", model.constant_name);
                if (_addMode)
                    rowHandle = gridViewZp.DataRowCount - 1;
                gridViewZp.FocusedRowHandle = rowHandle;
                _addMode = false;
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
                //["Имя константы"] = (() => string.IsNullOrWhiteSpace(customTextBoxName.Text), "Не заполнено поле Имя константы!"),
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
        #endregion
        #region раздел: Тарифы разовых работ
        private TarifRabotModel _currentTarif = null;

        private async void customGridControlTR_Load(object sender, EventArgs e)
        {
            var tableTarifRabot = await _tarifService.LoadTarifRabotList();
            customGridControlTR.DataSource = tableTarifRabot;
            customGridControlTR.RefreshDataSource();
        }

        private async void gridViewTR_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle >= 0 && gridViewTR.GetRow(e.RowHandle) is TarifRabotModel model)
            {
                await _dbService.UpdateFieldAsync("sp_ras_rabot", e.Column.FieldName, e.Value, "id_kod_o", model.id_kod_o);
            }
        }
        private async void customButtonAddTrr_Click(object sender, EventArgs e)
        {
            TarifRabotModel model = new TarifRabotModel { Text = "Новый тариф" };
            await _dbService.SaveEntityAsync("sp_ras_rabot", "id_kod_o", model);

            var tableTarifRabot = await _tarifService.LoadTarifRabotList();
            customGridControlTR.DataSource = tableTarifRabot;
            customGridControlTR.RefreshDataSource();

            gridViewTR.FocusedRowHandle = gridViewTR.GetRowHandle(gridViewTR.DataRowCount - 1);
            gridViewTR.MakeRowVisible(gridViewTR.FocusedRowHandle);

            gridViewTR.GridControl.BeginInvoke(new Action(() =>
            {
                if (gridViewTR.IsValidRowHandle(gridViewTR.FocusedRowHandle))
                    gridViewTR.ShowPopupEditForm();
            }));

        }
        #endregion

    }
}
