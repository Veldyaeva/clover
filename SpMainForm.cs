using DevExpress.XtraBars;
using DevExpress.XtraTabbedMdi;
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
            //CardByNom newMDIChild = new CardByNom();
            //newMDIChild.MdiParent = this;
            //newMDIChild.Show();
            CardByNom f = new CardByNom();
            //f.Text = "Child Form " + (++ctr).ToString();
            f.MdiParent = this;
            f.Show();
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
    }
}
