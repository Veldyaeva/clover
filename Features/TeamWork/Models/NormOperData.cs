using System.ComponentModel;
namespace SewingProduction.Models
{
    public class NormOperData : INotifyPropertyChanged
    {
        /// <summary>
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// </summary>
        public string KodO { get; set; }

        /// <summary>
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// </summary>
        public string Po { get; set; }

        /// <summary>
        /// </summary>
        public int? N { get; set; }

        /// <summary>
        /// </summary>
        public int? N1 { get; set; }

        /// <summary>
        /// </summary>
        public int? Sek { get; set; }

        /// <summary>
        /// </summary>
        public string New { get; set; }

        /// <summary>
        /// </summary>
        public int? Razryd { get; set; }

        /// <summary>
        /// </summary>
        public string Spec { get; set; }

        /// <summary>
        /// </summary>
        public string Obor { get; set; }

        /// <summary>
        /// </summary>
        public int? KodOb { get; set; }

        /// <summary>
        /// </summary>
        public int? KodProizv { get; set; }

        private bool _isChecked;
        /// <summary>
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
    //public class MyData
    //{
    //    public string Kod_o { get; set; }
    //    public string Text { get; set; }
    //    public string Spec { get; set; }
    //    public string razryd { get; set; }
    //    public string Obor { get; set; }
    //    public string Kod_proizv { get; set; }
    //    public string Text_proizv { get; set; }
    //    public string Text_vyaz { get; set; }
    //    public string Text_ob { get; set; }
    //}
}