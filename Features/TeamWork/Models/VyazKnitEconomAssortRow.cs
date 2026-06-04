using System;

namespace SewingProduction.Features.TeamWork.Models
{
    /// <summary>
    /// Строка вьюхи dbo.view_seb_vyaz_econom_assort (калькуляция вязального ассортимента).
    /// </summary>
    public sealed class VyazKnitEconomAssortRow
    {
        public int? nn { get; set; }
        public string nom_zadany { get; set; } = string.Empty;
        public int? razm_ryad { get; set; }
        public int? pach_min { get; set; }
        public int? pach_max { get; set; }
        public int? nom { get; set; }
        public string articul { get; set; } = string.Empty;
        public string mod { get; set; } = string.Empty;
        public DateTime? date_econom { get; set; }
        public int last_otm_izm { get; set; }
        public string grup { get; set; } = string.Empty;
        public DateTime? data_cd_min { get; set; }
        public decimal? koef_zatrat { get; set; }
        public int? id_podr { get; set; }
        public decimal? seb_all { get; set; }
    }

    /// <summary>
    /// Режим фильтра подразделения на форме калькуляции.
    /// </summary>
    public enum VyazEconomAssortPodrMode
    {
        /// <summary>Носочный: id_podr 10, 16.</summary>
        Sock,
        /// <summary>Вязальный: id_podr 6.</summary>
        Knit,
        /// <summary>Шнуры: id_podr 19.</summary>
        Cord
    }
}
