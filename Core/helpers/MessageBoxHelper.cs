using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using SewingProduction;
using SewingProduction.Features.UserDistribution.Class;
using SewingProduction.Features.UserDistribution.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static DevExpress.XtraEditors.XtraInputBox;
using Label = System.Windows.Forms.Label;

public enum ButtonLayout
{
    Horizontal,
    Vertical,
    Auto
}

public class MessageBoxOptions
{
    // Основные свойства
    public string Text { get; set; }
    public string Caption { get; set; }
    public Dictionary<string, DialogResult> Buttons { get; set; }

    // Иконка и внешний вид
    public MessageBoxIcon Icon { get; set; } = MessageBoxIcon.None;
    public bool ShowIcon { get; set; } = true;
    public ButtonLayout ButtonLayout { get; set; } = ButtonLayout.Auto;

    // Позиционирование
    public Point? Location { get; set; }
    public FormStartPosition StartPosition { get; set; } = FormStartPosition.CenterScreen;

    // Цвета и шрифты
    public Color? BackColor { get; set; }
    public Color? ForeColor { get; set; }
    public Font TextFont { get; set; }
    public Font ButtonFont { get; set; }

    // Размеры формы
    public int? Width { get; set; }
    public int? Height { get; set; }
    public int MaxFormWidth { get; set; } = 600;
    public int MaxFormHeight { get; set; } = 0; // 0 = без ограничения
    public int MinFormWidth { get; set; } = 250;
    public int MinFormHeight { get; set; } = 80; // Уменьшили для небольших сообщений

    // Настройки кнопок (уменьшаем размеры для компактности)
    public bool AutoSizeButtons { get; set; } = true;
    public int MaxButtonWidth { get; set; } = 250;
    public int MinButtonWidth { get; set; } = 100;
    public int ButtonHeight { get; set; } = 34; // Уменьшили высоту
    public int ButtonSpacing { get; set; } = 10;
    public int ButtonPadding { get; set; } = 12; // Уменьшили отступы
    public int ButtonTopMargin { get; set; } = 25;

    // Отступы
    public Padding FormPadding { get; set; } = new Padding(15);
    public int TextLeftMargin { get; set; } = 15;
    public int TextTopMargin { get; set; } = 15;

    // Дополнительные опции
    public bool WordWrapText { get; set; } = true;
    public bool CenterButtons { get; set; } = true;
    public bool ShowInTaskbar { get; set; } = false;

    // Настройки для вертикального расположения
    public int VerticalButtonSpacing { get; set; } = 8;
    public bool StretchVerticalButtons { get; set; } = true;
}

// Кастомная форма для MessageBox с поддержкой тем
public class CustomMessageBoxForm : CustomForm
{
    private PanelControl _mainPanel;
    private MessageBoxOptions _options;
    private bool _useVerticalLayout;
    private List<SimpleButton> _buttons = new List<SimpleButton>();

    public CustomMessageBoxForm(MessageBoxOptions options, bool useVerticalLayout) : base()
    {
        _options = options;
        _useVerticalLayout = useVerticalLayout;

        InitializeForm();
        // Тема применится автоматически через базовый конструктор CustomForm
    }

    public CustomMessageBoxForm(UserClass user, MessageBoxOptions options, bool useVerticalLayout) : base(user)
    {
        _options = options;
        _useVerticalLayout = useVerticalLayout;

        InitializeForm();
        // Тема применится автоматически через базовый конструктор CustomForm
    }

    private void InitializeForm()
    {
        this.Text = _options.Caption ?? "Сообщение";
        this.StartPosition = _options.StartPosition;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.ShowInTaskbar = _options.ShowInTaskbar;

        // Устанавливаем позицию если задана
        if (_options.Location.HasValue)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = _options.Location.Value;
        }

        // Устанавливаем размер если задан
        if (_options.Width.HasValue && _options.Height.HasValue)
        {
            this.ClientSize = new Size(_options.Width.Value, _options.Height.Value);
        }

        // Основной контейнер
        _mainPanel = new PanelControl
        {
            Dock = DockStyle.Fill,
            Padding = _options.FormPadding
        };

        // Устанавливаем цвет фона если задан, иначе оставляем для применения темы
        if (_options.BackColor.HasValue)
        {
            _mainPanel.BackColor = _options.BackColor.Value;
        }

        this.Controls.Add(_mainPanel);
    }

    public void AddContent(int contentHeight, int contentWidth, List<SimpleButton> buttons)
    {
        _buttons = buttons;

        // Рассчитываем финальный размер
        FinalizeFormSize(contentHeight, contentWidth);

        // Перераспределяем кнопки
        int buttonTop = _options.TextTopMargin + contentHeight + _options.ButtonTopMargin;

        if (_useVerticalLayout)
        {
            DistributeButtonsVertically(buttonTop);
        }
        else
        {
            DistributeButtonsHorizontally(buttonTop);
        }
    }

    private void FinalizeFormSize(int contentHeight, int contentWidth)
    {
        if (_buttons.Count == 0) return;

        // Рассчитываем размеры кнопок
        int buttonsHeight = 0;
        int buttonsWidth = 0;

        if (_useVerticalLayout)
        {
            foreach (var btn in _buttons)
            {
                buttonsHeight += btn.Height;
                buttonsWidth = Math.Max(buttonsWidth, btn.Width);
            }
            buttonsHeight += _options.VerticalButtonSpacing * (_buttons.Count - 1);
        }
        else
        {
            foreach (var btn in _buttons)
            {
                buttonsHeight = Math.Max(buttonsHeight, btn.Height);
                buttonsWidth += btn.Width;
            }
            buttonsWidth += _options.ButtonSpacing * (_buttons.Count - 1);
        }

        // Рассчитываем ширину формы
        int requiredWidth = Math.Max(contentWidth, buttonsWidth) +
                          _options.FormPadding.Horizontal + 10;

        int formWidth = _options.Width ??
            Math.Min(Math.Max(requiredWidth, _options.MinFormWidth), _options.MaxFormWidth);

        // Рассчитываем высоту на основе фактического контента
        int calculatedHeight = _options.TextTopMargin + contentHeight +
                              _options.ButtonTopMargin + buttonsHeight +
                              _options.FormPadding.Vertical + 10;

        // Берем максимальное из: расчетной высоты, минимальной высоты и минимальной для кнопок
        int minRequiredHeight = _options.MinFormHeight;
        int buttonAreaHeight = buttonsHeight + _options.ButtonTopMargin;

        if (buttonAreaHeight + 50 > minRequiredHeight) // Если кнопки требуют больше места
        {
            minRequiredHeight = buttonAreaHeight + 50;
        }

        int formHeight = _options.Height ?? Math.Max(calculatedHeight, minRequiredHeight);

        this.ClientSize = new Size(formWidth, formHeight);
    }

    private void DistributeButtonsHorizontally(int buttonTop)
    {
        if (_buttons.Count == 0) return;

        int totalButtonsWidth = 0;
        int maxButtonHeight = 0;

        foreach (var btn in _buttons)
        {
            totalButtonsWidth += btn.Width;
            maxButtonHeight = Math.Max(maxButtonHeight, btn.Height);
        }

        int totalSpacing = _options.ButtonSpacing * (_buttons.Count - 1);
        int totalWidth = totalButtonsWidth + totalSpacing;

        // Центрируем
        int startX = (_mainPanel.Width - totalWidth) / 2;
        if (startX < _options.FormPadding.Left)
            startX = _options.FormPadding.Left;

        // Располагаем кнопки
        for (int i = 0; i < _buttons.Count; i++)
        {
            var btn = _buttons[i];
            int verticalOffset = (maxButtonHeight - btn.Height) / 2;
            btn.Location = new Point(startX, buttonTop + verticalOffset);
            startX += btn.Width + _options.ButtonSpacing;
        }
    }

    private void DistributeButtonsVertically(int buttonTop)
    {
        if (_buttons.Count == 0) return;

        int maxButtonWidth = 0;
        foreach (var btn in _buttons)
            maxButtonWidth = Math.Max(maxButtonWidth, btn.Width);

        // Центрируем
        int startX = (_mainPanel.Width - maxButtonWidth) / 2;
        if (startX < _options.FormPadding.Left)
            startX = _options.FormPadding.Left;

        // Располагаем кнопки вертикально
        int currentY = buttonTop;
        for (int i = 0; i < _buttons.Count; i++)
        {
            var btn = _buttons[i];

            // Если нужно растягивать кнопки
            if (_options.StretchVerticalButtons)
            {
                btn.Width = _mainPanel.Width - _options.FormPadding.Horizontal;
                startX = _options.FormPadding.Left;
            }

            btn.Location = new Point(startX, currentY);
            currentY += btn.Height + _options.VerticalButtonSpacing;
        }
    }

    public void AddControl(Control control)
    {
        _mainPanel.Controls.Add(control);
    }

    // Применяем тему к контролам при загрузке формы
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        ApplyThemeToControls();
    }

    private void ApplyThemeToControls()
    {
        // Для сообщений теперь используем только системные цвета,
        // если явный цвет не был задан в опциях
        if (_mainPanel != null && !_options.BackColor.HasValue)
        {
            _mainPanel.BackColor = SystemColors.Control;
        }
    }
}

public static class AdvancedMessageBox
{
    private const int DEFAULT_ICON_SIZE = 32;
    private const int ICON_TEXT_SPACING = 12;

    // Получаем шрифт из темы или используем дефолтный
    private static Font GetDefaultFont()
    {
        try
        {
            if (ThemeManager.SharedSettings != null && ThemeManager.SharedSettings.DefaultFont != null)
                return ThemeManager.SharedSettings.DefaultFont;
        }
        catch { }

        return new Font("Segoe UI", 9f);
    }

    // Получаем цвет текста (системный, без завязки на кастомную тему)
    private static Color GetTextColor()
    {
        return SystemColors.ControlText;
    }

    // Получаем цвет фона (системный)
    private static Color GetBackgroundColor()
    {
        return SystemColors.Control;
    }

    #region Основной интеллектуальный метод Show

    /// <summary>
    /// Универсальный метод отображения сообщения. Автоматически определяет параметры отображения.
    /// </summary>
    public static DialogResult Show(object message, string caption = null, object buttons = null,
                                   MessageBoxIcon icon = MessageBoxIcon.None, ButtonLayout layout = ButtonLayout.Auto,
                                   UserClass user = null, Point? location = null)
    {
        // Извлекаем текст сообщения
        string text = ExtractText(message);

        // Определяем заголовок
        string finalCaption = caption ?? DetermineCaption(icon, text);

        // Определяем кнопки
        Dictionary<string, DialogResult> buttonDict = ExtractButtons(buttons, icon);

        // Определяем иконку
        MessageBoxIcon finalIcon = DetermineIcon(icon, text);

        // Определяем layout
        ButtonLayout finalLayout = DetermineLayout(layout, buttonDict, text);

        // Создаем опции
        var options = new MessageBoxOptions
        {
            Text = text,
            Caption = finalCaption,
            Buttons = buttonDict,
            Icon = finalIcon,
            ButtonLayout = finalLayout,
            ShowIcon = finalIcon != MessageBoxIcon.None,
            TextFont = GetDefaultFont(),
            ButtonFont = GetDefaultFont(),
            ForeColor = GetTextColor(),
            BackColor = GetBackgroundColor(),
            Location = location
        };

        // Анализируем и настраиваем опции
        AutoConfigureOptions(options);

        // Отображаем сообщение
        return ShowInternal(options, user);
    }

    #region Вспомогательные методы определения параметров

    private static string ExtractText(object message)
    {
        if (message == null) return string.Empty;

        if (message is string str)
            return str;

        if (message is Exception ex)
            return $"Ошибка: {ex.Message}\n\n{ex.StackTrace}";

        return message.ToString();
    }

    private static string DetermineCaption(MessageBoxIcon icon, string text)
    {
        if (icon != MessageBoxIcon.None)
        {
            return icon switch
            {
                MessageBoxIcon.Error => "Ошибка",
                MessageBoxIcon.Warning => "Предупреждение",
                MessageBoxIcon.Information => "Информация",
                MessageBoxIcon.Question => "Вопрос",
                _ => "Сообщение"
            };
        }

        // Пытаемся определить по тексту
        string lowerText = text.ToLower();
        if (lowerText.Contains("ошибка") || lowerText.Contains("error"))
            return "Ошибка";
        if (lowerText.Contains("внимание") || lowerText.Contains("warning") ||
            lowerText.Contains("предупреждение"))
            return "Предупреждение";
        if (lowerText.Contains("информация") || lowerText.Contains("info"))
            return "Информация";
        if (lowerText.Contains("вопрос") || lowerText.Contains("question"))
            return "Вопрос";

        return "Сообщение";
    }

    private static Dictionary<string, DialogResult> ExtractButtons(object buttons, MessageBoxIcon icon)
    {
        // Если передали Dictionary
        if (buttons is Dictionary<string, DialogResult> dict)
            return new Dictionary<string, DialogResult>(dict);

        // Если передали массив кортежей
        if (buttons is (string, DialogResult)[] tupleArray)
        {
            var result = new Dictionary<string, DialogResult>();
            foreach (var (text, resultValue) in tupleArray)
                result[text] = resultValue;
            return result;
        }

        // Если передали массив строк
        if (buttons is string[] stringArray)
        {
            var result = new Dictionary<string, DialogResult>();
            foreach (var text in stringArray)
            {
                DialogResult dialogResult = text.ToLower() switch
                {
                    "ok" or "ок" => DialogResult.OK,
                    "cancel" or "отмена" => DialogResult.Cancel,
                    "yes" or "да" => DialogResult.Yes,
                    "no" or "нет" => DialogResult.No,
                    "abort" or "прервать" => DialogResult.Abort,
                    "retry" or "повторить" => DialogResult.Retry,
                    "ignore" or "пропустить" => DialogResult.Ignore,
                    _ => DialogResult.None
                };
                result[text] = dialogResult;
            }
            return result;
        }

        // Если кнопки не указаны, используем кнопки по умолчанию в зависимости от иконки
        return GetDefaultButtons(icon);
    }

    private static Dictionary<string, DialogResult> GetDefaultButtons(MessageBoxIcon icon)
    {
        return icon switch
        {
            MessageBoxIcon.Error => new Dictionary<string, DialogResult> { { "OK", DialogResult.OK } },
            MessageBoxIcon.Warning => new Dictionary<string, DialogResult>
            {
                { "Понятно", DialogResult.OK },
                { "Отмена", DialogResult.Cancel }
            },
            MessageBoxIcon.Question => new Dictionary<string, DialogResult>
            {
                { "Да", DialogResult.Yes },
                { "Нет", DialogResult.No }
            },
            MessageBoxIcon.Information => new Dictionary<string, DialogResult> { { "OK", DialogResult.OK } },
            _ => new Dictionary<string, DialogResult> { { "OK", DialogResult.OK } }
        };
    }

    private static MessageBoxIcon DetermineIcon(MessageBoxIcon requestedIcon, string text)
    {
        if (requestedIcon != MessageBoxIcon.None)
            return requestedIcon;

        string lowerText = text.ToLower();
        if (lowerText.Contains("ошибка") || lowerText.Contains("error") || lowerText.Contains("exception"))
            return MessageBoxIcon.Error;
        if (lowerText.Contains("внимание") || lowerText.Contains("warning") ||
            lowerText.Contains("не совпадает") || lowerText.Contains("предупреждение"))
            return MessageBoxIcon.Warning;
        if (lowerText.Contains("вопрос") || lowerText.Contains("question") ||
            lowerText.Contains("продолжить") || lowerText.Contains("назначить") ||
            lowerText.Contains("подтверждение") || lowerText.Contains("подтвердите"))
            return MessageBoxIcon.Question;
        if (lowerText.Contains("информация") || lowerText.Contains("info") ||
            lowerText.Contains("успешно") || lowerText.Contains("success") ||
            lowerText.Contains("готово") || lowerText.Contains("complete"))
            return MessageBoxIcon.Information;

        return MessageBoxIcon.None;
    }

    private static ButtonLayout DetermineLayout(ButtonLayout requestedLayout,
                                               Dictionary<string, DialogResult> buttons, string text)
    {
        if (requestedLayout != ButtonLayout.Auto)
            return requestedLayout;

        // Автоматически определяем layout
        int buttonCount = buttons.Count;

        // Если много кнопок
        if (buttonCount > 3)
            return ButtonLayout.Vertical;

        // Проверяем длину текста кнопок
        using (var g = Graphics.FromHwnd(IntPtr.Zero))
        {
            int totalTextWidth = 0;
            int maxTextWidth = 0;

            foreach (var buttonText in buttons.Keys)
            {
                Size textSize = TextRenderer.MeasureText(g, buttonText, GetDefaultFont());
                totalTextWidth += textSize.Width;
                maxTextWidth = Math.Max(maxTextWidth, textSize.Width);

                // Если хотя бы одна кнопка имеет очень длинный текст
                if (textSize.Width > 200)
                    return ButtonLayout.Vertical;

                // Проверяем многострочный текст
                Size wrappedSize = TextRenderer.MeasureText(g, buttonText, GetDefaultFont(),
                    new Size(250, int.MaxValue), TextFormatFlags.WordBreak);
                if (wrappedSize.Height > 25) // Если текст требует несколько строк
                    return ButtonLayout.Vertical;
            }

            // Если суммарная ширина большая
            if (totalTextWidth > 500 || maxTextWidth > 180)
                return ButtonLayout.Vertical;
        }

        return ButtonLayout.Horizontal;
    }

    #endregion

    #endregion

    #region Специализированные методы (удобные обертки)

    /// <summary>Простое сообщение с текстом</summary>
    public static DialogResult Show(string text, Point? location = null)
    {
        return Show(text, null, null, MessageBoxIcon.None, ButtonLayout.Auto, null, location);
    }

    /// <summary>Сообщение с текстом и заголовком</summary>
    public static DialogResult Show(string text, string caption, Point? location = null)
    {
        return Show(text, caption, null, MessageBoxIcon.None, ButtonLayout.Auto, null, location);
    }

    /// <summary>Сообщение с текстом и кнопками</summary>
    public static DialogResult Show(string text, Dictionary<string, DialogResult> buttons, Point? location = null)
    {
        return Show(text, null, buttons, MessageBoxIcon.None, ButtonLayout.Auto, null, location);
    }

    /// <summary>Сообщение с текстом, заголовком и кнопками</summary>
    public static DialogResult Show(string text, string caption, Dictionary<string, DialogResult> buttons, Point? location = null)
    {
        return Show(text, caption, buttons, MessageBoxIcon.None, ButtonLayout.Auto, null, location);
    }

    /// <summary>Сообщение с текстом и кортежами кнопок</summary>
    public static DialogResult Show(string text, Point? location = null, params (string Text, DialogResult Result)[] buttons)
    {
        return Show(text, null, buttons, MessageBoxIcon.None, ButtonLayout.Auto, null, location);
    }

    /// <summary>Сообщение об ошибке (можно передать Exception или текст)</summary>
    public static DialogResult ShowError(object error, string caption = null, Point? location = null)
    {
        return Show(error, caption ?? "Ошибка", null, MessageBoxIcon.Error, ButtonLayout.Auto, null, location);
    }

    /// <summary>Предупреждение</summary>
    public static DialogResult ShowWarning(string text, string caption = null, Point? location = null)
    {
        return Show(text, caption ?? "Предупреждение", null, MessageBoxIcon.Warning, ButtonLayout.Auto, null, location);
    }

    /// <summary>Информационное сообщение</summary>
    public static DialogResult ShowInfo(string text, string caption = null, Point? location = null)
    {
        return Show(text, caption ?? "Информация", null, MessageBoxIcon.Information, ButtonLayout.Auto, null, location);
    }

    /// <summary>Вопрос с кнопками Да/Нет</summary>
    public static DialogResult ShowQuestion(string text, string caption = null, Point? location = null)
    {
        return Show(text, caption ?? "Вопрос", null, MessageBoxIcon.Question, ButtonLayout.Auto, null, location);
    }

    /// <summary>Подтверждение с кнопками Да/Нет</summary>
    public static DialogResult ShowConfirmation(string text, string caption = null, Point? location = null)
    {
        return Show(text, caption ?? "Подтверждение",
                   new Dictionary<string, DialogResult> { { "Да", DialogResult.Yes }, { "Нет", DialogResult.No } },
                   MessageBoxIcon.Question, ButtonLayout.Horizontal, null, location);
    }

    /// <summary>Вопрос с кнопками Да/Нет/Отмена</summary>
    public static DialogResult ShowYesNoCancel(string text, string caption = null, Point? location = null)
    {
        return Show(text, caption ?? "Вопрос",
                   new Dictionary<string, DialogResult>
                   {
                       { "Да", DialogResult.Yes },
                       { "Нет", DialogResult.No },
                       { "Отмена", DialogResult.Cancel }
                   },
                   MessageBoxIcon.Question, ButtonLayout.Auto, null, location);
    }

    /// <summary>Сообщение о несовпадении машин (специализированное)</summary>
    public static DialogResult ShowMachineMismatch(string operation, string description,
                                                  string currentMachine, string plannedMachine,
                                                  Point? location = null)
    {
        string text = $"Внимание!\nОперация: {operation}\nОписание: {description}\n\n" +
                     $"Назначаемая машина ({currentMachine}) не совпадает с плановой ({plannedMachine})";

        var buttons = new Dictionary<string, DialogResult>
        {
            { $"Продолжить: назначить В/М для оп. {operation} {description}", DialogResult.Yes },
            { $"Пропустить: НЕ назначать В/М для оп. {operation} {description}", DialogResult.No },
            { "Отмена", DialogResult.Cancel }
        };

        return Show(text, "Назначение В/М на операцию", buttons, MessageBoxIcon.Warning, ButtonLayout.Auto, null, location);
    }

    /// <summary>Сообщение о несовпадении машин с передачей пользователя</summary>
    public static DialogResult ShowMachineMismatch(string operation, string description,
                                                  string currentMachine, string plannedMachine,
                                                  UserClass user, Point? location = null)
    {
        string text = $"Внимание!\nОперация: {operation}\nОписание: {description}\n\n" +
                     $"Назначаемая машина ({currentMachine}) не совпадает с плановой ({plannedMachine})";

        var buttons = new Dictionary<string, DialogResult>
        {
            { $"Продолжить: назначить В/М для оп. {operation} {description}", DialogResult.Yes },
            { $"Пропустить: НЕ назначать В/М для оп. {operation} {description}", DialogResult.No },
            { "Отмена", DialogResult.Cancel }
        };

        return Show(text, "Назначение В/М на операцию", buttons, MessageBoxIcon.Warning, ButtonLayout.Auto, user, location);
    }

    /// <summary>Отображение с опциями для полного контроля</summary>
    public static DialogResult Show(MessageBoxOptions options, UserClass user = null)
    {
        if (options == null)
            throw new ArgumentNullException(nameof(options));

        if (options.Buttons == null || options.Buttons.Count == 0)
        {
            options.Buttons = new Dictionary<string, DialogResult>
            {
                { "OK", DialogResult.OK }
            };
        }

        AutoConfigureOptions(options);
        return ShowInternal(options, user);
    }

    #endregion

    #region Внутренняя реализация

    private static DialogResult ShowInternal(MessageBoxOptions options, UserClass user)
    {
        // Создаем форму
        bool useVerticalLayout = ShouldUseVerticalLayout(options);
        CustomMessageBoxForm form;

        if (user != null)
        {
            form = new CustomMessageBoxForm(user, options, useVerticalLayout);
        }
        else
        {
            form = new CustomMessageBoxForm(options, useVerticalLayout);
        }

        using (form)
        {
            // Добавляем содержимое
            AddContentToForm(form, options, out int contentHeight, out int contentWidth);

            // Создаем кнопки (более компактные)
            var buttons = CreateCompactButtons(options, form.Width - options.FormPadding.Horizontal, useVerticalLayout);

            // Добавляем кнопки на форму
            foreach (var btn in buttons)
            {
                AddButtonClickHandler(btn, form);
                form.AddControl(btn);
            }

            // Устанавливаем финальный размер и позиционирование
            form.AddContent(contentHeight, contentWidth, buttons);

            return form.ShowDialog();
        }
    }

    private static void AddContentToForm(CustomMessageBoxForm form, MessageBoxOptions options,
                                        out int contentHeight, out int contentWidth)
    {
        contentHeight = 0;
        contentWidth = 0;

        int currentTop = options.TextTopMargin;
        int currentLeft = options.TextLeftMargin;

        // Добавляем иконку если нужно
        if (options.ShowIcon && options.Icon != MessageBoxIcon.None)
        {
            PictureBox iconBox = CreateIconBox(options);
            iconBox.Location = new Point(currentLeft, currentTop);
            form.AddControl(iconBox);

            currentLeft += iconBox.Width + ICON_TEXT_SPACING;
            contentHeight = Math.Max(contentHeight, iconBox.Height);
        }

        // Добавляем текст - используем обычный Label для лучшего переноса
        Label label = CreateTextLabel(options, form.Width - currentLeft - options.FormPadding.Right - 20);
        label.Location = new Point(currentLeft, currentTop);
        form.AddControl(label);

        // Рассчитываем размеры контента
        contentWidth = currentLeft + label.Width;
        contentHeight = Math.Max(contentHeight, label.Height);
    }
    private static Size CalculateTextSize(MessageBoxOptions options, CustomMessageBoxForm form, int currentLeft)
    {
        Font font = options.TextFont ?? GetDefaultFont();

        // Определяем максимальную ширину для текста
        int maxTextWidth = options.MaxFormWidth - options.FormPadding.Horizontal -
                          currentLeft - 20;

        if (maxTextWidth < 100) maxTextWidth = 100; // Минимальная ширина

        // Измеряем текст с учетом переноса строк
        using (Graphics g = form.CreateGraphics())
        {
            if (options.WordWrapText)
            {
                // Для переноса текста
                SizeF sizeF = g.MeasureString(options.Text, font, maxTextWidth);
                return new Size((int)Math.Ceiling(sizeF.Width), (int)Math.Ceiling(sizeF.Height));
            }
            else
            {
                // Без переноса текста
                SizeF sizeF = g.MeasureString(options.Text, font);
                return new Size((int)Math.Ceiling(sizeF.Width), (int)Math.Ceiling(sizeF.Height));
            }
        }
    }
    private static PictureBox CreateIconBox(MessageBoxOptions options)
    {
        PictureBox iconBox = new PictureBox
        {
            Size = new Size(DEFAULT_ICON_SIZE, DEFAULT_ICON_SIZE),
            SizeMode = PictureBoxSizeMode.StretchImage
        };

        switch (options.Icon)
        {
            case MessageBoxIcon.Information:
                iconBox.Image = SystemIcons.Information.ToBitmap();
                break;
            case MessageBoxIcon.Question:
                iconBox.Image = SystemIcons.Question.ToBitmap();
                break;
            case MessageBoxIcon.Warning:
                iconBox.Image = SystemIcons.Warning.ToBitmap();
                break;
            case MessageBoxIcon.Error:
                iconBox.Image = SystemIcons.Error.ToBitmap();
                break;
            default:
                iconBox.Visible = false;
                break;
        }

        return iconBox;
    }

    private static Label CreateTextLabel(MessageBoxOptions options, int availableWidth)
    {
        Label label = new Label
        {
            Text = options.Text,
            AutoSize = false, // Отключаем авторазмер
            MaximumSize = new Size(availableWidth, 0),
            MinimumSize = new Size(100, 0)
        };

        if (options.TextFont != null)
            label.Font = options.TextFont;

        if (options.ForeColor.HasValue)
            label.ForeColor = options.ForeColor.Value;

        // Настраиваем перенос текста
        if (options.WordWrapText)
        {
            label.AutoSize = true; // Включаем авторазмер по высоте
            label.MaximumSize = new Size(availableWidth, 0);
        }
        else
        {
            // Без переноса - измеряем ширину текста
            using (var g = Graphics.FromHwnd(IntPtr.Zero))
            {
                Size textSize = TextRenderer.MeasureText(g, options.Text,
                    options.TextFont ?? GetDefaultFont());
                label.Size = new Size(textSize.Width, textSize.Height);
            }
        }

        return label;
    }

    private static List<SimpleButton> CreateCompactButtons(MessageBoxOptions options, int availableWidth, bool verticalLayout)
    {
        var buttonList = new List<KeyValuePair<string, DialogResult>>(options.Buttons);
        var buttons = new List<SimpleButton>();

        Font buttonFont = options.ButtonFont ?? GetDefaultFont();

        using (var g = Graphics.FromHwnd(IntPtr.Zero))
        {
            // Сначала измеряем все тексты для оптимального распределения
            var buttonInfoList = new List<ButtonInfo>();

            foreach (var button in buttonList)
            {
                // Определяем ширину кнопки
                int buttonWidth;
                if (verticalLayout && options.StretchVerticalButtons)
                {
                    // В вертикальном режиме растягиваем на всю доступную ширину
                    buttonWidth = availableWidth;
                }
                else
                {
                    // В горизонтальном режиме рассчитываем оптимальную ширину
                    buttonWidth = CalculateOptimalButtonWidth(g, button.Key, buttonFont,
                        options, availableWidth, buttonList.Count);
                }

                // Рассчитываем компактную высоту на основе текста
                int buttonHeight = CalculateCompactButtonHeight(g, button.Key, buttonFont,
                    buttonWidth - options.ButtonPadding * 2, options);

                buttonInfoList.Add(new ButtonInfo
                {
                    Text = button.Key,
                    Result = button.Value,
                    Width = buttonWidth,
                    Height = buttonHeight
                });
            }

            // Выравниваем высоту кнопок для единообразия
            int maxHeight = buttonInfoList.Max(b => b.Height);
            maxHeight = Math.Min(maxHeight, 40); // Максимум 40px для компактности

            foreach (var info in buttonInfoList)
            {
                // Создаем кнопку с выровненной высотой
                SimpleButton btn = new SimpleButton
                {
                    Text = info.Text,
                    Size = new Size(info.Width, maxHeight),
                    DialogResult = info.Result
                };

                ConfigureCompactButtonAppearance(btn, options);
                buttons.Add(btn);
            }
        }

        return buttons;
    }

    private class ButtonInfo
    {
        public string Text { get; set; }
        public DialogResult Result { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    private static int CalculateOptimalButtonWidth(Graphics g, string buttonText, Font font,
                                                  MessageBoxOptions options, int availableWidth, int buttonCount)
    {
        // Измеряем текст
        Size textSize = TextRenderer.MeasureText(g, buttonText, font);
        int requiredWidth = textSize.Width + options.ButtonPadding * 2;

        // Рассчитываем доступную ширину
        int totalSpacing = options.ButtonSpacing * (buttonCount - 1);
        int availableForButtons = availableWidth - totalSpacing;
        int maxWidthPerButton = Math.Max(availableForButtons / buttonCount, options.MinButtonWidth);

        // Применяем ограничения
        int finalWidth = Math.Max(requiredWidth, options.MinButtonWidth);
        finalWidth = Math.Min(finalWidth, Math.Min(maxWidthPerButton, options.MaxButtonWidth));

        // Для лучшего вида делаем кнопки одинаковой ширины
        if (buttonCount > 1)
        {
            finalWidth = Math.Max(finalWidth, (int)(maxWidthPerButton * 0.8));
        }

        return finalWidth;
    }

    private static int CalculateCompactButtonHeight(Graphics g, string buttonText, Font font,
                                                   int maxTextWidth, MessageBoxOptions options)
    {
        if (string.IsNullOrEmpty(buttonText))
            return Math.Min(options.ButtonHeight, 32); // Ограничиваем высоту

        // Измеряем текст с учетом переноса
        Size textSize = TextRenderer.MeasureText(g, buttonText, font,
            new Size(maxTextWidth, int.MaxValue),
            TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl);

        // Компактная высота = текст + минимальные отступы
        int calculatedHeight = textSize.Height + 8; // Минимальные отступы

        // Применяем ограничения
        calculatedHeight = Math.Max(calculatedHeight, 28); // Минимум 28px
        calculatedHeight = Math.Min(calculatedHeight, 40); // Максимум 40px (компактно)

        return calculatedHeight;
    }

    private static void AddButtonClickHandler(SimpleButton btn, CustomMessageBoxForm form)
    {
        btn.Click += (s, e) =>
        {
            var simpleBtn = s as SimpleButton;
            form.DialogResult = simpleBtn?.DialogResult ?? DialogResult.None;
            form.Close();
        };
    }

    private static void ConfigureCompactButtonAppearance(SimpleButton btn, MessageBoxOptions options)
    {
        if (options.ButtonFont != null)
            btn.Font = options.ButtonFont;

        // Настраиваем текст для компактного отображения
        btn.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
        btn.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisWord;
        btn.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
        btn.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

        // Компактные отступы внутри кнопки
        btn.Appearance.Options.UseTextOptions = true;

        // Используем системные цвета для кнопок
        btn.Appearance.BackColor = SystemColors.Control;
        btn.Appearance.ForeColor = SystemColors.ControlText;
        btn.Appearance.Options.UseBackColor = true;
        btn.Appearance.Options.UseForeColor = true;
    }

    private static void AutoConfigureOptions(MessageBoxOptions options)
    {
        int buttonCount = options.Buttons?.Count ?? 0;

        // Автоматически определяем параметры на основе количества кнопок
        switch (buttonCount)
        {
            case 1:
                ConfigureForSingleButton(options);
                break;
            case 2:
                ConfigureForTwoButtons(options);
                break;
            case 3:
                ConfigureForThreeButtons(options);
                break;
            case 4:
                ConfigureForFourButtons(options);
                break;
            default:
                ConfigureForManyButtons(options, buttonCount);
                break;
        }

        // Улучшенная логика определения переноса текста
        if (!string.IsNullOrEmpty(options.Text))
        {
            Font textFont = options.TextFont ?? GetDefaultFont();

            using (var g = Graphics.FromHwnd(IntPtr.Zero))
            {
                // Измеряем текст без ограничений
                Size textSize = TextRenderer.MeasureText(g, options.Text, textFont);

                // Определяем среднюю длину слова
                string[] words = options.Text.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                int avgWordLength = words.Length > 0 ?
                    options.Text.Length / words.Length : 5;

                // Если текст короткий или состоит из длинных слов - не переносим
                if (textSize.Width < 350 && avgWordLength < 10)
                {
                    options.WordWrapText = false;
                }
                else
                {
                    options.WordWrapText = true;
                    // Для переносимого текста уменьшаем ширину окна
                    options.MaxFormWidth = Math.Min(options.MaxFormWidth, 500);
                }
            }
        }

        // Устанавливаем цвета по умолчанию из темы если они не заданы
        if (!options.ForeColor.HasValue)
            options.ForeColor = GetTextColor();

        if (!options.BackColor.HasValue)
            options.BackColor = GetBackgroundColor();
    }

    private static void ConfigureForSingleButton(MessageBoxOptions options)
    {
        options.AutoSizeButtons = true;
        options.MinButtonWidth = 180;
        options.MaxButtonWidth = 300;
        options.ButtonHeight = 34;
        options.ButtonPadding = 12;
        options.ButtonSpacing = 10;
        options.CenterButtons = true;
        options.FormPadding = new Padding(15);
        options.TextTopMargin = 15;
        options.ButtonTopMargin = 20;
        options.MaxFormWidth = 500;
        options.MinFormHeight = 140; // Уменьшили минимальную высоту
        options.WordWrapText = true;
    }

    private static void ConfigureForTwoButtons(MessageBoxOptions options)
    {
        options.AutoSizeButtons = true;
        options.MinButtonWidth = 160;
        options.MaxButtonWidth = 250;
        options.ButtonHeight = 34;
        options.ButtonPadding = 12;
        options.ButtonSpacing = 10;
        options.CenterButtons = true;
        options.FormPadding = new Padding(15);
        options.TextTopMargin = 15;
        options.ButtonTopMargin = 20;
        options.MaxFormWidth = 550;
        options.MinFormHeight = 140; // Уменьшили минимальную высоту
        options.WordWrapText = true;
    }

    private static void ConfigureForThreeButtons(MessageBoxOptions options)
    {
        options.AutoSizeButtons = true;
        options.MinButtonWidth = 140;
        options.MaxButtonWidth = 220;
        options.ButtonHeight = 34;
        options.ButtonPadding = 10;
        options.ButtonSpacing = 8;
        options.CenterButtons = true;
        options.FormPadding = new Padding(12);
        options.TextTopMargin = 12;
        options.ButtonTopMargin = 18;
        options.MaxFormWidth = 600;
        options.MaxFormHeight = 400;
        options.WordWrapText = true;
    }

    private static void ConfigureForFourButtons(MessageBoxOptions options)
    {
        options.AutoSizeButtons = true;
        options.MinButtonWidth = 120;
        options.MaxButtonWidth = 200;
        options.ButtonHeight = 32;
        options.ButtonPadding = 10;
        options.ButtonSpacing = 8;
        options.CenterButtons = true;
        options.FormPadding = new Padding(12);
        options.TextTopMargin = 12;
        options.ButtonTopMargin = 18;
        options.MaxFormWidth = 650;
        options.MaxFormHeight = 450;
        options.WordWrapText = true;
    }

    private static void ConfigureForManyButtons(MessageBoxOptions options, int buttonCount)
    {
        options.AutoSizeButtons = true;
        options.MinButtonWidth = 100;
        options.MaxButtonWidth = 180;
        options.ButtonHeight = 30;
        options.ButtonPadding = 8;
        options.ButtonSpacing = 6;
        options.CenterButtons = true;
        options.FormPadding = new Padding(10);
        options.TextTopMargin = 10;
        options.ButtonTopMargin = 15;
        options.VerticalButtonSpacing = 6;
        options.MaxFormWidth = Math.Min(700, buttonCount * 130);
        options.MaxFormHeight = 500;
        options.WordWrapText = true;
    }

    private static void AnalyzeButtonTextsAndAdjust(MessageBoxOptions options)
    {
        if (options.Buttons == null) return;

        Font buttonFont = options.ButtonFont ?? GetDefaultFont();
        bool hasLongText = false;

        using (var g = Graphics.FromHwnd(IntPtr.Zero))
        {
            foreach (var buttonText in options.Buttons.Keys)
            {
                // Проверяем ширину
                Size textSize = TextRenderer.MeasureText(g, buttonText, buttonFont);
                if (textSize.Width > 150)
                    hasLongText = true;
            }
        }

        // Корректируем настройки для длинных текстов
        if (hasLongText && options.ButtonLayout == ButtonLayout.Horizontal)
        {
            // Для длинных текстов в горизонтальном режиме уменьшаем ширину кнопок
            options.MinButtonWidth = Math.Max(options.MinButtonWidth - 20, 100);
            options.MaxButtonWidth = Math.Max(options.MaxButtonWidth - 30, 180);
        }
    }

    private static bool ShouldUseVerticalLayout(MessageBoxOptions options)
    {
        switch (options.ButtonLayout)
        {
            case ButtonLayout.Horizontal: return false;
            case ButtonLayout.Vertical: return true;
            case ButtonLayout.Auto:
            default:
                int buttonCount = options.Buttons?.Count ?? 0;

                // Если много кнопок
                if (buttonCount > 3)
                    return true;

                // Проверяем длину текста
                Font buttonFont = options.ButtonFont ?? GetDefaultFont();
                using (var g = Graphics.FromHwnd(IntPtr.Zero))
                {
                    int totalTextWidth = 0;
                    foreach (var buttonText in options.Buttons.Keys)
                    {
                        Size textSize = TextRenderer.MeasureText(g, buttonText, buttonFont);
                        totalTextWidth += textSize.Width;

                        // Если хотя бы одна кнопка имеет очень длинный текст
                        if (textSize.Width > 200)
                            return true;
                    }

                    // Если суммарная ширина большая
                    if (totalTextWidth > 450)
                        return true;
                }
                return false;
        }
    }

    #endregion
}
//---------------------------------
//// 1. Простой вызов (без пользователя)
//AdvancedMessageBox.Show("Текст сообщения");

//// 2. С пользователем (для прав доступа)
//AdvancedMessageBox.Show("Текст", "Заголовок", null, MessageBoxIcon.None, ButtonLayout.Auto, user);

//// 3. Ваш специфический случай
//AdvancedMessageBox.ShowMachineMismatch("1/5", "Вязание воротник", "в2193", "в1615");

//// 4. С пользователем для специфического случая
//AdvancedMessageBox.ShowMachineMismatch("1/5", "Вязание воротник", "в2193", "в1615", user);

//// 5. С опциями
//var options = new MessageBoxOptions
//{
//    Text = "Текст",
//    Caption = "Заголовок",
//    Buttons = buttons,
//    Icon = MessageBoxIcon.Warning
//};
//AdvancedMessageBox.Show(options, user); // с пользователем
//// или
//AdvancedMessageBox.Show(options); // без пользователя

//Вариант 1: Используя основной метод Show с параметрами
//csharp
//// Создаем словарь кнопок
//var buttons = new Dictionary<string, DialogResult>
//{
//    { "Продолжить: назначить В/М для оп. 1/5 Вязание воротник", DialogResult.Yes },
//    { "Пропустить: НЕ назначать В/М для оп. 1/5 Вязание воротник", DialogResult.No },
//    { "Отмена", DialogResult.Cancel }
//};

//// Вызов с явным указанием вертикального расположения
//var result = AdvancedMessageBox.Show(
//    message: $"Внимание!\nОперация: 1/5\nОписание: Вязание воротник\n\n" +
//             $"Назначаемая машина (в2193) не совпадает с плановой (в1615)",
//    caption: "Назначение В/М на операцию",
//    buttons: buttons,
//    icon: MessageBoxIcon.Warning,
//    layout: ButtonLayout.Vertical // Явно указываем вертикальное расположение
//);
//Вариант 2: Используя специализированный метод с кортежами
//csharp
//// Более компактный синтаксис с кортежами
//var result = AdvancedMessageBox.Show(
//    $"Внимание!\nОперация: 1/5\nОписание: Вязание воротник\n\n" +
//    $"Назначаемая машина (в2193) не совпадает с плановой (в1615)",
//    "Назначение В/М на операцию",
//    ("Продолжить: назначить В/М для оп. 1/5 Вязание воротник", DialogResult.Yes),
//    ("Пропустить: НЕ назначать В/М для оп. 1/5 Вязание воротник", DialogResult.No),
//    ("Отмена", DialogResult.Cancel),
//    layout: ButtonLayout.Vertical // Указываем layout отдельно
//);
//Вариант 3: Через MessageBoxOptions(полный контроль)
//csharp
//// Создаем опции с полным контролем
//var options = new MessageBoxOptions
//{
//    Text = $"Внимание!\nОперация: 1/5\nОписание: Вязание воротник\n\n" +
//           $"Назначаемая машина (в2193) не совпадает с плановой (в1615)",
//    Caption = "Назначение В/М на операцию",
//    Buttons = new Dictionary<string, DialogResult>
//    {
//        { "Продолжить: назначить В/М для оп. 1/5 Вязание воротник", DialogResult.Yes },
//        { "Пропустить: НЕ назначать В/М для оп. 1/5 Вязание воротник", DialogResult.No },
//        { "Отмена", DialogResult.Cancel }
//    },
//    Icon = MessageBoxIcon.Warning,
//    ButtonLayout = ButtonLayout.Vertical, // Вертикальное расположение
//    StretchVerticalButtons = true, // Растянуть кнопки по ширине
//    VerticalButtonSpacing = 15, // Больше расстояние между кнопками
//    ButtonHeight = 45, // Высота кнопок для многострочного текста
//    ButtonPadding = 20 // Отступы внутри кнопок
//};

//var result = AdvancedMessageBox.Show(options);
//Вариант 4: Для вашего конкретного случая (машины не совпадают)
//csharp
//// Самый простой для вашего случая
//var result = AdvancedMessageBox.ShowMachineMismatch(
//    operation: "1/5",
//    description: "Вязание воротник",
//    currentMachine: "в2193",
//    plannedMachine: "в1615"
//    // layout по умолчанию Auto, но можно передать ButtonLayout.Vertical если нужно
//);

//// Или с явным указанием вертикального расположения
//var result = AdvancedMessageBox.Show(
//    message: $"Внимание!\nОперация: 1/5\nОписание: Вязание воротник\n\n" +
//             $"Назначаемая машина (в2193) не совпадает с плановой (в1615)",
//    caption: "Назначение В/М на операцию",
//    buttons: new Dictionary<string, DialogResult>
//    {
//        { "Продолжить: назначить В/М для оп. 1/5 Вязание воротник", DialogResult.Yes },
//        { "Пропустить: НЕ назначать В/М для оп. 1/5 Вязание воротник", DialogResult.No },
//        { "Отмена", DialogResult.Cancel }
//    },
//    layout: ButtonLayout.Vertical // Говорим "делай вертикально"
//);
//Вариант 5: С пользователем(если нужны проверки прав доступа)
//csharp
//// Если нужно передать пользователя для проверки прав
//var result = AdvancedMessageBox.Show(
//    message: "Текст сообщения",
//    caption: "Заголовок",
//    buttons: new Dictionary<string, DialogResult>
//    {
//        { "Кнопка 1", DialogResult.OK },
//        { "Кнопка 2", DialogResult.Cancel }
//    },
//    layout: ButtonLayout.Vertical,
//    user: currentUser // Передаем объект пользователя
//);

//1.По центру экрана(по умолчанию):
//csharp
//var result = AdvancedMessageBox.Show(
//    $"Внимание!\nОперация: 1/5\nОписание: Вязание воротник\n\n" +
//    $"Назначаемая машина (в2193) не совпадает с плановой (в1615)",
//    "Назначение В/М на операцию",
//    new Dictionary<string, DialogResult>
//    {
//        { "Продолжить: назначить В/М для оп. 1/5 Вязание воротник", DialogResult.Yes },
//        { "Пропустить: НЕ назначать В/М для оп. 1/5 Вязание воротник", DialogResult.No },
//        { "Отмена", DialogResult.Cancel }
//    },
//    MessageBoxIcon.Warning,
//    ButtonLayout.Vertical
//);
//2.В указанных координатах:
//csharp
//// Указываем точку для открытия окна
//Point openPosition = new Point(100, 100);

//var result = AdvancedMessageBox.Show(
//    "Текст сообщения",
//    "Заголовок",
//    buttons,
//    MessageBoxIcon.Warning,
//    ButtonLayout.Vertical,
//    location: openPosition
//);
//3.Компактный вариант для вашего случая:
//csharp
//// Используем специализированный метод
//var result = AdvancedMessageBox.ShowMachineMismatch(
//    "1/5",
//    "Вязание воротник",
//    "в2193",
//    "в1615",
//    location: new Point(150, 150) // если нужно в определенном месте
//);
//---------------------------------