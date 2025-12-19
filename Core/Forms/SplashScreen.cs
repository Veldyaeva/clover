using System;
using System.Globalization;
using System.Windows.Forms;

namespace SewingProduction
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
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
    }
}
