using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SewingProduction.Features.UserDistribution.Class;
using SewingProduction.Helpers;

namespace SewingProduction.Features.UserDistribution.Helpers
{
    public class UserClass
    {
        private readonly UserClassDataService _userClassDataService;
        DatabaseHelper dbHelper = new DatabaseHelper();
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int CreatorID { get; set; }
        public int FioID { get; set; }
        public string Fio { get; set; }
        public int BrigID { get; set; }
        public string Brig { get; set; }
        public string Password { get; set; } = "";
        public List<string> Roles { get; set; } = new List<string>();
        public List<UserClass> Children { get; set; } = new List<UserClass>();
        public DataTable myObjectForm;
        
        public UserClass()
        {
            _userClassDataService = new UserClassDataService(dbHelper);
        }
        public async Task LoadUserData()
        {
            DataTable UserData = await _userClassDataService.GetUserData(UserId);

            DataRow rowUserData = UserData.Rows[0];
            UserName = rowUserData["Username"]?.ToString() ?? "";
            CreatorID = rowUserData["CreatorID"] == null ? 0 : (int)rowUserData["CreatorID"];
            FioID = rowUserData["FioID"] == DBNull.Value ? 0 : Convert.ToInt32(rowUserData["FioID"]);
            BrigID = rowUserData["BrigID"] == DBNull.Value ? 0 : Convert.ToInt32(rowUserData["BrigID"]);

            Roles = await _userClassDataService.GetUserRoles(UserId);
        }
        public async Task LoadObjectForm(string NameForm)
        {
            myObjectForm = await _userClassDataService.GetObjectForm(UserId, NameForm);
            BuildPermIndex();
        }
        //public bool HasPermission(string objectName, string permissionType)
        //{
        //    if (myObjectForm == null || myObjectForm.Rows.Count == 0) 
        //    { 
        //        return false; 
        //    }

        //    foreach (DataRow row in myObjectForm.Rows)
        //    {
        //        string objectNameFromTable = row["ObjectName"]?.ToString(); 
        //        string modeIdFromTable = row["ModeName"]?.ToString();
        //        if (objectNameFromTable == objectName && modeIdFromTable == permissionType)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}
        private HashSet<(string ObjectName, string ModeName)> _permIndex;

        private static string Norm(string s)
            => (s ?? "").Trim(); // при желании можно .ToUpperInvariant()

        public void BuildPermIndex()
        {
            _permIndex = new HashSet<(string, string)>();

            if (myObjectForm == null || myObjectForm.Rows.Count == 0)
            {
                //Debug.WriteLine("[PermIndex] myObjectForm пустая — индекс пуст.");
                return;
            }

            foreach (DataRow row in myObjectForm.Rows)
            {
                var obj = Norm(row["ObjectName"]?.ToString());
                var mode = Norm(row["ModeName"]?.ToString());

                if (obj.Length == 0 || mode.Length == 0)
                    continue;

                _permIndex.Add((obj, mode));
            }

            //Debug.WriteLine($"[PermIndex] Построен. Записей: {_permIndex.Count}");
        }

        public void ClearPermIndex()
        {
            _permIndex = null;
        }

        public bool HasPermission(string objectName, string permissionType)
        {
            if (string.IsNullOrWhiteSpace(objectName) || string.IsNullOrWhiteSpace(permissionType))
                return false;

            // Если индекс не построен — это почти наверняка причина "нет доступа"
            if (_permIndex == null)
            {
                //Debug.WriteLine("[HasPermission] ⚠ Индекс не построен! BuildPermIndex не вызывался.");
                return false;
            }

            var key = (Norm(objectName), Norm(permissionType));
            return _permIndex.Contains(key);
        }

        public async void ExitUser()
        {
            UserId = 0;
            UserName = null;
            CreatorID = 0;
            FioID = 0;
            Roles.Clear();
            myObjectForm?.Clear();
            CurrentUser.Clear();
        }
    }
    public class UserClassDataService
    {
        private readonly DatabaseHelper _dbHelper;
        public UserClassDataService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<DataTable> GetUserData(int UserId)
        {
            string query = "SELECT * FROM Users WHERE UserId = @UserId";
            return await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@UserId", UserId } });
        }
        public async Task<List<string>> GetUserRoles(int UserId)
        {
            string query = @"
                SELECT r.RoleName
                FROM Users u
                INNER JOIN UserRoles ur ON u.UserId = ur.UserId
                INNER JOIN Roles r ON ur.RoleId = r.RoleId
                WHERE u.UserId = @UserId";

            DataTable result = await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@UserId", UserId } });

            List<string> roles = new List<string>();
            foreach (DataRow row in result.Rows)
            {
                roles.Add(row["RoleName"].ToString());
            }
            return roles;
        }
        public async Task<DataTable> GetObjectForm(int UserId, string NameForm)
        {
            string query = $@"
                SELECT o.ObjectName, m.ModeName
                    FROM Users u
                    LEFT JOIN UserRoles ur ON u.UserID = ur.UserID
                    LEFT JOIN Roles r ON ur.RoleID = r.RoleID
                    LEFT JOIN RoleObject ro ON r.RoleID = ro.RoleID
                    LEFT JOIN ObjectForm o ON ro.ObjectID = o.ObjectID
                    LEFT JOIN ProjectForms pf ON o.FormID = pf.ProjectFormsID
                    LEFT JOIN Mode m ON m.ModeID = ro.ModeID
                WHERE u.UserId = @UserId
                AND pf.NameForm = @NameForm";
            return await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@UserId", UserId }, { "@NameForm", NameForm } });
        }
    }

}
