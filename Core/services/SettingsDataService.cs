using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SewingProduction.Core.Models;
using SewingProduction.Features.CuttingProduction.Models;
using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Core.services
{
	public class SettingsDataService
	{
		private readonly DbService _dbService;
		private readonly DatabaseHelperSQL _dbHelper;
		public SettingsDataService()
		{
			_dbHelper = new DatabaseHelperSQL();
			_dbService = new DbService(_dbHelper);
		}

		public async Task<List<PrinterParameters>> GetPrinterParameters()
		{
			string query = @" SELECT * FROM printer_parameters ORDER BY pp_id";
			return await _dbService.GetListAsync<PrinterParameters>(query, new { });
		}
		public async Task<List<string>> GetCompName()
		{
			string query = @" SELECT komp_name FROM printer_parameters group by komp_name order by komp_name";
			return await _dbService.GetListAsync<string>(query, new { });
		}
			 
		public async Task<int> SaveAsync(PrinterParameters model)
		{
			return await _dbService.SaveEntityAsync("printer_parameters", "pp_id", model);
		}

		public async Task DeleteAsync(PrinterParameters model)
		{
			await _dbService.DeleteEntityAsync("printer_parameters", "pp_id", model);
		}
	}
}
