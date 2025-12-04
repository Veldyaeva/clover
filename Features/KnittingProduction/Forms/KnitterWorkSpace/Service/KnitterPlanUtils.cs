using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System;

namespace SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Service
{
    public static class KnitterPlanUtils
    {
        public static string NormalizeMachineKey(string kmlNumber)
        {
            return string.IsNullOrWhiteSpace(kmlNumber) ? string.Empty : kmlNumber.Trim();
        }
        public static string NormalizeTaskNum(string taskNum)
        {
            return string.IsNullOrWhiteSpace(taskNum) ? string.Empty : taskNum.Trim();
        }
        public static string NormalizeArtKey(string articul)
        {
            return string.IsNullOrWhiteSpace(articul) ? string.Empty : articul.Trim();
        }

        public static KnitterPZVModel CreateOperationRow(KnitterPZVModel parent, nrModel nr)
        {
            var operationRow = new KnitterPZVModel
            {
                pzvID = parent.pzvID,
                pzvDivision = parent.pzvDivision,
                pzvMod = parent.pzvMod,
                pzvArticul = parent.pzvArticul,
                pzvKmlID = parent.pzvKmlID,
                kmlNumber = parent.kmlNumber,
                pzvNomZad = parent.pzvNomZad,
                pzvAnnID = parent.pzvAnnID,
                pzvNom = parent.pzvNom,
                pzvKol = parent.pzvKol,
                pzvSek = parent.pzvSek,
                pzvRKol = parent.pzvRKol,
                pzvDateStart = parent.pzvDateStart,
                pzvDateEnd = parent.pzvDateEnd,
                pzvKolNazn = parent.pzvKolNazn,
                pzvTab = parent.pzvTab,
                n_pach = parent.n_pach,
                razm = parent.razm,
                sekEd_Effective = parent.sekEd_Effective,
                kol_Effective = parent.kol_Effective,
                nrN = nr?.nrN,
                nrN1 = nr?.nrN1,
                nrText = nr?.nrText,
                nrRazryd = nr?.nrRazryd,
                nrObor = nr?.nrObor,
                nr_kod_ob = nr?.nr_kod_ob,
                nr_kod_proizv = nr?.nr_kod_proizv
            };

            if (nr != null)
            {
                operationRow.nrModels = new BindingList<nrModel>(new List<nrModel>
                {
                    new nrModel
                    {
                        nr_kod_proizv = nr.nr_kod_proizv,
                        nrN = nr.nrN,
                        nrN1 = nr.nrN1,
                        nrRazryd = nr.nrRazryd,
                        nrText = nr.nrText,
                        nrObor = nr.nrObor,
                        nr_kod_ob = nr.nr_kod_ob,
                        kmlNumber = nr.kmlNumber
                    }
                });
            }

            return operationRow;
        }

        public static bool HasRzv(rzvModel r)
        {
            return r.n_pach != 0 || r.rzv_kod != 0 || r.rzv_kol != 0 || !string.IsNullOrEmpty(r.pach_kod) || !string.IsNullOrEmpty(r.razm);
        }

        public static bool ContainsNr(BindingList<nrModel> list, nrModel x)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var e = list[i];
                if (e.nrN == x.nrN && e.nrN1 == x.nrN1 && e.nr_kod_proizv == x.nr_kod_proizv && e.nr_kod_ob == x.nr_kod_ob)
                    return true;
            }
            return false;
        }

        public static bool ContainsRzv(BindingList<rzvModel> list, rzvModel x)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var e = list[i];
                if (e.n_pach == x.n_pach && e.rzv_kod == x.rzv_kod && string.Equals(e.razm, x.razm, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}


