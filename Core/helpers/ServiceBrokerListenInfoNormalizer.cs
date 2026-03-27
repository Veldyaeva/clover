using SewingProduction.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SewingProduction.Core.helpers
{
    internal static class ServiceBrokerListenInfoNormalizer
    {
        private static readonly Dictionary<string, string[]> CanonicalFieldsByTable =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ["dbo.planZagrVyaz"] = new[]
                {
                    "pzvAnnID","pzvArticul","pzvChasNazn","pzvCompAdd","pzvDateAdd","pzvDateEnd","pzvDateMast",
                    "pzvDateML","pzvDateMLUt","pzvDateNaznKm","pzvDateNaznTab","pzvDateStart","pzvDivision",
                    "pzvGradacia","pzvGsID","pzvID","pzvIdBrig","pzvIDMlOp","pzvIDParent","pzvKmlID","pzvKol",
                    "pzvKolNazn","pzvKwsID","pzvMod","pzvNChasi","pzvNom","pzvNomN","pzvNomZad","pzvNrID",
                    "pzvRKol","pzvSek","pzvSekNazn","pzvTab","pzvUpdDate","pzvVidPr"
                },
                ["dbo.knitWorkingShiftMachineListNew"] = new[]
                {
                    "kiwsmlLongRep","kwsmlKmlID","kwsmlKodOb","kwsmlKwsID"
                }
            };

        public static List<ServiceBrokerModel.TableListenInfo> Normalize(
            IEnumerable<ServiceBrokerModel.TableListenInfo> source)
        {
            if (source == null)
                return new List<ServiceBrokerModel.TableListenInfo>();

            var list = source.ToList();
            foreach (var item in list)
            {
                var tableKey = BuildTableKey(item.TableSchema, item.TableName);
                if (!CanonicalFieldsByTable.TryGetValue(tableKey, out var fields))
                    continue;

                item.TableFieldList = string.Join(",", fields);
            }

            return list;
        }

        private static string BuildTableKey(string schema, string table)
        {
            var normalizedSchema = string.IsNullOrWhiteSpace(schema) ? "dbo" : schema.Trim();
            var normalizedTable = table?.Trim() ?? string.Empty;
            return $"{normalizedSchema}.{normalizedTable}";
        }
    }
}
