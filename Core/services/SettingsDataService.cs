using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
		public async Task<List<PrinterParameters>> GetPrinterParametersListByNameAsync(string reportName)
		{
			if (string.IsNullOrWhiteSpace(reportName))
				return new List<PrinterParameters>();

			string kompName = Environment.MachineName + " # " + Environment.UserName;

			string query = @"
		SELECT *
		FROM printer_parameters
		WHERE sharpName = @reportName
		  AND komp_name = @kompName
		ORDER BY pp_id DESC";

			var list = await _dbService.GetListAsync<PrinterParameters>(query, new
			{
				reportName = reportName,
				kompName = kompName
			});

			if (list == null)
				return new List<PrinterParameters>();

			foreach (var model in list)
			{
				var items = PrinterParameters.ParseParamItems(model.param);
				model.ApplyParamItems(items);

				if (string.IsNullOrWhiteSpace(model.printName) && !string.IsNullOrWhiteSpace(model.Device))
					model.printName = model.Device;

				if (string.IsNullOrWhiteSpace(model.Device) && !string.IsNullOrWhiteSpace(model.printName))
					model.Device = model.printName;

				if (!model.orient.HasValue)
				{
					int orient;
					if (int.TryParse(model.Orientation, out orient))
						model.orient = orient;
				}

				if (!model.quantcopy.HasValue)
				{
					int copies;
					if (int.TryParse(model.Copies, out copies))
						model.quantcopy = copies;
				}
			}

			return list;
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
