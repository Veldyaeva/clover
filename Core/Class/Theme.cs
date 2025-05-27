using DevExpress.XtraGrid.Views.WinExplorer.ViewInfo;
using SewingProduction.form.TeamWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace SewingProduction
{
    public static class ThemeManager
    {
        public static event Action ThemeChanged; // Событие для уведомления об изменении темы

        // Общие настройки (размеры и шрифты)
        public static readonly ThemeSettings SharedSettings = new ThemeSettings
        {
            DefaultFont = new Font("Arial", 10, FontStyle.Regular),
            ButtonRoundRadius = 5,
            ButtonHeight = 25,
            TextBoxHeight = 25
        };

        // Переменные для текущей темы
        public static string CurrentTheme { get; private set; }// = "Lavander";

        #region Blue
        public static readonly Theme Blue = new Theme
        {
            // Цвета для кнопок
            ButtonBackground = Color.FromArgb(173, 216, 230), // LightBlue
            ButtonTextColor = Color.FromArgb(25, 25, 112), // MidnightBlue
            // Цвета для Label
            LabelTextColor = Color.FromArgb(25, 25, 112),
            // Цвета для текстовых полей
            TextBoxBackground = Color.FromArgb(240, 248, 255), // AliceBlue
            TextBoxText = Color.FromArgb(0, 0, 139), // DarkBlue
            // Цвета для выделения в таблице
            HighlightBackground = Color.FromArgb(135, 206, 250), // SkyBlue
            BandHighlightColor = Color.FromArgb(173, 216, 230), // LightBlue
            // Цвета для градиента на форме
            GradientStartColor = Color.FromArgb(240, 248, 255), // AliceBlue
            GradientEndColor = Color.FromArgb(70, 130, 180) // SteelBlue
        };
        #endregion

        #region Gold
        public static readonly Theme Gold = new Theme
        {
            ButtonBackground = Color.FromArgb(255, 235, 205), // BlanchedAlmond
            ButtonTextColor = Color.FromArgb(139, 69, 19), // SaddleBrown
            LabelTextColor = Color.FromArgb(139, 69, 19),
            TextBoxBackground = Color.FromArgb(250, 240, 230), // Linen
            TextBoxText = Color.FromArgb(105, 75, 45), // BrownShade
            HighlightBackground = Color.FromArgb(245, 222, 179), // Wheat
            BandHighlightColor = Color.FromArgb(255, 235, 205), // BlanchedAlmond
            GradientStartColor = Color.FromArgb(255, 239, 213), // PapayaWhip
            GradientEndColor = Color.FromArgb(218, 165, 32) // Goldenrod
        };
        #endregion

        #region Lavender
        public static readonly Theme Lavander = new Theme
        {
            ButtonBackground = Color.FromArgb(230, 230, 250), // Lavender
            ButtonTextColor = Color.FromArgb(72, 61, 139), // DarkSlateBlue
            LabelTextColor = Color.FromArgb(72, 61, 139),
            TextBoxBackground = Color.FromArgb(245, 245, 250), // SoftLavender
            TextBoxText = Color.FromArgb(85, 45, 115), // DeepLavender
            HighlightBackground = Color.FromArgb(240, 230, 250), // LightLavender
            BandHighlightColor = Color.FromArgb(230, 230, 250), // Lavender
            GradientStartColor = Color.FromArgb(245, 245, 255), // GhostWhite
            GradientEndColor = Color.FromArgb(176, 148, 226) // LavenderPurple
        };
        #endregion

        #region Peach
        public static readonly Theme Peach = new Theme
        {
            ButtonBackground = Color.FromArgb(255, 223, 196), // LightPeach
            ButtonTextColor = Color.FromArgb(139, 69, 19), // SaddleBrown
            LabelTextColor = Color.FromArgb(139, 69, 19),
            TextBoxBackground = Color.FromArgb(255, 245, 230), // LightPapaya
            TextBoxText = Color.FromArgb(120, 60, 30), // BrownShade
            HighlightBackground = Color.FromArgb(255, 228, 200), // SoftPeach
            BandHighlightColor = Color.FromArgb(255, 223, 196), // LightPeach
            GradientStartColor = Color.FromArgb(255, 239, 213), // PapayaWhip
            GradientEndColor = Color.FromArgb(255, 180, 150) // PeachCoral
        };
        #endregion

        #region Sea
        public static readonly Theme Sea = new Theme
        {
            ButtonBackground = Color.FromArgb(173, 216, 230), // LightBlue
            ButtonTextColor = Color.FromArgb(0, 105, 148), // SeaBlue
            LabelTextColor = Color.FromArgb(0, 105, 148), // SeaBlue
            TextBoxBackground = Color.FromArgb(240, 248, 255), // AliceBlue
            TextBoxText = Color.FromArgb(25, 25, 112), // MidnightBlue
            HighlightBackground = Color.FromArgb(200, 230, 240), // SoftBlue
            BandHighlightColor = Color.FromArgb(173, 216, 230), // LightBlue
            GradientStartColor = Color.FromArgb(224, 255, 255), // LightCyan
            GradientEndColor = Color.FromArgb(100, 150, 150) // CalmCyan
        };
        #endregion

        #region Green
        public static readonly Theme Green = new Theme
        {
            ButtonBackground = Color.FromArgb(200, 230, 210), // SoftGreen
            ButtonTextColor = Color.FromArgb(64, 115, 79), // DeepGreen
            LabelTextColor = Color.FromArgb(64, 115, 79), // DeepGreen
            TextBoxBackground = Color.FromArgb(245, 255, 250), // LightMint
            TextBoxText = Color.FromArgb(45, 90, 60), // ForestGreen
            HighlightBackground = Color.FromArgb(220, 240, 230), // CalmMint
            BandHighlightColor = Color.FromArgb(200, 230, 210), // SoftGreen
            GradientStartColor = Color.FromArgb(240, 255, 250), // MintWhite
            GradientEndColor = Color.FromArgb(120, 167, 137) // GentleGreen
        };
        #endregion

        #region Pink
        public static readonly Theme Pink = new Theme
        {
            ButtonBackground = Color.FromArgb(255, 192, 203), // LightPink
            ButtonTextColor = Color.FromArgb(139, 0, 139), // DarkMagenta
            LabelTextColor = Color.FromArgb(139, 0, 139), // DarkMagenta
            TextBoxBackground = Color.FromArgb(255, 228, 232), // BlushPink
            TextBoxText = Color.FromArgb(75, 0, 130), // Indigo
            HighlightBackground = Color.FromArgb(255, 240, 245), // SoftBlush
            BandHighlightColor = Color.FromArgb(255, 192, 203), // LightPink
            GradientStartColor = Color.FromArgb(255, 240, 245), // LavenderBlush
            GradientEndColor = Color.FromArgb(255, 145, 180) // CalmPink
        };
        #endregion

        #region Orange
        public static readonly Theme Orange = new Theme
        {
            ButtonBackground = Color.FromArgb(255, 200, 150), // SoftOrange
            ButtonTextColor = Color.FromArgb(139, 69, 19), // SaddleBrown
            LabelTextColor = Color.FromArgb(139, 69, 19), // SaddleBrown
            TextBoxBackground = Color.FromArgb(255, 245, 230), // LightPapaya
            TextBoxText = Color.FromArgb(120, 60, 30), // DeepBrown
            HighlightBackground = Color.FromArgb(255, 225, 180), // LightOrange
            BandHighlightColor = Color.FromArgb(255, 200, 150), // SoftOrange
            GradientStartColor = Color.FromArgb(255, 239, 213), // PapayaWhip
            GradientEndColor = Color.FromArgb(255, 165, 120) // CalmCoral
        };
        #endregion

        #region Wine
        public static readonly Theme Wine = new Theme
        {
            ButtonBackground = Color.FromArgb(139, 0, 26), // DeepWine
            ButtonTextColor = Color.FromArgb(255, 245, 238), // SoftWhite
            LabelTextColor = Color.FromArgb(255, 245, 238), // SoftWhite
            TextBoxBackground = Color.FromArgb(245, 222, 179), // Wheat
            TextBoxText = Color.FromArgb(85, 26, 139), // DarkPurple
            HighlightBackground = Color.FromArgb(160, 0, 30), // GentleRed
            BandHighlightColor = Color.FromArgb(139, 0, 26), // DeepWine
            GradientStartColor = Color.FromArgb(255, 245, 238), // Seashell
            GradientEndColor = Color.FromArgb(139, 0, 0) // CalmRed
        };
        #endregion

        #region Sky
        public static readonly Theme Sky = new Theme
        {
            ButtonBackground = Color.FromArgb(200, 225, 255), // SoftSkyBlue
            ButtonTextColor = Color.FromArgb(30, 70, 140), // DeepSkyBlue
            LabelTextColor = Color.FromArgb(30, 70, 140), // DeepSkyBlue
            TextBoxBackground = Color.FromArgb(230, 245, 255), // LightSky
            TextBoxText = Color.FromArgb(50, 90, 160), // CoolBlue
            HighlightBackground = Color.FromArgb(220, 235, 250), // GentleSky
            BandHighlightColor = Color.FromArgb(200, 225, 255), // SoftSkyBlue
            GradientStartColor = Color.FromArgb(240, 250, 255), // PaleSky
            GradientEndColor = Color.FromArgb(150, 190, 230) // DeepSky
        };
        #endregion

        #region Mint
        public static readonly Theme Mint = new Theme
        {
            ButtonBackground = Color.FromArgb(210, 245, 230), // SoftMint
            ButtonTextColor = Color.FromArgb(30, 90, 70), // DeepMint
            LabelTextColor = Color.FromArgb(30, 90, 70), // DeepMint
            TextBoxBackground = Color.FromArgb(230, 255, 240), // LightMint
            TextBoxText = Color.FromArgb(40, 100, 80), // CoolGreen
            HighlightBackground = Color.FromArgb(200, 240, 220), // GentleMint
            BandHighlightColor = Color.FromArgb(210, 245, 230), // SoftMint
            GradientStartColor = Color.FromArgb(240, 255, 250), // PaleMint
            GradientEndColor = Color.FromArgb(120, 200, 160) // DeepMint
        };
        #endregion

        #region Sand
        public static readonly Theme Sand = new Theme
        {
            ButtonBackground = Color.FromArgb(245, 222, 179), // SoftSand
            ButtonTextColor = Color.FromArgb(105, 70, 40), // DeepSand
            LabelTextColor = Color.FromArgb(105, 70, 40), // DeepSand
            TextBoxBackground = Color.FromArgb(255, 245, 230), // LightSand
            TextBoxText = Color.FromArgb(120, 80, 50), // GentleBrown
            HighlightBackground = Color.FromArgb(235, 215, 175), // CalmSand
            BandHighlightColor = Color.FromArgb(245, 222, 179), // SoftSand
            GradientStartColor = Color.FromArgb(250, 240, 210), // PaleSand
            GradientEndColor = Color.FromArgb(210, 190, 140) // DeepSand
        };
        #endregion

        #region Lilac
        public static readonly Theme Lilac = new Theme
        {
            ButtonBackground = Color.FromArgb(225, 200, 230), // SoftLilac
            ButtonTextColor = Color.FromArgb(90, 50, 120), // DeepLilac
            LabelTextColor = Color.FromArgb(90, 50, 120), // DeepLilac
            TextBoxBackground = Color.FromArgb(240, 220, 245), // LightLilac
            TextBoxText = Color.FromArgb(80, 40, 100), // CoolPurple
            HighlightBackground = Color.FromArgb(230, 210, 240), // GentleLilac
            BandHighlightColor = Color.FromArgb(225, 200, 230), // SoftLilac
            GradientStartColor = Color.FromArgb(245, 230, 250), // PaleLilac
            GradientEndColor = Color.FromArgb(190, 150, 210) // DeepLilac
        };
        #endregion

        #region Sunset
        public static readonly Theme Sunset = new Theme
        {
            ButtonBackground = Color.FromArgb(255, 200, 150), // SoftSunset
            ButtonTextColor = Color.FromArgb(139, 69, 19), // SaddleBrown
            LabelTextColor = Color.FromArgb(139, 69, 19), // SaddleBrown
            TextBoxBackground = Color.FromArgb(255, 240, 230), // WarmPeach
            TextBoxText = Color.FromArgb(110, 50, 25), // DeepCoral
            HighlightBackground = Color.FromArgb(255, 220, 190), // GentleSunset
            BandHighlightColor = Color.FromArgb(255, 200, 150), // SoftSunset
            GradientStartColor = Color.FromArgb(255, 230, 210), // PaleSunset
            GradientEndColor = Color.FromArgb(255, 165, 120) // DeepCoral
        };
        #endregion

        #region Forest
        public static readonly Theme Forest = new Theme
        {
            ButtonBackground = Color.FromArgb(200, 230, 200), // SoftForest
            ButtonTextColor = Color.FromArgb(34, 85, 34), // DeepForest
            LabelTextColor = Color.FromArgb(34, 85, 34), // DeepForest
            TextBoxBackground = Color.FromArgb(240, 250, 240), // LightForest
            TextBoxText = Color.FromArgb(45, 90, 45), // GentleGreen
            GridBackground = Color.FromArgb(240, 250, 240), // GentleMint
            GridRowBackground = Color.FromArgb(45, 90, 45),
            HighlightBackground = Color.FromArgb(220, 240, 220), // CalmForest
            BandHighlightColor = Color.FromArgb(200, 230, 200), // SoftForest
            GradientStartColor = Color.FromArgb(230, 250, 230), // PaleForest
            GradientEndColor = Color.FromArgb(120, 180, 120) // DeepForest
        };
        #endregion

        #region Ocean
        public static readonly Theme Ocean = new Theme
        {
            ButtonBackground = Color.FromArgb(180, 220, 240), // SoftOcean
            ButtonTextColor = Color.FromArgb(20, 70, 100), // DeepOcean
            LabelTextColor = Color.FromArgb(20, 70, 100), // DeepOcean
            TextBoxBackground = Color.FromArgb(220, 240, 250), // LightOcean
            TextBoxText = Color.FromArgb(25, 75, 105), // CoolBlue
            HighlightBackground = Color.FromArgb(200, 220, 240), // GentleOcean
            BandHighlightColor = Color.FromArgb(180, 220, 240), // SoftOcean
            GradientStartColor = Color.FromArgb(240, 250, 255), // PaleOcean
            GradientEndColor = Color.FromArgb(100, 150, 200) // DeepBlue
        };
        #endregion

        // Текущая активная тема
        public static Theme ActiveTheme { get; private set; }// = Lavander;
        static ThemeManager() { LoadTheme(); }
        public static List<string> GetAvailableThemes()
        {
            return new List<string> { "Gold", "Lavander", "Peach", "Sea", "Green", "Pink", "Orange", "Blue", "Wine", "Sky", "Mint", "Sand", "Lilac", "Sunset", "Forest", "Ocean" }; // Все доступные темы
        }

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
                case "Sky":
                    ActiveTheme = Sky;
                    break;
                case "Mint":
                    ActiveTheme = Mint;
                    break;
                case "Sand":
                    ActiveTheme = Sand;
                    break;
                case "Lilac":
                    ActiveTheme = Lilac;
                    break;
                case "Sunset":
                    ActiveTheme = Sunset;
                    break;
                case "Forest":
                    ActiveTheme = Forest;
                    break;
                case "Ocean":
                    ActiveTheme = Ocean;
                    break;
            }
            ThemeChanged?.Invoke(); // Уведомление всех подписчиков
        }

        public static void LoadTheme()
        {
            string path = "settings.json";
            if (File.Exists(path))
            {
                try
                {
                    var json = File.ReadAllText(path);
                    var config = Newtonsoft.Json.Linq.JObject.Parse(json);
                    string themeName = config["theme"]?.ToString();

                    if (!string.IsNullOrEmpty(themeName))
                    {
                        SetTheme(themeName); // применит тему и установит CurrentTheme
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки темы: " + ex.Message);
                }
            }

            // fallback если ничего не найдено
            SetTheme("Ocean");
        }

        
        public static void UpdateTheme(Control control)
        {
            foreach (Control child in control.Controls)
            {
                if (child is IThemeable themeable)
                {
                    themeable.ApplyTheme();
                }
                else if (child.HasChildren)
                {
                    UpdateTheme(child); // Рекурсивно обновляем вложенные элементы
                }
            }
        }

        public static void UpdateDefaultFont(Font newFont)
        {
            if (SharedSettings.DefaultFont != newFont)
            {
                SharedSettings.DefaultFont = newFont;
                ThemeChanged?.Invoke(); // Notify all subscribers about the change
            }
        }

        public class ThemeSettings
        {
            public Font DefaultFont { get; set; }
            public int ButtonRoundRadius { get; set; }
            public int ButtonHeight { get; set; }
            public int TextBoxHeight { get; set; }
        }

        public class Theme
        {
            public Color ButtonBackground { get; set; }
            public Color ButtonTextColor { get; set; }
            public Color GridBackground { get; set; }
            public Color GridRowBackground { get; set; }
            public Color GridTextColor { get; set; }
            public Color LabelTextColor { get; set; }

            // Цвета для текстовых полей
            public Color TextBoxBackground { get; set; }
            public Color TextBoxText { get; set; }
            public Color LabelText { get; set; }

            // Цвета для выделения в таблице
            public Color HighlightBackground { get; set; }
            public Color BandHighlightColor { get; set; }

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