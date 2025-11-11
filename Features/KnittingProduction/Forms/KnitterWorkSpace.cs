using DevExpress.XtraDataLayout;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service;
using SewingProduction.Features.TeamWork.Forms;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.KnittingProduction.Forms
{
    public partial class KnitterWorkSpace : Form
    {
        private readonly IKnitterOrchestrator _orchestrator;
        private readonly BindingSource _planBindingSource = new BindingSource();
        private readonly KnitterPlanPresenter _planPresenter = new KnitterPlanPresenter();

        // Вью для третьего уровня (деталь детальной таблицы)
        private RepositoryItemButtonEdit _pzvDateStartButtonEdit;
        private RepositoryItemTextEdit _pzvDateStartTextEdit;
        private RepositoryItemButtonEdit _pzvDateEndButtonEdit;
        private RepositoryItemTextEdit _pzvDateEndTextEdit;

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
                IKnitterRepository repo = new KnitterRepository(dbHelper);
                _orchestrator = new KnitterOrchestrator(repo, new FileLogger());

            PlanZagrVyazGridControl.DataSource = _planBindingSource;

                if (PlanZagrVyazGridControl.LevelTree.Nodes.Count > 0)
                {
                    var level1 = PlanZagrVyazGridControl.LevelTree.Nodes[0];
                    var level2 = new DevExpress.XtraGrid.GridLevelNode
                    {
                        RelationName = "Operations",
                        LevelTemplate = advBandedGridView1
                    };
                    level1.Nodes.Add(level2);
                }
            this.Load += async (s, e) => await InitializeAsync();

                SetupPzvDateStartColumn();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка инициализации формы: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public KnitterWorkSpace(IKnitterOrchestrator orchestrator)
        {
            try
            {
                InitializeComponent();
                _orchestrator = orchestrator ?? throw new ArgumentNullException(nameof(orchestrator));
                dataLayoutControl1.DataSource = _planBindingSource;
                ConfigureAdvBandedGridColumns();
                PlanZagrVyazGridControl.DataSource = _planBindingSource;
                if (PlanZagrVyazGridControl.LevelTree.Nodes.Count > 0)
                {
                    var level1 = PlanZagrVyazGridControl.LevelTree.Nodes[0];
                    var level2 = new DevExpress.XtraGrid.GridLevelNode
                    {
                        RelationName = "Operations",
                        LevelTemplate = advBandedGridView1
                    };
                    level1.Nodes.Add(level2);
                }
                this.Load += async (s, e) => await InitializeAsync();
                SetupPzvDateStartColumn();
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

                // Жестко выбираем табельный при загрузке формы
                const int defaultTab = 1438;
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

                // По умолчанию табельный номер не выбран — требуется явный выбор пользователем
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка загрузки списка сотрудников: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureAdvBandedGridColumns()
        {
            // Конфигурация колонок задана в Designer.cs
        }

        // Сборка иерархии — вынесено в KnitterPlanPresenter

        // master-detail логика перенесена в KnitterPlanPresenter

        public void LoadData(PlanZagrVyaz data)
        {
            _planBindingSource.DataSource = data;
        }

        private async void FioGridLookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (FioGridLookUpEdit.EditValue == null || !int.TryParse(FioGridLookUpEdit.EditValue.ToString(), out int tab))
                {
                    _planBindingSource.DataSource = null;
                    PlanZagrVyazGridControl.RefreshDataSource();
                    return;
                }

                var plan = await _orchestrator.GetPlanByTabAsync(tab);
                _planPresenter.BindGroupDetails(bandedGridView3, bandedGridView1, advBandedGridView1, _planBindingSource, plan ?? new List<KnitterPZVModel>());
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка загрузки плана: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(FioGridLookUpEdit.EditValue?.ToString(), out int selectedTab) || selectedTab <= 0)
                {
                    XtraMessageBox.Show(this, "Выберите сотрудника для назначения табельного номера.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var focusView = PlanZagrVyazGridControl.FocusedView as DevExpress.XtraGrid.Views.BandedGrid.BandedGridView;
                var rowsForUpdate = _planPresenter.GetRowsForViewSelection(focusView).ToList();
                if (!rowsForUpdate.Any())
                {
                    XtraMessageBox.Show(this, "Выберите строки плана для назначения табельного номера.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var pzvIds = rowsForUpdate
                    .Select(r => r.pzvID)
                    .Where(id => id > 0)
                    .Distinct()
                    .ToList();

                if (pzvIds.Count == 0)
                {
                    XtraMessageBox.Show(this, "Не удалось определить записи плана для обновления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                await _orchestrator.SetPzvTabAsync(pzvIds, selectedTab);

                var refreshedPlan = await _orchestrator.GetPlanByTabAsync(selectedTab);
                _planPresenter.BindGroupDetails(bandedGridView3, bandedGridView1, advBandedGridView1, _planBindingSource, refreshedPlan ?? new List<KnitterPZVModel>(), clearTabs: false);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка при назначении табельного номера: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Получение выбранных строк теперь доступно через _planPresenter.GetRowsForViewSelection(...)

        private void SetupPzvDateStartColumn()
        {
            bandedGridColumn18.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            bandedGridColumn18.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            bandedGridColumn18.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";

            _pzvDateStartButtonEdit = new RepositoryItemButtonEdit { TextEditStyle = TextEditStyles.HideTextEditor };
            _pzvDateStartButtonEdit.Buttons.Clear();
            _pzvDateStartButtonEdit.Buttons.Add(new EditorButton(ButtonPredefines.Glyph, "Проставить дату", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleLeft, null));
            _pzvDateStartButtonEdit.DoubleClick += PzvDateStartButtonEdit_DoubleClick;
            _pzvDateStartButtonEdit.ButtonClick += PzvDateStartButtonEdit_ButtonClick;

            _pzvDateStartTextEdit = new RepositoryItemTextEdit { ReadOnly = true };

            PlanZagrVyazGridControl.RepositoryItems.Add(_pzvDateStartButtonEdit);
            PlanZagrVyazGridControl.RepositoryItems.Add(_pzvDateStartTextEdit);
            BandedGridColumn dateStart = bandedGridColumn18;
             //  advBandedGridView1.CustomRowCellEdit += AdvBandedGridView1_CustomRowCellEdit;
            advBandedGridView1.CustomRowCellEdit += (s, e) =>
            {
                if (e.Column != null && e.Column.FieldName == dateStart.FieldName)
                {
                    var cellValue = e.CellValue;
                    bool isEmpty = cellValue == null ||
                                   cellValue == DBNull.Value ||
                                   (cellValue is DateTime dt && dt == DateTime.MinValue);
                    e.RepositoryItem = isEmpty ? _pzvDateStartButtonEdit : _pzvDateStartTextEdit;
                }
                // Закончено
                if (e.Column != null && e.Column.FieldName == bandedGridColumn19.FieldName)
                {
                    var cellValue = e.CellValue;
                    bool isEmpty = cellValue == null ||
                                   cellValue == DBNull.Value ||
                                   (cellValue is DateTime dt && dt == DateTime.MinValue);
                    e.RepositoryItem = isEmpty ? _pzvDateEndButtonEdit : _pzvDateEndTextEdit;
                }
            };

            // Настройка для "Закончено" (pzvDateEnd)
            bandedGridColumn19.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            bandedGridColumn19.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            bandedGridColumn19.DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";

            _pzvDateEndButtonEdit = new RepositoryItemButtonEdit { TextEditStyle = TextEditStyles.HideTextEditor };
            _pzvDateEndButtonEdit.Buttons.Clear();
            _pzvDateEndButtonEdit.Buttons.Add(new EditorButton(ButtonPredefines.Glyph, "Завершить", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleLeft, null));
            _pzvDateEndButtonEdit.DoubleClick += PzvDateEndButtonEdit_DoubleClick;
            _pzvDateEndButtonEdit.ButtonClick += PzvDateEndButtonEdit_ButtonClick;

            _pzvDateEndTextEdit = new RepositoryItemTextEdit { ReadOnly = true };
            PlanZagrVyazGridControl.RepositoryItems.Add(_pzvDateEndButtonEdit);
            PlanZagrVyazGridControl.RepositoryItems.Add(_pzvDateEndTextEdit);
        }

        private void AdvBandedGridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column != bandedGridColumn18)
                return;

            bool isEmpty = e.CellValue == null || 
                          e.CellValue == DBNull.Value || 
                          (e.CellValue is DateTime dt && dt == DateTime.MinValue);

            e.RepositoryItem = isEmpty ? _pzvDateStartButtonEdit : _pzvDateStartTextEdit;
        }

        private async void PzvDateStartButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            GridView view = PlanZagrVyazGridControl.FocusedView as GridView; 
            await ApplyPzvDateStartAsync(view);
        }

        private async void PzvDateStartButtonEdit_DoubleClick(object sender, EventArgs e)
        {
            GridView view = PlanZagrVyazGridControl.FocusedView as GridView;
            await ApplyPzvDateStartAsync(view);
        }

        private async Task ApplyPzvDateStartAsync(GridView view)//int rowHandle)
        {
            GridView _view = view;
            int rowHandle = _view.FocusedRowHandle;
            KnitterPZVModel row = _view.GetRow(rowHandle) as KnitterPZVModel;
            if (rowHandle < 0 || row.pzvID <= 0) //advBandedGridView1.GetRow(rowHandle) is not KnitterPZVModel row || row.pzvID <= 0)
                return;

            try
            {
                // Сохраняем в БД и применяем дельту без полной перезагрузки
                var updated = await _orchestrator.UpdatePzvDateStartAsync(row.pzvID);
                var newValue = updated?.pzvDateStart ?? row.pzvDateStart;
                row.pzvDateStart = newValue;
                // Мгновенно обновляем UI: записываем значение в ячейку и перерисовываем её
                _view.SetRowCellValue(rowHandle, bandedGridColumn18, newValue);
                _view.UpdateCurrentRow();
                _view.RefreshRowCell(rowHandle, bandedGridColumn18);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка при обновлении даты начала: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void PzvDateEndButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            GridView view = PlanZagrVyazGridControl.FocusedView as GridView;
            await ApplyPzvDateEndAsync(view);
        }

        private async void PzvDateEndButtonEdit_DoubleClick(object sender, EventArgs e)
        {
            GridView view = PlanZagrVyazGridControl.FocusedView as GridView;
            await ApplyPzvDateEndAsync(view);
        }

        private async Task ApplyPzvDateEndAsync(GridView view)
        {
            GridView _view = view;
            int rowHandle = _view.FocusedRowHandle;
            KnitterPZVModel row = _view.GetRow(rowHandle) as KnitterPZVModel;
            if (rowHandle < 0 || row.pzvID <= 0)
                return;

            try
            {
                var updated = await _orchestrator.UpdatePzvDateEndAsync(row.pzvID);
                var newValue = updated?.pzvDateEnd ?? row.pzvDateEnd;
                row.pzvDateEnd = newValue;
                _view.SetRowCellValue(rowHandle, bandedGridColumn19, newValue);
                _view.UpdateCurrentRow();
                _view.RefreshRowCell(rowHandle, bandedGridColumn19);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, $"Ошибка при обновлении даты окончания: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private ExpansionState CaptureExpansionState()
        {
            if (bandedGridView3 == null || bandedGridView3.DataRowCount == 0)
                return ExpansionState.Empty;

            var machines = new HashSet<string>();
            var artNom = new HashSet<(string MachineKey, string ArtKey, int? Nom)>();

            for (int i = 0; i < bandedGridView3.DataRowCount; i++)
            {
                if (!bandedGridView3.GetMasterRowExpanded(i))
                    continue;

                if (bandedGridView3.GetRow(i) is not KnitterPZVModel machineRow)
                    continue;

                var machineKey = NormalizeMachineKey(machineRow.kmlNumber);
                machines.Add(machineKey);

                if (bandedGridView3.GetDetailView(i, 0) is DevExpress.XtraGrid.Views.BandedGrid.BandedGridView detailView)
                {
                    for (int j = 0; j < detailView.DataRowCount; j++)
                    {
                        if (!detailView.GetMasterRowExpanded(j))
                            continue;

                        if (detailView.GetRow(j) is not KnitterPZVModel artRow)
                            continue;

                        artNom.Add((machineKey, NormalizeArtKey(artRow.pzvArticul), artRow.pzvNom));
                    }
                }
            }

            return new ExpansionState(machines, artNom);
        }

        private void RestoreExpansionState(ExpansionState state)
        {
            if (state == null || bandedGridView3 == null)
                return;

            bandedGridView3.BeginUpdate();
            try
            {
                for (int i = 0; i < bandedGridView3.DataRowCount; i++)
                {
                    if (bandedGridView3.GetRow(i) is not KnitterPZVModel machineRow)
                        continue;

                    var machineKey = NormalizeMachineKey(machineRow.kmlNumber);
                    if (!state.MachineKeys.Contains(machineKey))
                        continue;

                    bandedGridView3.SetMasterRowExpanded(i, true);

                    if (bandedGridView3.GetDetailView(i, 0) is not DevExpress.XtraGrid.Views.BandedGrid.BandedGridView detailView)
                        continue;

                    detailView.BeginUpdate();
                    try
                    {
                        for (int j = 0; j < detailView.DataRowCount; j++)
                        {
                            if (detailView.GetRow(j) is not KnitterPZVModel artRow)
                                continue;

                            var artKey = NormalizeArtKey(artRow.pzvArticul);
                            var key = (machineKey, artKey, artRow.pzvNom);
                            if (!state.ArtNomKeys.Contains(key))
                                continue;

                            detailView.SetMasterRowExpanded(j, true);
                        }
                    }
                    finally
                    {
                        detailView.EndUpdate();
                    }
                }
            }
            finally
            {
                bandedGridView3.EndUpdate();
            }
        }

        private sealed class ExpansionState
        {
            public static ExpansionState Empty { get; } = new ExpansionState(new HashSet<string>(), new HashSet<(string MachineKey, string ArtKey, int? Nom)>());

            public ExpansionState(HashSet<string> machineKeys, HashSet<(string MachineKey, string ArtKey, int? Nom)> artNomKeys)
            {
                MachineKeys = machineKeys ?? new HashSet<string>();
                ArtNomKeys = artNomKeys ?? new HashSet<(string MachineKey, string ArtKey, int? Nom)>();
            }

            public HashSet<string> MachineKeys { get; }
            public HashSet<(string MachineKey, string ArtKey, int? Nom)> ArtNomKeys { get; }
        }
 
        private static KnitterPZVModel CreateOperationRow(KnitterPZVModel parent, nrModel nr)
        {
            var operationRow = new KnitterPZVModel
            {
                pzvID = parent.pzvID,
                pzvDivision = parent.pzvDivision,
                pzvMod = parent.pzvMod,
                pzvArticul = parent.pzvArticul,
                pzvKmlID = parent.pzvKmlID,
                kmlNumber = parent.kmlNumber,
                pzvNomZad = parent.pzvNomZad,
                pzvAnnID = parent.pzvAnnID,
                pzvNom = parent.pzvNom,
                pzvKol = parent.pzvKol,
                pzvSek = parent.pzvSek,
                pzvDateStart = parent.pzvDateStart,
                pzvDateEnd = parent.pzvDateEnd,
                pzvKolNazn = parent.pzvKolNazn,
                pzvTab = parent.pzvTab,
                n_pach = parent.n_pach,
                razm = parent.razm,
                sekEd_Effective = parent.sekEd_Effective,
                kol_Effective = parent.kol_Effective,
                nrN = nr?.nrN,
                nrN1 = nr?.nrN1,
                nrText = nr?.nrText,
                nrRazryd = nr?.nrRazryd,
                nrObor = nr?.nrObor,
                nr_kod_ob = nr?.nr_kod_ob,
                nr_kod_proizv = nr?.nr_kod_proizv
            };

            if (nr != null)
            {
                operationRow.nrModels = new BindingList<nrModel>(new List<nrModel>
                {
                    new nrModel
                    {
                        nr_kod_proizv = nr.nr_kod_proizv,
                        nrN = nr.nrN,
                        nrN1 = nr.nrN1,
                        nrRazryd = nr.nrRazryd,
                        nrText = nr.nrText,
                        nrObor = nr.nrObor,
                        nr_kod_ob = nr.nr_kod_ob,
                        kmlNumber = nr.kmlNumber
                    }
                });
            }

            return operationRow;
        }

        private static bool HasRzv(rzvModel r)
        {
            // при LEFT JOIN null уедет в 0; 0 для n_pach/код/кол в реальных данных не используется
            return r.n_pach != 0 || r.rzv_kod != 0 || r.rzv_kol != 0 || !string.IsNullOrEmpty(r.pach_kod) || !string.IsNullOrEmpty(r.razm);
        }

        private static string NormalizeMachineKey(string kmlNumber)
        {
            return string.IsNullOrWhiteSpace(kmlNumber) ? string.Empty : kmlNumber.Trim();
        }

        private static string NormalizeArtKey(string articul)
        {
            return string.IsNullOrWhiteSpace(articul) ? string.Empty : articul.Trim();
        }

        private static bool ContainsNr(BindingList<nrModel> list, nrModel x)
        {
            // Обычно уникальность нормы внутри pzv — комбинация (nrN, nrN1, nr_kod_proizv, nr_kod_ob)
            for (int i = 0; i < list.Count; i++)
            {
                var e = list[i];
                if (e.nrN == x.nrN && e.nrN1 == x.nrN1 && e.nr_kod_proizv == x.nr_kod_proizv && e.nr_kod_ob == x.nr_kod_ob)
                    return true;
            }
            return false;
        }

        private static bool ContainsRzv(BindingList<rzvModel> list, rzvModel x)
        {
            // для раскроя: пачка + код + размер
            for (int i = 0; i < list.Count; i++)
            {
                var e = list[i];
                if (e.n_pach == x.n_pach && e.rzv_kod == x.rzv_kod && string.Equals(e.razm, x.razm, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
          //  var a = Block14Composer.Format(textEdit2.Text);

          //  textEdit3.Text = a.ToString();
        }
    }
}


