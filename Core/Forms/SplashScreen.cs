using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
            Random random = new Random();
            int minValue = 3;
            int maxValue = 16; 

            int randomNumber = random.Next(minValue, maxValue + 1);

            int month = DateTime.Now.Month;
            DateTime today = DateTime.Now.Date;
            if (month >= 3 && month <= 5)
            {
                imageSlider1.CurrentImageIndex = randomNumber;//spring
            }
            else if (month >= 6 && month <= 8)
            {
                minValue = 9;
                maxValue = 16;
                randomNumber = random.Next(minValue, maxValue + 1);
                imageSlider1.CurrentImageIndex = randomNumber;//summer
            }
            else if (month >= 9 && month <= 11)
            {
                minValue = 17;
                maxValue = 38;
                randomNumber = random.Next(minValue, maxValue + 1);
                imageSlider1.CurrentImageIndex = randomNumber;//fall
                //imageSlider1.CurrentImageIndex = 3;
            }
            else
            {
                imageSlider1.CurrentImageIndex = 0;//winter
            }
            DateTime sprStart = DateTime.ParseExact("03-05", "MM-dd", CultureInfo.InvariantCulture);
            DateTime sprEnd = DateTime.ParseExact("03-11", "MM-dd", CultureInfo.InvariantCulture);
            if (today >= sprStart&& today<=sprEnd)
            {
                imageSlider1.CurrentImageIndex = 4;//8.03
            }
            if (today == DateTime.ParseExact("02-14", "MM-dd", CultureInfo.InvariantCulture))
                { imageSlider1.CurrentImageIndex = 7; }//14.02
            if (today >= DateTime.ParseExact("02-17", "MM-dd", CultureInfo.InvariantCulture)&&today<=DateTime.ParseExact("02-25", "MM-dd", CultureInfo.InvariantCulture))
            { imageSlider1.CurrentImageIndex = 8;}//23.02
        }
    }
}
