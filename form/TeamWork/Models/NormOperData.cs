using System.ComponentModel;
namespace SewingProduction.Models
{
    public class NormOperData : INotifyPropertyChanged
    {
        /// <summary>
        /// ID записи (ключ)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Код операции (3 символа)
        /// </summary>
        public string KodO { get; set; }

        /// <summary>
        /// Описание операции (60 символов)
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Порядок выполнения (1 символ)
        /// </summary>
        public string Po { get; set; }

        /// <summary>
        /// Числовой параметр N
        /// </summary>
        public int? N { get; set; }

        /// <summary>
        /// Числовой параметр N1
        /// </summary>
        public int? N1 { get; set; }

        /// <summary>
        /// Время в секундах
        /// </summary>
        public int? Sek { get; set; }

        /// <summary>
        /// Новый параметр (1 символ)
        /// </summary>
        public string New { get; set; }

        /// <summary>
        /// Разряд (число)
        /// </summary>
        public int? Razryd { get; set; }

        /// <summary>
        /// Специализация (3 символа)
        /// </summary>
        public string Spec { get; set; }

        /// <summary>
        /// Оборудование (35 символов)
        /// </summary>
        public string Obor { get; set; }

        /// <summary>
        /// Код оборудования
        /// </summary>
        public int? KodOb { get; set; }

        /// <summary>
        /// Код производителя
        /// </summary>
        public int? KodProizv { get; set; }

        private bool _isChecked;
        /// <summary>
        /// Выбрано ли в UI
        /// </summary>
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    OnPropertyChanged(nameof(IsChecked));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    /// <summary>
    /// Класс данных для передачи информации о выбранной операции
    /// </summary>
    public class MyData
    {
        public string Kod_o { get; set; }
        public string Text { get; set; }
        public string Spec { get; set; }
        public string Razryad { get; set; }
        public string Obor { get; set; }
        public string Kod_proizv { get; set; }
        public string Text_proizv { get; set; }
        public string Text_vyaz { get; set; }
        public string Text_ob { get; set; }
    }
}