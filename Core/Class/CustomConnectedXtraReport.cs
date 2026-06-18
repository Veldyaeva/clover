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
		#region Fields / Constructor	
		private PrinterParameters _printerSettings;
		private bool _printSettingsApplied;
		private bool _printerSettingsSaving;
		public bool DebugPrinterSettings { get; set; } = true;

		public string ReportDbName { get; set; }

		private List<PrinterParameters> _printerSettingsList = new List<PrinterParameters>();
		private PrinterSettingsSaveMode _saveMode = PrinterSettingsSaveMode.None;
		private PrinterSettingsSelectMode _selectMode = PrinterSettingsSelectMode.None;
		private PrinterSettingsSnapshot _settingsBeforePrint;
		private string _selectedReason = string.Empty;

		public CustomConnectedXtraReport()
		{
			ReportDbName = GetType().Name;
		}
		#endregion

		#region Enums / Snapshot Models	
		private enum PrinterSettingsSaveMode
		{
			None = 0,

			// Спросить после печати, сохранять ли новую запись.
			AskInsertAfterPrint = 1,

			// Спросить после печати, обновлять ли существующую запись.
			AskUpdateAfterPrint = 2
		}
		private enum PrinterSettingsSelectMode
		{
			None = 0,
			CurrentUserSettings = 1,
			DefaultPrinterStandardReport = 2,
			OtherUserSettingsSamePrinter = 3,
			LastCurrentUserSettingsWithDefaultPrinter = 4
		}
		private class PrinterSettingsSnapshot
		{
			public string PrinterName { get; set; }
			public int Copies { get; set; }
			public bool Landscape { get; set; }
			public int PageWidth { get; set; }
			public int PageHeight { get; set; }
			public string PaperSize { get; set; }
		}
		#endregion

		#region Public Print API	   
		public async Task ShowOrPrintBySettingsAsync(IWin32Window owner = null)
		{
			await LoadPrinterSettingsAsync();

			//ShowDebugPrinterSettings("После LoadPrinterSettingsAsync");

			ApplyPrinterSettings();

			_settingsBeforePrint = CreateSnapshotFromCurrentState(_printerSettings);

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
			_settingsBeforePrint = CreateSnapshotFromCurrentState(_printerSettings);

			using (var tool = new ReportPrintTool(this))
			{
				tool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default);
			}
		}
		public async Task PrintBySettingsAsync()
		{
			await LoadPrinterSettingsAsync();

			ApplyPrinterSettings();

			_settingsBeforePrint = CreateSnapshotFromCurrentState(_printerSettings);

			using (var tool = new ReportPrintTool(this))
			{
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
		#endregion

		#region Load And Select Settings 
		public async Task LoadPrinterSettingsAsync()
		{
			string reportName = GetReportName();

			if (string.IsNullOrWhiteSpace(reportName))
				return;

			var service = new SettingsDataService();

			_printerSettingsList = await service.GetPrinterParametersListByNameAsync(reportName);

			_printerSettings = null;
			_saveMode = PrinterSettingsSaveMode.None;
			_selectMode = PrinterSettingsSelectMode.None;
			_selectedReason = string.Empty;

			string defaultPrinterName = GetDefaultPrinterName();

			if (_printerSettingsList == null || _printerSettingsList.Count == 0)
			{
				_printerSettings = await SelectFallbackSettingsAsync(
					service,
					reportName,
					defaultPrinterName,
					"В БД нет настроек для текущего пользователя/компьютера.");

				_settingsBeforePrint = CreateSnapshot(_printerSettings);
				return;
			}

			// 1. Ищем defaultPrinter среди настроек текущего пользователя.
			var defaultPrinterSettings = _printerSettingsList
				.FirstOrDefault(x => PrinterNameEquals(GetPrinterName(x), defaultPrinterName));

			if (defaultPrinterSettings != null && PrinterExists(GetPrinterName(defaultPrinterSettings)))
			{
				_printerSettings = defaultPrinterSettings;
				_saveMode = PrinterSettingsSaveMode.AskUpdateAfterPrint;
				_selectMode = PrinterSettingsSelectMode.CurrentUserSettings;
				_selectedReason = "Использованы настройки текущего пользователя для принтера Windows по умолчанию.";

				_settingsBeforePrint = CreateSnapshot(_printerSettings);
				return;
			}

			// 2. Если defaultPrinter нет или он не установлен — берем последнюю запись текущего пользователя.
			var lastSettings = _printerSettingsList
				.OrderByDescending(x => x.pp_id)
				.FirstOrDefault();

			if (lastSettings != null && PrinterExists(GetPrinterName(lastSettings)))
			{
				_printerSettings = lastSettings;
				_saveMode = PrinterSettingsSaveMode.AskUpdateAfterPrint;
				_selectMode = PrinterSettingsSelectMode.CurrentUserSettings;
				_selectedReason = "Использована последняя подходящая запись текущего пользователя.";

				_settingsBeforePrint = CreateSnapshot(_printerSettings);
				return;
			}

			// 3. Ищем следующий установленный принтер из списка БД.
			var installedFromDb = _printerSettingsList
				.OrderByDescending(x => x.pp_id)
				.FirstOrDefault(x => PrinterExists(GetPrinterName(x)));

			if (installedFromDb != null)
			{
				_printerSettings = installedFromDb;
				_saveMode = PrinterSettingsSaveMode.AskUpdateAfterPrint;
				_selectMode = PrinterSettingsSelectMode.CurrentUserSettings;
				_selectedReason = "Использован первый установленный принтер из списка настроек БД.";

				_settingsBeforePrint = CreateSnapshot(_printerSettings);
				return;
			}

			// 4. В списке текущего пользователя есть записи, но ни один принтер не подключен.
			_printerSettings = await SelectFallbackSettingsAsync(
				service,
				reportName,
				defaultPrinterName,
				"В БД есть настройки текущего пользователя, но ни один принтер из них не подключен к ПК.");

			_settingsBeforePrint = CreateSnapshot(_printerSettings);
		}
		private async Task<PrinterParameters> SelectFallbackSettingsAsync(
		SettingsDataService service,
		string reportName,
		string defaultPrinterName,
		string reason)
		{
			DialogResult result = AskUseOtherUserSettings();

			if (result == DialogResult.Yes)
			{
				var otherUserSettings = await service.GetLastPrinterParametersByReportAndPrinterAsync(
					reportName,
					defaultPrinterName);

				if (otherUserSettings != null)
				{
					var cloned = ClonePrinterSettings(otherUserSettings);

					cloned.pp_id = 0;
					cloned.komp_name = GetKompName();
					cloned.sharpName = reportName;
					cloned.printName = defaultPrinterName;
					cloned.Device = defaultPrinterName;
					cloned.param = BuildPrinterParam(cloned);

					_saveMode = PrinterSettingsSaveMode.AskInsertAfterPrint;
					_selectMode = PrinterSettingsSelectMode.OtherUserSettingsSamePrinter;
					_selectedReason = reason + "\nВыбраны настройки другого пользователя для этого же отчета и принтера.";

					return cloned;
				}

				MessageBox.Show(
					"Настройки других пользователей для этого отчета и принтера не найдены.\n" +
					"Будут использованы стандартные настройки отчета.",
					"Настройки печати",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information);
			}

			_saveMode = PrinterSettingsSaveMode.AskInsertAfterPrint;
			_selectMode = PrinterSettingsSelectMode.DefaultPrinterStandardReport;
			_selectedReason = reason + "\nВыбраны стандартные настройки отчета и принтер Windows по умолчанию.";

			return CreateDefaultPrinterSettings(reportName, defaultPrinterName);
		}
		private DialogResult AskUseOtherUserSettings()
		{
			return MessageBox.Show(
				"Для этого отчета нет подходящих настроек печати на текущем компьютере.\n\n" +
				"Да — найти настройки других пользователей для этого отчета и принтера Windows по умолчанию.\n" +
				"Нет — использовать принтер Windows по умолчанию и стандартные настройки отчета.",
				"Настройки печати",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question);
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
		#endregion

		#region Print Events		
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

				var afterPrintSnapshot = CreateSnapshot(
					printDocument.PrinterSettings,
					printDocument.DefaultPageSettings);

				bool settingsChanged = !SnapshotEquals(_settingsBeforePrint, afterPrintSnapshot);

				bool needAskSave =
					_saveMode == PrinterSettingsSaveMode.AskInsertAfterPrint
					|| (_saveMode == PrinterSettingsSaveMode.AskUpdateAfterPrint && settingsChanged);

				if (!needAskSave)
				{
					if (DebugPrinterSettings)
					{
						MessageBox.Show(
							"Сохранение настроек не требуется.\n\n" +
							$"Причина выбора:\n{_selectedReason}\n\n" +
							$"Настройки изменены: {settingsChanged}",
							"Настройки печати",
							MessageBoxButtons.OK,
							MessageBoxIcon.Information);
					}

					return;
				}

				DialogResult saveResult = MessageBox.Show(
					"Сохранить настройки печати для последующих печатей?\n\n" +
					$"Отчет: {GetReportName()}\n" +
					$"Принтер: {printDocument.PrinterSettings.PrinterName}\n\n" +
					$"Причина:\n{_selectedReason}",
					"Сохранение настроек печати",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question);

				if (saveResult != DialogResult.Yes)
					return;

				await SaveCurrentPrinterSettingsAsync(
					printDocument.PrinterSettings,
					printDocument.DefaultPageSettings);

				if (DebugPrinterSettings)
				{
					MessageBox.Show(
						"Настройки печати сохранены.",
						"Настройки печати",
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
		#endregion

		#region Save Settings			   
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

				await service.SavePrinterParametersByKeyAsync(model);

				_printerSettings = model;

				_saveMode = PrinterSettingsSaveMode.None;
			}
			finally
			{
				_printerSettingsSaving = false;
			}
		}
		#endregion

		#region Apply Settings	  
		protected virtual void ApplyPrinterSettings()
		{
			if (_printSettingsApplied)
				return;

			_printSettingsApplied = true;

			if (_printerSettings != null)
				ApplyReportPageSettings(_printerSettings);

			PrintingSystem.StartPrint += PrintingSystem_StartPrint;
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
		private static void ApplyPrinterPageSettings(PageSettings pageSettings, PrinterParameters settings)
		{
			int orientation = GetInt(settings.Orientation, settings.orient.GetValueOrDefault(-1));

			if (orientation == 1)
				pageSettings.Landscape = true;
			else if (orientation == 0)
				pageSettings.Landscape = false;
		}
		#endregion

		#region Snapshot / Change Detection	 
		private PrinterSettingsSnapshot CreateSnapshot(PrinterParameters settings)
		{
			if (settings == null)
				return null;

			int orientation = GetInt(settings.Orientation, settings.orient.GetValueOrDefault(0));

			return new PrinterSettingsSnapshot
			{
				PrinterName = GetPrinterName(settings),
				Copies = GetCopies(settings),
				Landscape = orientation == 1,
				PageWidth = this.PageWidth,
				PageHeight = this.PageHeight,
				PaperSize = settings.PaperSize ?? string.Empty
			};
		}
		private PrinterSettingsSnapshot CreateSnapshot(PrinterSettings printerSettings, PageSettings pageSettings)
		{
			return new PrinterSettingsSnapshot
			{
				PrinterName = printerSettings?.PrinterName ?? string.Empty,
				Copies = printerSettings?.Copies ?? 1,
				Landscape = pageSettings?.Landscape ?? false,
				PageWidth = this.PageWidth,
				PageHeight = this.PageHeight,
				PaperSize = pageSettings?.PaperSize?.RawKind.ToString() ?? string.Empty
			};
		}
		private PrinterSettingsSnapshot CreateSnapshotFromCurrentState(PrinterParameters settings)
		{
			if (settings == null)
				return null;

			int orientation = this.Landscape ? 1 : 0;

			return new PrinterSettingsSnapshot
			{
				PrinterName = GetPrinterName(settings),
				Copies = GetCopies(settings),
				Landscape = orientation == 1,
				PageWidth = this.PageWidth,
				PageHeight = this.PageHeight,
				PaperSize = settings.PaperSize ?? string.Empty
			};
		}
		private static bool SnapshotEquals(PrinterSettingsSnapshot a, PrinterSettingsSnapshot b)
		{
			if (a == null || b == null)
				return false;

			return PrinterNameEquals(a.PrinterName, b.PrinterName)
				&& a.Copies == b.Copies
				&& a.Landscape == b.Landscape
				&& a.PageWidth == b.PageWidth
				&& a.PageHeight == b.PageHeight;
		}
		#endregion

		#region Printer Helpers	 
		protected virtual string GetReportName()
		{
			if (!string.IsNullOrWhiteSpace(ReportDbName))
				return ReportDbName;

			return GetType().Name;
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
		private static bool PrinterExists(string printerName)
		{
			foreach (string installedPrinter in PrinterSettings.InstalledPrinters)
			{
				if (string.Equals(installedPrinter, printerName, StringComparison.OrdinalIgnoreCase))
					return true;
			}

			return false;
		}
		private static bool PrinterNameEquals(string printerName1, string printerName2)
		{
			return string.Equals(
				(printerName1 ?? string.Empty).Trim(),
				(printerName2 ?? string.Empty).Trim(),
				StringComparison.OrdinalIgnoreCase);
		}
		#endregion

		#region Param Helpers  
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
		private static int GetInt(string value, int defaultValue)
		{
			if (string.IsNullOrWhiteSpace(value))
				return defaultValue;

			if (int.TryParse(value.Trim(), out int result))
				return result;

			return defaultValue;
		}
		#endregion

		#region Size Conversion

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
		#endregion
	}
}
