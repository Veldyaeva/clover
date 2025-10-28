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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class KnitterWorkSpace : Form
    {
        private readonly KnitterOrchestrator _orchestrator;
        private readonly BindingSource _planBindingSource = new BindingSource();
        
        // Поля для группировки мастер-деталь
        private List<KnitterPZVModel> _allRows;
        private Dictionary<(string? nomzad, int? ann), List<KnitterPZVModel>> _byGroup;
        
        /// <summary>
        /// Инициализирует форму рабочего места вязальщика, настраивает источники данных и события.
        /// </summary>
        public KnitterWorkSpace()
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
            this.Load += async (s, e) => await InitializeAsync();
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
        }
        
        private void Master_MasterRowGetRelationCount(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationCountEventArgs e)
        {
            e.RelationCount = 1;
        }

        private void Master_MasterRowGetRelationName(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventArgs e)
        {
            e.RelationName = "Items";
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

                List<KnitterPZVModel> plan = await _orchestrator.GetPlanByTabAsync(0);//(tab);
                
                // Реализуем группировку для мастер-деталь вью
                BindGroupDetails(plan ?? new List<KnitterPZVModel>());
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка загрузки плана: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
