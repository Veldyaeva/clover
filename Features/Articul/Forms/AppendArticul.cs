using SewingProduction.Core.Models;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.Articul.Service;
using SewingProduction.Features.UserDistribution.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.Articul.Forms
{
    public partial class AppendArticul : CustomForm
    {
        private int _typeCreate = 0;
        private string _nn;
        private string _kod;
        private BindingSource _bindingSourceArticul = new BindingSource();

        private CreateArticulMatrService _createArticulMatrService = new CreateArticulMatrService();

        public AppendArticul(UserClass user) : base(user)
        {
            InitializeComponent();
        }

        public AppendArticul(UserClass user, string nn) : this(user)
        {
           // InitializeComponent();
            // 0 - создание,1 - стыковка
            _typeCreate = 0;
            _nn = nn;
            
        }
        public AppendArticul(UserClass user, string nn, string kod) : this(user)
        {
            //InitializeComponent();
            // 0 - создание,1 - стыковка
            _typeCreate = 1;
            _nn = nn;
            _kod = kod;
            
        }

        private async void AppendArticul_Load(object sender, EventArgs e)
        {
            _bindingSourceArticul.DataSource = await _createArticulMatrService.GetPreviewArticulAsync(_nn);
            InitializeBindings();

        }
        private void InitializeBindings()
        {
            txtArticul.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.Articul), true);
            txtMod.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.Mod), true);
            txtSeason.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.SeasonName), true);
            txtGrupMenName.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.GrupMenName), true);
            txtTM.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.GrupMenName), true);
            txtSost.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.Sost), true);
            txtSost2.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.Sost2), true);
            txtSost3.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.Sost3), true);
            chkKruj.DataBindings.Add("Checked", _bindingSourceArticul, nameof(SpArticulPreviewModel.Kruj), true);
            txtIdGost.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.Id_gost), true);
            txtGostName.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.Gost), true);
            txtGrup.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.Grup), true);
            txtTkb.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.Tkb), true);
            txtAssort.DataBindings.Add("Text", _bindingSourceArticul, nameof(SpArticulPreviewModel.AssortName), true);

        }
    }
}
