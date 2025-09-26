using DevExpress.XtraLayout;
using System;

namespace SewingProduction.Features.TeamWork.Forms
{
    /// <summary>
    /// Класс для управления доступностью кнопки "bind:unlink"
    /// </summary>
    public class ButtonUnbindWd
    {
        private readonly LayoutControlGroup _layoutGroup;
        private readonly string _buttonTag;
        private DevExpress.XtraEditors.ButtonPanel.BaseButton _button;

        /// <summary>
        /// Конструктор класса ButtonUnbindWd
        /// </summary>
        /// <param name="layoutGroup">Группа layout контролов</param>
        /// <param name="buttonTag">Тег кнопки для поиска</param>
        public ButtonUnbindWd(LayoutControlGroup layoutGroup, string buttonTag)
        {
            _layoutGroup = layoutGroup ?? throw new ArgumentNullException(nameof(layoutGroup));
            _buttonTag = buttonTag ?? throw new ArgumentNullException(nameof(buttonTag));
            
            InitializeButton();
        }

        /// <summary>
        /// Инициализирует кнопку по тегу
        /// </summary>
        private void InitializeButton()
        {
            _button = FindButtonByTag(_layoutGroup, _buttonTag);
        }

        /// <summary>
        /// Поиск кнопки по тегу в группе контролов
        /// </summary>
        /// <param name="layoutGroup">Группа layout контролов</param>
        /// <param name="tag">Тег для поиска</param>
        /// <returns>Найденная кнопка или null</returns>
        private DevExpress.XtraEditors.ButtonPanel.BaseButton FindButtonByTag(LayoutControlGroup layoutGroup, string tag)
        {
            if (layoutGroup?.CustomHeaderButtons != null)
            {
                foreach (var button in layoutGroup.CustomHeaderButtons)
                {
                    if (button is DevExpress.XtraEditors.ButtonPanel.BaseButton baseButton && 
                        baseButton.Tag?.ToString() == tag)
                    {
                        return baseButton;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Получает или устанавливает доступность кнопки
        /// </summary>
        public bool Enabled
        {
            get { return _button?.Enabled ?? false; }
            set 
            { 
                if (_button != null)
                {
                    _button.Enabled = value;
                }
            }
        }

        /// <summary>
        /// Получает или устанавливает видимость кнопки
        /// </summary>
        public bool Visible
        {
            get { return _button?.Visible ?? false; }
            set 
            { 
                if (_button != null)
                {
                    _button.Visible = value;
                }
            }
        }

        /// <summary>
        /// Проверяет, инициализирована ли кнопка
        /// </summary>
        public bool IsInitialized => _button != null;

        /// <summary>
        /// Включает кнопку (делает доступной)
        /// </summary>
        public void Enable()
        {
            Enabled = true;
        }

        /// <summary>
        /// Отключает кнопку (делает недоступной)
        /// </summary>
        public void Disable()
        {
            Enabled = false;
        }

        /// <summary>
        /// Показывает кнопку
        /// </summary>
        public void Show()
        {
            Visible = true;
        }

        /// <summary>
        /// Скрывает кнопку
        /// </summary>
        public void Hide()
        {
            Visible = false;
        }

        /// <summary>
        /// Получает текст кнопки
        /// </summary>
        public string Text
        {
            get { return _button?.Caption ?? string.Empty; }
            set 
            { 
                if (_button != null)
                {
                    _button.Caption = value;
                }
            }
        }

        /// <summary>
        /// Получает тег кнопки
        /// </summary>
        public string Tag => _buttonTag;
    }
}
