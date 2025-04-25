using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using SewingProduction.Interfaces;

namespace SewingProduction.Models
{
    public class UserRoleModel : INotifyPropertyChanged, INewable
    {
        private int _userRoleID;
        private int _userID;
        private int _roleID;

        public bool IsNew { get; set; } = true;

        [Column("UserRoleID")]
        public int UserRoleID
        {
            get => _userRoleID;
            set { if (_userRoleID != value) { _userRoleID = value; OnPropertyChanged(nameof(UserRoleID)); } }
        }

        [Column("UserID")]
        public int UserID
        {
            get => _userID;
            set { if (_userID != value) { _userID = value; OnPropertyChanged(nameof(UserID)); } }
        }

        [Column("RoleID")]
        public int RoleID
        {
            get => _roleID;
            set { if (_roleID != value) { _roleID = value; OnPropertyChanged(nameof(RoleID)); } }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
