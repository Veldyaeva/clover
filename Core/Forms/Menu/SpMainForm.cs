using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.XtraBars;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTabbedMdi;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using SewingProduction.Core;
using SewingProduction.Core.Class.Settings;
using SewingProduction.Features.Articul;
using SewingProduction.Features.Articul.Reports;
using SewingProduction.Features.CuttingProduction.Forms;
using SewingProduction.Features.KnittingProduction.Forms;
using SewingProduction.Features.Sprav;
using SewingProduction.Features.Sprav.Forms;
using SewingProduction.Features.Tabel.Forms;
using SewingProduction.Features.TeamWork.Forms;
using SewingProduction.Features.UserDistribution.Class;
using SewingProduction.Features.UserDistribution.Forms;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.form;
using SewingProduction.Helpers;

namespace SewingProduction
{
    public partial class SpMainForm : Form
    {
        private readonly string _baseFormTitle;
        private readonly string _buildVersion;
        public UserClass _user = new UserClass();
        private readonly IPasswordHasher _passwordHasher;
        private ToolStripMenuItem[] toolStripMenuItems;
        public FormManager _formManager;
        private XtraTabbedMdiManager mdiManager => xtraTabbedMdiManager1;
        public DevExpress.XtraBars.BarManager MainBarManager => barManager1;

        public SpMainForm()
        {
            InitializeComponent();
            _baseFormTitle = Text;
            _buildVersion = AppVersionHelper.GetDisplayVersion();
            UserLookAndFeel.Default.StyleChanged += (_, __) =>
            {
                Properties.Settings.Default.AppSkin = UserLookAndFeel.Default.SkinName;
                Properties.Settings.Default.Save();
            };
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

                _formManager = new FormManager(this, barManager1, _user);
                await _user.LoadUserData();
                UpdateFormTitle();
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
        private void оПрограммеToolStripMenuItem_Click(object sender, ItemClickEventArgs e)
        {
            AboutBox f = new AboutBox();
            f.ShowDialog();
        }
        private void настройкиToolStripMenuItem_Click(object sender, ItemClickEventArgs e)
        {
            SettingsForm f = new SettingsForm(_user);
            f.ShowDialog();
        }
        private void профильToolStripMenuItem_Click(object sender, ItemClickEventArgs e)
        {
            OpenForm(new UserProfile(_user), e.Item);
        }
        private void помощьToolStripMenuItem_Click(object sender, ItemClickEventArgs e)
        {
            string helpPath = System.IO.Path.Combine(AppContext.BaseDirectory, "Help", "Help.html");
            showHelpForm(helpPath);
        }
        #endregion
        #region Справочники
        #region Оборудование
        private void оборудованиеВБригадахToolStripMenuItem_Click(object sender, ItemClickEventArgs e)
        {
            OpenForm(new OborudBrig(_user), e.Item);
        }
        private void оборудованиеToolStripMenuItem_Click(object sender, ItemClickEventArgs e)
        {
            OpenForm(new SpravOborud(_user), e.Item);
        }
        private void видыОборудованияToolStripMenuItem_Click(object sender, ItemClickEventArgs e)
        {
            OpenForm(new SpravForAll("oborud_shv_ob", rusNameTableSQL: "Справочник Группы об.", user: _user), e.Item);
        }
        private void матрицаКлассовToolStripMenuItem_Click(object sender, ItemClickEventArgs e)
        {
            OpenForm(new SpravForAll("matrix_class", rusNameTableSQL: "Справочник Клас. вяз. об.", user: _user), e.Item);
        }
        private void видОперацToolStripMenuItem_Click(object sender, ItemClickEventArgs e)
        {
            OpenForm(new SpravForAll("spOborudMachine", rusNameTableSQL: "Справочник Виды операций", user: _user), e.Item);
        }
        #endregion
        #region Бригады/цеха
        private void бригадыToolStripMenuItem_Click(object sender, ItemClickEventArgs e)
        {
            OpenForm(new SpravBrig(_user, "spBrig", "Справочник Бригад"), e.Item);
        }
        private void цехаToolStripMenuItem1_Click(object sender, ItemClickEventArgs e)
        {
            OpenForm(new SpravZeh(_user, "ZehList", "Справочник Цехов"), e.Item);
        }
        private void видыПроизводстваToolStripMenuItem_Click(object sender, ItemClickEventArgs e)
        {
            OpenForm(new SpravForAll("spVidProizv", rusNameTableSQL: "Справочник Вид произв", user: _user), e.Item);
        }
        #endregion
        private void карточкаРасчетаToolStripMenuItem1_Click(object sender, ItemClickEventArgs e)
        {
            OpenForm(new CardByNom(_user), e.Item);
        }
        private void работникиToolStripMenuItem_Click(object sender, ItemClickEventArgs e)
        {
            OpenForm(new Fio(_user, "fio", "Справочник работников"), e.Item);
        }
        private void тарифыToolStripMenuItem_Click(object sender, ItemClickEventArgs e)
        {
            OpenForm(new EditTarif(_user), e.Item);
        }
        private void наценкиToolStripMenuItem_Click(object sender, ItemClickEventArgs e)
        {
            OpenForm(new SpravForAll("grup_men", "men,name,koef", "", "справочник коэффициентов наценки", _user, false, false, false), e.Item);
        }
        private void моделиСПризнакомМаркировкToolStripMenuItem_Click(object sender, ItemClickEventArgs e)
        {
            OpenForm(new SpravForAll("spisok_t_id_nn_crpt", "snc_id,t_id,nn", rusNameTableSQL: "Список моделей для маркировки"), e.Item);
        }
        #region Виды браков пряжи
        private void видыБраковНосковToolStripMenuItem_Click(object sender, ItemClickEventArgs e)
        {
            OpenForm(new SpravForAll("view_NameDefectsSpisPryzSocks", "*", "", "Виды браков пряжи - Носки", user: _user, servBrok: false), e.Item);
        }
        #endregion
        #endregion
        #region Производство
        #region Вязальное производство
        private void оперативноеПланированиеToolStripMenuItem_Click(object sender, ItemClickEventArgs e)
        {
            OpenForm(new KnittingProductionPlanning(_user), e.Item);
        }
        private void рабочийСтолМастераВязЦехаToolStripMenuItem1_Click(object sender, ItemClickEventArgs e)
        {
            OpenForm(new PlanZagrVyaz(CurrentUser.User, 1), e.Item);
        }
        private void рабочийСтолВязальщицыToolStripMenuItem_Click(object sender, ItemClickEventArgs e)
        {
            var form = AppServices.Services.GetRequiredService<KnitterWorkSpace>();
            OpenForm(form, e.Item);
        }
        private void аналитикаToolStripMenuItem_Click(object sender, ItemClickEventArgs e)
        {
            OpenForm(new KnittingProductionAnalytics(), e.Item);
        }
        private void barButtonItemSteamMasterWorkTable_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(new PlanZagrVyaz(CurrentUser.User, 2), e.Item);
        }
        private void barButtonItemCutMasterWorkTable_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(new PlanZagrVyaz(CurrentUser.User, 3), e.Item);
        }
        #endregion
        #region Швейное производство
        private void рабочийСтолМастераToolStripMenuItem_Click(object sender, ItemClickEventArgs e)
        {
            OpenForm(new PlanZagrBrig(), e.Item);
        }
        private void раскройныйЦехToolStripMenuItem_Click(object sender, ItemClickEventArgs e)
        {
            OpenForm(new CuttingForm(), e.Item);
        }
        #endregion
        #endregion
        #region Технологическая схема
        private void TeamWorktoolStripMenuItem_Click(object sender, ItemClickEventArgs e)
        {
            OpenForm(new TeamWork(_user), e.Item);
        }
        #endregion
        #region Артикул
        private void артикулToolStripMenuItem_Click(object sender, ItemClickEventArgs e)
        {
            OpenForm(new Articul(_user), e.Item);
        }
        #endregion
        #region Карточка расчета
        private void карточкаРасчетаToolStripMenuItem_Click(object sender, ItemClickEventArgs e)
        {
            OpenForm(new CardByNom(_user), e.Item);
        }
        #endregion
        #region Отчеты
        private void barBtnPublicArticul_ItemClick(object sender, ItemClickEventArgs e)
        {
            var report = new PrintPublicArticul();
            report.ShowPreviewDialog(); 
        }
        #endregion
        #region Табель
        private void табельToolStripMenuItem_Click(object sender, ItemClickEventArgs e)
        {
            OpenForm(new TabelMain(_user), e.Item);
        }
        #endregion

        #region процедуры
        /// <summary>
        /// Универсальное открытие формы, если форма открыта, сделает активной
        /// </summary>
        /// <param name="form">Конструктор формы</param>
        /// <param name="Item">Пункт меню (объект или название) (Можно не передавать)</param>
        public void OpenForm(Form form, object Item = null)
        {
            _formManager.OpenForm(form, Item);
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
        public void LoadObjectForm()
        {
            if (barManager1 != null)
                barManager1.ApplyPermissions(_user);
        }

        public void UpdateFormTitle()
        {
            var title = $"{_baseFormTitle}  v{_buildVersion} ({GetAppBitness()})";
            Text = string.IsNullOrWhiteSpace(_user?.UserName)
                ? title
                : $"{title}  - {_user.UserName}";
        }

        private static string GetAppBitness()
        {
            return Environment.Is64BitProcess ? "x64" : "x86";
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

        #region help
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
            helpForm.ShowDialog();
        }
        private void справкаtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new SewingProduction.HelpAdmin.Forms.AdminHelpEditorForm(this._formManager);
            f.Show();
        }
        #endregion
        #region скриншот окна 
        private void barButtonItemScreen_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                string screenshotPath = SaveWindowScreenshot(this);

                MessageBox.Show(
                    $"Скриншот окна сохранен:\n{screenshotPath}",
                    "Скриншот",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Process.Start(new ProcessStartInfo
                {
                    FileName = screenshotPath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Ошибка при создании скриншота:\n{ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private string SaveWindowScreenshot(Form form)
        {
            Rectangle bounds = form.Bounds;

            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(
                        new Point(bounds.Left, bounds.Top),
                        Point.Empty,
                        bounds.Size);
                }

                string folderPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                    "SewingProductionScreenshots");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                string fileName = $"WindowScreenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                string fullPath = Path.Combine(folderPath, fileName);

                bitmap.Save(fullPath, ImageFormat.Png);

                return fullPath;
            }
        }
        #endregion


    }
}
