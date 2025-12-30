using DevExpress.Data;
using DevExpress.Mvvm.Native;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Newtonsoft.Json;
using SewingProduction.Core.helpers;
using SewingProduction.Extensions;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Features.KnittingProduction.Services;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SewingProduction.Core.helpers.BindingSourceHelper;
using static SewingProduction.Helpers.GridHelper;

namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class PlanZagrVyazCheck : CustomForm, IThemeable
    {
        private static DatabaseHelper _dbHelper;
        private static DbService _dbService;
        private static BulkHelper _bulkHelper;
        private static GridHelper _gridHelper;
        private readonly ILogger _logger = new FileLogger();
        private readonly SprMonthService _sprMonthService;
        private readonly VyazService _vyazService;

        //private CancellationTokenSource? _loadCts;
        private readonly DebouncedLoader _loader = new(delayMs: 300);

        private BindingSource _sprMonthBindingSource;
        private BindingSource _podrVyazBindingSource;


        public PlanZagrVyazCheck()
        {
            InitializeComponent();
            _dbHelper = new DatabaseHelper("ace");
            _dbService = new DbService(_dbHelper);
            _bulkHelper = new BulkHelper();
            _gridHelper = new GridHelper();

            _sprMonthService = new SprMonthService(_dbHelper);
            _vyazService = new VyazService(_dbHelper);

            ThemeManager.UpdateTheme(this);
        }
        private void InitializeBindings()
        {
            _sprMonthBindingSource = new BindingSource
            {
                DataSource = new BindingList<SprMonth>()
            };

            comboBoxMonthList.DataSource = _sprMonthBindingSource;
            comboBoxMonthList.SelectedIndex = -1;
            comboBoxMonthList.ValueMember = "kod";
            comboBoxMonthList.DisplayMember = "name_cl";

            _podrVyazBindingSource = new BindingSource
            {
                DataSource = new BindingList<PodrVyaz>()
            };

            comboBoxPodrVyazList.DataSource = _podrVyazBindingSource;
            comboBoxPodrVyazList.SelectedIndex = -1;
            comboBoxPodrVyazList.ValueMember = "kod_vyaz";
            comboBoxPodrVyazList.DisplayMember = "text_vyaz";
        }
        private Task SetStatusAsync(string text)
            => this.UI(() => labelStatus.Text = text);

        private Task SetLoadingAsync(bool isLoading)
            => this.UI(() =>
            {
                labelStatus.Text = isLoading ? "Загрузка…" : "Готово";
                Cursor = isLoading ? Cursors.WaitCursor : Cursors.Default;
            });

        private async Task LoadSprMonthDataAsync()
        {
            await _loader.RunAsync(
                async token =>
                {
                    await SetLoadingAsync(true);

                    // ❌ НЕ очищаем ComboBox здесь

                    var bs = await _sprMonthService.GetSprMonth(token);

                    await this.UI(() =>
                    {
                        comboBoxMonthList.BeginUpdate();
                        _sprMonthBindingSource.DataSource = bs.DataSource;
                        comboBoxMonthList.EndUpdate();
                    });

                    await SetStatusAsync(bs.Count == 0 ? "Нет данных" : "Готово");
                },
                onError: async ex =>
                {
                    await _logger.LogErrorAsync(ex, "Ошибка загрузки spr_month");
                    await SetStatusAsync("Ошибка загрузки");
                    await SetLoadingAsync(false);
                },
                onCanceled: async byLifetime =>
                {
                    if (!byLifetime)
                        await SetStatusAsync("Отменено");

                    await SetLoadingAsync(false);
                }
            );

            await SetLoadingAsync(false);
        }


        private async Task LoadPodrVyazDataAsync()
        {
            await _loader.RunAsync(
                async token =>
                {
                    await SetLoadingAsync(true);

                    await this.UI(() =>
                    {
                        comboBoxPodrVyazList.BeginUpdate();
                        _podrVyazBindingSource.DataSource = new BindingList<PodrVyaz>();
                        comboBoxPodrVyazList.EndUpdate();
                    });

                    var bs = await _vyazService.GetPodrVyaz(token);

                    await this.UI(() =>
                    {
                        comboBoxPodrVyazList.BeginUpdate();
                        _podrVyazBindingSource.DataSource = bs.DataSource;
                        comboBoxPodrVyazList.EndUpdate();
                    });

                    await SetStatusAsync(bs.Count == 0 ? "Нет данных" : "Готово");
                },
                onError: async ex =>
                {
                    await _logger.LogErrorAsync(ex, "Ошибка загрузки podr_vyaz");
                    await SetStatusAsync("Ошибка загрузки");
                    await SetLoadingAsync(false);
                },
                onCanceled: async byLifetime =>
                {
                    if (!byLifetime)
                        await SetStatusAsync("Отменено");

                    await SetLoadingAsync(false);
                }
            );

            await SetLoadingAsync(false);
        }
        private async void PlanZagrVyazCheck_Load(object sender, EventArgs e)
        {
            InitializeBindings();

            await LoadSprMonthDataAsync();

            comboBoxMonthList.SelectedValue = DateTime.Now.Month;
            spinEditYear.Value = DateTime.Now.Year;

            await LoadPodrVyazDataAsync();
        }

        private async void comboBoxMonthList_SelectedValueChanged(object sender, EventArgs e)
        {
            _loader.CancelUser();          // ⛔ отменяем предыдущие загрузки
            await LoadPodrVyazDataAsync();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _loader.CancelLifetime();
            _loader.Dispose();
            base.OnFormClosed(e);
        }

        private void simpleButtonPrevMonth_Click(object sender, EventArgs e)
        {

        }
    }
}