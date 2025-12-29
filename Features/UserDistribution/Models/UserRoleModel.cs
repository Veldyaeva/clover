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
    public class UserRoleModel : INotifyPropertyChanged//, INewable
    {
        private int _userRolesID;
        private int _userID;
        private int _roleID;
        private string _roleName;
        private string _description;
        private bool _hasRole;
        //public bool IsNew { get; set; } = true;

        [Column("UserRolesID")]
        public int UserRolesID
        {
            get => _userRolesID;
            set { if (_userRolesID != value) { _userRolesID = value; OnPropertyChanged(nameof(UserRolesID)); } }
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

        [Column("RoleName")]
        [NotMapped]
        public string RoleName
        {
            get => _roleName;
            set { if (_roleName != value) { _roleName = value; OnPropertyChanged(nameof(RoleName)); } }
        }
        [NotMapped]
        public string Description
        {
            get => _description;
            set { if (_description != value) { _description = value; OnPropertyChanged(nameof(Description)); } }
        }

        [NotMapped]
        public bool HasRole
        {
            get => _hasRole;
            set { if (_hasRole != value) { _hasRole = value; OnPropertyChanged(nameof(HasRole)); } }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
