using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTabbedMdi;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNetCore.Identity;
using SewingProduction.form;
using SewingProduction.form.Nadezhda;
using SewingProduction.form.UserDistribution;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraTabbedMdi;
using Newtonsoft.Json;
using System.IO;
using SewingProduction.Features.UserDistribution.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;
using SewingProduction.Core.Class.Settings;
using System.Diagnostics;
using SewingProduction.Features.UserDistribution.Forms;
using SewingProduction.Forms;


//nemain

namespace SewingProduction
{
    public partial class SpMainForm : Form
    {
        public UserClass _user = new UserClass();
        private readonly IPasswordHasher _passwordHasher;
        private ToolStripMenuItem[] toolStripMenuItems; 
        private string loginHistoryFile = "settings.json";
        //private Dictionary<string, Form> openedForms = new Dictionary<string, Form>(); 
        public FormManager _formManager;
        private XtraTabbedMdiManager mdiManager => xtraTabbedMdiManager1;

        public SpMainForm()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            _passwordHasher = new PasswordHasher();
            //ThemeSelectorComboBox.Items.AddRange(ThemeManager.GetAvailableThemes().ToArray());
            //if (ThemeManager.CurrentTheme is null)
            //    ThemeSelectorComboBox.SelectedIndex = 0;
            //else
            //    ThemeSelectorComboBox.SelectedItem = ThemeManager.CurrentTheme;

            //// Обработчик смены темы
            //ThemeSelectorComboBox.SelectedIndexChanged += (sender, e) =>
            //{
            //    string selectedTheme = ThemeSelectorComboBox.SelectedItem.ToString();
            //    ThemeManager.SetTheme(selectedTheme);
            //};
        }


        private void отгрузкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private async void SpMainForm_Load(object sender, EventArgs e)
        {
            //RestoreOpenTabs();
            LoginForm loginForm = new LoginForm(_user);
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                this.WindowState = FormWindowState.Maximized;

                _formManager = new FormManager(this, menuStrip1, _user);
                await _user.LoadUserData();
                await _user.LoadObjectForm(this.Name);

                LoadObjectForm(); // Загружаем права доступа и применяем их
                await _formManager.RestoreOpenTabs();
                //RestoreOpenTabs();
            }
            else
            {
                this.Close();
            }
        }
        
        private void xtraTabbedMdiManager1_PageAdded(object sender, MdiTabPageEventArgs e)
        {
            XtraMdiTabPage page = e.Page;
            page.Tooltip = "Tooltip for the page " + page.Text;
        }
        int ctr = 0;
        void barItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Create an MDI child form.
            //CardByNom f = new CardByNom();
            //f.Text = "Child Form " + (++ctr).ToString();
            //f.MdiParent = this;
            //f.Show();
        }

       
        private void оборудованиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new SpravOborud(_user), sender);
        }

        private void карточкаРасчетаToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            //CardByNom newMDIChild = new CardByNom();
            //newMDIChild.MdiParent = this;
            //newMDIChild.Show();
            CardByNom f = new CardByNom();
            //f.Text = "Child Form " + (++ctr).ToString();
            f.MdiParent = this;
            f.Show();
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

        private void цехаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenForm(new SpravZeh(_user, "ZehList", "Справочник Цехов"), sender);
        }
        private void бригадыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new SpravBrig(_user, "spBrig", "Справочник Бригад"), sender);
        }
        private void видыПроизводстваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new SpravForAll("spVidProizv", rusNameTableSQL: "Справочник Вид произв", user: _user), sender);
        }
        private void работникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new Fio(_user, "fio", "Справочник работников"), sender);
        }
        private void оборудованиеВБригадахToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new OborudBrig(_user), sender);
        }

        private void изделияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Articul f = new Articul();
            //f.MdiParent = this;
            //f.Show();
            OpenForm(new Articul(), sender);
        }

        private void рабочийСтолМастераToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlanZagrBrig f = new PlanZagrBrig();
            f.MdiParent = this;
            f.Show();
        }
        
        private void TeamWorktoolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new TeamWork(), sender);
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void моделиСПризнакомМаркировкToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpravForAll f = new SpravForAll("spisok_t_id_nn_crpt", "snc_id,t_id,nn", "Список моделей для маркировки");
            f.MdiParent = this;
            f.Show();
            //OpenForm(new SpravForAll("spisok_t_id_nn_crpt", "snc_id,t_id,nn", "Список моделей для маркировки"), sender);
        }

        private void артикулToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Articul f = new Articul();
            f.MdiParent = this;
            f.Show();
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
            var helpForm = new HelpForm(this._formManager);
            helpForm.Show();
        }
        #endregion

        private void рабочийСтолМастераToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            PlanZagrBrig planZagrBrig = new PlanZagrBrig();
            planZagrBrig.MdiParent = this;
            planZagrBrig.Show();
        }

        private void карточкаРасчетаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CardByNom cardByNom = new CardByNom();
            cardByNom.MdiParent = this;
            cardByNom.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //PlanZagrBrig planZagrBrig = new PlanZagrBrig();
            //planZagrBrig.MdiParent = this;
            //planZagrBrig.Show();
        }


        private void оперативноеПланированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //KnittingProductionPlanning knittingProductionPlanning = new KnittingProductionPlanning();
            //knittingProductionPlanning.MdiParent = this;
            //knittingProductionPlanning.Show();
            OpenForm(new KnittingProductionPlanning(), sender);
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
            foreach (Control control in this.Controls)
            {
                if (control is MenuStrip menuStrip)
                {
                    ApplyPermissionsToMenuItems(menuStrip.Items);
                }
            }
        }
        private void ApplyPermissionsToMenuItems(ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {
                if (string.IsNullOrWhiteSpace(item.Name)) continue;

                string objectName = item.Tag as string ?? item.Name;

                bool hasWrite = _user.HasPermission(objectName, "Редактор");
                bool hasRead = _user.HasPermission(objectName, "Просмотр");

                item.Visible = hasRead || hasWrite;
                item.Enabled = hasWrite;
                Console.WriteLine(" Объект: " + objectName + " Чтение: " + hasRead + " Запись: " + hasWrite);
                // если это пункт меню с подменю — рекурсивно
                if (item is ToolStripMenuItem menuItem && menuItem.HasDropDownItems)
                {
                    ApplyPermissionsToMenuItems(menuItem.DropDownItems);
                }
            }
        }
        #endregion
        private void XtraTabbedMdiManager1_PageAdded(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
        {
            if (e.Page != null && e.Page.MdiChild != null)
            {
                e.Page.Text = TruncateWithEllipsis(e.Page.MdiChild.Text, 25); // обрезка с многоточием
                e.Page.Tooltip = e.Page.MdiChild.Text; // полное имя во всплывающей подсказке
            }
        }

        // Вспомогательный метод для обрезки
        private string TruncateWithEllipsis(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text) || text.Length <= maxLength)
                return text;
            return text.Substring(0, maxLength - 3) + "...";
        }

    }
}
