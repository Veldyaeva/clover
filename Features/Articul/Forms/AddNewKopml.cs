using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.Articul.Forms
{
    public partial class AddNewKopml : CustomForm // FoxPro: kompl_new_2016
    {
        public AddNewKopml()
        {
            InitializeComponent();
        }

        private void customNumericUpDownValueTab_ValueChanged(object sender, EventArgs e)
        {
            int count = (int)customNumericUpDownValueTab.Value;
            if (count < 1 || count > 10)
                return;

            // Удаляем все вкладки, кроме первой
            while (customTabControlKomplRazm.TabPages.Count > 1)
            {
                customTabControlKomplRazm.TabPages.RemoveAt(customTabControlKomplRazm.TabPages.Count - 1);
            }

            // Клонируем первую вкладку
            for (int i = 2; i <= count; i++)
            {
                var newPage = new DevExpress.XtraTab.XtraTabPage
                {
                    Name = $"xtraTabPageKomplRazm{i}",
                    Text = i.ToString()
                };

                // Копируем содержимое первой вкладки (один Control)
                var originalControl = customTabControlKomplRazm.TabPages[0].Controls[0];
                var clonedControl = (Control)Activator.CreateInstance(originalControl.GetType());

                CopyControlProperties(originalControl, clonedControl);
                clonedControl.Dock = DockStyle.Fill;

                newPage.Controls.Add(clonedControl);
                customTabControlKomplRazm.TabPages.Add(newPage);
            }

        }
        private void CopyControlProperties(Control source, Control target)
        {
            target.Text = source.Text;
            target.Font = source.Font;
            target.Dock = source.Dock;
            target.Size = source.Size;
            target.MinimumSize = source.MinimumSize;
            target.MaximumSize = source.MaximumSize;
            target.Margin = source.Margin;
            target.Padding = source.Padding;
            target.BackColor = source.BackColor;
            target.ForeColor = source.ForeColor;
        }

        private void customButtonNext_Click(object sender, EventArgs e)
        {
            if (customTabControlKomplRazm.SelectedTabPageIndex < customTabControlKomplRazm.TabPages.Count - 1)
            {
                customTabControlKomplRazm.SelectedTabPageIndex++;
            }
        }

        private void customButtonBack_Click(object sender, EventArgs e)
        {
            if (customTabControlKomplRazm.SelectedTabPageIndex > 0)
            {
                customTabControlKomplRazm.SelectedTabPageIndex--;
            }
        }
    }
}
