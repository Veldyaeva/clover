using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpo.DB.Helpers;
using System.Windows.Forms;
using SewingProduction.Helpers;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraVerticalGrid;
//using Microsoft.AspNetCore.Identity;
using DevExpress.CodeParser;
using Microsoft.AspNet.Identity;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Core.Class.Settings;
using SewingProduction.Features.UserDistribution.Forms;


namespace SewingProduction.form.UserDistribution
{
    public partial class UserProfile : CustomForm
    {
        private readonly UserProfileDataService _userProfileDataService;
        DatabaseHelper dbHelper = new DatabaseHelper("ace");
        private readonly UserClass _user;
        private readonly IPasswordHasher _passwordHasher;
        private readonly LoginFormDataService _loginService;
        private FormManager _formManager;
        public UserProfile(UserClass user) : base(user)
        {
            InitializeComponent();
            _userProfileDataService = new UserProfileDataService(dbHelper);
            _user = user;
            customLabelProfileName.Text = _user.UserName;
            listBoxRole.DataSource = _user.Roles;
            customLabelCompName.Text = Environment.MachineName;
            _passwordHasher = new PasswordHasher();

        }

        private void customButtonChangeUser_Click(object sender, EventArgs e)
        {
            if (this.MdiParent is SpMainForm mainForm)
            {
                mainForm.SaveOpenTabsSafe(); // ДО ExitUser
            }
            _user.ExitUser();
            // Перезапуск приложения с авторизацией
            Application.Restart();
        }

        private void customButtonAllRpofile_Click(object sender, EventArgs e)
        {
            if (this.MdiParent is SpMainForm mainForm)
            {
                mainForm.OpenForm(new AllUser(_user));
            }
        }

        private void customButtonAllRole_Click(object sender, EventArgs e)
        {
            if (this.MdiParent is SpMainForm mainForm)
            {
                mainForm.OpenForm(new AllRole(_user));
            }
        }
        private void customButtonAdminForm_Click(object sender, EventArgs e)
        {
            if (this.MdiParent is SpMainForm mainForm)
            {
                mainForm.OpenForm(new AdminForm(_user));
            }
        }

        private void customButtonUserHierarchy_Click(object sender, EventArgs e)
        {
            if (this.MdiParent is SpMainForm mainForm)
            {
                mainForm.OpenForm(new UsersHierarchy(_user));
            }
        }
        private void customButtonAdminSprav_Click(object sender, EventArgs e)
        {
            if (this.MdiParent is SpMainForm mainForm)
            {
                mainForm.OpenForm(new AdminSprav(_user));
            }
        }
        private void customButtonHistory_Click(object sender, EventArgs e)
        {
            if (this.MdiParent is SpMainForm mainForm)
            {
                mainForm.OpenForm(new ActionHistory(_user));
            }
        }
        private void customButtonSpravTable_Click(object sender, EventArgs e)
        {
            if (this.MdiParent is SpMainForm mainForm)
            {
                mainForm.OpenForm(new Dictionary(_user));
            }
        }
        private async void customButtonEditPassword_Click(object sender, EventArgs e)
        {
            string currentPassword = customTextBoxOldPassword.Text;
            string newPassword = customTextBoxNewPassword.Text;
            string newPassword2 = customTextBoxNewPassword2.Text;

            string dbHash = await _userProfileDataService.GetPasswordHash(_user.UserId);
            if (_passwordHasher.VerifyHashedPassword(dbHash, currentPassword) != PasswordVerificationResult.Success)
            {
                MessageBox.Show("Текущий пароль неверен!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (newPassword != newPassword2)
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string newHash = _passwordHasher.HashPassword(newPassword);
            await _userProfileDataService.UpdatePasswordHash(_user.UserId, newHash);
            MessageBox.Show("Пароль успешно изменен!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //this.Close();
        }

    }
    public class UserProfileDataService
    {
        private readonly DatabaseHelper _dbHelper;
        public UserProfileDataService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public async Task<string> GetPasswordHash(int userId)
        {
            string query = "SELECT PasswordHash FROM Users WHERE UserID = @UserId";
            DataTable dt = await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object>
            {
                { "@UserId", userId }
            });
            string PasswordHash = dt.Rows.Count > 0 ? dt.Rows[0]["PasswordHash"].ToString() : "0";
            return PasswordHash;
        }
        public async Task UpdatePasswordHash(int userId, string newPasswordHash)
        {
            string query = "UPDATE Users SET PasswordHash = @NewHash WHERE UserId = @UserId";
            await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object>
            {
                { "@NewHash", newPasswordHash },
                { "@UserId", userId }
            });
        }
    }
}
