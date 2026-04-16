using System;
using System.Collections.Generic;
using System.ComponentModel; // EventHandlerList
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Features.UserDistribution.DataService;
using SewingProduction.Features.UserDistribution.Forms; // AllRoleDataService
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;

namespace SewingProduction.Features.UserDistribution
{
    public partial class AllDistribution : CustomForm // форма создана Сhat GPT
    {
        private readonly AllRoleDataService _data;   // готовый сервис из AllRole.cs
        private readonly DatabaseHelperSQL _db = new DatabaseHelperSQL();
        private readonly UserClass _user;
        private readonly UserModelDataService _userModelDataService;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox _repoMode;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox _repoFormMode;

        private int _roleId = -1;
        private int _formId = -1;

        public AllDistribution(UserClass user) : base(user)
        {
            InitializeComponent();
            _user = user;
            _data = new AllRoleDataService();
            SetupUiBehavior();
        }
        public AllDistribution()
        {
            InitializeComponent();
        }

        #region Конструкторы и загрузка
        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadRolesAsync();
        }
        /// <summary>
        /// Загрузка ролей, доступных текущему пользователю (как в AllRole.GetRoles).
        /// </summary>
        private async Task LoadRolesAsync()
        {
            try
            {
                // Тот же метод, что и в AllRole: учитывает доступность ролей по объектам:contentReference[oaicite:4]{index=4}
                DataTable roles = await _data.GetRoles(_user.UserId);
                customGridControlRoles.DataSource = roles;        // без BindingSource — прямо в GridControl
                gridViewRoles.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке ролей:\n{ex.Message}", "AllDistribution");
            }
        }

        private async Task LoadFormsForSelectedRoleAsync(bool preserveSelection)
        {
            try
            {
                var idObj = gridViewRoles.GetFocusedRowCellValue("RoleID");
                _roleId = (idObj != null && idObj != DBNull.Value) ? Convert.ToInt32(idObj) : 0;
                if (_roleId <= 0) return;

                // запомним выбранную форму (и верхнюю строку для “липкой” прокрутки)
                int prevFormId = _formId;
                int prevTopRow = gridViewForms.TopRowIndex;

                DataTable forms = await _data.GetFormsForRoles(_roleId, _user.UserId);
                customGridControlForms.DataSource = forms;
                gridViewForms.BestFitColumns();

                if (preserveSelection && prevFormId > 0)
                {
                    int handle = FindRowHandleByValue(gridViewForms, "ProjectFormsID", prevFormId);
                    if (handle >= 0)
                    {
                        gridViewForms.FocusedRowHandle = handle;
                        gridViewForms.TopRowIndex = prevTopRow;
                    }
                }
                EnsureFormModeEditor();
                await LoadObjectsForSelectedFormAsync(preserveSelection);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке форм:\n{ex.Message}", "AllDistribution");
            }
        }
        private async Task LoadObjectsForSelectedFormAsync(bool preserveSelection)
        {
            try
            {
                if (_roleId <= 0 || gridViewForms.FocusedRowHandle < 0) return;

                var idObj = gridViewForms.GetFocusedRowCellValue("ProjectFormsID");
                _formId = (idObj != null && idObj != DBNull.Value) ? Convert.ToInt32(idObj) : 0;
                if (_formId <= 0) return;

                int prevObjectId = -1;
                int prevTopRow = gridViewObject.TopRowIndex;
                var prevObjectIdVal = gridViewObject.GetFocusedRowCellValue("ObjectID");
                if (prevObjectIdVal != null && prevObjectIdVal != DBNull.Value)
                    prevObjectId = Convert.ToInt32(prevObjectIdVal);

                DataTable objects = await _data.GetObjectsForFormRoles(_roleId, _formId, _user.UserId);
                customGridControlObject.DataSource = objects;
                gridViewObject.BestFitColumns();

                if (preserveSelection && prevObjectId > 0)
                {
                    int handle = FindRowHandleByValue(gridViewObject, "ObjectID", prevObjectId);
                    if (handle >= 0)
                    {
                        gridViewObject.FocusedRowHandle = handle;
                        gridViewObject.TopRowIndex = prevTopRow;
                    }
                }
                EnsureObjectModeEditor();
                ApplyToPreviewIfReady();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке объектов:\n{ex.Message}", "AllDistribution");
            }
        }

        #endregion

        #region Настройка UI
        /// <summary>
        /// Все подписки и режим "только просмотр".
        /// </summary>
        private void SetupUiBehavior()
        {
            // --- Мастер-деталь "Роль -> Пользователи" внутри customGridControlRoles ---
            gridViewRoles.MasterRowGetRelationCount += (s, e) => e.RelationCount = 1;
            gridViewRoles.MasterRowGetRelationName += (s, e) => e.RelationName = "Пользователи";
            gridViewRoles.MasterRowGetChildList += async (s, e) =>
            {
                var view = (GridView)s;
                var row = view.GetDataRow(e.RowHandle);
                if (row == null) { e.ChildList = null; return; }

                int roleId = row["RoleID"] != DBNull.Value ? Convert.ToInt32(row["RoleID"]) : 0;

                DataTable allUsers = await _data.GetUsersWithRolesInfo(roleId, _user.CreatorID);
                var onlyOwners = allUsers.Clone();
                foreach (var r in allUsers.Select("HasRole = true"))
                    onlyOwners.ImportRow(r);

                e.ChildList = onlyOwners.DefaultView;
            };

            // --- выбор роли / формы ---
            gridViewRoles.FocusedRowChanged += async (s, e) => await LoadFormsForSelectedRoleAsync(preserveSelection: true);
            gridViewForms.FocusedRowChanged += async (s, e) =>
            {
                await LoadObjectsForSelectedFormAsync(preserveSelection: true);
                PreviewSelectedForm();               // показать превью выбранной формы
                ApplyToPreviewIfReady();             // и сразу применить права, если превью уже на месте
            };
            gridViewForms.CellValueChanged += async (s, e) =>
            {
                if (e.Column?.FieldName == "HasAccess")
                    await OnFormAccessChangedAsync();
            };

            // --- события для живого применения прав ---
            gridViewObject.CellValueChanged += async (s, e) =>
            {
                if (e.Column?.FieldName == "HasAccessObject")
                    await OnObjectAccessChangedAsync(e.RowHandle);
            };
            gridViewForms.CellValueChanged += (s, e) =>
            {
                if (e.Column?.FieldName == "HasAccess")
                    ApplyToPreviewIfReady();
            };

            // --- фиксация значения сразу после выбора пункта в комбобоксе --- 
            gridViewForms.ShownEditor += (s, e) =>
            {
                if (gridViewForms.ActiveEditor is DevExpress.XtraEditors.ComboBoxEdit editor)
                {
                    editor.SelectedIndexChanged += (sender2, e2) =>
                    {
                        gridViewForms.CloseEditor();
                        gridViewForms.UpdateCurrentRow();
                    };
                }
            };

            gridViewObject.ShownEditor += (s, e) =>
            {
                if (gridViewObject.ActiveEditor is DevExpress.XtraEditors.ComboBoxEdit editor)
                {
                    editor.SelectedIndexChanged += (sender2, e2) =>
                    {
                        gridViewObject.CloseEditor();
                        gridViewObject.UpdateCurrentRow();
                    };
                }
            };

            // --- Режимы редактирования: весь грид в ReadOnly, но колонку HasAccessObject делаем редактируемой (через комбобокс) ---
            MakeReadOnly(gridViewRoles);
            MakeReadOnly(gridViewUsers);
            MakeReadOnly(gridViewForms);

            // для gridViewObject включим редактирование точечно в методе настройки колонок
            gridViewObject.OptionsBehavior.Editable = true;
            gridViewObject.OptionsBehavior.ReadOnly = false;
            gridViewObject.OptionsView.ShowGroupPanel = false;

            // ------ формы ------
            gridViewForms.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Inplace;
            gridViewForms.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;

            // ------ объекты ------
            gridViewObject.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Inplace;
            gridViewObject.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;
        }
        private void EnsureFormModeEditor()
        {
            if (_repoFormMode == null)
            {
                _repoFormMode = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox
                {
                    TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor, // запрет ввода
                    ImmediatePopup = true, // сразу раскрывать
                    ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.SingleClick // один клик
                };
                _repoFormMode.Items.AddRange(new[] { "Нет доступа", "Просмотр", "Редактор" });
                customGridControlForms.RepositoryItems.Add(_repoFormMode);
            }

            var colMode = gridViewForms.Columns.ColumnByFieldName("HasAccess");
            if (colMode != null)
            {
                colMode.OptionsColumn.AllowEdit = true;
                colMode.OptionsColumn.ReadOnly = false;
                colMode.ColumnEdit = _repoFormMode;
            }

            foreach (DevExpress.XtraGrid.Columns.GridColumn c in gridViewForms.Columns)
                if (c != colMode) { c.OptionsColumn.AllowEdit = false; c.OptionsColumn.ReadOnly = true; }
        }

        private void EnsureObjectModeEditor()
        {
            if (_repoMode == null)
            {
                _repoMode = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox
                {
                    TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor,
                    ImmediatePopup = true,
                    ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.SingleClick
                };
                _repoMode.Items.AddRange(new[] { "Нет доступа", "Просмотр", "Редактор" });
                customGridControlObject.RepositoryItems.Add(_repoMode);
            }

            var colMode = gridViewObject.Columns.ColumnByFieldName("HasAccessObject");
            if (colMode != null)
            {
                colMode.OptionsColumn.AllowEdit = true;
                colMode.OptionsColumn.ReadOnly = false;
                colMode.ColumnEdit = _repoMode;
            }

            foreach (DevExpress.XtraGrid.Columns.GridColumn c in gridViewObject.Columns)
                if (c != colMode) { c.OptionsColumn.AllowEdit = false; c.OptionsColumn.ReadOnly = true; }
        }
        #endregion

        #region Работа с правами
        /// <summary>
        /// При изменении прав формы — обновляем права всех объектов этой формы.
        /// </summary>
        private async Task OnFormAccessChangedAsync()
        {
            try
            {
                if (_roleId <= 0 || gridViewForms.FocusedRowHandle < 0)
                    return;

                var formIdObj = gridViewForms.GetFocusedRowCellValue("ProjectFormsID");
                if (formIdObj == null || formIdObj == DBNull.Value)
                    return;
                int formId = Convert.ToInt32(formIdObj);

                string accessText = gridViewForms.GetFocusedRowCellValue("HasAccess")?.ToString() ?? "Нет доступа";
                int mode = accessText switch
                {
                    "Просмотр" => 1,
                    "Редактор" => 2,
                    _ => 0
                };

                // Получаем все объекты формы
                DataTable objects = await _data.GetObjectsForFormRoles(_roleId, formId, _user.UserId);

                // Обновляем права у всех объектов
                foreach (DataRow obj in objects.Rows)
                {
                    int objectId = Convert.ToInt32(obj["ObjectID"]);
                    await _data.UpdateRoleObjectMode(_roleId, objectId, mode);
                }

                // Обновляем отображение в gridViewObject
                for (int i = 0; i < gridViewObject.RowCount; i++)
                    gridViewObject.SetRowCellValue(i, "HasAccessObject", accessText);

                ApplyToPreviewIfReady();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении прав формы:\r\n{ex.Message}",
                    "AllDistribution", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// При изменении прав объекта — сохраняем в БД.
        /// </summary>
        private async Task OnObjectAccessChangedAsync(int rowHandle)
        {
            try
            {
                if (_roleId <= 0 || rowHandle < 0) return;

                int objectId = Convert.ToInt32(gridViewObject.GetRowCellValue(rowHandle, "ObjectID"));
                string access = gridViewObject.GetRowCellValue(rowHandle, "HasAccessObject")?.ToString() ?? "Нет доступа";

                int mode = access switch
                {
                    "Просмотр" => 1,
                    "Редактор" => 2,
                    _ => 0
                };

                await _data.UpdateRoleObjectMode(_roleId, objectId, mode);
                ApplyToPreviewIfReady();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении прав объекта:\r\n{ex.Message}",
                    "AllDistribution", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Превью формы и клики
        private void PreviewSelectedForm()
        {
            if (gridViewForms.FocusedRowHandle < 0)
            {
                panelControl1?.Controls.Clear();
                return;
            }

            var nameForm = gridViewForms.GetFocusedRowCellValue("NameForm")?.ToString();
            try
            {
                // твой резолвер: сначала ctor(UserClass), потом пустой ctor()
                var form = CreateFormInstance_UserOrEmpty(nameForm);
                if (form == null)
                {
                    ShowErrorInPanel($"Форма \"{nameForm}\": нет ctor(UserClass) и пустого ctor(), либо это MDI-контейнер.");
                    return;
                }

                // превью-режим для наследников CustomForm (гасит тяжёлую базовую логику)
                if (form is CustomForm cf)
                    cf.IsPreview = true;

                // по желанию: инжект «затычек», если нужно (DataService/ServiceBroker)
                InjectPreviewDefaults(form);

                // по желанию: снять Load/Shown у формы и всех детей (если хочешь полностью глушить загрузки)
                SuppressLoadAndShownHandlersDeep(form);

                // безопасный показ (ловит исключения из Load/Shown)
                ShowFormInPanel(form);

                // если форма показалась — накладываем права из gridViewObject
                if (_previewForm != null && !_previewForm.IsDisposed)
                    ApplyGridPermissionsToFormControls(_previewForm);

                HookPreviewClickHandlers(form);
            }
            catch (Exception ex)
            {
                ShowErrorInPanel($"Ошибка предпросмотра формы \"{nameForm}\":\r\n{ex.Message}");
            }
        }
        private void ShowFormInPanel(Form form)
        {
            if (panelControl1 == null || form == null) return;

            // закрыть предыдущую
            if (_previewForm != null && !_previewForm.IsDisposed)
            {
                try { _previewForm.Close(); _previewForm.Dispose(); } catch { /* ignore */ }
                _previewForm = null;
            }

            // нельзя MDI
            if (form.IsMdiContainer)
            {
                ShowErrorInPanel($"Форму \"{form.GetType().Name}\" нельзя встроить (MDI-контейнер).");
                form.Dispose();
                return;
            }

            try { form.MdiParent = null; } catch { /* ignore */ }
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            panelControl1.Controls.Clear();
            panelControl1.Controls.Add(form);

            try
            {
                // ВАЖНО: если исключение возникает в Load, оно попадёт сюда и будет поймано
                form.Show();
                _previewForm = form;
            }
            catch (Exception ex)
            {
                // показать сообщение и не уронить приложение
                ShowErrorInPanel($"Ошибка при показе формы \"{form.GetType().Name}\":\r\n{ex.Message}");
                try { form.Dispose(); } catch { /* ignore */ }
            }
        }
        /// <summary>
        /// Подключает перехват кликов на все контролы формы в режиме предпросмотра.
        /// </summary>
        private void HookPreviewClickHandlers(Form form)
        {
            if (form == null) return;

            void Hook(Control c)
            {
                // отключаем стандартные клики
                c.Click -= PreviewControl_Click;
                DisableOriginalClickHandlers(c);
                c.Click += PreviewControl_Click;

                foreach (Control child in c.Controls)
                    Hook(child);
            }

            Hook(form);
        }

        /// <summary>
        /// Обработчик клика по контролу в превью.
        /// </summary>
        private void PreviewControl_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender is Control ctrl)
                {
                    string objName = ResolveObjectName(ctrl); // используем тот же метод, что при применении прав
                    if (string.IsNullOrEmpty(objName))
                        return;

                    // Находим строку в gridViewObject
                    for (int i = 0; i < gridViewObject.RowCount; i++)
                    {
                        var name = gridViewObject.GetRowCellValue(i, "ObjectName")?.ToString();
                        if (string.Equals(name, objName, StringComparison.OrdinalIgnoreCase))
                        {
                            gridViewObject.FocusedRowHandle = i;
                            gridViewObject.SelectRow(i);
                            return;
                        }
                    }

                    // Если не нашли — сообщаем
                    MessageBox.Show($"Объект \"{objName}\" отсутствует в списке объектов формы.",
                        "Предпросмотр прав", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выборе объекта:\n{ex.Message}",
                    "Предпросмотр прав", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Вспомогательные методы
        /// <summary>
        /// Извлекаем "имя объекта" для сопоставления: свойство ObjectName (если есть), иначе Name.
        /// </summary>
        private string ResolveObjectName(Control ctrl)
        {
            // Многие кастомные контролы в проекте имеют свойство ObjectName (см. инструкцию по ролям):contentReference[oaicite:3]{index=3}
            var prop = ctrl.GetType().GetProperty("ObjectName", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (prop != null && prop.PropertyType == typeof(string))
            {
                var v = prop.GetValue(ctrl) as string;
                if (!string.IsNullOrEmpty(v)) return v;
            }
            return ctrl.Name;
        }

        /// <summary>
        /// Применить режим к контролу: 0=скрыть, 1=видим, но Disabled, 2=полный доступ.
        /// </summary>
        private void ApplyModeToControl(Control ctrl, int mode)
        {
            switch (mode)
            {
                case 0: // Нет доступа
                    ctrl.Visible = false;
                    break;
                case 1: // Просмотр
                    ctrl.Visible = true;
                    ctrl.Enabled = false;
                    break;
                case 2: // Редактор
                    ctrl.Visible = true;
                    ctrl.Enabled = true;
                    break;
                default:
                    ctrl.Visible = true;
                    ctrl.Enabled = true;
                    break;
            }
        }

        /// <summary>
        /// Рекурсивно применяет права ко всем контролам формы согласно словарю objectName->mode.
        /// Контролы, которых нет в словаре, не трогаем (оставляем как есть).
        /// </summary>
        private void ApplyGridPermissionsToFormControls(Form form)
        {
            var modes = GetCurrentObjectModesFromGrid();
            if (modes.Count == 0) return;

            void Recurse(Control parent)
            {
                foreach (Control c in parent.Controls)
                {
                    string objName = ResolveObjectName(c);
                    if (!string.IsNullOrEmpty(objName) && modes.TryGetValue(objName, out int mode))
                    {
                        ApplyModeToControl(c, mode);
                    }

                    if (c.HasChildren)
                        Recurse(c);
                }
            }

            Recurse(form);
        }
        private void InjectPreviewDefaults(Form form)
        {
            if (form == null) return;

            var flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

            // 1) Заполнение *DataService полей
            foreach (var field in form.GetType().GetFields(flags))
            {
                if (!field.Name.EndsWith("DataService", StringComparison.Ordinal)) continue;
                var current = field.GetValue(form);
                if (current != null) continue;

                var svcType = field.FieldType;
                var ctor = svcType.GetConstructor(new[] { typeof(DatabaseHelperSQL) });
                if (ctor != null)
                {
                    try
                    {
                        var svc = ctor.Invoke(new object[] { new DatabaseHelperSQL() });
                        field.SetValue(form, svc);
                    }
                    catch { /* молча пропускаем, если сервис не создался */ }
                }
            }

            // 2) Подстраховка строковых полей-констант (табличные имена и т.п.)
            // Частый кейс в подобных справочниках — поле tableString
            var tableStrField = form.GetType().GetField("tableString", flags);
            if (tableStrField != null && tableStrField.FieldType == typeof(string))
            {
                var cur = tableStrField.GetValue(form) as string;
                if (string.IsNullOrWhiteSpace(cur))
                {
                    // дефолт по имени формы
                    var def = form.GetType().Name.Equals("Fio", StringComparison.OrdinalIgnoreCase) ? "fio" : "unknown_table";
                    tableStrField.SetValue(form, def);
                }
            }

            // 3) Безопасный заголовок (если используется)
            if (string.IsNullOrWhiteSpace(form.Text))
            {
                form.Text = form.GetType().Name;
            }
        }
        // снимаем обработчики Load/Shown у КАЖДОГО контрола в дереве (форма + все дети)
        private void SuppressLoadAndShownHandlersDeep(Control root)
        {
            if (root == null) return;

            var keyLoad = typeof(Control).GetField("EventLoad", BindingFlags.NonPublic | BindingFlags.Static)?.GetValue(null);
            var keyShown = typeof(Control).GetField("EventShown", BindingFlags.NonPublic | BindingFlags.Static)?.GetValue(null);
            var eventsProp = typeof(Component).GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);

            void Strip(Control c)
            {
                var eventList = (EventHandlerList)eventsProp?.GetValue(c, null);
                if (eventList != null)
                {
                    if (keyLoad != null) eventList.RemoveHandler(keyLoad, eventList[keyLoad]);
                    if (keyShown != null) eventList.RemoveHandler(keyShown, eventList[keyShown]);
                }
                foreach (Control child in c.Controls)
                    Strip(child);

                // На будущее: если в форму ДОБАВЯТ новые контролы уже ПОСЛЕ Show()
                c.ControlAdded += (s, e) => Strip(e.Control);
            }

            Strip(root);
        }

        private Form CreateFormInstance_UserOrEmpty(string nameForm)
        {
            if (string.IsNullOrWhiteSpace(nameForm)) return null;

            // Находим тип по полному и короткому имени
            Type found = null;
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    found = asm.GetType(nameForm, throwOnError: false, ignoreCase: false);
                    if (found != null && typeof(Form).IsAssignableFrom(found)) break;

                    foreach (var t in asm.GetTypes())
                    {
                        if (typeof(Form).IsAssignableFrom(t) &&
                            string.Equals(t.Name, nameForm, StringComparison.Ordinal))
                        {
                            found = t;
                            break;
                        }
                    }
                    if (found != null) break;
                }
                catch { /* пропускаем проблемные сборки */ }
            }
            if (found == null) return null;

            try
            {
                // 1) РОВНО ctor(UserClass)
                foreach (var ctor in found.GetConstructors())
                {
                    var pars = ctor.GetParameters();
                    if (pars.Length == 1 && pars[0].ParameterType == typeof(UserClass))
                    {
                        var f = (Form)ctor.Invoke(new object[] { _user });
                        if (f.IsMdiContainer) { f.Dispose(); return null; }
                        return f;
                    }
                }

                // 2) РОВНО пустой ctor()
                var empty = found.GetConstructor(Type.EmptyTypes);
                if (empty != null)
                {
                    var f = (Form)empty.Invoke(null);
                    if (f.IsMdiContainer) { f.Dispose(); return null; }
                    return f;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Вспомогательные методы UI
        private void ApplyToPreviewIfReady()
        {
            if (_previewForm == null || _previewForm.IsDisposed) return;

            // Соберём текущие режимы из таблицы и применим ко всем контролам превью-формы
            ApplyGridPermissionsToFormControls(_previewForm);
        }

        private int FindRowHandleByValue(GridView view, string fieldName, object value)
        {
            for (int i = 0; i < view.RowCount; i++)
            {
                var v = view.GetRowCellValue(i, fieldName);
                if (v != null && v != DBNull.Value && v.Equals(value))
                    return i;
            }
            return DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        }
        
        
        private void MakeReadOnly(GridView gv)
        {
            gv.OptionsBehavior.Editable = false;
            gv.OptionsBehavior.ReadOnly = true;
            gv.OptionsSelection.MultiSelect = true;
            gv.OptionsView.ShowGroupPanel = false;
        }

        private void ShowErrorInPanel(string message)
        {
            panelControl1.Controls.Clear();
            panelControl1.Controls.Add(new Label
            {
                Dock = DockStyle.Fill,
                Text = message,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            });
        }

        private Form _previewForm;

        /// <summary>
        /// Собираем словарь прав из gridViewObject: ObjectName -> ModeID (0/1/2).
        /// </summary>
        private Dictionary<string, int> GetCurrentObjectModesFromGrid()
        {
            var dict = new Dictionary<string, int>(StringComparer.Ordinal);

            for (int i = 0; i < gridViewObject.RowCount; i++)
            {
                var name = gridViewObject.GetRowCellValue(i, "ObjectName")?.ToString();
                var modeText = gridViewObject.GetRowCellValue(i, "HasAccessObject")?.ToString();

                if (string.IsNullOrWhiteSpace(name)) continue;

                int mode = modeText switch
                {
                    "Просмотр" => 1,
                    "Редактор" => 2,
                    _ => 0 // "Нет доступа" или NULL
                };
                dict[name] = mode;
            }

            return dict;
        }
        void DisableOriginalClickHandlers(Control c)
        {
            var eventsProp = typeof(Control).GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);
            var eventList = (EventHandlerList)eventsProp?.GetValue(c, null);

            var clickKey = typeof(Control).GetField("EventClick", BindingFlags.NonPublic | BindingFlags.Static)?.GetValue(null);
            if (clickKey != null && eventList != null)
                eventList.RemoveHandler(clickKey, eventList[clickKey]);
        }
        #endregion
    }
}
