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

namespace SewingProduction.form.UserDistribution
{
    public partial class LoginForm : Form
    {
        private readonly LoginFormDataService _loginFormDataService;
        private readonly UserClass _user;
        private readonly IPasswordHasher _passwordHasher;
        private string loginHistoryFile = "login_data.js";
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
            Console.WriteLine(hashedPassword);
            string hashedPasswordFromDb = await _loginFormDataService.GetPasswordHash(formLogin);
            if (hashedPasswordFromDb != null)
            {
                Microsoft.AspNet.Identity.PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(hashedPasswordFromDb, formPassword);
                if (result == Microsoft.AspNet.Identity.PasswordVerificationResult.Success)
                {
                    try
                    {
                        _user.UserId = await _loginFormDataService.GetId(formLogin);
                        this.DialogResult = DialogResult.OK;
                        Event += "Вход осуществлен!";
                        SaveLoginToHistory(comboBoxEditLogin.Text);
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
            _loginFormDataService.EventForHistory(_user.UserId, Event);
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                simpleButton_Click(sender, e);
            }
        }
        private void LoadLoginHistory()
        {
            if (System.IO.File.Exists(loginHistoryFile))
            {
                string json = System.IO.File.ReadAllText(loginHistoryFile);
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

                if (data != null && data.ContainsKey("logins"))
                {
                    var logins = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(data["logins"].ToString());
                    if (logins.Count > 0)
                    {
                        comboBoxEditLogin.Properties.Items.AddRange(logins);
                        comboBoxEditLogin.Text = logins.Last();
                    }
                }
            }
        }

        private void SaveLoginToHistory(string login)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();

            if (System.IO.File.Exists(loginHistoryFile))
            {
                string json = System.IO.File.ReadAllText(loginHistoryFile);
                data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(json) ?? new Dictionary<string, object>();
            }

            List<string> logins = data.ContainsKey("logins")
                ? Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(data["logins"].ToString())
                : new List<string>();

            logins.RemoveAll(l => l.Equals(login, StringComparison.OrdinalIgnoreCase));
            logins.Add(login);

            data["logins"] = logins;

            string updatedJson = Newtonsoft.Json.JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
            System.IO.File.WriteAllText(loginHistoryFile, updatedJson);

            comboBoxEditLogin.Properties.Items.Clear();
            comboBoxEditLogin.Properties.Items.AddRange(logins);
        }
        private void LoginForm_Shown(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(comboBoxEditLogin.Text))
            {
                textEditPassword.Focus();
            }
            else
            {
                comboBoxEditLogin.Focus();
            }
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
