using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction
{
    #region green
    /* public static class Theme
     {
         // Общие шрифты
         public static readonly Font DefaultFont = new Font("Arial", 12, FontStyle.Regular);

         // Цвета для кнопок
         public static readonly Color ButtonBackground = Color.FromArgb(209, 241, 221);
         public static readonly Color ButtonText = Color.FromArgb(64, 115, 79);

         // Радиус скругления у кнопок
         public static readonly int ButtonRoundRadius = 2;

         // Высота кнопки
         public static readonly int ButtonHeight = 30;

         // Цвета для текстовых полей
         public static readonly Color TextBoxBackground = Color.White;
         public static readonly Color TextBoxText = Color.Black;

         // Высота текстовых полей
         public static readonly int TextBoxHeight = 20;

         // Цвета для выделения в таблице
         public static readonly Color HighlightBackground = Color.LightBlue;
         public static readonly Color HighlightText = Color.Black;

         // Цвета для градиента на форме
         public static readonly Color GradientStartColor = Color.FromArgb(255, 248, 240);
         public static readonly Color GradientEndColor = Color.FromArgb(120, 167, 137);

     }*/
    #endregion
    #region Pink
    //public static class Theme
    //{
    //    // Общие шрифты
    //    public static readonly Font DefaultFont = new Font("Arial", 12, FontStyle.Regular);

    //    // Цвета для кнопок
    //    public static readonly Color ButtonBackground = Color.FromArgb(255, 182, 193); // LightPink
    //    public static readonly Color ButtonText = Color.FromArgb(139, 0, 139); // DarkMagenta

    //    // Радиус скругления у кнопок
    //    public static readonly int ButtonRoundRadius = 2;

    //    // Высота кнопки
    //    public static readonly int ButtonHeight = 30;

    //    // Цвета для текстовых полей
    //    public static readonly Color TextBoxBackground = Color.FromArgb(255, 240, 245); // LavenderBlush
    //    public static readonly Color TextBoxText = Color.FromArgb(75, 0, 130); // Indigo

    //    // Высота текстовых полей
    //    public static readonly int TextBoxHeight = 20;

    //    // Цвета для выделения в таблице
    //    public static readonly Color HighlightBackground = Color.FromArgb(255, 228, 225); // MistyRose
    //    public static readonly Color HighlightText = Color.Black;

    //    // Цвета для градиента на форме
    //    public static readonly Color GradientStartColor = Color.FromArgb(255, 240, 245); // LavenderBlush
    //    public static readonly Color GradientEndColor = Color.FromArgb(255, 105, 180); // HotPink
    //}
    #endregion

    #region orange
    //public static class Theme
    //{
    //    // Общие шрифты
    //    public static readonly Font DefaultFont = new Font("Arial", 12, FontStyle.Regular);

    //    // Цвета для кнопок
    //    public static readonly Color ButtonBackground = Color.FromArgb(255, 165, 0); // Orange
    //    public static readonly Color ButtonText = Color.FromArgb(255, 69, 0); // Red-Orange

    //    // Радиус скругления у кнопок
    //    public static readonly int ButtonRoundRadius = 2;

    //    // Высота кнопки
    //    public static readonly int ButtonHeight = 30;

    //    // Цвета для текстовых полей
    //    public static readonly Color TextBoxBackground = Color.FromArgb(255, 248, 220); // Cornsilk
    //    public static readonly Color TextBoxText = Color.FromArgb(139, 69, 19); // SaddleBrown

    //    // Высота текстовых полей
    //    public static readonly int TextBoxHeight = 20;

    //    // Цвета для выделения в таблице
    //    public static readonly Color HighlightBackground = Color.FromArgb(255, 228, 181); // Moccasin
    //    public static readonly Color HighlightText = Color.Black;

    //    // Цвета для градиента на форме
    //    public static readonly Color GradientStartColor = Color.FromArgb(255, 239, 213); // PapayaWhip
    //    public static readonly Color GradientEndColor = Color.FromArgb(255, 127, 80); // Coral
    //}
    #endregion

    #region blue
    //public static class Theme
    //{
    //    // Общие шрифты
    //    public static readonly Font DefaultFont = new Font("Arial", 12, FontStyle.Regular);

    //    // Цвета для кнопок
    //    public static readonly Color ButtonBackground = Color.FromArgb(173, 216, 230); // LightBlue
    //    public static readonly Color ButtonText = Color.FromArgb(25, 25, 112); // MidnightBlue

    //    // Радиус скругления у кнопок
    //    public static readonly int ButtonRoundRadius = 2;

    //    // Высота кнопки
    //    public static readonly int ButtonHeight = 30;

    //    // Цвета для текстовых полей
    //    public static readonly Color TextBoxBackground = Color.FromArgb(240, 248, 255); // AliceBlue
    //    public static readonly Color TextBoxText = Color.FromArgb(0, 0, 139); // DarkBlue

    //    // Высота текстовых полей
    //    public static readonly int TextBoxHeight = 20;

    //    // Цвета для выделения в таблице
    //    public static readonly Color HighlightBackground = Color.FromArgb(135, 206, 250); // SkyBlue
    //    public static readonly Color HighlightText = Color.Black;

    //    // Цвета для градиента на форме
    //    public static readonly Color GradientStartColor = Color.FromArgb(240, 248, 255); // AliceBlue
    //    public static readonly Color GradientEndColor = Color.FromArgb(70, 130, 180); // SteelBlue
    //}
    #endregion

    #region wine
    //public static class Theme
    //{
    //    // Общие шрифты
    //    public static readonly Font DefaultFont = new Font("Arial", 12, FontStyle.Regular);

    //    // Цвета для кнопок
    //    public static readonly Color ButtonBackground = Color.FromArgb(128, 0, 32); // Burgundy
    //    public static readonly Color ButtonText = Color.FromArgb(255, 228, 225); // MistyRose

    //    // Радиус скругления у кнопок
    //    public static readonly int ButtonRoundRadius = 6;

    //    // Высота кнопки
    //    public static readonly int ButtonHeight = 30;

    //    // Цвета для текстовых полей
    //    public static readonly Color TextBoxBackground = Color.FromArgb(245, 222, 179); // Wheat
    //    public static readonly Color TextBoxText = Color.FromArgb(85, 26, 139); // DarkPurple

    //    // Высота текстовых полей
    //    public static readonly int TextBoxHeight = 20;

    //    // Цвета для выделения в таблице
    //    public static readonly Color HighlightBackground = Color.FromArgb(139, 0, 0); // DarkRed
    //    public static readonly Color HighlightText = Color.White;

    //    // Цвета для градиента на форме
    //    public static readonly Color GradientStartColor = Color.FromArgb(255, 245, 238); // Seashell
    //    public static readonly Color GradientEndColor = Color.FromArgb(139, 0, 0); // DarkRed
    //}
    #endregion

    #region sea
    //public static class Theme
    //{
    //    // Общие шрифты
    //    public static readonly Font DefaultFont = new Font("Arial", 12, FontStyle.Regular);

    //    // Цвета для кнопок
    //    public static readonly Color ButtonBackground = Color.FromArgb(135, 206, 235); // SkyBlue
    //    public static readonly Color ButtonText = Color.FromArgb(0, 105, 148); // SeaBlue

    //    // Радиус скругления у кнопок
    //    public static readonly int ButtonRoundRadius = 5;

    //    // Высота кнопки
    //    public static readonly int ButtonHeight = 30;

    //    // Цвета для текстовых полей
    //    public static readonly Color TextBoxBackground = Color.FromArgb(240, 248, 255); // AliceBlue
    //    public static readonly Color TextBoxText = Color.FromArgb(25, 25, 112); // MidnightBlue

    //    // Высота текстовых полей
    //    public static readonly int TextBoxHeight = 20;

    //    // Цвета для выделения в таблице
    //    public static readonly Color HighlightBackground = Color.FromArgb(173, 216, 230); // LightBlue
    //    public static readonly Color HighlightText = Color.Black;

    //    // Цвета для градиента на форме
    //    public static readonly Color GradientStartColor = Color.FromArgb(224, 255, 255); // LightCyan
    //    public static readonly Color GradientEndColor = Color.FromArgb(0, 139, 139); // DarkCyan
    //}
    #endregion
    #region peach
    //public static class Theme
    //{
    //    // Общие шрифты
    //    public static readonly Font DefaultFont = new Font("Arial", 12, FontStyle.Regular);

    //    // Цвета для кнопок
    //    public static readonly Color ButtonBackground = Color.FromArgb(255, 204, 178); // Peach
    //    public static readonly Color ButtonText = Color.FromArgb(210, 105, 30); // Chocolate

    //    // Радиус скругления у кнопок
    //    public static readonly int ButtonRoundRadius = 4;

    //    // Высота кнопки
    //    public static readonly int ButtonHeight = 30;

    //    // Цвета для текстовых полей
    //    public static readonly Color TextBoxBackground = Color.FromArgb(255, 239, 213); // PapayaWhip
    //    public static readonly Color TextBoxText = Color.FromArgb(128, 64, 0); // Brown

    //    // Высота текстовых полей
    //    public static readonly int TextBoxHeight = 20;

    //    // Цвета для выделения в таблице
    //    public static readonly Color HighlightBackground = Color.FromArgb(255, 218, 185); // PeachPuff
    //    public static readonly Color HighlightText = Color.Black;

    //    // Цвета для градиента на форме
    //    public static readonly Color GradientStartColor = Color.FromArgb(255, 239, 213); // PapayaWhip
    //    public static readonly Color GradientEndColor = Color.FromArgb(255, 160, 122); // LightSalmon
    //}
    #endregion


    #region lavander

    public  class Theme
    {   
        // Общие шрифты
        public Font DefaultFont { get; set; } // = new Font("Arial", 10, FontStyle.Regular);

        // Цвета для кнопок
        public static Color ButtonBackground { get; set; } //= Color.FromArgb(230, 230, 250); // Lavender
        public static Color ButtonText { get; set; } //= Color.FromArgb(106, 90, 205); // SlateBlue

        // Радиус скругления у кнопок
        public static int ButtonRoundRadius { get; set; } //= 5;

        // Высота кнопки
        public static int ButtonHeight { get; set; } //= 25;

        // Цвета для текстовых полей
        public static Color TextBoxBackground { get; set; } //= Color.FromArgb(248, 248, 255); // GhostWhite
        public static Color TextBoxText { get; set; } //= Color.FromArgb(72, 61, 139); // DarkSlateBlue
        public static Color LabelText { get; set; } //= Color.FromArgb(0, 0, 0); // Black

        // Высота текстовых полей
        public static int TextBoxHeight { get; set; } // = 20;

        // Цвета для выделения в таблице
        public static Color HighlightBackground { get; set; } // = Color.Red;//Color.FromArgb(216, 191, 216);

        // Цвета для градиента на форме
        public static Color GradientStartColor { get; set; } // = Color.FromArgb(240, 240, 255); // LightLavander
        public static Color GradientEndColor { get; set; } // = Color.FromArgb(176, 148, 226); // Goldenrod


        // Стили кнопок
        public static Color OkButtonBackground { get; set; } // { get; set; } = Color.LightGreen;
        public static Color OkButtonTextColor { get; set; } // { get; set; } = Color.Black;
        public static string OkButtonText { get; set; } // { get; set; } = "OK";
        public static Color CancelButtonBackground { get; set; } // { get; set; } = Color.IndianRed;
        public static Color CancelButtonTextColor { get; set; } // { get; set; } = Color.White;
        public static string CancelButtonText { get; set; } // { get; set; } = "Cancel";
    }

    #endregion

    #region lavander
    public static class Theme
    {
        // Общие шрифты
        public static readonly Font DefaultFont = new Font("Arial", 10, FontStyle.Regular);

        // Цвета для кнопок
        public static readonly Color ButtonBackground = Color.FromArgb(230, 230, 250); // Lavender
        public static readonly Color ButtonText = Color.FromArgb(106, 90, 205); // SlateBlue

        // Радиус скругления у кнопок
        public static readonly int ButtonRoundRadius = 5;

        // Высота кнопки
        public static readonly int ButtonHeight = 30;

        // Цвета для текстовых полей
        public static readonly Color TextBoxBackground = Color.FromArgb(248, 248, 255); // GhostWhite
        public static readonly Color TextBoxText = Color.FromArgb(72, 61, 139); // DarkSlateBlue

            // Высота текстовых полей
            TextBoxHeight = 20,

            // Цвета для выделения в таблице
            HighlightBackground = Color.FromArgb(255, 239, 213), // PapayaWhip

            // Цвета для градиента на форме
            GradientStartColor = Color.FromArgb(255, 248, 220), // Cornsilk
            GradientEndColor = Color.FromArgb(218, 165, 32) // Goldenrod
        };
        #endregion
        //Текущая активная тема
        public static Theme ActiveTheme { get; private set; } = Lavander;

        public static Theme Lavander => lavander;

        public static void SetTheme(string themeName)
        {
            CurrentTheme = themeName;
            ActiveTheme = themeName == "Gold" ? Gold : Lavander;
            ThemeChanged?.Invoke(); // Уведомление всех подписчиков
        }



    }
}
