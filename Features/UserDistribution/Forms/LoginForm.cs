using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SewingProduction.Helpers;
using Microsoft.Extensions.Identity;
using SewingProduction.Features.UserDistribution.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;
using SewingProduction.Core.Class.Settings;
using System.Diagnostics;

namespace SewingProduction.Features.UserDistribution.Forms
{
    public partial class LoginForm : Form
    {
        private readonly LoginFormDataService _loginFormDataService;
        private readonly UserClass _user;
        private readonly IPasswordHasher _passwordHasher;
        public LoginForm(UserClass user)
        {
            InitializeComponent();
            LoadLoginHistory();

            _user = user ?? throw new ArgumentNullException(nameof(user));
            _passwordHasher = new PasswordHasher();

            DatabaseHelper dbHelper = new DatabaseHelper("ace");
            _loginFormDataService = new LoginFormDataService(dbHelper);

            this.Shown += LoginForm_Shown;
        }

        private async void simpleButton_Click(object sender, EventArgs e)
        {
            string formLogin = comboBoxEditLogin.Text;
            string formPassword = textEditPassword.Text;

            string Event = $"Попытка входа {formLogin}: ";
            string hashedPassword = _passwordHasher.HashPassword(formPassword);
            //Console.WriteLine(hashedPassword);
            string hashedPasswordFromDb = await _loginFormDataService.GetPasswordHash(formLogin);
            if (hashedPasswordFromDb != null)
            {
                Microsoft.AspNet.Identity.PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(hashedPasswordFromDb, formPassword);
                //if (result == Microsoft.AspNet.Identity.PasswordVerificationResult.Success || formPassword == "вход без пароля")
                if (result == Microsoft.AspNet.Identity.PasswordVerificationResult.Success)
                {
                    try
                    {
                        _user.UserId = await _loginFormDataService.GetId(formLogin);
                        this.DialogResult = DialogResult.OK;
                        Event += "Вход осуществлен!";
                        SaveLoginToHistory(comboBoxEditLogin.Text);
                        if (customCheckBox1.Checked)
                            SettingsManager.SavePassword(formLogin, formPassword);
                        else
                            SettingsManager.ClearSavedPassword(formLogin);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        Event += $"Ошибка при получении данных пользователя: {ex.Message}";
                        MessageBox.Show(Event, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.DialogResult = DialogResult.Cancel;
                    }
                }
                else
                {
                    Event += $"Неверный пароль!";
                    MessageBox.Show(Event, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Event += $"Логин не найден!";
                MessageBox.Show(Event, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Debug.WriteLine(Event);
            await _loginFormDataService.EventForHistory(_user.UserId, Event);
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (e.Control)
                    customCheckBox1.Checked = true;
                simpleButton_Click(sender, e);
            }
        }
        private void LoadLoginHistory()
        {
            comboBoxEditLogin.Properties.Items.AddRange(SettingsManager.GetLoginHistory());
            comboBoxEditLogin.Text = SettingsManager.GetLoginHistory().LastOrDefault() ?? "";
        }

        private void SaveLoginToHistory(string login)
        {
            SettingsManager.AddLogin(login);
        }
        private void LoginForm_Shown(object sender, EventArgs e)
        {
            string login = comboBoxEditLogin.Text;

            if (!string.IsNullOrWhiteSpace(login))
            {
                string savedPassword = SettingsManager.GetSavedPassword(login);
                if (!string.IsNullOrWhiteSpace(savedPassword))
                {
                    textEditPassword.Text = savedPassword;
                    customCheckBox1.Visible = true;
                    customCheckBox1.Checked = true;
                }
                else
                {
                    customCheckBox1.Visible = false;
                    customCheckBox1.Checked = false;
                }
                textEditPassword.Focus();
            }
            else
            {
                comboBoxEditLogin.Focus();
            }
        }

        private void labelGlaz_MouseMove(object sender, MouseEventArgs e)
        {
            labelGlaz.Text = "👀";
            textEditPassword.Properties.UseSystemPasswordChar = false;
        }

        private void labelGlaz_MouseLeave(object sender, EventArgs e)
        {
            labelGlaz.Text = "👁";
            textEditPassword.Properties.UseSystemPasswordChar = true;
        }
    }

    public class LoginFormDataService
    {
        private readonly DatabaseHelper _dbHelper;
        public LoginFormDataService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public async Task<string> GetPasswordHash(string login)
        {
            string query = "SELECT PasswordHash FROM Users WHERE UserName = @Login";
            System.Data.DataTable sqlPassword = await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@Login", login } });
            return sqlPassword.Rows.Count > 0 ? sqlPassword.Rows[0]["PasswordHash"].ToString() ?? null : null;
        }
        public async Task<int> GetId(string login)
        {
            Debug.WriteLine(login);
            string query = "SELECT UserId FROM Users WHERE UserName = @Login";
            System.Data.DataTable sqlId = await _dbHelper.ExecuteQueryAsync(query, new Dictionary<string, object> { { "@Login", login } });
            return Convert.ToInt32(sqlId.Rows[0]["UserId"]);
        }
        public async Task EventForHistory(int UserId, string Event)
        {
            await ActionLogger.Log(UserId, Event, NameForm: "LoginForm");
        }
    }
}
