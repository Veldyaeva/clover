using SewingProduction.Features.CardByNom.Models;
using SewingProduction.Features.KnittingProduction.Models;
using SewingProduction.Features.UserDistribution.Models;
using Z.Dapper.Plus;

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
            DapperPlusManager.Entity<NaklView>();
            DapperPlusManager.Entity<RasInfo>();
            DapperPlusManager.Entity<HistoryRazdelNaklViewByIz>();
            DapperPlusManager.Entity<ChipInfo>();
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