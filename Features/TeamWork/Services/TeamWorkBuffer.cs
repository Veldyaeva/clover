using System;
using System.Collections.Generic;
using System.Linq;
using SewingProduction.Models;

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
        // Keep track of multiple Ann IDs when copying multiple work divisions (e.g. for kit mode).
        private static List<int> _bufferIds = new List<int>();

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
            private set => _bufferId = value;
        }

        /// <summary>
        /// Gets the list of buffered Ann IDs. When multiple work divisions are copied
        /// into the buffer (e.g. in kit creation mode) this list will contain all of
        /// their identifiers. For backward compatibility the first element (if any)
        /// will also be exposed via <see cref="BufferId"/>.
        /// </summary>
        public static IReadOnlyList<int> BufferIds => _bufferIds.AsReadOnly();

        /// <summary>
        /// Текстовое описание буферизованного элемента
        /// </summary>
        public static string BufferText
        {
            get => _bufferText;
            private set => _bufferText = value;
        }

        /// <summary>
        /// Данные буферизованного элемента
        /// </summary>
        public static ArtNormN BufferData
        {
            get => _bufferData;
            private set => _bufferData = value;
        }

        /// <summary>
        /// Проверяет, есть ли данные в буфере
        /// </summary>
        public static bool HasData => _bufferIds != null && _bufferIds.Count > 0;

        /// <summary>
        /// Копирует данные в буфер
        /// </summary>
        /// <param name="annId">ID разделения труда</param>
        /// <param name="displayText">Текст для отображения</param>
        /// <param name="data">Данные разделения труда</param>
        /// <summary>
        /// Copies a single work division into the buffer. Existing buffer contents
        /// will be cleared. For multi‑selection scenarios, use
        /// <see cref="CopyToBuffer(IEnumerable{int}, string, ArtNormN)"/>.
        /// </summary>
        /// <param name="annId">The ANN identifier to store.</param>
        /// <param name="displayText">Text describing the buffered item.</param>
        /// <param name="data">Optional data object associated with the ANN.</param>
        public static void CopyToBuffer(int annId, string displayText, ArtNormN data = null)
        {
            // Delegate to the overload that handles multiple identifiers.
            CopyToBuffer(new[] { annId }, displayText, data);
        }

        /// <summary>
        /// Copies one or more work divisions into the buffer. Existing buffer contents
        /// will be replaced. When multiple IDs are provided the first element will be
        /// exposed via <see cref="BufferId"/> for backward compatibility. The
        /// <see cref="BufferIds"/> property will expose the full list.
        /// </summary>
        /// <param name="annIds">A collection of ANN identifiers to buffer.</param>
        /// <param name="displayText">The text to display in the UI for the buffer.</param>
        /// <param name="data">Optional data associated with the first ANN (for backward compatibility).</param>
        public static void CopyToBuffer(IEnumerable<int> annIds, string displayText, ArtNormN data = null)
        {
            if (annIds == null) throw new ArgumentNullException(nameof(annIds));
            var ids = annIds.ToList();
            _bufferIds.Clear();
            _bufferIds.AddRange(ids);
            BufferId = ids.FirstOrDefault();
            BufferText = displayText;
            BufferData = data?.Clone();
            OnBufferChanged();
        }

        /// <summary>
        /// Очищает буфер
        /// </summary>
        public static void ClearBuffer()
        {
            _bufferIds.Clear();
            BufferId = 0;
            BufferText = string.Empty;
            BufferData = null;
            OnBufferChanged();
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
                HasData = HasData,
                BufferIds = BufferIds.ToList()
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

        /// <summary>
        /// Gets or sets the list of buffer identifiers. When multiple work divisions
        /// are copied into the buffer this collection will expose all of their ANN
        /// identifiers. If only one element is present it corresponds to
        /// <see cref="BufferId"/>.
        /// </summary>
        public IReadOnlyList<int> BufferIds { get; set; }

        /// <summary>
        /// Returns true if more than one element has been buffered (kit mode).
        /// </summary>
        public bool IsKit => BufferIds != null && BufferIds.Count > 1;
    }
}