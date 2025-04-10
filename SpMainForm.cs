using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTabbedMdi;
using Microsoft.AspNetCore.Identity;
using SewingProduction.form;
using SewingProduction.form.UserDistribution;
using SewingProduction.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//nemain

namespace SewingProduction
{
    public partial class SpMainForm : Form
    {
        UserClass _user = new UserClass();
        private readonly IPasswordHasher<UserClass> _passwordHasher;
        private ToolStripMenuItem[] toolStripMenuItems;
        public SpMainForm()
        {
            InitializeComponent();
            _passwordHasher = new PasswordHasher<UserClass>();
            ThemeSelectorComboBox.Items.AddRange(ThemeManager.GetAvailableThemes().ToArray());
            if (ThemeManager.CurrentTheme is null) { ThemeSelectorComboBox.SelectedIndex = 0; }
            else
            {
                ThemeSelectorComboBox.SelectedItem = ThemeManager.CurrentTheme;
            }
            // Обработчик смены темы
            ThemeSelectorComboBox.SelectedIndexChanged += (sender, e) =>
            {
                string selectedTheme = ThemeSelectorComboBox.SelectedItem.ToString();
                ThemeManager.SetTheme(selectedTheme);
            };
        }


        XtraTabbedMdiManager mdiManager;
        private void отгрузкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private async void SpMainForm_Load(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm(_user);
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                this.WindowState = FormWindowState.Maximized;

                await _user.LoadUserData();
                await _user.LoadObjectForm(this.Name);

                LoadObjectForm(); // Загружаем права доступа и применяем их
            }
            else
            {
                this.Close();
            }
            //// Create a Bar Manager that will display a bar of commands at the top of the main form.
            //BarManager barManager = new BarManager();
            //barManager.Form = this;
            //// Create a bar with a New button.
            //barManager.BeginUpdate();
            //Bar bar = new Bar(barManager, "My Bar");
            //bar.DockStyle = BarDockStyle.Top;
            //barManager.MainMenu = bar;
            //BarItem barItem = new BarButtonItem(barManager, "New");
            //barItem.ItemClick += new ItemClickEventHandler(barItem_ItemClick);
            //bar.ItemLinks.Add(barItem);
            //barManager.EndUpdate();
            //// Create an XtraTabbedMdiManager that will manage MDI child windows.
            ////mdiManager = new XtraTabbedMdiManager(components);
            ////mdiManager.MdiParent = this;
            ////mdiManager.PageAdded += xtraTabbedMdiManager1_PageAdded;
        }
#region Видимость для обьектов (в меню)
        private void LoadObjectForm()
        {
            foreach (Control control in this.Controls)
            {
                if (control is MenuStrip menuStrip)
                {
                    ApplyPermissionsToMenuItems(menuStrip.Items);
                }
            }
            //toolStripMenuItems = new ToolStripMenuItem[] { 
            //    МенюToolStripMenuItem, 
            //        профильToolStripMenuItem,
            //        настройкиToolStripMenuItem,
            //        оПрограммеToolStripMenuItem,
            //        помощьToolStripMenuItem,
            //    справочникиToolStripMenuItem,
            //        оборудованиеToolStripMenuItem,
            //            оборудованиеВБригадахToolStripMenuItem,
            //            оборудованиеToolStripMenuItem1,
            //            видыОборудованияToolStripMenuItem,
            //            матрицыКлассовToolStripMenuItem,
            //            видыОперацийToolStripMenuItem,
            //        бригадыЦехаToolStripMenuItem,
            //            бригадыToolStripMenuItem,
            //            цехаToolStripMenuItem,
            //            видыПроизводствToolStripMenuItem,
            //        карточкаРасчетаToolStripMenuItem1,
            //        работникиToolStripMenuItem,
            //        тарифыToolStripMenuItem,
            //        изделияToolStripMenuItem,
            //        моделиСПризнакомМаркировкToolStripMenuItem,
            //    производствоToolStripMenuItem,
            //        рабочийСтолМастераToolStripMenuItem,
            //    TeamWorktoolStripMenuItem,
            //    артикулToolStripMenuItem
            //};
            //foreach (ToolStripMenuItem menuItem in toolStripMenuItems)
            //{
            //    bool hasWrite = _user.HasPermission(menuItem.Name, "Редактор");
            //    bool hasRead = _user.HasPermission(menuItem.Name, "Просмотр");
            //    menuItem.Visible = hasRead || hasWrite ? true : false;
            //    menuItem.Enabled = hasWrite;
            //}
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

                // если это пункт меню с подменю — рекурсивно
                if (item is ToolStripMenuItem menuItem && menuItem.HasDropDownItems)
                {
                    ApplyPermissionsToMenuItems(menuItem.DropDownItems);
                }
            }
        }
        #endregion

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
            SpravOborud f = new SpravOborud(_user);
            f.MdiParent = this;
            f.Show();
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
            SpravForAll f = new SpravForAll("oborud_shv_ob", rusNameTableSQL: "Справочник Группы об.");
            f.MdiParent = this;
            f.Show();
        }

        private void матрицаКлассовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpravForAll f = new SpravForAll("matrix_class", rusNameTableSQL: "Справочник Клас. вяз. об.");
            f.MdiParent = this;
            f.Show();
        }

        private void видОперацToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpravForAll f = new SpravForAll("spOborudMachine", rusNameTableSQL: "Справочник Виды операций");
            f.MdiParent = this;
            f.Show();
        }

        private void цехаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SpravZeh f = new SpravZeh(_user, "ZehList", "Справочник Цехов");
            f.MdiParent = this;
            f.Show();
        }
        private void бригадыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpravBrig f = new SpravBrig(_user, "spBrig", "Справочник Бригад");
            f.MdiParent = this;
            f.Show();
        }
        private void видыПроизводстваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpravForAll f = new SpravForAll("spVidProizv", rusNameTableSQL: "Справочник Вид произв");
            f.MdiParent = this;
            f.Show();
        }
        private void работникиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Fio f = new Fio(_user,"fio", "Справочник работников");
            f.MdiParent = this;
            f.Show();
        }
        private void оборудованиеВБригадахToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OborudBrig f = new OborudBrig(_user);
            f.MdiParent = this;
            f.Show();

        }

        private void изделияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Articul f = new Articul();
            f.MdiParent = this;
            f.Show();
        }

        private void рабочийСтолМастераToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlanZagrBrig f = new PlanZagrBrig();
            f.MdiParent = this;
            f.Show();
        }
        
        private void TeamWorktoolStripMenuItem_Click(object sender, EventArgs e)
        {
            TeamWork teamWork = new TeamWork();
            teamWork.MdiParent = this;
            teamWork.Show();

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void моделиСПризнакомМаркировкToolStripMenuItem_Click(object sender, EventArgs e)
        {
        
            SpravForAll f = new SpravForAll("spisok_t_id_nn_crpt", "snc_id,t_id,nn", "Список моделей для маркировки");
            f.MdiParent = this;
            f.Show();
        
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
            UserProfile f = new UserProfile(_user);
            f.MdiParent = this;
            f.Show();
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void рабочийСтолМастераToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            PlanZagrBrig planZagrBrig = new PlanZagrBrig();
            planZagrBrig.MdiParent = this;
            planZagrBrig.Show();
        }
    }
}
