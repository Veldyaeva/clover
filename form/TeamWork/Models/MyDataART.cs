using DevExpress.Mvvm.Native;
using System.ComponentModel;

namespace SewingProduction.Models
{
    public class MyDataART : Interfaces.ICheckable, INotifyPropertyChanged
    {
        public string Articul { get; set; }
        public int Kod { get; set; }
        public string Group { get; set; }
        public string Model { get; set; }
        public string BindedArt { get; set; }

        private bool _isChecked;
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
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
