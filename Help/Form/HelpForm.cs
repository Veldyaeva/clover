using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils.Html;
using DevExpress.XtraEditors;
using SewingProduction.Core.Class.Settings;
using SewingProduction.Features.UserDistribution.Forms;


namespace SewingProduction.Features.UserDistribution.Forms
{
    public partial class HelpForm : Form
    {
        private FormManager _formManager;
        string _filePath;
        public HelpForm(FormManager formManager, string filePath = null)
        {
            InitializeComponent();
            _formManager = formManager ?? throw new ArgumentNullException(nameof(formManager));
            _filePath = filePath;
        }
        private void HelpForm_Load(object sender, EventArgs e)
        {
            string projectRoot = AppContext.BaseDirectory;

            Debug.WriteLine("1)" + projectRoot);
            string fileToOpen = _filePath;
            Debug.WriteLine("2)" + fileToOpen);
            if (string.IsNullOrEmpty(fileToOpen))
            {
                var activeForm = _formManager.GetActiveForm();

                Debug.WriteLine("3)" + activeForm);
                if (activeForm != null)
                {
                    var type = activeForm.GetType();
                    var ns = type.Namespace ?? "";
                    var className = type.Name;

                    // Убираем только "SewingProduction." в начале, оставляем Features
                    string relativeNamespace = ns.StartsWith("SewingProduction.")
                        ? ns.Substring("SewingProduction.".Length)
                        : ns;

                    Debug.WriteLine("4)" + relativeNamespace);

                    string relativePath = Path.Combine(
                        relativeNamespace.Replace('.', Path.DirectorySeparatorChar),
                        className + ".html"
                    );

                    Debug.WriteLine("5)" + relativePath);

                    fileToOpen = Path.Combine(projectRoot, "Help", relativePath);
                }
                else
                {
                    fileToOpen = Path.Combine(projectRoot, "Help", "Help.html");
                }
            }
            Debug.WriteLine("6)" + fileToOpen);

            if (File.Exists(fileToOpen))
            {
                WebBrowserHelp.Navigate(fileToOpen);
            }
            else
            {
                WebBrowserHelp.DocumentText = $"<html><body><h2 style='color:red'>Файл справки не найден:<br>{fileToOpen}</h2></body></html>";
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
