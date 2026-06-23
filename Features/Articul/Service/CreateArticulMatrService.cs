using Dapper;
using DevExpress.CodeParser;
using DevExpress.Utils;
using DevExpress.Utils.Gesture;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.Xpo.Logger.Transport;
using DevExpress.XtraEditors;
using DevExpress.XtraMap.Drawing.DirectD3D9;
using DevExpress.XtraScheduler.Native;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Org.BouncyCastle.Crypto;
using SewingProduction.Core.Class;
using SewingProduction.Core.Models;
using SewingProduction.Features.Articul;
using SewingProduction.Features.Articul.Models;
using SewingProduction.Features.CardByNom.Models;
using SewingProduction.Features.KnittingProduction.Forms.KnitterWS.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SewingProduction.Features.Articul.Service
{
    public class CreateArticulMatrService
    {
        private readonly DbService _dbService = new DbService(new DatabaseHelperSQL());
        private readonly FileLogger _logger = new FileLogger();
        private readonly DatabaseHelperSQL _dbHelper = new DatabaseHelperSQL();

        public CreateArticulMatrService()
        {
            //_dbService = new DbService(new DatabaseHelper());
            //_dbHelper = new DatabaseHelper();
        }
        public async Task<BindingList<CreateArticulMatrModel>> GetMatrForArticulAsync()
        {
            try
            {
                using var connection = _dbHelper.GetConnection();
                var result = await connection.QueryAsync<CreateArticulMatrModel>(
                    "dbo.spCreateArticulMatr",
                    param: null,
                    transaction: null,
                    commandType: CommandType.StoredProcedure,
                    commandTimeout: 120 //в секундах
                    );

                return new BindingList<CreateArticulMatrModel>(result.AsList());
            }
            catch (SqlException ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetMatrForArticulAsync");
                return null;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetMatrForArticulAsync");
                return null;
            }
        }
        public async Task<List<CreateArticulMatrModel>> GetMatrForNNAsync(string nn)
        {
            try
            {
                using var connection = _dbHelper.GetConnection();
                var result = await connection.QueryAsync<CreateArticulMatrModel>(
                    "dbo.spCreateArticulMatr",
                    param: new { nn = nn },
                    transaction: null,
                    commandType: CommandType.StoredProcedure,
                    commandTimeout: 120 //в секундах
                    );

                return result.AsList();
            }
            catch (SqlException ex)
            {
                await _logger.LogErrorAsync(ex, $"SQL Ошибка при получении данных GetMatrForNNAsync");
                return null;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetMatrForNNAsync");
                return null;
            }
        }
        //public async Task<string> FoundExistArticle(string foundArticle, string foundMod, string foundNN, string foundTm, string foundSost)
        //{
        //    try
        //    {
        //        var param = new DynamicParameters();
        //        param.Add("@foundArticle", foundArticle);
        //        param.Add("@foundMod", foundMod);
        //        param.Add("@foundNN", foundNN);
        //        param.Add("@foundTm", foundTm);
        //        param.Add("@foundSost", foundSost);
        //        //возвращаемый параметр для получения результата из хранимой процедуры
        //        param.Add("@res", dbType: DbType.String, direction: ParameterDirection.ReturnValue, size: 200);

        //        using var connection = _dbHelper.GetConnection();
        //        var result = await connection.QueryAsync<CreateArticulMatrModel>(
        //            "dbo.spFoundExistArticle",
        //            param,
        //            transaction: null,
        //            commandType: CommandType.StoredProcedure,
        //            commandTimeout: 120 //в секундах
        //            );

        //        string resultValue = param.Get<string>("@res");
        //        return resultValue;
        //    }
        //    catch (SqlException ex)
        //    {
        //        await _logger.LogErrorAsync(ex, $"SQL Ошибка при получении данных foundExistArticle");
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex, $"Ошибка при получении данных foundExistArticle");
        //        return null;
        //    }
        //}

        /// <summary>
        /// возвращает данные по артикулу по коду матрицы преобразуя данные в модель SpArticulPreviewModel из модели CreateArticulMatrModel
        /// </summary>
        /// <param name="nn"></param>
        /// <returns></returns>
        public async Task<BindingList<SpArticulPreviewModel>> GetPreviewArticulAsync(string nn, string kod )
        {
            var _articulDataService = new ArticulDataService();
            try
            {
                //SpArticulPreviewModel
                var curArticulTask = _articulDataService.GetByKodAsync(kod);
                //CreateArticulMatrModel
                var listnnTask = GetMatrForNNAsync(nn);

                await Task.WhenAll(curArticulTask, listnnTask);

                var listnn = await listnnTask;
                var curArt = await curArticulTask;

                var result = listnn
                    .Select(x => ArticulMapper.ToArticulModel(x, curArt))
                    .ToList();
                return new BindingList<SpArticulPreviewModel>(result);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetPreviewArticulAsync");
                return null;
            }
            finally
            {
                _articulDataService = null;
            }
        }

        public async Task<CreateArticulMatrModel> GetStatusForArticulAsync(string nn, int idgost, int agid )
        {
            try
            {
                using var connection = _dbHelper.GetConnection();
                var result = await connection.QueryFirstOrDefaultAsync<CreateArticulMatrModel>(
                    "dbo.spSetDateCertificationApproval",
                    new
                    {
                        nn,
                        idgost,
                        agid
                    },
                        commandType: CommandType.StoredProcedure
                    );

                return result;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetStatusForArticulAsync");
                return null;
            }
        }
        public async Task<BindingList<GostModel>> GetGostAsync()
        {
            try
            {
                string query = "SELECT  id_gost,name_gost,opi_gost FROM dbo.gost where ust = 0 order by id_gost";

                return new BindingList<GostModel>(await _dbService.GetListAsync<GostModel>(query, new {  }));
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetGostAsync");
                return null;
            }
        }

        public async Task<List<GostGrupIzdViewModel>> GetGrupGostAsync()
        {
            try
            {
            
                string query = "SELECT * FROM view_gost_grup_metadata ";

                return await _dbService.GetListAsync<GostGrupIzdViewModel>(query, new { });

            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetGrupGostAsync");
                return null;
            }
        }

        public async Task<BindingList<SpArtPreviewModel>> GetArticulsForCompareAsync(string articul)
        {
            try
            {
                articul = articul.Trim();
                string substring = "-";
                int indexOfSubstring = articul.IndexOf(substring);
                if (indexOfSubstring > 0)
                {
                    articul = articul.Substring(0, indexOfSubstring);
                }


                string query = "select vsk.kod, vsk.kodd,vsk.articul,vsk.grup,vsk.mod,va.tmName, vsk.arh "+
                    "from dbo.view_spArticulKodd_all vsk "+
                    "inner join view_art va on vsk.kod = va.kod "+
                    "WHERE vsk.articul like @articul ";

                return new BindingList<SpArtPreviewModel>(await _dbService.GetListAsync<SpArtPreviewModel>(query, new { articul = articul + "%" }));
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetArticulsForCompareAsync");
                return null;
            }


        }

        public async Task<BindingList<PlanRazmSetkaModel>> GetMatrPlanRazm(string nn,string kod, string po )
        {
            if (String.IsNullOrEmpty(kod) || !char.IsDigit(kod.Last()))
            {
                XtraMessageBox.Show("Укажите новый код изделия", "Подтверждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            try
            {
                string query = "SELECT nn, razm_ind, razm_matr, rost FROM view_matrPlanRazm where  nn = @nn ";
                var list = await _dbService.GetListAsync<PlanRazmSetkaModel>(query, new { nn });
                
                int lastind = kod.Last() - '0';

                foreach (var el in list)
                {
                    el.Kod = kod.Substring(0,7) + lastind.ToString();
                    lastind ++;
                    el.Po = po;
                }

                return new BindingList<PlanRazmSetkaModel>(list);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetMatrPlanRazm");
                return null;
            }
        }

        public async Task<BindingList<PlanRazmSetkaModel>> GetArticulRazmAsync(string kodd)
        {
            try
            {
                string query = "select kod, po, razm from View_sp_articul where kodd = @kodd";
                return new BindingList<PlanRazmSetkaModel>(await _dbService.GetListAsync<PlanRazmSetkaModel>(query, new { kodd }));
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetArticulRazmAsync");
                return null;
            }
        }
        public async Task<string> GetCheckArticulKomplsCompareAsync(string nn, string kod) 
        {
            try
            {
                string query = "select dbo.checkArticulKomplsCompare (@nn, @kod)";
                return await _dbService.GetFirstOrDefaultAsync<string>(query, new { nn, kod });
            } 
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetStatusForArticulAsync");
                return "Ошибка при получении данных dbo.checkArticulKomplsCompare";
            }
        }
        public async Task<bool> HasNzpForArticul (string kodd)
        {
            
                try
            {
                string query = "select dbo.checkHasNzpForArticul(@kodd)";
                return await _dbService.GetFirstOrDefaultAsync<bool>(query, new { kodd});
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных HasNzpForArticul");
                return false;
            }
        }
        public async Task<int> GetGostArhAsync( string kod)
        {
            try
            {
                string query = @"select g.ust from gost g 
                inner join sp_articul sp on g.id_gost = sp.id_gost 
                where sp.kod =  @kod";
            var res = await _dbService.GetFirstOrDefaultAsync<string>(query, new { kod });
            
            return int.TryParse(res, out int result) ? result : 1 ;
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, $"Ошибка при получении данных GetStatusForArticulAsync");
                return 1;
            }
        }
    }



    public static class ArticulMapper
    {
        public static SpArticulPreviewModel ToArticulModel(CreateArticulMatrModel matrArt, SpArticulPreviewModel curArt)
        {
            return new SpArticulPreviewModel
            {
                Kod = curArt.Kod,
                Po = curArt.Po,
                Kodd = curArt.Kodd,
                Articul = matrArt.Articul,
                Mod = matrArt.Article,
                Baza = matrArt.Baza,// сезон
                SeasonName = matrArt.SeasonName,
                Grupp = matrArt.Men_int,
                GrupMenName = matrArt.Grupmen_name,
                TmName = matrArt.SeasonName,
                Kle = matrArt.Kle,//ТМ sp 
                Sost = matrArt.Sost,
                Sost2 = matrArt.Sost2,
                Sost3 = matrArt.Sost3,
                Kruj = matrArt.Kruj,
                Id_gost = matrArt.Id_gost,//ГОСТ
                Gost = matrArt.GostName,
                Ag_id = matrArt.Ag_id,// группа по ГОСТ
                Grup = matrArt.Grup,
                Tkb = matrArt.Tkb,
                Kod_v = matrArt.Kod_v,//код ассортимента
                AssortName = matrArt.AssortName


            };
        }
    }

}
