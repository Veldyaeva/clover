using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNet.Identity;
using SewingProduction.Core.Class.Settings;
using SewingProduction.Features.Articul;
using SewingProduction.Features.UserDistribution.Forms;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.form;
using SewingProduction.Helpers;
using SewingProduction.Services;


namespace SewingProduction.Features.UserDistribution.Forms
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
            //Application.Restart();
            string exePath = Application.ExecutablePath;
            Process.Start(exePath, "--restart");
            // Завершаем процесс без перебора коллекции окон, чтобы избежать исключения
            Environment.Exit(0);
        }

        private void customButtonAllRpofile_Click(object sender, EventArgs e)
        {
            OpenForm(new AllUser(_user));
        }

        private void customButtonAllRole_Click(object sender, EventArgs e)
        {
            OpenForm(new AllRole(_user));
        }
        private void customButtonAdminForm_Click(object sender, EventArgs e)
        {
            OpenForm(new AdminForm(_user));
        }

        private void customButtonUserHierarchy_Click(object sender, EventArgs e)
        {
            OpenForm(new UserHierarchy(_user));
        }
        private void customButtonAdminSprav_Click(object sender, EventArgs e)
        {
            OpenForm(new RoleColumn(_user));
        }
        private void customButtonHistory_Click(object sender, EventArgs e)
        {
            OpenForm(new ActionHistory(_user));
        }
        private void customButtonSpravTable_Click(object sender, EventArgs e)
        {
            OpenForm(new Dictionary(_user));
        }
        private void OpenForm(Form oForm)
        {
            if (this.MdiParent is SpMainForm mainForm)
            {
                mainForm.OpenForm(oForm);
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

        private void customActionButtonNon_Click(object sender, EventArgs e)
        {
            if (this.MdiParent is SpMainForm mainForm)
            {
                mainForm.OpenForm(new TestForm1(_user));
            }
        }

        private async void customActionButtonEditLogin_Click(object sender, EventArgs e)
        {
            string currentPassword = customTextBoxPassword.Text;
            string newLogin = customTextBoxNewLogin.Text;

            string dbHash = await _userProfileDataService.GetPasswordHash(_user.UserId);
            if (_passwordHasher.VerifyHashedPassword(dbHash, currentPassword) != PasswordVerificationResult.Success)
            {
                MessageBox.Show("Пароль неверен!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            await _userProfileDataService.UpdateLogin(_user.UserId, newLogin);
            MessageBox.Show("Логин успешно изменен!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void customButtonAllDistribution_Click(object sender, EventArgs e)
        {
            OpenForm(new AllDistribution(_user));
        }

        private void customButtonUserPodr_Click(object sender, EventArgs e)
        {
            OpenForm(new UserPodr(_user));
        }

        private void customButton3_Click(object sender, EventArgs e)
        {
            OpenForm(new RolePodr(_user));
        }

        private void customButtonShareUser_Click(object sender, EventArgs e)
        {
            //OpenForm(new ShareUser(_user));
            ShareUser f = new ShareUser(_user);
            f.ShowDialog();
        }

        private void customButtonUserRole_Click(object sender, EventArgs e)
        {
            OpenForm(new UserRole(_user));

        }

        private void customButtonRoleFormObject_Click(object sender, EventArgs e)
        {
            OpenForm(new RoleFormObject(_user));
        }

        private void customButtonMr_Click(object sender, EventArgs e)
        {
            MrMainForm form = new MrMainForm(_user);
            form.ShowDialog();
        }

        private void listBoxFast_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxFast == null) return;
            if (listBoxFast.SelectedItem == null) return;

            string selectedItem = listBoxFast.SelectedItem.ToString();

            switch (selectedItem)
            {
                case "Администрирование форм":
                    OpenForm(new AdminForm(_user));
                    break;

                case "Список ролей":
                    OpenForm(new AllRole(_user));
                    break;

                case "Список пользователей":
                    OpenForm(new AllUser(_user));
                    break;

                case "Распределение прав":
                    OpenForm(new UserRole(_user));
                    break;

                default:
                    MessageBox.Show("Неизвестный пункт");
                    break;
            }
        }

    }
    public class UserProfileDataService
    {
        private readonly DbService _dbService;
        public UserProfileDataService(DatabaseHelper dbHelper)
        {
            _dbService = new DbService(dbHelper);
        }
        public async Task<string> GetPasswordHash(int userId)
        {
            return await _dbService.SelectOneFieldAsync<string>(
                "Users",
                "PasswordHash",
                new Dictionary<string, object> { { "UserID", userId } }) ?? "0";
        }
        public async Task UpdatePasswordHash(int userId, string newPasswordHash)
        {
            await _dbService.UpdateFieldAsync(TableNames.Users, "PasswordHash", newPasswordHash, "UserId", userId);
        }
        public async Task UpdateLogin(int userId, string login)
        {
            await _dbService.UpdateFieldAsync(TableNames.Users, "login", login, "UserId", userId);
        }
    }
}
