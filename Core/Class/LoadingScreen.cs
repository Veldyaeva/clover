using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SewingProduction.Core.Class
{
    public class LoadingScreen
    {
        private Panel overlay = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.FromArgb(160, Color.White), // полупрозрачный серый
            Visible = false
        };
        private PictureBox spinnerPb = new PictureBox
        {
            SizeMode = PictureBoxSizeMode.CenterImage,
            Size = new Size(64, 64),
            Anchor = AnchorStyles.None
        };
        public void ShowOverlay() => overlay.Visible = true;
        public void HideOverlay() => overlay.Visible = false;
        private void CenterSpinner()
        {
            if (overlay == null || spinnerPb == null) return;
            spinnerPb.Left = (overlay.ClientSize.Width - spinnerPb.Width) / 2;
            spinnerPb.Top = (overlay.ClientSize.Height - spinnerPb.Height) / 2;
        }
        public void CreateOverlaySpinner(Form form)
        {

            // Путь к GIF-файлу; положите файл рядом с .exe или укажите абсолютный путь.
            string gifPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "loading_dude.gif");
            if (File.Exists(gifPath))
            {
                spinnerPb.Image = Image.FromFile(gifPath); // сохраняет анимацию
            }
            else
            {
                MessageBox.Show("GIF не найден: " + gifPath);
            }

            // Центрируем PictureBox внутри оверлея
            overlay.Controls.Add(spinnerPb);
            overlay.ControlAdded += (s, e) => CenterSpinner();

            form.Controls.Add(overlay);
            overlay.BringToFront();

            // при изменении размера формы — заново центрировать
            form.Resize += (s, e) => CenterSpinner();
        }
    }

}
