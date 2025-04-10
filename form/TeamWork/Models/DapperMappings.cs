using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Dapper.Plus;

namespace SewingProduction.Models
{
    public static class DapperMappings
    {
        public static void Configure()
        {
            DapperPlusManager.Entity<NormRasz>().Table(TableNames.Rasz).Identity(x => x.nrId);
            DapperPlusManager.Entity<NormRask>().Table(TableNames.Rask).Identity(x => x.id);
            DapperPlusManager.Entity<NormKont>().Table(TableNames.Kont).Identity(x => x.id);
            DapperPlusManager.Entity<NormDopObr>().Table(TableNames.Obr).Identity(x => x.id);
        }
    }
}