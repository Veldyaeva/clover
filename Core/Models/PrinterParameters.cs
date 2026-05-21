using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Core.Models
{
	public class PrinterParameters
	{
		public int pp_id { get; set; }

		public string pp_prg_name { get; set; }
		public string nameReport { get; set; }
		public string shortName { get; set; }
		public string printName { get; set; }

		public int? orient { get; set; }
		public int? quantcopy { get; set; }
		public int? preview { get; set; }

		public string printAlias { get; set; }
		public string param { get; set; }
		public string komp_name { get; set; }
		public string sharpName { get; set; }

		//param
		[NotMapped]
		public string Driver { get; set; }
		[NotMapped]
		public string Device { get; set; }
		[NotMapped]
		public string Output { get; set; }
		[NotMapped]
		public string Orientation { get; set; }
		[NotMapped]
		public string PaperSize { get; set; }
		[NotMapped]
		public string PaperLength { get; set; }
		[NotMapped]
		public string PaperWidth { get; set; }
		[NotMapped]
		public string Copies { get; set; }
		[NotMapped]
		public string PrintQuality { get; set; }
		[NotMapped]
		public string YResolution { get; set; }
		
		public static List<PrinterParamItem> ParseParamItems(string param)
		{
			var result = new List<PrinterParamItem>();

			if (string.IsNullOrWhiteSpace(param))
				return result;

			var lines = param
				.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

			foreach (var line in lines)
			{
				int index = line.IndexOf('=');
				if (index <= 0)
					continue;

				string key = line.Substring(0, index).Trim();
				string value = line.Substring(index + 1).Trim();

				result.Add(new PrinterParamItem
				{
					nameParam = key,
					infoParam = value
				});
			}

			return result;
		}
		public static string BuildParam(PrinterParameters model)
		{
			return
				"DRIVER=" + (model.Driver ?? string.Empty).Trim() + Environment.NewLine +
				"DEVICE=" + (model.Device ?? string.Empty).Trim() + Environment.NewLine +
				"OUTPUT=" + (model.Output ?? string.Empty).Trim() + Environment.NewLine +
				"ORIENTATION=" + (model.Orientation ?? string.Empty).Trim() + Environment.NewLine +
				"PAPERSIZE=" + (model.PaperSize ?? string.Empty).Trim() + Environment.NewLine +
				"PAPERLENGTH=" + (model.PaperLength ?? string.Empty).Trim() + Environment.NewLine +
				"PAPERWIDTH=" + (model.PaperWidth ?? string.Empty).Trim() + Environment.NewLine +
				"COPIES=" + (model.Copies ?? string.Empty).Trim() + Environment.NewLine +
				"PRINTQUALITY=" + (model.PrintQuality ?? string.Empty).Trim() + Environment.NewLine +
				"YRESOLUTION=" + (model.YResolution ?? string.Empty).Trim();
		}
		public void ApplyParamItems(IEnumerable<PrinterParamItem> items)
		{
			Driver = GetValue(items, "DRIVER");
			Device = GetValue(items, "DEVICE");
			Output = GetValue(items, "OUTPUT");
			Orientation = GetValue(items, "ORIENTATION");
			PaperSize = GetValue(items, "PAPERSIZE");
			PaperLength = GetValue(items, "PAPERLENGTH");
			PaperWidth = GetValue(items, "PAPERWIDTH");
			Copies = GetValue(items, "COPIES");
			PrintQuality = GetValue(items, "PRINTQUALITY");
			YResolution = GetValue(items, "YRESOLUTION");
		}
		private static string GetValue(IEnumerable<PrinterParamItem> items, string key)
		{
			foreach (var item in items)
			{
				if (string.Equals(item.nameParam, key, StringComparison.OrdinalIgnoreCase))
					return item.infoParam ?? string.Empty;
			}

			return string.Empty;
		}
	}
	public class PrinterParamItem
	{
		public string nameParam { get; set; }
		public string infoParam { get; set; }
	}
}
