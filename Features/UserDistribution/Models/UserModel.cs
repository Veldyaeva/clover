using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Threading.Tasks;
using SewingProduction.Features.UserDistribution.Helpers;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.UserDistribution.Models
{
    public class UserModel : INotifyPropertyChanged//, INewable
    {
        private int _userId;
        private string _userName;
        private int _brigID;
        public string _brigName;
        private int _creatorId;
        private string _creatorName;
        private int _fioId;
        private string _fio;
        private string _passwordHash;

        //public bool IsNew { get; set; } = true;

        [Column("UserID")]
        public int UserID
        {
            get => _userId;
            set { if (_userId != value) { _userId = value; OnPropertyChanged(nameof(UserID)); } }
        }

        [Column("UserName")]
        public string UserName
        {
            get => _userName;
            set { if (_userName != value) { _userName = value; OnPropertyChanged(nameof(UserName)); } }
        }

        [Column("CreatorID")]
        public int CreatorID
        {
            get => _creatorId;
            set { if (_creatorId != value) { _creatorId = value; OnPropertyChanged(nameof(CreatorID)); } }
        }

        [Column("FioID")]
        public int FioID
        {
            get => _fioId;
            set { if (_fioId != value) { _fioId = value; OnPropertyChanged(nameof(FioID)); } }
        }

        [Column("BrigID")]
        public int BrigID
        {
            get => _brigID;
            set { if (_brigID != value) { _brigID = value; OnPropertyChanged(nameof(BrigID)); } }
        }

        [NotMapped]
        public string Fio
        {
            get => _fio;
            set { if (_fio != value) { _fio = value; OnPropertyChanged(nameof(Fio)); } }
        }

        [NotMapped]
        public string BrigName
        {
            get => _brigName;
            set { if (_brigName != value) { _brigName = value; OnPropertyChanged(nameof(BrigName)); } }
        }

        [NotMapped]
        public string CreatorName
        {
            get => _creatorName;
            set { if (_creatorName != value) { _creatorName = value; OnPropertyChanged(nameof(CreatorName)); } }
        }
        [NotMapped]
        public string Password { get; set; }
        [Column("PasswordHash")]
        public string PasswordHash
        {
            get => _passwordHash;
            set { if (_passwordHash != value) { _passwordHash = value; OnPropertyChanged(nameof(PasswordHash)); } }
        }

        [NotMapped]
        public bool IsSelected { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public class BrigDto
    {
        public int BrigID { get; set; }
        public string Brig { get; set; }
    }
    public class FioDto
    {
        public int FioID { get; set; }
        public string Fio { get; set; }
        public string Rab { get; set; }
    }
    
}
