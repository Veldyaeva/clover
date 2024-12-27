using DevExpress.XtraBars;
using DevExpress.XtraTabbedMdi;
using SewingProduction.form;
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
        public SpMainForm()
        {
            InitializeComponent();
        }

        XtraTabbedMdiManager mdiManager;
        private void отгрузкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void SpMainForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
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
            SpravOborud f = new SpravOborud();
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
            SpravForAll f = new SpravForAll("oborud_shv_ob", "Справочник Группы оборудования");
            f.MdiParent = this;
            f.Show();
        }

        private void матрицаКлассовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpravForAll f = new SpravForAll("matrix_class", "Справочник Клас. вяз. об.");
            f.MdiParent = this;
            f.Show();
        }

        private void видОперацToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpravForAll f = new SpravForAll("spOborudMachine", "Справочник Виды операций");
            f.MdiParent = this;
            f.Show();
        }

        private void оборудованиеВБригадахToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OborudBrig f = new OborudBrig();
            f.MdiParent = this;
            f.Show();

        }

        private void цехаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SpravZeh f = new SpravZeh("ZehList", "Справочник Цехов");
            f.MdiParent = this;
            f.Show();
        }
        private void бригадыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpravBrig f = new SpravBrig("spBrig", "Справочник Бригад");
            f.MdiParent = this;
            f.Show();
        }

        private void видыПроизводстваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpravForAll f = new SpravForAll("spVidProizv", "Справочник Вид произв");
            f.MdiParent = this;
            f.Show();
        }

    }
}
