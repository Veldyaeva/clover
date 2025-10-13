using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using SewingProduction.Features.TeamWork.Forms;
using SewingProduction.Features.TeamWork.Services;
using SewingProduction.Helpers;
using SewingProduction.Models;
using SewingProduction.Services;

namespace SewingProduction.Features.TeamWork.Operations
{
    public sealed class CloneService
    {
        private readonly ArtNormService _artNormService;

        public CloneService(ArtNormService artNormService)
        {
            _artNormService = artNormService;
        }

        public async Task LoadForEditAsync(int annId,
            BindingList<NormRasz> raszList,
            BindingList<NormRask> raskList,
            BindingList<NormKont> kontList)
        {
            var rasz = await _artNormService.GetRelatedNormRasz(annId);
            var rask = await _artNormService.GetRelatedNormRask(annId);
            var kont = await _artNormService.GetRelatedNormKont(annId);

            foreach (var item in rasz) { item.IsNew = false; item.IsModified = false; }
            foreach (var item in rask) { item.IsNew = false; item.IsModified = false; }
            foreach (var item in kont) { item.IsNew = false; item.IsModified = false; }

            raszList.BulkLoad(rasz);
            raskList.BulkLoad(rask);
            kontList.BulkLoad(kont);
        }

        public async Task CloneFromAsync(int sourceAnnId, int newAnnId,
            BindingList<NormRasz> raszList,
            BindingList<NormRask> raskList,
            BindingList<NormKont> kontList)
        {
            var rasz = await _artNormService.GetRelatedNormRasz(sourceAnnId);
            var rask = await _artNormService.GetRelatedNormRask(sourceAnnId);
            var kont = await _artNormService.GetRelatedNormKont(sourceAnnId);

            raszList.BulkLoad(TeamWork_AdvanceTW.CloneUtils.CloneList(rasz, newAnnId, "nrId", false));
            raskList.BulkLoad(TeamWork_AdvanceTW.CloneUtils.CloneList(rask, newAnnId, "id", false));
            kontList.BulkLoad(TeamWork_AdvanceTW.CloneUtils.CloneList(kont, newAnnId, "nkId", false));
        }
    }
}



