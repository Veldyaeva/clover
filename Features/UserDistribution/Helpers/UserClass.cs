using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraBars.Ribbon;
using SewingProduction.Helpers;

namespace SewingProduction.Features.UserDistribution.Helpers
{
    public class UserClass
    {
        private readonly UserClassDataService _userClassDataService;
        DatabaseHelper dbHelper = new DatabaseHelper("ace");
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
        }
        public bool HasPermission(string objectName, string permissionType)
        {
            if (myObjectForm == null || myObjectForm.Rows.Count == 0)
            {
                // Если DataTable пустой, считаем, что прав нет
                return false;
            }
            // Ищем строку, где ObjectName соответствует имени объекта и соответствует нужному разрешению
            foreach (DataRow row in myObjectForm.Rows)
            {
                string objectNameFromTable = row["ObjectName"]?.ToString();
                string modeIdFromTable = row["ModeName"]?.ToString();
                if (objectNameFromTable == objectName && modeIdFromTable == permissionType)
                {
                    return true; // Нашли разрешение
                }
            }
            return false; // Разрешение не найдено
        }
        public async void ExitUser()
        {
            UserId = 0;
            UserName = null;
            CreatorID = 0;
            FioID = 0;
            Roles.Clear();
            myObjectForm?.Clear();
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
            return await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@UserId", UserId } , { "@NameForm", NameForm } });
        }
    }

}
