using SewingProduction.Features.Sprav.Models;

namespace SewingProduction.Features.Sprav.Application.Contexts
{
    public sealed class VyazEconomPrintContext
    {
        public int Nn { get; init; }
        public int IdPodr { get; init; }
        public string NomZadany { get; init; }
        public decimal? SebAll { get; init; }

        public static VyazEconomPrintContext? FromRow(VyazKnitEconomAssortRow? row)
        {
            if (row == null)
            {
                return null;
            }

            return new VyazEconomPrintContext
            {
                Nn = row.nn ?? 0,
                IdPodr = row.id_podr ?? 0,
                NomZadany = row.nom_zadany,
                SebAll = row.seb_all
            };
        }
    }
}
