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
using Microsoft.AspNetCore.Identity;

namespace SewingProduction.form.UserDistribution
{
    public partial class LoginForm : Form
    {
        private readonly LoginFormDataService _loginFormDataService;
        private readonly UserClass _user;
        private readonly IPasswordHasher<UserClass> _passwordHasher;
        public LoginForm(UserClass user)
        {
            InitializeComponent();
            _user = user ?? throw new ArgumentNullException(nameof(user));
            _passwordHasher = new PasswordHasher<UserClass>();

            DatabaseHelper dbHelper = new DatabaseHelper("ace");
            _loginFormDataService = new LoginFormDataService(dbHelper);
        }

        private async void simpleButton_Click(object sender, EventArgs e)
        {
            string formLogin = textEditLogin.Text;
            string formPassword = textEditPassword.Text;

            //string hashedPassword = _passwordHasher.HashPassword(null, formPassword);
            string hashedPasswordFromDb = await _loginFormDataService.GetPasswordHash(formLogin);
            if (hashedPasswordFromDb != null)
            {
                PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(null, hashedPasswordFromDb, formPassword);
                if (result == PasswordVerificationResult.Success)
                {
                    try
                    {
                        _user.UserId = await _loginFormDataService.GetId(formLogin);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при получении данных пользователя: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.DialogResult = DialogResult.Cancel;
                    }
                }
                else
                {
                    labelError.Visible = true;
                    labelError.Text = "Пароль не правильный";
                }
            }
            else
            {
                labelError.Visible = true;
                labelError.Text = "Логин не найден";
            }
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                simpleButton_Click(sender, e);
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
    }
}
