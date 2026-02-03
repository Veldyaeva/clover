using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SewingProduction
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
            LoadBackgroundImagesFromSettings();
            /*Random random = new Random();
            int minValue = 0;
            int maxValue = 15;

            int randomNumber = random.Next(minValue, maxValue + 1);

            int month = DateTime.Now.Month;
            DateTime today = DateTime.Now.Date;
            if (month >= 3 && month <= 5)
            {
                imageSlider1.CurrentImageIndex = randomNumber;//spring
            }
            else if (month >= 6 && month <= 8)
            {
                minValue = 8;
                maxValue = 16;
                randomNumber = random.Next(minValue, maxValue + 1);
                imageSlider1.CurrentImageIndex = randomNumber;//summer
            }
            else if (month >= 9 && month <= 11)
            {
                minValue = 16;
                maxValue = 36;
                randomNumber = random.Next(minValue, maxValue + 1);
                imageSlider1.CurrentImageIndex = randomNumber;//fall
                //imageSlider1.CurrentImageIndex = 3;
            }
            else
            {
                minValue = 37;
                maxValue = 49;
                randomNumber = random.Next(minValue, maxValue + 1);
                imageSlider1.CurrentImageIndex = randomNumber;//winter
            }
            DateTime sprStart = DateTime.ParseExact("03-05", "MM-dd", CultureInfo.InvariantCulture);
            DateTime sprEnd = DateTime.ParseExact("03-11", "MM-dd", CultureInfo.InvariantCulture);
            if (today >= sprStart && today <= sprEnd)
            {
                imageSlider1.CurrentImageIndex = 3;//8.03
            }
            if (today == DateTime.ParseExact("02-14", "MM-dd", CultureInfo.InvariantCulture))
            { imageSlider1.CurrentImageIndex = 1; }//14.02
            if (today >= DateTime.ParseExact("02-17", "MM-dd", CultureInfo.InvariantCulture) && today <= DateTime.ParseExact("02-25", "MM-dd", CultureInfo.InvariantCulture))
            { imageSlider1.CurrentImageIndex = 2; }//23.02
            */
        }

        private void LoadBackgroundImagesFromSettings()
        {
            string settingsFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "SewingProduction",
                "settings");

            if (!Directory.Exists(settingsFolder))
            {
                return;
            }

            var imageFiles = Directory.GetFiles(settingsFolder)
                .Where(IsImageFile)
                .OrderBy(path => path, StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (imageFiles.Count == 0)
            {
                return;
            }

            imageSlider1.Images.Clear();

            foreach (string imageFile in imageFiles)
            {
                try
                {
                    using (var stream = new FileStream(imageFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var image = Image.FromStream(stream))
                    {
                        imageSlider1.Images.Add((Image)image.Clone());
                    }
                }
                catch
                {
                    // Ignore invalid or locked images.
                }
            }

            if (imageSlider1.Images.Count > 0)
            {
                imageSlider1.CurrentImageIndex = 0;
            }
        }

        private static bool IsImageFile(string path)
        {
            string extension = Path.GetExtension(path);
            return extension.Equals(".png", StringComparison.OrdinalIgnoreCase)
                || extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase)
                || extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase)
                || extension.Equals(".bmp", StringComparison.OrdinalIgnoreCase)
                || extension.Equals(".gif", StringComparison.OrdinalIgnoreCase);
        }
    }
}
