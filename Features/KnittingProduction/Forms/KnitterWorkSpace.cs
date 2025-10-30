using DevExpress.XtraDataLayout;
using DevExpress.XtraEditors;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using SewingProduction.Features.CardByNom.Services;

namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class KnitterWorkSpace : Form
    {
        private readonly KnitterOrchestrator _orchestrator;
        private readonly BindingSource _planBindingSource = new BindingSource();

        // Поля для группировки мастер-деталь
        private List<KnitterPZVModel> _allRows;
        private Dictionary<(string? nomzad, int? ann), List<KnitterPZVModel>> _byGroup;
        // Вью для третьего уровня (деталь детальной таблицы)
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView2;

        /// <summary>
        /// Инициализирует форму рабочего места вязальщика, настраивает источники данных и события.
        /// </summary>
        public KnitterWorkSpace()
        {
            try
            {
                InitializeComponent();
                dataLayoutControl1.DataSource = _planBindingSource;

                ConfigureAdvBandedGridColumns();

                //    dataLayoutControl1.RetrieveFields(new RetrieveFieldsParameters
                //    {
                //        DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
                //    });

                var dbHelper = new DatabaseHelper();
                var repo = new KnitterRepository(dbHelper);
                _orchestrator = new KnitterOrchestrator(repo, new FileLogger());

                PlanZagrVyazGridControl.DataSource = _planBindingSource;

                // === Конфигурация третьего уровня (по art+№рассчёта) ===
                bandedGridView2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView(PlanZagrVyazGridControl);
                bandedGridView2.Name = "bandedGridView2";
                bandedGridView2.GridControl = PlanZagrVyazGridControl;
                bandedGridView2.OptionsDetail.EnableMasterViewMode = false;

                var band2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand { Caption = "Операции (Art+Nom)" };
                var colN = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn { Caption = "n", FieldName = "N", Visible = true };
                var colN1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn { Caption = "n1", FieldName = "N1", Visible = true };
                var colText = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn { Caption = "наименование операции", FieldName = "nrText", Visible = true };
                band2.Columns.Add(colN);
                band2.Columns.Add(colN1);
                band2.Columns.Add(colText);
                bandedGridView2.Bands.Add(band2);

                PlanZagrVyazGridControl.ViewCollection.Add(bandedGridView2);

                // Уровни вложенности: 1-й (Items) уже задан в Designer, добавим 2-й (ArtNom) внутрь него
                if (PlanZagrVyazGridControl.LevelTree.Nodes.Count > 0)
                {
                    var level1 = PlanZagrVyazGridControl.LevelTree.Nodes[0];
                    var level2 = new DevExpress.XtraGrid.GridLevelNode
                    {
                        RelationName = "ArtNom",
                        LevelTemplate = bandedGridView2
                    };
                    level1.Nodes.Add(level2);
                }
                this.Load += async (s, e) => await InitializeAsync();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка инициализации формы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Первичная инициализация: загрузка справочника ФИО, установка дефолтного табеля (999) и автозагрузка плана.
        /// </summary>
        private async Task InitializeAsync()
        {
            try
            {
                // Заполняем список ФИО
                List<FioModel> fioList = await _orchestrator.GetFioListAsync();
                fioList ??= new List<FioModel>();

                // Жестко выбираем табельный 999 при загрузке формы
                const int defaultTab = 0;
                bool hasDefault = fioList.Any(f => f.Tab == defaultTab);
                if (!hasDefault)
                {
                    string defaultFio = await _orchestrator.GetFioByTabAsync(defaultTab);
                    if (!string.IsNullOrWhiteSpace(defaultFio))
                    {
                        fioList.Insert(0, new FioModel { Tab = defaultTab, Fio = defaultFio });
                    }
                }

                FioGridLookUpEdit.Properties.DisplayMember = nameof(FioModel.Fio);
                FioGridLookUpEdit.Properties.ValueMember = nameof(FioModel.Tab);
                FioGridLookUpEdit.Properties.DataSource = fioList;

                if (fioList.Count > 0)
                {
                    FioGridLookUpEdit.EditValue = fioList.Any(f => f.Tab == defaultTab)
                        ? defaultTab
                        : fioList[0].Tab;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка загрузки списка сотрудников: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Конфигурирует соответствие колонок грида полям модели <see cref="KnitterPZVModel"/>.
        /// </summary>
        private void ConfigureAdvBandedGridColumns()
        {
            // Map columns to KnitterPZVModel properties
            //bandedGridColumn1.FieldName = nameof(KnitterPZVModel.pzvID);
            //bandedGridColumn1.Caption = "ID";

            //bandedGridColumn2.FieldName = nameof(KnitterPZVModel.pzvDivision);
            //bandedGridColumn2.Caption = "Подразделение";

            //bandedGridColumn3.FieldName = nameof(KnitterPZVModel.pzvMod);
            //bandedGridColumn3.Caption = "Модель";

            //bandedGridColumn4.FieldName = nameof(KnitterPZVModel.pzvArticul);
            //bandedGridColumn4.Caption = "Артикул";

            //bandedGridColumn5.FieldName = nameof(KnitterPZVModel.pzvKol);
            //bandedGridColumn5.Caption = "Кол-во";

            //bandedGridColumn6.FieldName = nameof(KnitterPZVModel.pzvDateStart);
            //bandedGridColumn6.Caption = "Начало";
            //bandedGridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //bandedGridColumn6.DisplayFormat.FormatString = "g";

            //bandedGridColumn7.FieldName = nameof(KnitterPZVModel.pzvDateEnd);
            //bandedGridColumn7.Caption = "Окончание";
            //bandedGridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //bandedGridColumn7.DisplayFormat.FormatString = "g";

            //// Hide unused column if present
            //bandedGridColumn8.Visible = false;
        }

        /// <summary>
        /// Реализует группировку для мастер-деталь вью по ключу (pzvNomZad, pzvAnnID).
        /// В мастере показываются только заголовки групп, в деталях - все строки группы.
        /// </summary>
        private void BindGroupDetails(List<KnitterPZVModel> rows)
        {
            _allRows = rows ?? new List<KnitterPZVModel>();
            _byGroup = _allRows
                .GroupBy(r => ((string?)r.pzvNomZad, (int?)r.pzvAnnID))
                .ToDictionary(g => g.Key, g => g.ToList());

            // В мастере показываем только «заголовки» групп
            var masterData = _byGroup.Select(kv => kv.Value.First()).ToList();
            _planBindingSource.DataSource = masterData;
            PlanZagrVyazGridControl.RefreshDataSource();

            // Настройка событий для мастер-деталь вью
            var master = gridView1;

            // Подписка на события (удаляем старые подписки если есть)
            master.MasterRowGetRelationCount -= Master_MasterRowGetRelationCount;
            master.MasterRowGetRelationName -= Master_MasterRowGetRelationName;
            master.MasterRowGetChildList -= Master_MasterRowGetChildList;

            // Подписываемся заново
            master.MasterRowGetRelationCount += Master_MasterRowGetRelationCount;
            master.MasterRowGetRelationName += Master_MasterRowGetRelationName;
            master.MasterRowGetChildList += Master_MasterRowGetChildList;

            master.OptionsDetail.EnableMasterViewMode = true;
            master.OptionsDetail.AllowOnlyOneMasterRowExpanded = false; // Разрешаем раскрытие нескольких строк
                                                                        //для бОльшей производительности лучше запрещать

            // Настройка второго уровня (деталь детальной таблицы)
            var detail = advBandedGridView1;
            detail.MasterRowGetRelationCount -= Detail_MasterRowGetRelationCount;
            detail.MasterRowGetRelationName -= Detail_MasterRowGetRelationName;
            detail.MasterRowGetChildList -= Detail_MasterRowGetChildList;

            detail.MasterRowGetRelationCount += Detail_MasterRowGetRelationCount;
            detail.MasterRowGetRelationName += Detail_MasterRowGetRelationName;
            detail.MasterRowGetChildList += Detail_MasterRowGetChildList;
        }

        private void Master_MasterRowGetRelationCount(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationCountEventArgs e)
        {
            e.RelationCount = 1;
        }

        private void Master_MasterRowGetRelationName(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventArgs e)
        {
            // e.RelationName = "Items";
            e.RelationName = "ArtNom";
        }

        private void Master_MasterRowGetChildList(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetChildListEventArgs e)
        {
            var head = (KnitterPZVModel)gridView1.GetRow(e.RowHandle);
            if (head != null)
            {
                var key = ((string?)head.pzvNomZad, (int?)head.pzvAnnID);
                // Возвращаем все строки группы (включая операции) из исходного набора данных
                e.ChildList = _byGroup.TryGetValue(key, out var list) ? list : new List<KnitterPZVModel>();
            }
        }

        // === Вложенный уровень: по art + №рассчёта ===
        private void Detail_MasterRowGetRelationCount(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationCountEventArgs e)
        {
            e.RelationCount = 1;
        }

        private void Detail_MasterRowGetRelationName(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventArgs e)
        {
            // e.RelationName = "ArtNom";
            e.RelationName = "Items";
        }

        private void Detail_MasterRowGetChildList(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetChildListEventArgs e)
        {
            try
            {
                var parentView = (DevExpress.XtraGrid.Views.BandedGrid.BandedGridView)sender;
                var head = (KnitterPZVModel)parentView.GetRow(e.RowHandle);
                if (head == null)
                {
                    e.ChildList = new List<KnitterPZVModel>();
                    return;
                }

                // Фильтруем по арт+№рассчёта в пределах всего набора
                var art = head.pzvArticul;
                var nom = head.pzvNom;
                var filtered = _allRows?.Where(r => r.pzvArticul == art && r.pzvNom == nom).ToList() ?? new List<KnitterPZVModel>();
                e.ChildList = filtered;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка загрузки плана: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadData(PlanZagrVyaz data)
        {
            _planBindingSource.DataSource = data;
        }

        /// <summary>
        /// Обработчик смены выбранного сотрудника: подгружает план по табельному номеру и отображает в гриде.
        /// </summary>
        private async void FioGridLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (FioGridLookUpEdit.EditValue == null)
                {
                    _planBindingSource.DataSource = null;
                    PlanZagrVyazGridControl.RefreshDataSource();
                    return;
                }

                if (!int.TryParse(FioGridLookUpEdit.EditValue.ToString(), out int tab))
                {
                    return;
                }

                List<KnitterPZVModel> plan = await _orchestrator.GetPlanByTabAsync(tab);

                // Реализуем группировку для мастер-деталь вью
                BindGroupDetails(plan ?? new List<KnitterPZVModel>());
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка загрузки плана: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var a = Block14Composer.Format(textEdit2.Text);

            textEdit3.Text = a.ToString();
        }
    }
}


