using DevExpress.XtraSpreadsheet.DocumentFormats.Xlsb;
using SewingProduction.Core.Models;
using SewingProduction.form;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SewingProduction.Models;
using DevExpress.CodeParser;
using SewingProduction.Features.Articul.Models;

namespace SewingProduction.Features.Articul.Service
{
    /// <summary>
    /// Provides static methods and properties for loading and accessing reference data related to GOSTs and TMs
    /// in an advanced editing context.
    /// </summary>
    /// <remarks>This class manages the asynchronous loading and caching of reference data used in advanced
    /// editing scenarios. Data is loaded from the database on demand and can be refreshed or cleared as needed. All
    /// members are static and thread-safe for typical usage patterns.</remarks>
    public class CommonSpravArticulEditAdvance
    {
        
        private static Task _loadTask;

        public static IReadOnlyList<GostModel> Gosts { get; private set; }
        = Array.Empty<GostModel>();
        public static IReadOnlyList<Szon_newModel> Seasons { get; private set; }
        = Array.Empty<Szon_newModel>();
        public static IReadOnlyList<TmModel> Tms { get; private set; }
        = Array.Empty<TmModel>();
        public static IReadOnlyList<CountryModel> Countries { get; private set; }
        = Array.Empty<CountryModel>();
        public static IReadOnlyList<GrupMenModel> GrupMen { get; private set; }
        = Array.Empty<GrupMenModel>();
        public static IReadOnlyList<AssortModel> Assorts { get; private set; }
        = Array.Empty<AssortModel>();
        public static IReadOnlyList<SpArticulTkanSokr> Tkans { get; private set; }
        = Array.Empty<SpArticulTkanSokr>();
        public static IReadOnlyList<GostGrupIzdViewModel> GostGroupNames { get; private set; }
        = Array.Empty<GostGrupIzdViewModel>();

        public static Task EnsureLoadedAsync(DbService db)
        {
            // атомарно создаём или берём существующую Task
            return _loadTask ??= LoadInternalAsync(db);
        }
        private static async Task LoadInternalAsync(DbService db)
        {
            
            var seasonsTask = db.GetListAsync<Szon_newModel>(
                "select n,txt from dbo.szon_new", new { } );

            var gostTask = db.GetListAsync<GostModel>(
              "SELECT  id_gost,name_gost,opi_gost FROM dbo.gost where ust = 0 ", new { }
              );

            var tmTask = db.GetListAsync<TmModel>(
                "select * from view_tmArticul", new { });

            var CountryTask = db.GetListAsync<CountryModel>(
                "select frm_id_country, frm_country, frm_cu_id FROM dbo.frm_country", new { });
            var GrupMenTask = db.GetListAsync<GrupMenModel>(
                "select men_int,name from dbo.view_grup_men where men_int > 0 order by gm_index ", new { });
            var AssortTask = db.GetListAsync<AssortModel>(
                "select kod_v, txt_v from gtin.assort", new { });
            var TkanTask = db.GetListAsync<SpArticulTkanSokr>(
                "select Kod_t, Tkan, Tkb, IsDifficult, Difficult_koef from dbo.view_tkan order by tkb", new { });
            var GostGroupNamesTask = db.GetListAsync<GostGrupIzdViewModel>(
                "SELECT id_gost, ag_id, ag_name_sokr,n_i FROM View_GostGrupIzd ", new { });

            //ожидаем все задачи
            await Task.WhenAll(seasonsTask, gostTask, tmTask, CountryTask, GrupMenTask, AssortTask, TkanTask, GostGroupNamesTask);
            
            Gosts = (await gostTask).AsReadOnly();
            Tms = (await tmTask).AsReadOnly();
            Seasons = (await seasonsTask).AsReadOnly();
            Countries = (await CountryTask).AsReadOnly();
            GrupMen = (await GrupMenTask).AsReadOnly();
            Assorts = (await AssortTask).AsReadOnly();
            Tkans = (await TkanTask).AsReadOnly();
            GostGroupNames = (await GostGroupNamesTask).AsReadOnly();
        }

        public static async Task ReloadAsync(DbService db)
        {
            // сброс
            Clear();
            await EnsureLoadedAsync(db);
        }

        public static void Clear()
        {
            _loadTask = null;
            Seasons = Array.Empty<Szon_newModel>();
            Gosts = Array.Empty<GostModel>();
            Tms = Array.Empty<TmModel>();
            Countries = Array.Empty<CountryModel>();
            GrupMen = Array.Empty<GrupMenModel>();
            Assorts = Array.Empty<AssortModel>();
            Tkans = Array.Empty<SpArticulTkanSokr>();

        }



    }
}
