using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.UserDistribution.Models
{
    public class RoleModel : INotifyPropertyChanged
    {
        private int _roleID;
        private string _roleName;
        private string _description;
        private string _creatorName; // из JOIN с таблицей Users
        private int _creatorID; // из Insert

        [Column("RoleID")]
        public int RoleID
        {
            get => _roleID;
            set { if (_roleID != value) { _roleID = value; OnPropertyChanged(nameof(RoleID)); } }
        }

        [Column("RoleName")]
        public string RoleName
        {
            get => _roleName;
            set { if (_roleName != value) { _roleName = value; OnPropertyChanged(nameof(RoleName)); } }
        }

        [Column("Description")]
        public string Description
        {
            get => _description;
            set { if (_description != value) { _description = value; OnPropertyChanged(nameof(Description)); } }
        }

        [NotMapped]
        public string CreatorName
        {
            get => _creatorName;
            set { if (_creatorName != value) { _creatorName = value; OnPropertyChanged(nameof(CreatorName)); } }
        }

        [Column("CreatorID")]
        public int CreatorID
        {
            get => _creatorID;
            set { if (_creatorID != value) { _creatorID = value; OnPropertyChanged(nameof(CreatorID)); } }
        }

        [NotMapped]
        public bool IsSelected { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
