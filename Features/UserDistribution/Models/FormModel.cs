using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.UserDistribution.Models
{

    public class FormModel : INotifyPropertyChanged
    {
        private int _projectFormsID;
        private string _nameForm;
        private string _nameFormRus;
        private int _creatorID;

        [Column("ProjectFormsID")]
        public int ProjectFormsID
        {
            get => _projectFormsID;
            set { if (_projectFormsID != value) { _projectFormsID = value; OnPropertyChanged(nameof(ProjectFormsID)); } }
        }

        [Column("NameForm")]
        public string NameForm
        {
            get => _nameForm;
            set { if (_nameForm != value) { _nameForm = value; OnPropertyChanged(nameof(NameForm)); } }
        }

        [Column("NameFormRus")]
        public string NameFormRus
        {
            get => _nameFormRus;
            set { if (_nameFormRus != value) { _nameFormRus = value; OnPropertyChanged(nameof(NameFormRus)); } }
        }

        [Column("CreatorID")]
        public int CreatorID
        {
            get => _creatorID;
            set { if (_creatorID != value) { _creatorID = value; OnPropertyChanged(nameof(CreatorID)); } }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }


}


