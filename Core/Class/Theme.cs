using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SewingProduction.Core.Class.Settings;
using System.Linq;

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

        // Дефолтные темы используются только для инициализации JSON при первом запуске
        // После первого запуска все темы хранятся в settings.json и редактируются там
        private static readonly Dictionary<string, Theme> DefaultThemes = new Dictionary<string, Theme>
        {
            ["White"] = new Theme
            {
                ButtonBackground = Color.FromArgb(224, 224, 224),
                ButtonTextColor = Color.Black,
                LabelTextColor = Color.Black,
                TextBoxBackground = Color.White,
                TextBoxText = Color.Black,
                HighlightBackground = Color.FromArgb(200, 200, 200),
                BandHighlightColor = Color.FromArgb(180, 180, 180),
                GradientStartColor = Color.FromArgb(255, 255, 255),
                GradientEndColor = Color.FromArgb(255, 255, 255)
            },
            ["Gray"] = new Theme
            {
                ButtonBackground = Color.FromArgb(224, 224, 224),
                ButtonTextColor = Color.Black,
                LabelTextColor = Color.Black,
                TextBoxBackground = Color.White,
                TextBoxText = Color.Black,
                HighlightBackground = Color.FromArgb(200, 200, 200),
                BandHighlightColor = Color.FromArgb(180, 180, 180),
                GradientStartColor = Color.FromArgb(240, 240, 240),
                GradientEndColor = Color.FromArgb(200, 200, 200)
            },
            ["Rainbow"] = new Theme
            {
                ButtonBackground = ColorTranslator.FromHtml("#B388FF"),
                ButtonTextColor = ColorTranslator.FromHtml("#1C1C1C"),
                LabelTextColor = ColorTranslator.FromHtml("#333333"),
                TextBoxBackground = ColorTranslator.FromHtml("#FFF59D"),
                TextBoxText = ColorTranslator.FromHtml("#000000"),
                HighlightBackground = ColorTranslator.FromHtml("#80DEEA"),
                BandHighlightColor = ColorTranslator.FromHtml("#81C784"),
                GradientStartColor = ColorTranslator.FromHtml("#FF8A80"),
                GradientEndColor = ColorTranslator.FromHtml("#82B1FF")
            },
            ["Blue"] = new Theme
            {
                ButtonBackground = Color.FromArgb(173, 216, 230),
                ButtonTextColor = Color.FromArgb(25, 25, 112),
                LabelTextColor = Color.FromArgb(25, 25, 112),
                TextBoxBackground = Color.FromArgb(240, 248, 255),
                TextBoxText = Color.FromArgb(0, 0, 139),
                HighlightBackground = Color.FromArgb(135, 206, 250),
                BandHighlightColor = Color.FromArgb(173, 216, 230),
                GradientStartColor = Color.FromArgb(240, 248, 255),
                GradientEndColor = Color.FromArgb(70, 130, 180)
            },
            ["Gold"] = new Theme
            {
                ButtonBackground = Color.FromArgb(255, 235, 205),
                ButtonTextColor = Color.FromArgb(139, 69, 19),
                LabelTextColor = Color.FromArgb(139, 69, 19),
                TextBoxBackground = Color.FromArgb(250, 240, 230),
                TextBoxText = Color.FromArgb(105, 75, 45),
                HighlightBackground = Color.FromArgb(245, 222, 179),
                BandHighlightColor = Color.FromArgb(255, 235, 205),
                GradientStartColor = Color.FromArgb(255, 239, 213),
                GradientEndColor = Color.FromArgb(218, 165, 32)
            },
            ["Lavander"] = new Theme
            {
                ButtonBackground = Color.FromArgb(230, 230, 250),
                ButtonTextColor = Color.FromArgb(72, 61, 139),
                LabelTextColor = Color.FromArgb(72, 61, 139),
                TextBoxBackground = Color.FromArgb(245, 245, 250),
                TextBoxText = Color.FromArgb(85, 45, 115),
                HighlightBackground = Color.FromArgb(240, 230, 250),
                BandHighlightColor = Color.FromArgb(230, 230, 250),
                GradientStartColor = Color.FromArgb(245, 245, 255),
                GradientEndColor = Color.FromArgb(176, 148, 226)
            },
            ["Peach"] = new Theme
            {
                ButtonBackground = Color.FromArgb(255, 223, 196),
                ButtonTextColor = Color.FromArgb(139, 69, 19),
                LabelTextColor = Color.FromArgb(139, 69, 19),
                TextBoxBackground = Color.FromArgb(255, 245, 230),
                TextBoxText = Color.FromArgb(120, 60, 30),
                HighlightBackground = Color.FromArgb(255, 228, 200),
                BandHighlightColor = Color.FromArgb(255, 223, 196),
                GradientStartColor = Color.FromArgb(255, 239, 213),
                GradientEndColor = Color.FromArgb(255, 180, 150)
            },
            ["Sea"] = new Theme
            {
                ButtonBackground = Color.FromArgb(173, 216, 230),
                ButtonTextColor = Color.FromArgb(0, 105, 148),
                LabelTextColor = Color.FromArgb(0, 105, 148),
                TextBoxBackground = Color.FromArgb(240, 248, 255),
                TextBoxText = Color.FromArgb(25, 25, 112),
                HighlightBackground = Color.FromArgb(200, 230, 240),
                BandHighlightColor = Color.FromArgb(173, 216, 230),
                GradientStartColor = Color.FromArgb(224, 255, 255),
                GradientEndColor = Color.FromArgb(100, 150, 150)
            },
            ["Green"] = new Theme
            {
                ButtonBackground = Color.FromArgb(200, 230, 210),
                ButtonTextColor = Color.FromArgb(64, 115, 79),
                LabelTextColor = Color.FromArgb(64, 115, 79),
                TextBoxBackground = Color.FromArgb(245, 255, 250),
                TextBoxText = Color.FromArgb(45, 90, 60),
                HighlightBackground = Color.FromArgb(220, 240, 230),
                BandHighlightColor = Color.FromArgb(200, 230, 210),
                GradientStartColor = Color.FromArgb(240, 255, 250),
                GradientEndColor = Color.FromArgb(120, 167, 137)
            },
            ["Pink"] = new Theme
            {
                ButtonBackground = Color.FromArgb(255, 192, 203),
                ButtonTextColor = Color.FromArgb(139, 0, 139),
                LabelTextColor = Color.FromArgb(139, 0, 139),
                TextBoxBackground = Color.FromArgb(255, 228, 232),
                TextBoxText = Color.FromArgb(75, 0, 130),
                HighlightBackground = Color.FromArgb(255, 240, 245),
                BandHighlightColor = Color.FromArgb(255, 192, 203),
                GradientStartColor = Color.FromArgb(255, 240, 245),
                GradientEndColor = Color.FromArgb(255, 145, 180)
            },
            ["Orange"] = new Theme
            {
                ButtonBackground = Color.FromArgb(255, 200, 150),
                ButtonTextColor = Color.FromArgb(139, 69, 19),
                LabelTextColor = Color.FromArgb(139, 69, 19),
                TextBoxBackground = Color.FromArgb(255, 245, 230),
                TextBoxText = Color.FromArgb(120, 60, 30),
                HighlightBackground = Color.FromArgb(255, 225, 180),
                BandHighlightColor = Color.FromArgb(255, 200, 150),
                GradientStartColor = Color.FromArgb(255, 239, 213),
                GradientEndColor = Color.FromArgb(255, 165, 120)
            },
            ["Wine"] = new Theme
            {
                ButtonBackground = Color.FromArgb(139, 0, 26),
                ButtonTextColor = Color.FromArgb(255, 245, 238),
                LabelTextColor = Color.FromArgb(255, 245, 238),
                TextBoxBackground = Color.FromArgb(245, 222, 179),
                TextBoxText = Color.FromArgb(85, 26, 139),
                HighlightBackground = Color.FromArgb(160, 0, 30),
                BandHighlightColor = Color.FromArgb(139, 0, 26),
                GradientStartColor = Color.FromArgb(255, 245, 238),
                GradientEndColor = Color.FromArgb(139, 0, 0)
            },
            ["Sky"] = new Theme
            {
                ButtonBackground = Color.FromArgb(200, 225, 255),
                ButtonTextColor = Color.FromArgb(30, 70, 140),
                LabelTextColor = Color.FromArgb(30, 70, 140),
                TextBoxBackground = Color.FromArgb(230, 245, 255),
                TextBoxText = Color.FromArgb(50, 90, 160),
                HighlightBackground = Color.FromArgb(220, 235, 250),
                BandHighlightColor = Color.FromArgb(200, 225, 255),
                GradientStartColor = Color.FromArgb(240, 250, 255),
                GradientEndColor = Color.FromArgb(150, 190, 230)
            },
            ["Mint"] = new Theme
            {
                ButtonBackground = Color.FromArgb(210, 245, 230),
                ButtonTextColor = Color.FromArgb(30, 90, 70),
                LabelTextColor = Color.FromArgb(30, 90, 70),
                TextBoxBackground = Color.FromArgb(230, 255, 240),
                TextBoxText = Color.FromArgb(40, 100, 80),
                HighlightBackground = Color.FromArgb(200, 240, 220),
                BandHighlightColor = Color.FromArgb(210, 245, 230),
                GradientStartColor = Color.FromArgb(240, 255, 250),
                GradientEndColor = Color.FromArgb(120, 200, 160)
            },
            ["Sand"] = new Theme
            {
                ButtonBackground = Color.FromArgb(245, 222, 179),
                ButtonTextColor = Color.FromArgb(105, 70, 40),
                LabelTextColor = Color.FromArgb(105, 70, 40),
                TextBoxBackground = Color.FromArgb(255, 245, 230),
                TextBoxText = Color.FromArgb(120, 80, 50),
                HighlightBackground = Color.FromArgb(235, 215, 175),
                BandHighlightColor = Color.FromArgb(245, 222, 179),
                GradientStartColor = Color.FromArgb(250, 240, 210),
                GradientEndColor = Color.FromArgb(210, 190, 140)
            },
            ["Lilac"] = new Theme
            {
                ButtonBackground = Color.FromArgb(225, 200, 230),
                ButtonTextColor = Color.FromArgb(90, 50, 120),
                LabelTextColor = Color.FromArgb(90, 50, 120),
                TextBoxBackground = Color.FromArgb(240, 220, 245),
                TextBoxText = Color.FromArgb(80, 40, 100),
                HighlightBackground = Color.FromArgb(230, 210, 240),
                BandHighlightColor = Color.FromArgb(225, 200, 230),
                GradientStartColor = Color.FromArgb(245, 230, 250),
                GradientEndColor = Color.FromArgb(190, 150, 210)
            },
            ["Sunset"] = new Theme
            {
                ButtonBackground = Color.FromArgb(255, 200, 150),
                ButtonTextColor = Color.FromArgb(139, 69, 19),
                LabelTextColor = Color.FromArgb(139, 69, 19),
                TextBoxBackground = Color.FromArgb(255, 240, 230),
                TextBoxText = Color.FromArgb(110, 50, 25),
                HighlightBackground = Color.FromArgb(255, 220, 190),
                BandHighlightColor = Color.FromArgb(255, 200, 150),
                GradientStartColor = Color.FromArgb(255, 230, 210),
                GradientEndColor = Color.FromArgb(255, 165, 120)
            },
            ["Forest"] = new Theme
            {
                ButtonBackground = Color.FromArgb(200, 230, 200),
                ButtonTextColor = Color.FromArgb(34, 85, 34),
                LabelTextColor = Color.FromArgb(34, 85, 34),
                TextBoxBackground = Color.FromArgb(240, 250, 240),
                TextBoxText = Color.FromArgb(45, 90, 45),
                GridBackground = Color.FromArgb(240, 250, 240),
                GridRowBackground = Color.FromArgb(45, 90, 45),
                HighlightBackground = Color.FromArgb(220, 240, 220),
                BandHighlightColor = Color.FromArgb(200, 230, 200),
                GradientStartColor = Color.FromArgb(230, 250, 230),
                GradientEndColor = Color.FromArgb(120, 180, 120)
            },
            ["Ocean"] = new Theme
            {
                ButtonBackground = Color.FromArgb(180, 220, 240),
                ButtonTextColor = Color.FromArgb(20, 70, 100),
                LabelTextColor = Color.FromArgb(20, 70, 100),
                TextBoxBackground = Color.FromArgb(220, 240, 250),
                TextBoxText = Color.FromArgb(25, 75, 105),
                HighlightBackground = Color.FromArgb(200, 220, 240),
                BandHighlightColor = Color.FromArgb(180, 220, 240),
                GradientStartColor = Color.FromArgb(240, 250, 255),
                GradientEndColor = Color.FromArgb(100, 150, 200)
            }
        };

        // Текущая активная тема
        public static Theme ActiveTheme { get; private set; }// = Lavander;
        static ThemeManager() { LoadTheme(); }
        public static List<string> GetAvailableThemes() => SettingsManager.GetThemes().Keys.ToList();

        public static void SetTheme(string themeName)
        {
            CurrentTheme = themeName;
            var themes = SettingsManager.GetThemes();
            if (themes.TryGetValue(themeName, out var themeFromSettings))
                ActiveTheme = CloneTheme(themeFromSettings);
            else
                ActiveTheme = CloneTheme(DefaultThemes.TryGetValue("Gray", out var gray) ? gray : themes.Values.FirstOrDefault());

            // Цвета уже загружены из словаря тем, просто уведомляем об изменении
            ThemeChanged?.Invoke(); // Уведомление всех подписчиков
        }

        public static void LoadTheme()
        {
            SettingsManager.LoadTheme();
        }

        public static Dictionary<string, Theme> GetDefaultThemes()
        {
            return DefaultThemes.ToDictionary(kvp => kvp.Key, kvp => CloneTheme(kvp.Value));
        }

        public static void ApplyUserColorOverrides(Color? labelOverride = null, Color? textBoxOverride = null, bool raiseEvent = true)
        {
            if (ActiveTheme == null)
                return;

            // Применяем переданные цвета для предпросмотра (если переданы)
            if (labelOverride.HasValue)
                ActiveTheme.LabelTextColor = labelOverride.Value;

            if (textBoxOverride.HasValue)
                ActiveTheme.TextBoxBackground = textBoxOverride.Value;

            if (raiseEvent)
                ThemeChanged?.Invoke();
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
                ThemeChanged?.Invoke(); // Оповещаем всех подписчиков об изменении темы
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

        private static Theme CloneTheme(Theme source)
        {
            if (source == null) return null;
            return new Theme
            {
                ButtonBackground = source.ButtonBackground,
                ButtonTextColor = source.ButtonTextColor,
                GridBackground = source.GridBackground,
                GridRowBackground = source.GridRowBackground,
                GridTextColor = source.GridTextColor,
                LabelTextColor = source.LabelTextColor,
                TextBoxBackground = source.TextBoxBackground,
                TextBoxText = source.TextBoxText,
                LabelText = source.LabelText,
                HighlightBackground = source.HighlightBackground,
                BandHighlightColor = source.BandHighlightColor,
                GradientStartColor = source.GradientStartColor,
                GradientEndColor = source.GradientEndColor,
                OkButtonBackground = source.OkButtonBackground,
                OkButtonTextColor = source.OkButtonTextColor,
                OkButtonText = source.OkButtonText,
                CancelButtonBackground = source.CancelButtonBackground,
                CancelButtonTextColor = source.CancelButtonTextColor,
                CancelButtonText = source.CancelButtonText
            };
        }
    }
}