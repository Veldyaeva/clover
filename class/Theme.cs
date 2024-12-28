using DevExpress.XtraGrid.Views.WinExplorer.ViewInfo;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SewingProduction
{
    public static class ThemeManager
    {
        public static event Action ThemeChanged; // Событие для уведомления об изменении темы

        // Переменные для текущей темы
        public static string CurrentTheme { get; private set; }// = "Lavander";

        // Доступные темы
        #region Gold
        public static readonly Theme Gold = new Theme
        {
            DefaultFont = new Font("Arial", 12, FontStyle.Regular),
            ButtonBackground = Color.FromArgb(255, 223, 186), // LightGoldenrod
            ButtonTextColor = Color.FromArgb(184, 134, 11), // DarkGoldenrod
            ButtonRoundRadius = 3,
            ButtonHeight = 30,
            TextBoxBackground = Color.FromArgb(255, 248, 220), // Cornsilk
            TextBoxText = Color.FromArgb(139, 69, 19), // SaddleBrown
            HighlightBackground = Color.FromArgb(255, 239, 213), // PapayaWhip
            GradientStartColor = Color.FromArgb(255, 248, 220), // Cornsilk
            GradientEndColor = Color.FromArgb(218, 165, 32) // Goldenrod
        };
        #endregion

        #region Lavander
        public static readonly Theme Lavander = new Theme
        {
            DefaultFont = new Font("Arial", 10, FontStyle.Regular),
            ButtonBackground = Color.FromArgb(230, 230, 250), // Lavender
            ButtonTextColor = Color.FromArgb(106, 90, 205), // SlateBlue
            ButtonRoundRadius = 5,
            ButtonHeight = 25,
            TextBoxBackground = Color.FromArgb(248, 248, 255), // GhostWhite
            TextBoxText = Color.FromArgb(72, 61, 139), // DarkSlateBlue
            HighlightBackground = Color.Red, // Выделение
            GradientStartColor = Color.FromArgb(240, 240, 255), // LightLavander
            GradientEndColor = Color.FromArgb(176, 148, 226) // Lavender Purple
        };
        #endregion

        #region Peach
        public static readonly Theme Peach = new Theme
        {
            // Общие шрифты
            DefaultFont = new Font("Arial", 10, FontStyle.Regular),

            // Цвета для кнопок
            ButtonBackground = Color.FromArgb(255, 204, 178), // Peach
            ButtonTextColor = Color.FromArgb(210, 105, 30), // Chocolate

            // Радиус скругления у кнопок
            ButtonRoundRadius = 4,

            // Высота кнопки
            ButtonHeight = 25,

            // Цвета для текстовых полей
            TextBoxBackground = Color.FromArgb(255, 239, 213), // PapayaWhip
            TextBoxText = Color.FromArgb(128, 64, 0), // Brown

            // Высота текстовых полей
            TextBoxHeight = 20,

            // Цвета для выделения в таблице
            HighlightBackground = Color.FromArgb(255, 218, 185), // PeachPuff
                                                                 //  HighlightText = Color.Black,

            // Цвета для градиента на форме
            GradientStartColor = Color.FromArgb(255, 239, 213), // PapayaWhip
            GradientEndColor = Color.FromArgb(255, 160, 122), // LightSalmon
        };
        #endregion

        #region Sea
        public static readonly Theme Sea = new Theme
        {
            // Общие шрифты
            DefaultFont = new Font("Arial", 12, FontStyle.Regular),

            // Цвета для кнопок
            ButtonBackground = Color.FromArgb(135, 206, 235), // SkyBlue
            ButtonTextColor = Color.FromArgb(0, 105, 148), // SeaBlue

            // Радиус скругления у кнопок
            ButtonRoundRadius = 5,

            // Высота кнопки
            ButtonHeight = 30,

            // Цвета для текстовых полей
            TextBoxBackground = Color.FromArgb(240, 248, 255), // AliceBlue
            TextBoxText = Color.FromArgb(25, 25, 112), // MidnightBlue

            // Высота текстовых полей
            TextBoxHeight = 20,

            // Цвета для выделения в таблице
            HighlightBackground = Color.FromArgb(173, 216, 230), // LightBlue
            //HighlightText = Color.Black,

            // Цвета для градиента на форме
            GradientStartColor = Color.FromArgb(224, 255, 255), // LightCyan
            GradientEndColor = Color.FromArgb(0, 139, 139) // DarkCyan
        };
        #endregion

        #region Green
        public static readonly Theme Green = new Theme
        {
            // Общие шрифты
            DefaultFont = new Font("Arial", 12, FontStyle.Regular),

            // Цвета для кнопок
            ButtonBackground = Color.FromArgb(209, 241, 221),
            ButtonTextColor = Color.FromArgb(64, 115, 79),

            // Радиус скругления у кнопок
            ButtonRoundRadius = 2,

            // Высота кнопки
            ButtonHeight = 30,

            // Цвета для текстовых полей
            TextBoxBackground = Color.White,
            TextBoxText = Color.Black,

            // Высота текстовых полей
            TextBoxHeight = 20,

            // Цвета для выделения в таблице
            HighlightBackground = Color.LightBlue,
            //HighlightText = Color.Black,

            // Цвета для градиента на форме
            GradientStartColor = Color.FromArgb(255, 248, 240),
            GradientEndColor = Color.FromArgb(120, 167, 137)

        };
        #endregion

        #region Pink
        public static readonly Theme Pink = new Theme
        {
            // Общие шрифты
            DefaultFont = new Font("Arial", 12, FontStyle.Regular),

            // Цвета для кнопок
            ButtonBackground = Color.FromArgb(255, 182, 193), // LightPink
            ButtonTextColor = Color.FromArgb(139, 0, 139), // DarkMagenta

            // Радиус скругления у кнопок
            ButtonRoundRadius = 2,

            // Высота кнопки
            ButtonHeight = 30,

            // Цвета для текстовых полей
            TextBoxBackground = Color.FromArgb(255, 240, 245), // LavenderBlush
            TextBoxText = Color.FromArgb(75, 0, 130), // Indigo

            // Высота текстовых полей
            TextBoxHeight = 20,

            // Цвета для выделения в таблице
            HighlightBackground = Color.FromArgb(255, 228, 225), // MistyRose
            //HighlightText = Color.Black;

            // Цвета для градиента на форме
            GradientStartColor = Color.FromArgb(255, 240, 245), // LavenderBlush
            GradientEndColor = Color.FromArgb(255, 105, 180) // HotPink
        };
        #endregion

        #region Orange
        public static readonly Theme Orange = new Theme
        {
            // Общие шрифты
            DefaultFont = new Font("Arial", 12, FontStyle.Regular),

            // Цвета для кнопок
            ButtonBackground = Color.FromArgb(255, 165, 0), // Orange
            ButtonTextColor = Color.FromArgb(255, 69, 0), // Red-Orange

            // Радиус скругления у кнопок
            ButtonRoundRadius = 2,

            // Высота кнопки
            ButtonHeight = 30,

            // Цвета для текстовых полей
            TextBoxBackground = Color.FromArgb(255, 248, 220), // Cornsilk
            TextBoxText = Color.FromArgb(139, 69, 19), // SaddleBrown

            // Высота текстовых полей
            TextBoxHeight = 20,

            // Цвета для выделения в таблице
            HighlightBackground = Color.FromArgb(255, 228, 181), // Moccasin
            //public static readonly Color HighlightText = Color.Black,

            // Цвета для градиента на форме
            GradientStartColor = Color.FromArgb(255, 239, 213), // PapayaWhip
            GradientEndColor = Color.FromArgb(255, 127, 80) // Coral
        };
        #endregion

        #region Blue
        public static readonly Theme Blue = new Theme
        {
            // Общие шрифты
            DefaultFont = new Font("Arial", 12, FontStyle.Regular),

            // Цвета для кнопок
            ButtonBackground = Color.FromArgb(173, 216, 230), // LightBlue
            ButtonTextColor = Color.FromArgb(25, 25, 112), // MidnightBlue

            // Радиус скругления у кнопок
            ButtonRoundRadius = 2,

            // Высота кнопки
            ButtonHeight = 30,

            // Цвета для текстовых полей
            TextBoxBackground = Color.FromArgb(240, 248, 255), // AliceBlue
            TextBoxText = Color.FromArgb(0, 0, 139), // DarkBlue

            // Высота текстовых полей
            TextBoxHeight = 20,

            // Цвета для выделения в таблице
            HighlightBackground = Color.FromArgb(135, 206, 250), // SkyBlue
                                                                 //HighlightText = Color.Black;

            // Цвета для градиента на форме
            GradientStartColor = Color.FromArgb(240, 248, 255), // AliceBlue
            GradientEndColor = Color.FromArgb(70, 130, 180) // SteelBlue
        };
        #endregion

        #region Wine
        public static readonly Theme Wine = new Theme
        {
            // Общие шрифты
            DefaultFont = new Font("Arial", 12, FontStyle.Regular),

            // Цвета для кнопок
            ButtonBackground = Color.FromArgb(128, 0, 32), // Burgundy
            ButtonTextColor = Color.FromArgb(255, 228, 225), // MistyRose

            // Радиус скругления у кнопок
            ButtonRoundRadius = 6,

            // Высота кнопки
            ButtonHeight = 30,

            // Цвета для текстовых полей
            TextBoxBackground = Color.FromArgb(245, 222, 179), // Wheat
            TextBoxText = Color.FromArgb(85, 26, 139), // DarkPurple

            // Настройки кнопок да/нет
            OkButtonBackground = Color.FromArgb(0, 0, 0),
            OkButtonTextColor = Color.FromArgb(0, 0, 0),
            OkButtonText = "Хорошо",
            CancelButtonBackground = Color.FromArgb(255, 0, 0),
            CancelButtonTextColor = Color.FromArgb(255, 0, 0),
            CancelButtonText = "Отмена",

            // Высота текстовых полей
            TextBoxHeight = 20,

            // Цвета для выделения в таблице
            HighlightBackground = Color.FromArgb(139, 0, 0), // DarkRed
            //HighlightText = Color.White;

            // Цвета для градиента на форме
            GradientStartColor = Color.FromArgb(255, 245, 238), // Seashell
            GradientEndColor = Color.FromArgb(139, 0, 0), // DarkRed
        };
        #endregion
        // Текущая активная тема

        public static Theme ActiveTheme { get; private set; }// = Lavander;
        static ThemeManager() { LoadTheme(); }
        public static List<string> GetAvailableThemes()
        {
            return new List<string> { "Gold", "Lavander", "Peach", "Sea", "Green", "Pink", "Orange", "Blue", "Wine" }; // Все доступные темы
        }


        //    } private static void SaveTheme()  private static void LoadTheme() { CurrentTheme = Properties.Settings.Default.SelectedTheme; ActiveTheme = CurrentTheme switch { "Gold" => Gold, _ => Lavander }; } public class Theme { public Color ButtonBackground { get; set; } public Color ButtonTextColor { get; set; } public Color GradientStartColor { get; set; } public Color GradientEndColor { get; set; } public Font DefaultFont { get; set; } } } } 
        public static void SetTheme(string themeName)
        {
            CurrentTheme = themeName;

            switch (themeName)
            {
                case "Gold":
                    ActiveTheme = Gold;
                    break;
                case "Lavander":
                    ActiveTheme = Lavander;
                    break;
                case "Peach":
                    ActiveTheme = Peach;
                    break;
                case "Sea":
                    ActiveTheme = Sea;
                    break;
                case "Green":
                    ActiveTheme = Green;
                    break;
                case "Pink":
                    ActiveTheme = Pink;
                    break;
                case "Orange":
                    ActiveTheme = Orange;
                    break;
                case "Blue":
                    ActiveTheme = Blue;
                    break;
                case "Wine":
                    ActiveTheme = Wine;
                    break;
            }
            ThemeChanged?.Invoke(); // Уведомление всех подписчиков
            SaveTheme(); }
        
        private static void SaveTheme()
        { 
            Properties.Settings.Default.SelectedTheme = CurrentTheme;
            Properties.Settings.Default.Save(); 
        }
        private static void LoadTheme()
        {
            CurrentTheme = Properties.Settings.Default.SelectedTheme;
            switch (CurrentTheme)
            {
                case "Gold":
                    ActiveTheme = Gold;
                    break;
                case "Lavander":
                    ActiveTheme = Lavander;
                    break;
                case "Peach":
                    ActiveTheme = Peach;
                    break;
                case "Sea":
                    ActiveTheme = Sea;
                    break;
                case "Green":
                    ActiveTheme = Green;
                    break;
                case "Pink":
                    ActiveTheme = Pink;
                    break;
                case "Orange":
                    ActiveTheme = Orange;
                    break;
                case "Blue":
                    ActiveTheme = Blue;
                    break;
                case "Wine":
                    ActiveTheme = Wine;
                    break;
            }

        }

        public class Theme
        {
            public Color ButtonBackground { get; set; }
            public Color ButtonTextColor { get; set; }
            public Color GridBackground { get; set; }
            public Color GridRowBackground { get; set; }
            public Color GridTextColor { get; set; }
            public Color LabelTextColor { get; set; }
            public Font DefaultFont { get; set; }
            public int ButtonRoundRadius { get; set; }
            public int ButtonHeight { get; set; }

            // Цвета для текстовых полей
            public Color TextBoxBackground { get; set; }
            public Color TextBoxText { get; set; }
            public Color LabelText { get; set; }

            // Высота текстовых полей
            public int TextBoxHeight { get; set; }

            // Цвета для выделения в таблице
            public Color HighlightBackground { get; set; }

            // Цвета для градиента на форме
            public Color GradientStartColor { get; set; }
            public Color GradientEndColor { get; set; }

            // Стили кнопок
            public Color OkButtonBackground { get; set; }
            public Color OkButtonTextColor { get; set; }
            public string OkButtonText { get; set; }
            public Color CancelButtonBackground { get; set; }
            public Color CancelButtonTextColor { get; set; }
            public string CancelButtonText { get; set; }
        }
    }
}