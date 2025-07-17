using DevExpress.Mvvm.Native;
using SewingProduction.Services;
using SewingProduction.Features.UserDistribution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Dapper.Plus;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Features.CardByNom.Models;
using SewingProduction.Features.UserDistribution.Models;

namespace SewingProduction.Models
{
    public static class DapperMappings
    {
        public static void Configure()
        {
            DapperPlusManager.Entity<ArtNormN>().Table(TableNames.Ann).Identity(x => x.AnnID);
            DapperPlusManager.Entity<NormRasz>().Table(TableNames.Rasz).Identity(x => x.nrID);
            DapperPlusManager.Entity<NormRasz>().Table(TableNames.Rasz).Key(x => x.nrID);
            DapperPlusManager.Entity<NormRask>().Table(TableNames.Rask).Identity(x => x.id);
            DapperPlusManager.Entity<NormRask>().Table(TableNames.Rask).Key(x => x.id);
            DapperPlusManager.Entity<NormKont>().Table(TableNames.Kont).Identity(x => x.nkId);
            DapperPlusManager.Entity<NormKont>().Table(TableNames.Kont).Key(x => x.nkId);
            DapperPlusManager.Entity<NaklViewByPachKod>();
            DapperPlusManager.Entity<RasInfoByPachKod>();
            DapperPlusManager.Entity<HistoryRazdelNaklViewByIz>();
            DapperPlusManager.Entity<ChipInfoByNomZad>();
            DapperPlusManager.Entity<ProizvCombIzd>();
            DapperPlusManager.Entity<VyazPlanView>();
            DapperPlusManager.Entity<ArtPrFioProgr>();
            DapperPlusManager.Entity<PlanSezonZadanyView>().Table("plan_sezon_zad_knitMachine").Identity("pszkmID");
            DapperPlusManager.Entity<NormKont>().Table(TableNames.Kont).Identity(x => x.nkId);
            DapperPlusManager.Entity<AllTableNameModel>().Table(TableNames.ATN).Identity(x => x.id_atn);
            DapperPlusManager.Entity<AllColumnNameModel>().Table(TableNames.ACN).Identity(x => x.id_acn);

        }
    }
}