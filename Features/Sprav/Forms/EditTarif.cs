using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Microsoft.IdentityModel.Tokens;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Features.UserDistribution.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.Sprav
{
    public partial class EditTarif : Form
    {
        private readonly TarifDataService _tarifService;
        public EditTarif(UserClass user)
        {
            InitializeComponent();
            var dbHelper = new DatabaseHelper();
            var dbService = new DbService(dbHelper);
            _tarifService = new TarifDataService(dbService, dbHelper);
        }

        private async void customGridControlZp_Load(object sender, EventArgs e)
        {
            var tableTarif = await _tarifService.LoadTarifList();
            customGridControlZp.DataSource = tableTarif;
            customGridControlZp.RefreshDataSource();
        }

        private async void customGridControlTR_Load(object sender, EventArgs e)
        {
            var tableTarif = await _tarifService.LoadTarifRabotList();
            customGridControlTR.DataSource = tableTarif;
            customGridControlTR.RefreshDataSource();
        }
        public void Filter(object sender, EventArgs e)
        {
            string filter = string.Empty;
            if (customRadioButtonAll.Checked == true)
            {
                gridViewZp.ActiveFilterString = string.Empty;
                return;
            }
            if (customRadioButtonMay.Checked == true)
            {
                filter += "[firm] like 'may'";
            }
            else if (customRadioButtonExp.Checked == true)
            {
                filter += "[firm] like 'exp'";
            }
            else if (customRadioButtonAceKle.Checked == true)
            {
                filter += "[firm] like 'ace/cle'";
            }
            gridViewZp.ActiveFilterString = filter;
        }

        private async void customButtonSave_Click(object sender, EventArgs e)
        {
            if (!proverka()) return;

            int pcstId = (int)customComboBoxType.SelectedValue;
            string znach = customTextBoxZnach.Text.Trim();

            var model = new TarifModel
            {
                constant_name = customTextBoxName.Text.Trim(),
                dimens = customTextBoxRazm.Text.Trim(),
                describe = customTextBoxOpis.Text.Trim(),
                pcstId = pcstId,
                begin_dt = customDateTimePickerBegin.Value,
                firm = customComboBoxOrg.SelectedValue?.ToString(),
                typeConst = customComboBoxType.Text, 
                nameTable = "proizv_constant_stor",
                nameField = "pscdt_id"
            };

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
                await _tarifService.SaveTarifAsync(model);
                MessageBox.Show("Сохранено успешно.");
                customGroupBoxAdd.Visible = false;

                var updated = await _tarifService.LoadTarifList();
                customGridControlZp.DataSource = updated;
                customGridControlZp.RefreshDataSource();
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
            customGroupBoxAdd.Visible = true;
            customComboBoxOrg.DataSource = await _tarifService.LoadFirmAsync();
            var typeList = await _tarifService.LoadTypeAsync();
            customComboBoxType.DisplayMember = "field_name";
            customComboBoxType.ValueMember = "pcst_id";
            customComboBoxType.DataSource = typeList;

        }

        private void customCheckBoxNotRazm_CheckedChanged(object sender, EventArgs e)
        {
            customTextBoxRazm.Text = customCheckBoxNotRazm.Checked ? customCheckBoxNotRazm.Text : "";
        }

        private void customButtonOtm_Click(object sender, EventArgs e)
        {
            customGroupBoxAdd.Visible = false;
        }
    }



}
