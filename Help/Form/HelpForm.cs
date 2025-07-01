using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils.Html;
using DevExpress.XtraEditors;
using SewingProduction.Core.Class.Settings;
using SewingProduction.form.UserDistribution;


namespace SewingProduction.Features.UserDistribution.Forms
{
    public partial class HelpForm : Form
    {
        private FormManager _formManager;
        public HelpForm(FormManager formManager)
        {
            InitializeComponent();
            _formManager = formManager ?? throw new ArgumentNullException(nameof(formManager));
        }
        private void HelpForm_Load(object sender, EventArgs e)
        {
            string projectRoot = Directory.GetParent(Application.StartupPath).Parent.Parent.Parent.FullName;
            string filePath = Path.Combine(projectRoot, "Help", "Str", "help.html");

            if (File.Exists(filePath))
            {
                WebBrowserHelp.Navigate(filePath);
            }
            else
            {
                MessageBox.Show("Файл справки не найден: " + filePath, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void webBrowserHelp_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (e.Url.Scheme == "menu")
            {
                e.Cancel = true;

                string encoded = e.Url.AbsoluteUri.Replace("menu:", "");
                string menuItemName = Uri.UnescapeDataString(encoded);

                ToolStripMenuItem item = _formManager.GetMenuItemByName(menuItemName);
                if (item != null)
                {
                    item.PerformClick();
                }
                else
                {
                    MessageBox.Show($"Пункт меню с именем '{menuItemName}' не найден.");
                }
            }
        }
        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (WebBrowserHelp.CanGoBack)
            {
                WebBrowserHelp.GoBack();
            }
        }
    }
}
