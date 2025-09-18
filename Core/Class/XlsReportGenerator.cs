using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SewingProduction
{
    public class ExcelReportGenerator
    {
        // Универсальный метод для генерации "Excel"-отчета из BindingSource (с именем отчета, открывает диалог сохранения)
        public void GenerateExcel(string reportName, BindingSource bindingSource, bool openAfterSave = false)
        {
            string filePath = GetFilePathFromDialog(reportName);
            if (string.IsNullOrEmpty(filePath))
            {
                return; // Выходим, если пользователь отменил диалог
            }
            GenerateExcelReport(filePath, bindingSource, openAfterSave); // Вызываем основной метод с указанием пути
        }

        // Универсальный метод для генерации Excel-отчета из BindingSource и сохранением в файл по указанному пути
        public void GenerateExcelReport(string filePath, BindingSource bindingSource, bool openAfterSave = false)
        {
            try
            {
                Console.WriteLine($"GenerateExcelReport");
                DataTable dataTable = GetDataTableFromBindingSource(bindingSource);
                if (dataTable == null) { return; }
                XtraReport report = new XtraReport();
                report.DataSource = dataTable;

                if (dataTable.Rows.Count > 0)
                {
                    Console.WriteLine($"data.Count: {dataTable.Rows.Count}");
                    var properties = dataTable.Columns.Cast<DataColumn>().ToList();
                    Console.WriteLine("Свойства:");
                    foreach (var property in properties)
                    {
                        Console.WriteLine($"- Name: {property.ColumnName}, Type: {property.DataType.Name}");
                    }

                    // Измерение максимальной ширины текста для каждого столбца
                    Dictionary<string, float> columnWidths = CalculateColumnWidths(dataTable, properties);

                    // Создание шапки таблицы
                    PageHeaderBand pageHeader = new PageHeaderBand();
                    report.Bands.Add(pageHeader);
                    pageHeader.HeightF = 0f;
                    float currentXPosition = 0;
                    foreach (var property in properties)
                    {
                        XRLabel headerLabel = new XRLabel();
                        headerLabel.Text = property.ColumnName;
                        Console.WriteLine($"headerLabel: {headerLabel.Text}");
                        headerLabel.Font = new System.Drawing.Font("Times New Roman", 10, System.Drawing.FontStyle.Bold);
                        headerLabel.WidthF = columnWidths[property.ColumnName];
                        headerLabel.LocationF = new System.Drawing.PointF(currentXPosition, 0);
                        pageHeader.Controls.Add(headerLabel);
                        currentXPosition += columnWidths[property.ColumnName];
                    }
                    // Создаем DetailBand и добавляем его к отчету
                    DetailBand detailBand = new DetailBand();
                    report.Bands.Add(detailBand);
                    detailBand.HeightF = 0f;
                    currentXPosition = 0;
                    // Добавляем столбцы и привязываем их к данным
                    foreach (var property in properties)
                    {
                        Console.WriteLine($"property.Name: {property.ColumnName}");
                        XRLabel label = new XRLabel();
                        label.DataBindings.Add("Text", null, property.ColumnName);
                        label.WidthF = columnWidths[property.ColumnName];
                        label.CanShrink = true;
                        label.LocationF = new System.Drawing.PointF(currentXPosition, 0);
                        detailBand.Controls.Add(label);
                        currentXPosition += columnWidths[property.ColumnName];
                    }
                }
                // Настройка параметров экспорта в Excel
                DevExpress.XtraPrinting.XlsxExportOptions exportOptions = new DevExpress.XtraPrinting.XlsxExportOptions();
                exportOptions.ShowGridLines = true;
                exportOptions.SheetName = "Отчет";
                Console.WriteLine($"Название листа: {exportOptions.SheetName}");
                // Экспорт отчета в Excel
                report.ExportToXlsx(filePath, exportOptions);
                // Открытие файла, если нужно
                if (openAfterSave)
                {
                    OpenFile(filePath);
                }
            }
            catch (Exception ex)
            {
                // Логгирование ошибок или вывод сообщения об ошибке
                Console.WriteLine($"Ошибка при генерации Excel-отчета: {ex.Message}");
                throw; // Перебрасываем исключение для дальнейшей обработки
            }
        }
        // Метод для измерения максимальной ширины текста для каждого столбца
        private Dictionary<string, float> CalculateColumnWidths(DataTable dataTable, List<DataColumn> properties)
        {
            Dictionary<string, float> columnWidths = new Dictionary<string, float>();
            using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
            {
                foreach (var property in properties)
                {
                    float maxWidth = 0;
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string value = row[property].ToString();
                        SizeF size = graphics.MeasureString(value, new Font("Times New Roman", 10));
                        maxWidth = Math.Max(maxWidth, size.Width);
                    }
                    // Add extra space
                    columnWidths[property.ColumnName] = maxWidth + 22;
                }
            }
            return columnWidths;
        }


        // Метод для открытия файла
        private void OpenFile(string filePath)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                };
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при открытии файла: {ex.Message}");
                MessageBox.Show($"Не удалось открыть файл: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Метод для открытия диалога выбора файла
        private string GetFilePathFromDialog(string reportName)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"; // Фильтр типов файлов
            saveFileDialog.Title = "Сохранить отчет в Excel";
            saveFileDialog.FileName = $"{reportName}.xlsx"; // Имя по умолчанию
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                return saveFileDialog.FileName; // Возвращаем путь к файлу, выбранный пользователем
            }
            return null; // Возвращаем null, если пользователь отменил диалог
        }

        // Метод для извлечения данных из BindingSource
        private DataTable GetDataTableFromBindingSource(BindingSource bindingSource)
        {
            if (bindingSource == null || bindingSource.DataSource == null)
            {
                Console.WriteLine($"bS =null || bS.DS =null");
                return null;
            }
            if (bindingSource.DataSource is DataTable dataTable)
            {
                Console.WriteLine($"bindingSource.DataSource is DataTable dataTable");
                return dataTable; // Возвращаем DataTable
            }
            return null; // Возвращаем null, если DataSource не является DataTable
        }
    }
}
