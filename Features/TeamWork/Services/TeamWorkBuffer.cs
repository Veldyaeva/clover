using SewingProduction.Models;
using System;

namespace SewingProduction.Services
{
    /// <summary>
    /// Класс для управления буфером данных TeamWork
    /// </summary>
    public static class TeamWorkBuffer
    {
        private static int _bufferId = 0;
        private static string _bufferText = string.Empty;
        private static ArtNormN _bufferData = null;

        /// <summary>
        /// Событие изменения буфера
        /// </summary>
        public static event EventHandler<BufferChangedEventArgs> BufferChanged;

        /// <summary>
        /// ID буферизованного элемента
        /// </summary>
        public static int BufferId 
        { 
            get => _bufferId;
            private set
            {
                if (_bufferId != value)
                {
                    _bufferId = value;
                    OnBufferChanged();
                }
            }
        }

        /// <summary>
        /// Текстовое описание буферизованного элемента
        /// </summary>
        public static string BufferText 
        { 
            get => _bufferText;
            private set
            {
                if (_bufferText != value)
                {
                    _bufferText = value;
                    OnBufferChanged();
                }
            }
        }

        /// <summary>
        /// Данные буферизованного элемента
        /// </summary>
        public static ArtNormN BufferData 
        { 
            get => _bufferData;
            private set
            {
                _bufferData = value;
                OnBufferChanged();
            }
        }

        /// <summary>
        /// Проверяет, есть ли данные в буфере
        /// </summary>
        public static bool HasData => _bufferId > 0;

        /// <summary>
        /// Копирует данные в буфер
        /// </summary>
        /// <param name="annId">ID разделения труда</param>
        /// <param name="displayText">Текст для отображения</param>
        /// <param name="data">Данные разделения труда</param>
        public static void CopyToBuffer(int annId, string displayText, ArtNormN data = null)
        {
            BufferId = annId;
            BufferText = displayText;
            BufferData = data?.Clone();
        }

        /// <summary>
        /// Очищает буфер
        /// </summary>
        public static void ClearBuffer()
        {
            BufferId = 0;
            BufferText = string.Empty;
            BufferData = null;
        }

        /// <summary>
        /// Вызывает событие изменения буфера
        /// </summary>
        private static void OnBufferChanged()
        {
            BufferChanged?.Invoke(null, new BufferChangedEventArgs 
            { 
                BufferId = _bufferId, 
                BufferText = _bufferText,
                HasData = HasData
            });
        }
    }

    /// <summary>
    /// Аргументы события изменения буфера
    /// </summary>
    public class BufferChangedEventArgs : EventArgs
    {
        public int BufferId { get; set; }
        public string BufferText { get; set; }
        public bool HasData { get; set; }
    }
} 