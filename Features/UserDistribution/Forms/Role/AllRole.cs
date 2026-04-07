using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Features.UserDistribution.Forms;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.UserDistribution.Forms
{
    public partial class AllRole : CustomForm
    {
        public MrRoleMode StartMode { get; set; } = MrRoleMode.None;
        private readonly AllRoleDataService _allRoleDataService;
        DatabaseHelper dbHelper = new DatabaseHelper();
        private readonly UserClass _user;
        private int selectedRoleId = -1;
        private int selectedUserId = -1;
        private int selectedFormId = -1;
        private int selectedObjectId = -1;
        public AllRole(UserClass user) : base(user)
        {
            InitializeComponent();
            _allRoleDataService = new AllRoleDataService();
            _user = user;
            SetupGrid();
        }
        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);

            await Task.Delay(100);

            switch (StartMode)
            {
                case MrRoleMode.New:
                    StartCreateNewRole();
                    break;

                case MrRoleMode.Copy:
                    StartCopyRole();
                    break;
            }
        }
        private async void customGridControlRoles_Load(object sender, EventArgs e)
        {
            await Roles_Load();
            LoadUserPermissions();
            await LoadEditableCreatorIds();
        }

        private async Task Roles_Load()
        {
            int eFocusedRowHandle = gridViewRoles.FocusedRowHandle;
            bindingSourceRoles.DataSource = await _allRoleDataService.GetRoles(_user.UserId);
            gridViewRoles.FocusedRowHandle = eFocusedRowHandle;
        }
        #region добавление / редактирование / удаление роли
        private void customButtonAddRole_Click(object sender, EventArgs e)
        {
            gridViewRoles.AddNewRow();
        }
        private void gridViewRoles_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gridViewRoles.GridControl.BeginInvoke(new Action(() =>
            {
                if (gridViewRoles.IsValidRowHandle(e.RowHandle))
                {
                    gridViewRoles.FocusedRowHandle = e.RowHandle;
                    gridViewRoles.ShowPopupEditForm();
                }
            }));
        }
        private async void gridViewRoles_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            Console.WriteLine("RowUpdated start");
            DataRow row = ((DataRowView)e.Row).Row;
            if (row == null)
                return;
            gridViewRoles_FocusedRow();

            int rowHandle = gridViewRoles.GetRowHandle(gridViewRoles.DataSource is BindingSource ? ((BindingSource)gridViewRoles.DataSource).IndexOf(e.Row) : -1);

            int id = row["RoleID"] != DBNull.Value ? Convert.ToInt32(row["RoleID"]) : 0;

            string RoleName = row["RoleName"]?.ToString() ?? "";
            string Description = row["Description"]?.ToString() ?? "";
            try
            {
                if (id > 0)
                {
                    if (!await checkEditRole())
                        return;
                    _allRoleDataService.UpdateRoles("RoleName", RoleName, id);
                    _allRoleDataService.UpdateRoles("Description", Description, id);
                }
                else
                {
                    int newId = await _allRoleDataService.InsertRoles(RoleName, Description, _user.UserId);
                    row["RoleID"] = newId;
                }
                await Roles_Load();
                Console.WriteLine("RowUpdated");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Message.Contains("UQ_RoleName"))
                    MessageBox.Show($"Роль с именем \"{RoleName}\" уже существует. Имя должно быть уникальным.", "Ошибка добавления", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Ошибка базы данных: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gridViewRoles.DeleteRow(gridViewRoles.FocusedRowHandle);
            }
        }
        private async void customButtonDeleteRole_Click(object sender, EventArgs e)
        {
            if (!await checkEditRole())
                return;
            int eID = Convert.ToInt32(gridViewRoles.GetFocusedRowCellValue(gridViewRoles.Columns["RoleID"]));
            string RoleName = gridViewRoles.GetFocusedRowCellValue(gridViewRoles.Columns["RoleName"]).ToString();
            string message = "Удаление РОЛИ приведет к удалению ЕЁ у всех ее владельцев, Вы уверены что хотите удалить '" + RoleName + "' ?";
            var result = MessageBox.Show(message, "Удалить?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                _allRoleDataService.DeleteRoles(eID);
            //await Roles_Load();
            gridViewRoles.DeleteRow(gridViewRoles.FocusedRowHandle);
        }
        #endregion
        private async Task<bool> checkEditRole()
        {
            if (selectedRoleId <= 0)
                gridViewRoles_FocusedRow();
            if (_user.Roles.Contains("Администратор"))
                return true;
            int creatorId = await _allRoleDataService.GetCreatorIdByRole(selectedRoleId);
            if (!_editableRoleCreatorIds.Contains(creatorId))
            {
                MessageBox.Show("Вы не можете редактировать эту роль. Она не создана вами или вашими потомками.");
                GridView activeView = customGridControlRoles.FocusedView as GridView;
                if (activeView != null)
                {
                    //activeView.update();
                }
                return false;
            }

            return true;
        }
        private void SetupGrid()
        {
            customGridControlRoles.LevelTree.Nodes[0].RelationName = "Формы";
            customGridControlRoles.LevelTree.Nodes[0].LevelTemplate = gridViewForms;

            customGridControlRoles.LevelTree.Nodes[0].Nodes[0].RelationName = "Объекты";
            customGridControlRoles.LevelTree.Nodes[0].Nodes[0].LevelTemplate = gridViewObject;

            customGridControlRoles.LevelTree.Nodes[1].RelationName = "Пользователи";
            customGridControlRoles.LevelTree.Nodes[1].LevelTemplate = gridViewUsers;

            gridViewRoles_MasterRowGet();
            gridViewForms_MasterRowGet();
            gridViewForms_CellValueChanged();
            gridViewObject_CellValueChanged();
            gridViewUsers_CellValueChanged();
        }
        private void gridViewRoles_MasterRowGet()
        {
            gridViewRoles.MasterRowGetRelationCount += (s, e) => e.RelationCount = 2;

            gridViewRoles.MasterRowGetRelationName += (s, e) =>
            {
                e.RelationName = e.RelationIndex == 0 ? "Формы" : "Пользователи";
            };

            gridViewRoles.MasterRowGetChildList += async (s, e) =>
            {
                var view = s as GridView;
                var row = view.GetDataRow(e.RowHandle);
                if (selectedRoleId <= 0)
                    gridViewRoles_FocusedRow();

                if (e.RelationIndex == 0)
                {
                    DataTable forms = await _allRoleDataService.GetFormsForRoles(selectedRoleId, _user.UserId);
                    e.ChildList = forms.DefaultView;
                }
                else if (e.RelationIndex == 1)
                {
                    DataTable users = await _allRoleDataService.GetUsersWithRolesInfo(selectedRoleId, _user.CreatorID);
                    e.ChildList = users.DefaultView;
                }
            };
        }
        private void gridViewForms_MasterRowGet()
        {
            gridViewForms.MasterRowGetRelationCount += (s, e) => e.RelationCount = 1;
            gridViewForms.MasterRowGetRelationName += (s, e) => e.RelationName = "Объекты";
            gridViewForms.MasterRowGetChildList += async (s, e) =>
            {
                var view = s as GridView;
                var row = view.GetDataRow(e.RowHandle);
                if (row == null) return;

                //int roleId = Convert.ToInt32(gridViewRoles.GetFocusedRowCellValue("RoleID"));
                if (selectedRoleId <= 0)
                    gridViewRoles_FocusedRow();
                int formId = Convert.ToInt32(row["ProjectFormsID"]);
                DataTable objects = await _allRoleDataService.GetObjectsForFormRoles(selectedRoleId, formId, _user.UserId);
                e.ChildList = objects.DefaultView;
            };
        }
        private void gridViewForms_CellValueChanged()
        {
            gridViewForms.CellValueChanged += async (sender, e) =>
            {
                if (e.Column.FieldName == "HasAccess")
                {
                    if (!await checkEditRole())
                        return;
                    gridViewRoles_FocusedRow();
                    GridView view = sender as GridView;
                    var row = view.GetDataRow(e.RowHandle);
                    if (row == null) return;

                    int formId = Convert.ToInt32(row["ProjectFormsID"]);
                    string selectedMode = row["HasAccess"].ToString();
                    string mode = row["HasAccess"].ToString();

                    int requestedMode = getRequestedMode(mode);

                    DataTable objects = await _allRoleDataService.GetObjectsForFormRoles(selectedRoleId, formId, _user.UserId);
                    foreach (DataRow obj in objects.Rows)
                    {
                        int objectId = Convert.ToInt32(obj["ObjectID"]);
                        int userMode = _userPermissions.TryGetValue(objectId, out var lvl) ? lvl : 0;
                        int finalMode = Math.Min(requestedMode, userMode);
                        await _allRoleDataService.UpdateRoleObjectMode(selectedRoleId, objectId, finalMode);
                    }

                    view.SetMasterRowExpanded(e.RowHandle, false);
                    view.SetMasterRowExpanded(e.RowHandle, true);
                }
            };
        }
        private int getRequestedMode(string mode)
        {
            switch (mode)
            {
                case "Нет доступа":
                    return 0;
                case "Просмотр":
                    return 1;
                case "Редактор":
                    return 2;
                default:
                    return 0;
            }
        }
        private void gridViewObject_CellValueChanged()
        {
            gridViewObject.CellValueChanged += async (sender, e) =>
            {
                if (e.Column.FieldName == "HasAccessObject")
                {
                    if (!await checkEditRole())
                        return;
                    gridViewObject_FocusedRow();
                    gridViewRoles_FocusedRow();
                    int mode = getRequestedMode(e.Value.ToString());
                    await _allRoleDataService.UpdateRoleObjectMode(selectedRoleId, selectedObjectId, mode);
                }
            };
        }
        private void gridViewUsers_CellValueChanged()
        {
            gridViewUsers.CellValueChanging += async (sender, e) =>
            {
                Console.WriteLine(e.Column.FieldName);
                if (e.Column.FieldName == "HasRole")
                {
                    gridViewUsers_FocusedRow();
                    gridViewRoles_FocusedRow();
                    bool hasRole = Convert.ToBoolean(e.Value);

                    if (hasRole)
                    {
                        await _allRoleDataService.AddUserRoles(selectedUserId, selectedRoleId);
                        Console.WriteLine("Add UserID: " + selectedUserId + " RoleID" + selectedRoleId);
                    }
                    else
                    {
                        await _allRoleDataService.RemoveUserRoles(selectedUserId, selectedRoleId);
                        Console.WriteLine("Remove UserID: " + selectedUserId + " RoleID" + selectedRoleId);
                    }
                }
            };
        }

        private void gridViewRoles_FocusedRow()
        {
            var Id = gridViewRoles.GetFocusedRowCellValue("RoleID");
            selectedRoleId = (Id != null && Id != DBNull.Value) ? Convert.ToInt32(Id) : 0;
            Console.WriteLine("selected RoleId: " + selectedRoleId);
        }

        private void gridViewObject_FocusedRow()
        {
            GridView activeView = customGridControlRoles.FocusedView as GridView;
            if (activeView != null && activeView.Name == "gridViewObject" && activeView.FocusedRowHandle >= 0)
            {
                var Id = activeView.GetFocusedRowCellValue("ObjectID");
                selectedObjectId = Id != null ? Convert.ToInt32(Id) : 0;
                Console.WriteLine("selected ObjectID: " + selectedObjectId);
            }
        }

        private void gridViewForms_FocusedRow()
        {
            GridView activeView = customGridControlRoles.FocusedView as GridView;
            if (activeView != null && activeView.Name == "gridViewForms" && activeView.FocusedRowHandle >= 0)
            {
                var Id = activeView.GetFocusedRowCellValue("ProjectFormsID");
                selectedFormId = Id != null ? Convert.ToInt32(Id) : 0;
                Console.WriteLine("selected ProjectFormsID: " + selectedFormId);
            }
        }

        private void gridViewUsers_FocusedRow()
        {
            GridView activeView = customGridControlRoles.FocusedView as GridView;
            if (activeView != null && activeView.Name == "gridViewUsers" && activeView.FocusedRowHandle >= 0)
            {
                var Id = activeView.GetFocusedRowCellValue("UserID");
                selectedUserId = Id != null ? Convert.ToInt32(Id) : 0;
                Console.WriteLine("selected UserId: " + selectedUserId);
            }
        }
        private void gridViewRoles_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridViewRoles_FocusedRow();
        }
        private void gridViewObject_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridViewObject_FocusedRow();
        }
        private void gridViewForms_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridViewForms_FocusedRow();
        }
        private void gridViewUsers_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridViewUsers_FocusedRow();
        }
        private Dictionary<int, int> _userPermissions;
        private async void LoadUserPermissions()
        {
            _userPermissions = await _allRoleDataService.GetUserObjectPermissions(_user.UserId);
        }
        private void gridViewObject_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "HasAccessObject")
            {
                GridView view = sender as GridView;
                if (view == null || e.RowHandle < 0) return;

                int objectId = Convert.ToInt32(view.GetRowCellValue(e.RowHandle, "ObjectID"));

                if (_userPermissions != null && _userPermissions.TryGetValue(objectId, out int maxPermission))
                {
                    var combo = new RepositoryItemComboBox
                    {
                        TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor, // запрет ручного ввода
                        DropDownRows = 3 // гарантирует отображение всех значений
                    };

                    combo.Items.Clear();
                    if (maxPermission >= 0) combo.Items.Add("Нет доступа");
                    if (maxPermission >= 1) combo.Items.Add("Просмотр");
                    if (maxPermission >= 2) combo.Items.Add("Редактор");

                    e.RepositoryItem = combo;
                }
            }
        }

        private HashSet<int> _editableRoleCreatorIds = new HashSet<int>();
        private async Task LoadEditableCreatorIds()
        {
            // Получим себя + всех потомков
            DataTable descendants = await _allRoleDataService.LoadEditableCreatorIds(_user.UserId);

            _editableRoleCreatorIds = descendants.AsEnumerable()
                .Select(r => Convert.ToInt32(r["UserID"]))
                .ToHashSet();
        }

        #region копирование роли
        private async void customButtonCopyRole_Click(object sender, EventArgs e)
        {
            int originalRoleId = selectedRoleId;
            await _allRoleDataService.CopyRole(originalRoleId, _user.UserId);
            await Roles_Load();
            // Устанавливаем фокус на последнюю строку
            int lastRowHandle = gridViewRoles.RowCount - 1;
            if (lastRowHandle >= 0)
            {
                gridViewRoles.FocusedRowHandle = lastRowHandle;
                gridViewRoles.ShowPopupEditForm(); // Открываем форму редактирования
            }
            int newRoleId = selectedRoleId;
            Console.WriteLine(originalRoleId.ToString() + ", " + newRoleId.ToString());
        }

        #endregion

        #region master right
        public void StartCreateNewRole()
        {
            gridViewRoles.AddNewRow();
            customButtonAddRole.Visible = false;
            customButtonCopyRole.Visible = false;
            customButtonDeleteRole.Visible = false;
        }

        public void StartCopyRole()
        {
            customButtonAddRole.Visible = false;
            customButtonDeleteRole.Visible = false;
        }
        #endregion
    }
    public class AllRoleDataService
    {
        private readonly DatabaseHelper _dbHelper;
        public AllRoleDataService()
        {
            _dbHelper = new DatabaseHelper();
        }

        #region бд роли
        public async Task<DataTable> GetRoles(int userId)
        {
            string query = @"
            SELECT r.RoleID, r.RoleName, r.Description, u.UserName
            FROM Roles r
            JOIN Users u ON r.CreatorID = u.UserID
            WHERE NOT EXISTS(
                SELECT 1
                FROM RoleObject ro
                WHERE ro.RoleID = r.RoleID
                AND NOT EXISTS(
                    SELECT 1
                    FROM
                      (SELECT ro.ObjectID, MAX(ro.ModeID) AS UserModeID
                      FROM RoleObject ro
                      JOIN UserRoles ur ON ro.RoleID = ur.RoleID
                      WHERE ur.UserID = @UserID
                      GROUP BY ro.ObjectID) uo
                    WHERE uo.ObjectID = ro.ObjectID
                    AND uo.UserModeID >= ro.ModeID
                )
            )";

            //@"SELECT r.RoleID, r.RoleName, r.Description, u.UserName
            //FROM Roles r
            //    LEFT JOIN Users u ON r.CreatorID = u.UserID
            //    WHERE
            //        r.CreatorID = @UserID
            //        OR r.RoleID IN(SELECT ur.RoleID FROM UserRoles ur WHERE ur.UserID = @UserID)
            //        OR r.CreatorID IN(SELECT UserID FROM GetDescendants(@UserID))";

            return await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@UserID", userId } });
        }
        public async void UpdateRoles(string eColumn, object eValue, int eId)
        {
            string query = $@"
                UPDATE Roles SET {eColumn} = @eValue
                WHERE RoleID = @eId";
            await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@eValue", eValue }, { "@eId", eId } });
        }
        public async Task<int> InsertRoles(string eRoleName, string eDescription, int CreatorID)
        {
            string query = $@"
                INSERT INTO Roles (RoleName, Description, CreatorID)
                VALUES (@RoleName, @Description, @CreatorID);

                SELECT CAST(SCOPE_IDENTITY() AS INT); ";
            var result = await _dbHelper.ExecuteScalarAsync(query, new Dictionary<string, object>
            {
                { "@RoleName", eRoleName },
                { "@Description", eDescription },
                { "@CreatorID", CreatorID }
            });
            Console.WriteLine($"InsertRoles => result: {result}");
            return Convert.ToInt32(result);
        }
        public async void DeleteRoles(int eId)
        {
            string query = $@"
                DELETE Roles 
                WHERE RoleID = @eId
                DELETE RoleObject 
                WHERE RoleID = @eId;";
            await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@eId", eId } });
        }
        public async Task<int> GetCreatorIdByRole(int roleId)
        {
            string query = "SELECT CreatorID FROM Roles WHERE RoleID = @RoleID";
            DataTable dt = await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@RoleID", roleId } });
            return dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0]["CreatorID"]) : -1;
        }
        public async Task<bool> RoleNameExists(string roleName)
        {
            string query = "SELECT COUNT(*) FROM Roles WHERE RoleName = @RoleName";
            object result = await _dbHelper.ExecuteScalarAsync(query, new Dictionary<string, object> { { "@RoleName", roleName } });
            return Convert.ToInt32(result) > 0;
        }
        #endregion

        public async Task<DataTable> GetUsersWithRolesInfo(int roleId, int userId)
        {
            string query = @"
                SELECT 
                    u.UserID, 
                    u.UserName,
                    CASE 
                        WHEN ur.UserID IS NULL THEN CAST(0 AS BIT) 
                        ELSE CAST(1 AS BIT) 
                    END AS HasRole
                FROM Users u
                LEFT JOIN UserRoles ur 
                    ON ur.UserID = u.UserID AND ur.RoleID = @RoleID
                WHERE u.UserID IN (SELECT UserID FROM GetDescendants(@UserID))";

            return await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@RoleID", roleId }, { "@UserID", userId } });

        }

        public async Task<DataTable> GetFormsForRoles(int roleId, int userId)
        {
            string query = @"SELECT DISTINCT pf.ProjectFormsID, pf.NameFormRus, pf.NameForm,
                CASE ISNULL((
                    SELECT TOP 1 ro.ModeID
                    FROM RoleObject ro
                    JOIN ObjectForm ofm ON ro.ObjectID = ofm.ObjectID
                    WHERE ofm.FormID = pf.ProjectFormsID AND ro.RoleID = @RoleID
                ), 0)
                    WHEN 0 THEN N'Нет доступа'
                    WHEN 1 THEN N'Просмотр'
                    WHEN 2 THEN N'Редактор'
                END AS HasAccess
            FROM ProjectForms pf
            WHERE pf.ProjectFormsID IN (
                SELECT DISTINCT pf2.ProjectFormsID
                FROM ObjectForm ofm
                JOIN ProjectForms pf2 ON ofm.FormID = pf2.ProjectFormsID
                JOIN RoleObject ro ON ro.ObjectID = ofm.ObjectID
                JOIN UserRoles ur ON ro.RoleID = ur.RoleID
                WHERE ur.UserID = @UserID
            )";
            return await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@RoleID", roleId }, { "@UserID", userId } });
        }

        public async Task<DataTable> GetObjectsForFormRoles(int roleId, int formId, int userId)
        {
            string query = @"
            SELECT o.ObjectID, o.ObjectNameRus, o.ObjectName,
                CASE ISNULL(ro.ModeID, 0)
                    WHEN 0 THEN N'Нет доступа'
                    WHEN 1 THEN N'Просмотр'
                    WHEN 2 THEN N'Редактор'
                END AS HasAccessObject
            FROM ObjectForm o
            LEFT JOIN RoleObject ro ON ro.ObjectID = o.ObjectID AND ro.RoleID = @RoleID
            WHERE o.FormID = @FormID
              AND o.ObjectID IN (
                  SELECT ro2.ObjectID
                  FROM RoleObject ro2
                  JOIN UserRoles ur2 ON ro2.RoleID = ur2.RoleID
                  WHERE ur2.UserID = @UserID
              )";

            return await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object>
            {
                { "@RoleID", roleId },
                { "@FormID", formId },
                { "@UserID", userId }
            });
        }
        #region пользователи-роли
        public async Task AddUserRoles(int userId, int roleId)
        {
            string query = "INSERT INTO UserRoles (UserID, RoleID) VALUES (@UserID, @RoleID)";
            await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object>
            {
                { "@UserID", userId },
                { "@RoleID", roleId }
            });
        }
        public async Task RemoveUserRoles(int userId, int roleId)
        {
            string query = "DELETE FROM UserRoles WHERE UserID = @UserID AND RoleID = @RoleID";
            await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object>
            {
                { "@UserID", userId },
                { "@RoleID", roleId }
            });
        }

        #endregion
        public async Task UpdateRoleObjectMode(int roleId, int objectId, int mode)
        {
            string deleteQuery = "DELETE FROM RoleObject WHERE RoleID = @RoleID AND ObjectID = @ObjectID";
            await _dbHelper.ExecuteQueryAsync(deleteQuery, new Dictionary<string, object>
            {
                { "@RoleID", roleId },
                { "@ObjectID", objectId }
            });

            if (mode > 0)
            {
                string insertQuery = @"INSERT INTO RoleObject (RoleID, ObjectID, ModeID) VALUES (@RoleID, @ObjectID, @Mode)";
                await _dbHelper.ExecuteQueryAsync(insertQuery, new Dictionary<string, object>
                {
                    { "@RoleID", roleId },
                    { "@ObjectID", objectId },
                    { "@Mode", mode }
                });
            }
        }
        public async Task<Dictionary<int, int>> GetUserObjectPermissions(int userId)
        {
            string query = @"
                SELECT o.ObjectID, MAX(ISNULL(m.ModeID, 0)) AS MaxModeID
                FROM Users u
                LEFT JOIN UserRoles ur ON u.UserID = ur.UserID
                LEFT JOIN Roles r ON ur.RoleID = r.RoleID
                LEFT JOIN RoleObject ro ON r.RoleID = ro.RoleID
                LEFT JOIN ObjectForm o ON ro.ObjectID = o.ObjectID
                LEFT JOIN Mode m ON m.ModeID = ro.ModeID
                WHERE u.UserID = @UserId 
                AND o.ObjectID IS NOT NULL
                GROUP BY o.ObjectID";

            DataTable result = await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@UserId", userId } });

            var permissions = new Dictionary<int, int>();
            foreach (DataRow row in result.Rows)
            {
                int objectId = row["ObjectID"] != DBNull.Value ? Convert.ToInt32(row["ObjectID"]) : 0;
                int maxMode = row["MaxModeID"] != DBNull.Value ? Convert.ToInt32(row["MaxModeID"]) : 0;
                permissions[objectId] = maxMode;
            }

            return permissions;
        }
        public async Task<DataTable> LoadEditableCreatorIds(int userId)
        {
            string query = @" SELECT UserID FROM GetDescendants(@UserID) UNION SELECT @UserID";

            return await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@UserID", userId } });
        }

        public async Task CopyRole(int originalRoleId, int userId)
        {
            int newRoleId = await InsertRoles("", "", userId);
            // Копируем привязки к объектам (RoleObject)
            string query = @"
                INSERT INTO RoleObject (RoleID, ObjectID, ModeID)
                SELECT @NewRoleID, ObjectID, ModeID
                FROM RoleObject
                WHERE RoleID = @OriginalRoleID";
            await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object>
            {
                { "@NewRoleID", newRoleId },
                { "@OriginalRoleID", originalRoleId }
            });
        }
    }
}
