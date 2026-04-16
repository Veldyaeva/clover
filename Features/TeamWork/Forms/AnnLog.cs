using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SewingProduction.Services;
using SewingProduction.Helpers;
using DevExpress.XtraGrid.Views.Grid;

namespace SewingProduction.Features.TeamWork.Forms
{
    // Источник данных для журнала
    public enum LogSourceType
    {
        Ann,
        Rasz
    }

    // Тонкая форма (View) по паттерну MVP: отвечает только за отображение и делегирует логику презентеру
    public partial class Log : Form, IAnnLogView
    {
        private IAnnLogPresenter _presenter;
        private readonly DatabaseHelperSQL _dbHelper;
        private readonly DbService _dbService;
        private readonly ILogger _logger;
        private int _annId;
        private LogSourceType _sourceType = LogSourceType.Ann;

        // Базовый конструктор: создаёт зависимости и подписывается на загрузку
        public Log()
        {
            InitializeComponent();
            this.Load += AnnLog_Load;

            _dbHelper = new DatabaseHelperSQL();
            _dbService = new DbService(_dbHelper);
            _logger = new FileLogger();
        }

        // Конструктор с передачей AnnID для фильтрации лога
        public Log(int annId) : this()
        {
            _annId = annId;
        }

        // Конструктор с передачей AnnID и типа источника (ANN или RASZ)
        public Log(int annId, LogSourceType sourceType) : this()
        {
            _annId = annId;
            _sourceType = sourceType;
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        // На загрузке формы создаём презентер и инициируем загрузку данных
        private async void AnnLog_Load(object sender, EventArgs e)
        {
			// Устанавливаем заголовок формы в зависимости от источника данных
			this.Text = _sourceType == LogSourceType.Ann ? "art_norm_n" : "norm_rasz";

            _presenter = new AnnLogPresenter(this, _dbService, _logger, _dbHelper, _annId, _sourceType);
            await _presenter.InitializeAsync();
        }

        // Установка источника данных для грида (DataTable / BindingList и др.)
        public void SetDataSource(object dataSource)
        {
            customGridControl1.DataSource = dataSource;
			// Настраиваем отображение столбцов и превью после привязки данных
			ConfigureGridForCompactRow();
        }

        public Control AsControl => this;

		// Внутренняя настройка грида: оставляем в строке только ключевые поля и выносим остальные в превью
		private void ConfigureGridForCompactRow()
		{
			try
			{
				if (gridView1 == null || gridView1.Columns.Count == 0) return;

				// Включаем превью и задаём количество строк превью
				gridView1.OptionsView.ShowPreview = true;
				gridView1.PreviewLineCount = 3;

				// Определяем имена столбцов для ключевых полей с учётом возможных синонимов
				string dateDel = FindFirstExistingColumn("annDateDel", "nrDateDel", "dateDel");
				string compDel = FindFirstExistingColumn("annCompDel", "nrCompDel", "compDel");
				string updType = FindFirstExistingColumn("updType", "upd_type");
				string updDate = FindFirstExistingColumn("updDate", "date_add", "upd_date");
				string compUpd = FindFirstExistingColumn("compUpd", "komp_name", "compname");
				string kod = FindFirstExistingColumn("kod", "KOD", "ko");
				string grup = FindFirstExistingColumn("grup", "group", "gr");
				string articul = FindFirstExistingColumn("articul", "art");
				string mod = FindFirstExistingColumn("mod", "model");
				string sizeLabel = FindFirstExistingColumn("size_label", "sizeLabel", "razm", "Razm");
				string parentId = FindFirstExistingColumn("parentId", "parentid", "parent_id");
                

				// Набор столбцов, которые показываем в строке
				var keep = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
				{
					"annId",
					dateDel ?? string.Empty,
					compDel ?? string.Empty,
					"status",
					"arh",
					updType ?? string.Empty,
					updDate ?? string.Empty,
					compUpd ?? string.Empty,
					kod ?? string.Empty,
					grup ?? string.Empty,
					articul ?? string.Empty,
					mod ?? string.Empty,
					sizeLabel ?? string.Empty,
					parentId ?? string.Empty
				};

				// Прячем все остальные столбцы
				foreach (var col in gridView1.Columns.Cast<DevExpress.XtraGrid.Columns.GridColumn>())
				{
					bool show = !string.IsNullOrEmpty(col.FieldName) && keep.Contains(col.FieldName);
					col.Visible = show;
				}

				// Человекочитаемые заголовки (при наличии)
				SetCaptionIfExists("annId", "AnnID");
				if (!string.IsNullOrEmpty(dateDel)) SetCaptionIfExists(dateDel, "Дата удаления");
				if (!string.IsNullOrEmpty(compDel)) SetCaptionIfExists(compDel, "Кто удалил");
				SetCaptionIfExists("status", "Статус");
				SetCaptionIfExists("arh", "Архив");
                SetCaptionIfExists("updType", "Тип обновления");
                SetCaptionIfExists("updDate", "Дата обновления");
                if (!string.IsNullOrEmpty(compUpd)) SetCaptionIfExists(compUpd, "Кто обновил");
				if (!string.IsNullOrEmpty(kod)) SetCaptionIfExists(kod, "Код");
				if (!string.IsNullOrEmpty(grup)) SetCaptionIfExists(grup, "Группа");
				if (!string.IsNullOrEmpty(articul)) SetCaptionIfExists(articul, "Артикул");
				if (!string.IsNullOrEmpty(mod)) SetCaptionIfExists(mod, "Мод");
				if (!string.IsNullOrEmpty(sizeLabel)) SetCaptionIfExists(sizeLabel, "Размер");
				if (!string.IsNullOrEmpty(parentId)) SetCaptionIfExists(parentId, "Родительский ID");

				// Настраиваем текст превью: перечисляем остальные непустые поля
				gridView1.CalcPreviewText -= GridView1_CalcPreviewText;
				gridView1.CalcPreviewText += GridView1_CalcPreviewText;
			}
			catch (Exception ex)
			{
				_ = _logger.LogErrorAsync(ex, "ConfigureGridForCompactRow");
			}
		}

		// Обработчик формирования текста превью
		private void GridView1_CalcPreviewText(object sender, CalcPreviewTextEventArgs e)
		{
			try
			{
				if (e.RowHandle < 0) return;
				var view = sender as GridView;
				var drv = view?.GetRow(e.RowHandle) as DataRowView;
				if (drv == null) return;

				// Поля, которые уже показаны в строке (не дублируем в превью)
				string dateDel = FindFirstExistingColumn("annDateDel", "nrDateDel", "dateDel");
				string compDel = FindFirstExistingColumn("annCompDel", "nrCompDel", "compDel");
				string updType = FindFirstExistingColumn("updType");
				string updDate = FindFirstExistingColumn("updDate");
				string compUpd = FindFirstExistingColumn("compUpd", "compname");
				string kod = FindFirstExistingColumn("kod");
				string grup = FindFirstExistingColumn("grup", "group");
				string articul = FindFirstExistingColumn("articul");
				string mod = FindFirstExistingColumn("mod", "model");
				string sizeLabel = FindFirstExistingColumn("size_label", "sizeLabel", "razm", "Razm");
				string parentId = FindFirstExistingColumn("parentId");

				var skip = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
				{
					"annId",
					dateDel ?? string.Empty,
					compDel ?? string.Empty,
					"status",
					"arh",
					updType ?? string.Empty,
					updDate ?? string.Empty,
					compUpd ?? string.Empty,
					kod ?? string.Empty,
					grup ?? string.Empty,
					articul ?? string.Empty,
					mod ?? string.Empty,
					sizeLabel ?? string.Empty,
					parentId ?? string.Empty
				};

				var sb = new StringBuilder();
				foreach (DataColumn col in drv.Row.Table.Columns)
				{
					string name = col.ColumnName;
					if (skip.Contains(name)) continue;
					var val = drv.Row[name];
					if (val == null || val == DBNull.Value) continue;
					string text = val.ToString();
					if (string.IsNullOrWhiteSpace(text)) continue;
					sb.Append(name).Append(": ").Append(text).Append("; ");
				}
				e.PreviewText = sb.ToString();
			}
			catch (Exception ex)
			{
				_ = _logger.LogErrorAsync(ex, "GridView1_CalcPreviewText");
			}
		}

		// Поиск первого существующего столбца по списку возможных имён
		private string FindFirstExistingColumn(params string[] candidates)
		{
			if (gridView1 == null) return null;
			foreach (var c in candidates)
			{
				var col = gridView1.Columns.ColumnByFieldName(c);
				if (col != null) return c;
			}
			return null;
		}

		// Установка заголовка колонки, если она существует
		private void SetCaptionIfExists(string fieldName, string caption)
		{
			if (string.IsNullOrEmpty(fieldName)) return;
			var col = gridView1.Columns.ColumnByFieldName(fieldName);
			if (col != null) col.Caption = caption;
        }
    }

    // Контракт представления для презентера
    public interface IAnnLogView
    {
        void SetDataSource(object dataSource);
        Control AsControl { get; }
    }

    // Контракт презентера
    public interface IAnnLogPresenter
    {
        Task InitializeAsync();
    }

    // Презентер: содержит бизнес-логику загрузки журнала и передачи данных во View
    public class AnnLogPresenter : IAnnLogPresenter
    {
        private readonly IAnnLogView _view;
        private readonly DbService _dbService;
        private readonly ILogger _logger;
        private readonly DatabaseHelperSQL _dbHelper;
        private readonly int _annId;
        private readonly LogSourceType _sourceType;

        // Принимает зависимости и параметры контекста (AnnID и тип источника)
        public AnnLogPresenter(IAnnLogView view, DbService dbService, ILogger logger, DatabaseHelperSQL dbHelper, int annId, LogSourceType sourceType)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _dbService = dbService ?? throw new ArgumentNullException(nameof(dbService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
            _annId = annId;
            _sourceType = sourceType;
        }

        // Загружает последние 100 записей из соответствующей таблицы лога по AnnID
        public async Task InitializeAsync()
        {
            try
            {
                string query;
                if (_sourceType == LogSourceType.Ann)
                {
                    // Журнал изменений art_norm_n
                    query = @"SELECT TOP (100) *
                              FROM ACE_log.dbo.art_norm_n_updLog annl
                              WHERE annl.annID = @annId
                              ORDER BY annl.annlID DESC";
                }
                else
                {
                    // Журнал изменений norm_rasz
                    query = @"SELECT TOP (100) *
                              FROM ACE_log.dbo.norm_rasz_updLog nrl
                              WHERE nrl.annId = @annId
                              ORDER BY nrl.nrlID DESC";
                }

                // Параметризованный запрос по AnnID
                var parameters = new Dictionary<string, object> { { "@annId", _annId } };

                // Загружаем таблицу и передаём её во View для отображения
                DataTable table = await _dbHelper.ExecuteQueryAsync(query, parameters);
                _view.SetDataSource(table);
            }
            catch (Exception ex)
            {
                // Логирование ошибки и уведомление пользователя
                await _logger.LogErrorAsync(ex, "Ошибка загрузки данных для AnnLog");
                MessageBox.Show($"Ошибка загрузки журнала: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
