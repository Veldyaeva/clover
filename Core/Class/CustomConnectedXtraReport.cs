using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using SewingProduction.Core.Models;
using SewingProduction.Core.services;
using SewingProduction.Report;

namespace SewingProduction.Core.Class
{
	public class CustomConnectedXtraReport : ConnectedXtraReport
	{
		private PrinterParameters _printerSettings;
		private bool _printSettingsApplied;
		private bool _printerSettingsSaving;
		//private bool _needSavePrinterSettingsAfterPrint;
		private string _needSaveReason;
		public bool DebugPrinterSettings { get; set; } = true;

		public string ReportDbName { get; set; }
		private enum PrinterSettingsSaveMode
		{
			None = 0,
			InsertAfterPrint = 1,
			UpdateAfterPrint = 2
		}

		private List<PrinterParameters> _printerSettingsList = new List<PrinterParameters>();
		private PrinterSettingsSaveMode _saveMode = PrinterSettingsSaveMode.None;
		private string _selectedReason = string.Empty;

		public CustomConnectedXtraReport()
		{
			ReportDbName = GetType().Name;
		}
		private static string GetKompName()
		{
			return Environment.MachineName + " # " + Environment.UserName;
		}

		private static string GetDefaultPrinterName()
		{
			try
			{
				var printerSettings = new PrinterSettings();
				return printerSettings.PrinterName ?? string.Empty;
			}
			catch
			{
				return string.Empty;
			}
		}

		private static bool PrinterNameEquals(string printerName1, string printerName2)
		{
			return string.Equals(
				(printerName1 ?? string.Empty).Trim(),
				(printerName2 ?? string.Empty).Trim(),
				StringComparison.OrdinalIgnoreCase);
		}

		private static List<string> GetInstalledPrinterNames()
		{
			var result = new List<string>();

			foreach (string printerName in PrinterSettings.InstalledPrinters)
			{
				if (!string.IsNullOrWhiteSpace(printerName))
					result.Add(printerName);
			}

			return result;
		}

		private static PrinterParameters ClonePrinterSettings(PrinterParameters source)
		{
			if (source == null)
				return null;

			return new PrinterParameters
			{
				pp_id = source.pp_id,
				pp_prg_name = source.pp_prg_name,
				nameReport = source.nameReport,
				shortName = source.shortName,
				printName = source.printName,
				orient = source.orient,
				quantcopy = source.quantcopy,
				preview = source.preview,
				printAlias = source.printAlias,
				param = source.param,
				komp_name = source.komp_name,
				sharpName = source.sharpName,

				Driver = source.Driver,
				Device = source.Device,
				Output = source.Output,
				Orientation = source.Orientation,
				PaperSize = source.PaperSize,
				PaperLength = source.PaperLength,
				PaperWidth = source.PaperWidth,
				Copies = source.Copies,
				PrintQuality = source.PrintQuality,
				YResolution = source.YResolution
			};
		}

		private PrinterParameters CreateDefaultPrinterSettings(string reportName, string printerName)
		{
			string kompName = GetKompName();

			int paperWidthDb = PaperSizeFromReportToDb(this.PageWidth);
			int paperLengthDb = PaperSizeFromReportToDb(this.PageHeight);
			int orientation = this.Landscape ? 1 : 0;

			var settings = new PrinterParameters
			{
				pp_id = 0,
				pp_prg_name = "proizv_set",
				nameReport = reportName,
				shortName = reportName,
				sharpName = reportName,
				komp_name = kompName,
				printAlias = "auto",

				printName = printerName,
				Device = printerName,

				preview = 1,
				quantcopy = 1,
				orient = orientation,

				Driver = "winspool",
				Output = string.Empty,
				Orientation = orientation.ToString(),
				PaperSize = "256",
				PaperLength = paperLengthDb.ToString(),
				PaperWidth = paperWidthDb.ToString(),
				Copies = "1",
				PrintQuality = string.Empty,
				YResolution = string.Empty
			};

			settings.param = BuildPrinterParam(settings);

			return settings;
		}

		private void ShowPrinterSelectionMessage(string text)
		{
			if (!DebugPrinterSettings)
				return;

			MessageBox.Show(
				text,
				"Выбор настроек печати",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information);
		}
		public async Task LoadPrinterSettingsAsync()
		{
			string reportName = GetReportName();

			if (string.IsNullOrWhiteSpace(reportName))
				return;

			var service = new SettingsDataService();

			_printerSettingsList = await service.GetPrinterParametersListByNameAsync(reportName);

			_printerSettings = null;
			_saveMode = PrinterSettingsSaveMode.None;
			_selectedReason = string.Empty;
			_needSaveReason = string.Empty;

			string defaultPrinterName = GetDefaultPrinterName();

			if (_printerSettingsList == null || _printerSettingsList.Count == 0)
			{
				_printerSettings = CreateDefaultPrinterSettings(reportName, defaultPrinterName);

				_saveMode = PrinterSettingsSaveMode.InsertAfterPrint;
				_selectedReason = "В БД нет настроек. Используется принтер Windows по умолчанию.";
				_needSaveReason = "Создание первой записи настроек";

				return;
			}

			// 1. Сначала ищем принтер Windows по умолчанию в списке БД.
			var defaultPrinterSettings = _printerSettingsList
				.FirstOrDefault(x => PrinterNameEquals(GetPrinterName(x), defaultPrinterName));

			if (defaultPrinterSettings != null)
			{
				if (PrinterExists(GetPrinterName(defaultPrinterSettings)))
				{
					_printerSettings = defaultPrinterSettings;
					_saveMode = PrinterSettingsSaveMode.UpdateAfterPrint;
					_selectedReason = "Принтер Windows по умолчанию найден в БД и установлен на ПК.";
					return;
				}

				_selectedReason = $"Принтер по умолчанию найден в БД, но не установлен: {defaultPrinterName}";
			}

			// 2. Если defaultPrinter в БД нет или он не установлен:
			// берем последнюю запись из БД по pp_id DESC.
			var lastSettings = _printerSettingsList
				.OrderByDescending(x => x.pp_id)
				.FirstOrDefault();

			if (lastSettings != null && PrinterExists(GetPrinterName(lastSettings)))
			{
				_printerSettings = lastSettings;
				_saveMode = PrinterSettingsSaveMode.UpdateAfterPrint;
				_selectedReason =
					"Принтер Windows по умолчанию не подошел. Используется последняя установленная запись из БД.";

				return;
			}

			// 3. Если последняя запись не подходит — ищем следующий установленный принтер из списка БД.
			var installedFromDb = _printerSettingsList
				.OrderByDescending(x => x.pp_id)
				.FirstOrDefault(x => PrinterExists(GetPrinterName(x)));

			if (installedFromDb != null)
			{
				_printerSettings = installedFromDb;
				_saveMode = PrinterSettingsSaveMode.UpdateAfterPrint;
				_selectedReason =
					"Последний принтер из БД не установлен. Используется следующий установленный принтер из БД.";

				return;
			}

			// 4. Если ни один принтер из БД не установлен:
			// берем настройки последней записи, но заменяем принтер на defaultPrinter.
			if (lastSettings != null)
			{
				_printerSettings = ClonePrinterSettings(lastSettings);

				_printerSettings.pp_id = 0;
				_printerSettings.printName = defaultPrinterName;
				_printerSettings.Device = defaultPrinterName;
				_printerSettings.sharpName = reportName;
				_printerSettings.komp_name = GetKompName();
				_printerSettings.param = BuildPrinterParam(_printerSettings);

				_saveMode = PrinterSettingsSaveMode.InsertAfterPrint;
				_selectedReason =
					"Ни один принтер из БД не установлен. Взяты настройки последней записи, принтер заменен на Windows defaultPrinter.";
				_needSaveReason = "Создание записи для defaultPrinter на основе последней записи БД";

				return;
			}

			// Теоретическая страховка.
			_printerSettings = CreateDefaultPrinterSettings(reportName, defaultPrinterName);
			_saveMode = PrinterSettingsSaveMode.InsertAfterPrint;
			_selectedReason = "Резервный сценарий. Используется defaultPrinter.";
			_needSaveReason = "Создание резервной записи";
		}
		private static bool IsPrinterSettingsIncomplete(PrinterParameters settings)
		{
			if (settings == null)
				return true;

			string printerName = GetPrinterName(settings);

			if (string.IsNullOrWhiteSpace(printerName))
				return true;

			int copies = GetCopies(settings);
			if (copies <= 0)
				return true;

			int orientation = GetInt(settings.Orientation, settings.orient.GetValueOrDefault(-1));
			if (orientation < 0)
				return true;

			return false;
		}
		public async Task ShowOrPrintBySettingsAsync(IWin32Window owner = null)
		{
			await LoadPrinterSettingsAsync();

			//ShowDebugPrinterSettings("После LoadPrinterSettingsAsync");

			ApplyPrinterSettings();

			//ShowDebugPrinterSettings("После ApplyPrinterSettings");

			using (var tool = new ReportPrintTool(this))
			{
				if (_printerSettings == null)
				{
					tool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default);
					return;
				}

				bool needPreview = _printerSettings.preview.GetValueOrDefault(1) == 1;

				if (needPreview)
				{
					tool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default);
					return;
				}

				string printerName = GetPrinterName(_printerSettings);

				if (string.IsNullOrWhiteSpace(printerName))
				{
					tool.Print();
					return;
				}

				if (!PrinterExists(printerName))
				{
					MessageBox.Show(
						$"Принтер не установлен на ПК:\n{printerName}\n\n" +
						$"Печать будет выполнена на принтер Windows по умолчанию:\n{GetDefaultPrinterName()}",
						"Принтер не найден",
						MessageBoxButtons.OK,
						MessageBoxIcon.Warning);

					tool.Print();
					return;
				}

				tool.Print(printerName);
			}
		}
		public async Task ShowPreviewBySettingsAsync(IWin32Window owner = null)
		{
			await LoadPrinterSettingsAsync();

			ApplyPrinterSettings();

			using (var tool = new ReportPrintTool(this))
			{
				tool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default);
			}
		}

		public async Task PrintBySettingsAsync()
		{
			await LoadPrinterSettingsAsync();

			ApplyPrinterSettings();

			using (var tool = new ReportPrintTool(this))
			{
				string printerName = GetPrinterName(_printerSettings);

				if (string.IsNullOrWhiteSpace(printerName))
				{
					tool.Print();
					return;
				}

				if (!PrinterExists(printerName))
					throw new Exception($"Принтер не найден в системе: {printerName}");

				tool.Print(printerName);
			}
		}

		protected virtual string GetReportName()
		{
			if (!string.IsNullOrWhiteSpace(ReportDbName))
				return ReportDbName;

			return GetType().Name;
		}

		protected virtual void ApplyPrinterSettings()
		{
			if (_printSettingsApplied)
				return;

			_printSettingsApplied = true;

			if (_printerSettings != null)
				ApplyReportPageSettings(_printerSettings);

			PrintingSystem.StartPrint += PrintingSystem_StartPrint;
		}
		private void PrintingSystem_StartPrint(object sender, DevExpress.XtraPrinting.PrintDocumentEventArgs e)
		{
			if (e?.PrintDocument == null)
				return;

			e.PrintDocument.EndPrint -= PrintDocument_EndPrint;
			e.PrintDocument.EndPrint += PrintDocument_EndPrint;

			if (_printerSettings == null)
				return;

			string printerName = GetPrinterName(_printerSettings);

			if (!string.IsNullOrWhiteSpace(printerName) && PrinterExists(printerName))
				e.PrintDocument.PrinterSettings.PrinterName = printerName;

			int copies = GetCopies(_printerSettings);
			if (copies > 0)
				e.PrintDocument.PrinterSettings.Copies = (short)copies;

			ApplyPrinterPageSettings(e.PrintDocument.DefaultPageSettings, _printerSettings);

			// Пока не трогаем PaperSize драйвера, чтобы не ломать этикетки.
			// Размер макета применяется через ApplyReportPageSettings().
		}
		private async void PrintDocument_EndPrint(object sender, PrintEventArgs e)
		{
			try
			{
				if (sender is not PrintDocument printDocument)
					return;

				if (_saveMode == PrinterSettingsSaveMode.None)
				{
					if (DebugPrinterSettings)
					{
						MessageBox.Show(
							"Сохранение настроек не требуется.\n\n" +
							$"Причина выбора:\n{_selectedReason}\n\n" +
							$"Принтер: {printDocument.PrinterSettings.PrinterName}",
							"Настройки печати",
							MessageBoxButtons.OK,
							MessageBoxIcon.Information);
					}

					return;
				}

				await SaveCurrentPrinterSettingsAsync(
					printDocument.PrinterSettings,
					printDocument.DefaultPageSettings);

				if (DebugPrinterSettings)
				{
					var printerSettings = printDocument.PrinterSettings;
					var pageSettings = printDocument.DefaultPageSettings;
					var paperSize = pageSettings?.PaperSize;

					MessageBox.Show(
						"Этап: PrintDocument.EndPrint\n\n" +
						$"Режим сохранения: {_saveMode}\n" +
						$"Причина выбора:\n{_selectedReason}\n\n" +
						$"Принтер: {printerSettings.PrinterName}\n" +
						$"Copies: {printerSettings.Copies}\n" +
						$"Landscape: {pageSettings?.Landscape}\n" +
						$"PaperSize: {paperSize?.PaperName}\n" +
						$"PaperSize.RawKind: {paperSize?.RawKind}\n" +
						$"PaperSize.Width: {paperSize?.Width}\n" +
						$"PaperSize.Height: {paperSize?.Height}",
						"Сохраненные настройки печати",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					$"Ошибка сохранения настроек печати:\n{ex.Message}",
					"Печать",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);
			}
		}
		private void ApplyReportPageSettings(PrinterParameters settings)
{
	if (settings == null)
		return;

	int orientation = GetInt(settings.Orientation, settings.orient.GetValueOrDefault(-1));

	if (orientation == 1)
		this.Landscape = true;
	else if (orientation == 0)
		this.Landscape = false;

	int paperWidthDb = GetInt(settings.PaperWidth, 0);
	int paperLengthDb = GetInt(settings.PaperLength, 0);

	int paperWidthReport = PaperSizeFromDbToReport(paperWidthDb);
	int paperLengthReport = PaperSizeFromDbToReport(paperLengthDb);

	if (paperWidthReport > 0 && paperLengthReport > 0)
	{
		this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.Custom;
		this.PageWidth = paperWidthReport;
		this.PageHeight = paperLengthReport;
	}
}

		private static void ApplyPrinterPaperSettings(
		PrinterSettings printerSettings,
		PageSettings pageSettings,
		PrinterParameters settings)
		{
			if (printerSettings == null || pageSettings == null || settings == null)
				return;

			int paperSizeRawKind = GetInt(settings.PaperSize, 0);

			if (paperSizeRawKind <= 0)
				return;

			var paperSize = printerSettings.PaperSizes
				.Cast<PaperSize>()
				.FirstOrDefault(x => x.RawKind == paperSizeRawKind);

			if (paperSize != null)
				pageSettings.PaperSize = paperSize;
		}

		private static void ApplyPrinterPageSettings(PageSettings pageSettings, PrinterParameters settings)
		{
			int orientation = GetInt(settings.Orientation, settings.orient.GetValueOrDefault(-1));

			if (orientation == 1)
				pageSettings.Landscape = true;
			else if (orientation == 0)
				pageSettings.Landscape = false;
		}

		private static string GetPrinterName(PrinterParameters settings)
		{
			if (settings == null)
				return string.Empty;

			if (!string.IsNullOrWhiteSpace(settings.printName))
				return settings.printName.Trim();

			if (!string.IsNullOrWhiteSpace(settings.Device))
				return settings.Device.Trim();

			return string.Empty;
		}

		private static int GetCopies(PrinterParameters settings)
		{
			if (settings == null)
				return 1;

			int copiesFromParam = GetInt(settings.Copies, 0);

			if (copiesFromParam > 0)
				return copiesFromParam;

			return settings.quantcopy.GetValueOrDefault(1);
		}

		private static int GetInt(string value, int defaultValue)
		{
			if (string.IsNullOrWhiteSpace(value))
				return defaultValue;

			if (int.TryParse(value.Trim(), out int result))
				return result;

			return defaultValue;
		}

		private static bool PrinterExists(string printerName)
		{
			foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
			{
				if (string.Equals(installedPrinter, printerName, StringComparison.OrdinalIgnoreCase))
					return true;
			}

			return false;
		}
		private async Task SaveCurrentPrinterSettingsAsync(PrinterSettings printerSettings, PageSettings pageSettings)
		{
			if (_printerSettingsSaving)
				return;

			_printerSettingsSaving = true;

			try
			{
				if (printerSettings == null)
					return;

				string reportName = GetReportName();
				string kompName = GetKompName();

				string actualPrinterName = printerSettings.PrinterName;

				if (string.IsNullOrWhiteSpace(actualPrinterName))
					actualPrinterName = GetDefaultPrinterName();

				if (string.IsNullOrWhiteSpace(actualPrinterName))
					return;

				var service = new SettingsDataService();

				// Ищем запись именно под фактический принтер.
				// Если пользователь в Preview сменил принтер — попадем в другую запись.
				PrinterParameters model = _printerSettingsList?
					.FirstOrDefault(x => PrinterNameEquals(GetPrinterName(x), actualPrinterName));

				if (model == null)
				{
					model = new PrinterParameters
					{
						pp_id = 0,
						pp_prg_name = "proizv_set",
						nameReport = reportName,
						shortName = reportName,
						sharpName = reportName,
						komp_name = kompName,
						printAlias = "auto",
						preview = 1
					};
				}

				short copies = printerSettings.Copies;

				bool landscape = pageSettings != null && pageSettings.Landscape;
				int orientation = landscape ? 1 : 0;

				PaperSize paperSize = pageSettings?.PaperSize;
				int paperRawKind = paperSize?.RawKind ?? 256;

				model.nameReport = string.IsNullOrWhiteSpace(model.nameReport)
					? reportName
					: model.nameReport;

				model.shortName = string.IsNullOrWhiteSpace(model.shortName)
					? reportName
					: model.shortName;

				model.sharpName = reportName;
				model.komp_name = kompName;

				model.printName = actualPrinterName;
				model.Device = actualPrinterName;

				model.quantcopy = copies;
				model.orient = orientation;

				model.Driver = "winspool";
				model.Output = string.Empty;
				model.Orientation = orientation.ToString();
				model.PaperSize = paperRawKind.ToString();
				model.Copies = copies.ToString();

				// В БД пишем размеры в формате FoxPro: в 10 раз больше.
				int paperWidthDb = PaperSizeFromReportToDb(this.PageWidth);
				int paperLengthDb = PaperSizeFromReportToDb(this.PageHeight);

				model.PaperWidth = paperWidthDb.ToString();
				model.PaperLength = paperLengthDb.ToString();

				model.param = BuildPrinterParam(model);

				await service.SaveAsync(model);

				_printerSettings = model;

				_saveMode = PrinterSettingsSaveMode.None;
				_needSaveReason = string.Empty;
			}
			finally
			{
				_printerSettingsSaving = false;
			}
		}
		private static string BuildPrinterParam(PrinterParameters settings)
		{
			if (settings == null)
				return string.Empty;

			return
				$"DRIVER={settings.Driver ?? "winspool"}\r\n" +
				$"DEVICE={settings.Device ?? settings.printName ?? string.Empty}\r\n" +
				$"OUTPUT={settings.Output ?? string.Empty}\r\n" +
				$"ORIENTATION={settings.Orientation ?? string.Empty}\r\n" +
				$"PAPERSIZE={settings.PaperSize ?? string.Empty}\r\n" +
				$"PAPERLENGTH={settings.PaperLength ?? string.Empty}\r\n" +
				$"PAPERWIDTH={settings.PaperWidth ?? string.Empty}\r\n" +
				$"COPIES={settings.Copies ?? string.Empty}\r\n" +
				$"PRINTQUALITY={settings.PrintQuality ?? string.Empty}\r\n" +
				$"YRESOLUTION={settings.YResolution ?? string.Empty}";
		}
		private void ShowDebugPrinterSettings(string stage)
		{
			if (!DebugPrinterSettings)
				return;

			if (_printerSettings == null)
			{
				MessageBox.Show(
					$"Этап: {stage}\n\nНастройки принтера НЕ найдены.\nReportDbName: {ReportDbName}\nTypeName: {GetType().Name}",
					"Диагностика печати",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning);

				return;
			}

			string text =
				$"Этап: {stage}\n\n" +
				$"ReportDbName: {ReportDbName}\n" +
				$"TypeName: {GetType().Name}\n\n" +

				$"pp_id: {_printerSettings.pp_id}\n" +
				$"nameReport: {_printerSettings.nameReport}\n" +
				$"sharpName: {_printerSettings.sharpName}\n" +
				$"komp_name: {_printerSettings.komp_name}\n\n" +

				$"printName: {_printerSettings.printName}\n" +
				$"Device: {_printerSettings.Device}\n" +
				$"preview: {_printerSettings.preview}\n" +
				$"quantcopy: {_printerSettings.quantcopy}\n" +
				$"orient: {_printerSettings.orient}\n\n" +

				$"Orientation: {_printerSettings.Orientation}\n" +
				$"PaperSize: {_printerSettings.PaperSize}\n" +
				$"PaperWidth: {_printerSettings.PaperWidth}\n" +
				$"PaperLength: {_printerSettings.PaperLength}\n" +
				$"Copies: {_printerSettings.Copies}";

			MessageBox.Show(
				text,
				"Диагностика печати",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information);
		}
		// БД хранит размеры в формате FoxPro: значение * 10
		private const int PaperSizeDbScale = 10;

		private static int PaperSizeFromDbToReport(int dbValue)
		{
			if (dbValue <= 0)
				return 0;

			return (int)Math.Round(dbValue / (decimal)PaperSizeDbScale);
		}

		private static int PaperSizeFromReportToDb(int reportValue)
		{
			if (reportValue <= 0)
				return 0;

			return reportValue * PaperSizeDbScale;
		}
	}
}
