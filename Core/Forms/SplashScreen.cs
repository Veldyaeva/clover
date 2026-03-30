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
            imageSlider1.LayoutMode = DevExpress.Utils.Drawing.ImageLayoutMode.ZoomInside;
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
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "SewingProduction",
                "SplashImages");

            if (!Directory.Exists(settingsFolder))
            {
                return;
            }

            int seasonIndex = GetSeasonIndex(DateTime.Now.Month);
            string seasonFolder = Path.Combine(settingsFolder, $"Season_{seasonIndex}");

            string eventFolder = GetEventFolder(DateTime.Now, settingsFolder);

            var imageFiles = !string.IsNullOrEmpty(eventFolder)
                ? GetImageFiles(eventFolder)
                : new System.Collections.Generic.List<string>();

            if (imageFiles.Count == 0)
            {
                imageFiles = GetImageFiles(seasonFolder);
            }

            if (imageFiles.Count == 0)
            {
                imageFiles = GetImageFiles(settingsFolder);
            }

            if (imageFiles.Count == 0)
            {
                return;
            }

            var random = new Random();
            int startIndex = random.Next(imageFiles.Count);

            for (int i = 0; i < imageFiles.Count; i++)
            {
                string imageFile = imageFiles[(startIndex + i) % imageFiles.Count];
                if (TryLoadSingleImage(imageFile))
                {
                    return;
                }
            }
        }
        private static string GetEventFolder(DateTime date, string settingsFolder)
        {
            if (IsMarch8EventPeriod(date))
            {
                return Path.Combine(settingsFolder, "Events", "8.03");
            }

            if (IsApril1EventPeriod(date))
            {
                return Path.Combine(settingsFolder, "Events", "1.04");
            }

            return null;
        }

        private static bool IsNewYearEventPeriod(DateTime date)
        {
            DateTime eventDate = new DateTime(date.Year, 1, 1);
            return date.Date >= eventDate.AddDays(-7) && date.Date <= eventDate.AddDays(14);
        }
        private static bool IsFeb23EventPeriod(DateTime date)
        {
            DateTime eventDate = new DateTime(date.Year, 2, 23);
            return date.Date >= eventDate.AddDays(-4) && date.Date <= eventDate.AddDays(4);
        }
        private static bool IsMarch8EventPeriod(DateTime date)
        {
            DateTime eventDate = new DateTime(date.Year, 3, 8);
            return date.Date >= eventDate.AddDays(-4) && date.Date <= eventDate.AddDays(4);
        }
        private static bool IsApril1EventPeriod(DateTime date)
        {
            DateTime eventDate = new DateTime(date.Year, 4, 1);
            return date.Date == eventDate.Date;
        }

        private static int GetSeasonIndex(int month)
        {
            if (month >= 3 && month <= 5)
            {
                return 0; // spring
            }

            if (month >= 6 && month <= 8)
            {
                return 1; // summer
            }

            if (month >= 9 && month <= 11)
            {
                return 2; // autumn
            }

            return 3; // winter
        }

        private static System.Collections.Generic.List<string> GetImageFiles(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                return new System.Collections.Generic.List<string>();
            }

            return Directory.GetFiles(folderPath)
                .Where(IsImageFile)
                .OrderBy(path => path, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private bool TryLoadSingleImage(string imageFile)
        {
            try
            {
                using (var stream = new FileStream(imageFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var image = Image.FromStream(stream))
                {
                    imageSlider1.Images.Clear();
                    imageSlider1.Images.Add((Image)image.Clone());
                    AdjustSplashSizeToImage(image);
                    imageSlider1.CurrentImageIndex = 0;
                    return true;
                }
            }
            catch
            {
                // Ignore invalid or locked images.
                return false;
            }
        }

        private void AdjustSplashSizeToImage(Image image)
        {
            if (image.Width <= 0 || image.Height <= 0)
            {
                return;
            }

            Rectangle workingArea = Screen.PrimaryScreen?.WorkingArea ?? new Rectangle(0, 0, 933, 519);
            const double maxScreenFill = 0.82;

            double maxWidth = Math.Max(320, workingArea.Width * maxScreenFill);
            double maxHeight = Math.Max(240, workingArea.Height * maxScreenFill);
            double scale = Math.Min(maxWidth / image.Width, maxHeight / image.Height);
            scale = Math.Min(scale, 1.0);

            int targetWidth = Math.Max(320, (int)Math.Round(image.Width * scale));
            int targetHeight = Math.Max(240, (int)Math.Round(image.Height * scale));

            ClientSize = new Size(targetWidth, targetHeight);
            CenterToScreen();
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
