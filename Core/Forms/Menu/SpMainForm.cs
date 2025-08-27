using DevExpress.XtraBars;
using DevExpress.XtraTabbedMdi;
using SewingProduction.form;
using SewingProduction.form.Nadezhda;
using System;
using System.Windows.Forms;
using SewingProduction.Features.UserDistribution.Helpers;
using Microsoft.AspNet.Identity;
using SewingProduction.Core.Class.Settings;
using SewingProduction.Features.UserDistribution.Forms;
using SewingProduction.Features.TeamWork;
using System.Diagnostics;
using SewingProduction.Features.TeamWork.Forms;
using SewingProduction.Features.Sprav;
using SewingProduction.Features.Articul;
using SewingProduction.Features.UserDistribution.Class;
using SewingProduction.Features.KnittingProduction.Forms;

namespace SewingProduction
{
    public partial class SpMainForm : Form
    {
        public UserClass _user = new UserClass();
        private readonly IPasswordHasher _passwordHasher;
        private ToolStripMenuItem[] toolStripMenuItems;
        public FormManager _formManager;
        private XtraTabbedMdiManager mdiManager => xtraTabbedMdiManager1;

        public SpMainForm()
        {
            InitializeComponent();
            UserFilePaths.EnsureFolderExists();
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            _passwordHasher = new PasswordHasher();
        }
        private async void SpMainForm_Load(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm(_user);
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                this.WindowState = FormWindowState.Maximized;

                _formManager = new FormManager(this, menuStrip1, _user);
                await _user.LoadUserData();
                CurrentUser.SetUser(_user);
                await _user.LoadObjectForm(this.Name);

                LoadObjectForm();
                if (SettingsManager.GetSaveOpenTabs())
                    await _formManager.RestoreOpenTabs();
            }
            else
            {
                this.Close();
            }
        }

        #region МЕНЮ
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox f = new AboutBox();
            f.Show();
        }
        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm f = new SettingsForm(_user);
            f.Show();
        }
        private void профильToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new UserProfile(_user), sender);
        }
        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string helpPath = System.IO.Path.Combine(AppContext.BaseDirectory, "Help", "Help.html");
            showHelpForm(helpPath);
        }
        #endregion
        #region Справочники
        #region Оборудование
        private void оборудованиеВБригадахToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new OborudBrig(_user), sender);
        }
        private void оборудованиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new SpravOborud(_user), sender);
        }
        private void видыОборудованияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new SpravForAll("oborud_shv_ob", rusNameTableSQL: "Справочник Группы об.", user: _user), sender);
        }
        private void матрицаКлассовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new SpravForAll("matrix_class", rusNameTableSQL: "Справочник Клас. вяз. об.", user: _user), sender);
        }
        private void видОперацToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new SpravForAll("spOborudMachine", rusNameTableSQL: "Справочник Виды операций", user: _user), sender);
        }
        #endregion
        #region Бригады/цеха
        private void бригадыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new SpravBrig(_user, "spBrig", "Справочник Бригад"), sender);
        }
        private void цехаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenForm(new SpravZeh(_user, "ZehList", "Справочник Цехов"), sender);
        }
        private void видыПроизводстваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new SpravForAll("spVidProizv", rusNameTableSQL: "Справочник Вид произв", user: _user), sender);
        }
        #endregion
        private void карточкаРасчетаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenForm(new CardByNom(), sender);
        }
        private void работникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new Fio(_user, "fio", "Справочник работников"), sender);
        }
        private void тарифыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new EditTarif(_user), sender);
        }
        private void изделияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new Articul(_user), sender);
        }
        private void моделиСПризнакомМаркировкToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new SpravForAll("spisok_t_id_nn_crpt", "snc_id,t_id,nn", rusNameTableSQL: "Список моделей для маркировки"), sender);
        }
        #region Виды браков пряжи
        private void видыБраковНосковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new SpravForAll("view_NameDefectsSpisPryzSocks", "*", "", "Виды браков пряжи - Носки", user: _user, servBrok: false), sender);
        }
        #endregion
        #endregion
        #region Производство
        private void оперативноеПланированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new KnittingProductionPlanning(), sender);
        }
        private void рабочийСтолМастераToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new PlanZagrBrig(), sender);
        }
        #endregion
        private void TeamWorktoolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new TeamWork(_user), sender);
        }
        private void артикулToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new Articul(_user), sender);
        }
        private void карточкаРасчетаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new CardByNom(), sender);
        }
        #region процедуры
        /// <summary>
        /// Универсальное открытие формы, если форма открыта, сделает активной
        /// </summary>
        /// <param name="form">Конструктор формы</param>
        /// <param name="sender">Пункт меню (объект или название) (Можно не передавать)</param>
        public void OpenForm(Form form, object sender = null)
        {
            _formManager.OpenForm(form, sender);
        }

        private void SpMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SettingsManager.GetSaveOpenTabs())
                SaveOpenTabsSafe();
        }
        public void SaveOpenTabsSafe()
        {
            if (!string.IsNullOrWhiteSpace(_user?.UserName))
                _formManager.SaveOpenTabs();
        }

        /// <summary>
        /// Видимость для обьектов (в меню)
        /// </summary>
        private void LoadObjectForm()
        {
            var scanner = new MenuScanner(null, _user);
            foreach (Control control in this.Controls)
            {
                if (control is MenuStrip menuStrip)
                {
                    scanner.ApplyPermissionsToMenu(menuStrip);
                }
            }
        }
        private void XtraTabbedMdiManager1_PageAdded(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
        {
            if (e.Page != null && e.Page.MdiChild != null)
            {
                string fullText = e.Page.MdiChild.Text;
                bool shortNames = SettingsManager.GetShortTabNames(); // новая настройка

                if (shortNames)
                {
                    e.Page.Text = TruncateWithEllipsis(fullText, 25);
                    xtraTabbedMdiManager1.TabPageWidth = 150;
                }
                else
                {
                    e.Page.Text = fullText;
                    xtraTabbedMdiManager1.TabPageWidth = 0;
                }

                e.Page.Tooltip = fullText; // полное имя во всплывающей подсказке
            }
        }

        // Вспомогательный метод для обрезки
        private string TruncateWithEllipsis(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
                return text;
            return text.Substring(0, maxLength - 3) + "...";
        }

        #endregion
        private void SpMainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                showHelpForm();
            }
        }
        private void кнопкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showHelpForm();
        }
        private void showHelpForm(string filePath = null)
        {
            var helpForm = new HelpForm(this._formManager, filePath);
            helpForm.Show();
        }

        private void рабочийСтолМастераToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenForm(new PlanZagrVyaz(), sender);
        }
    }
}
