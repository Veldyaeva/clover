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

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class HistoryLog : UserControl, IHistoryLogView
    {
        private IHistoryLogPresenter _presenter;

        public HistoryLog()
        {
            InitializeComponent();
            this.Load += HistoryLog_Load;
            var dbHelper = new DatabaseHelper();
            var dbService = new DbService(dbHelper);
            var logger = new FileLogger();
            _presenter = new HistoryLogPresenter(this, dbService, logger);
        }

        private async void HistoryLog_Load(object sender, EventArgs e)
        {
            await _presenter.InitializeAsync();
        }

        public void SetDataSource(object dataSource)
        {
            customGridControl1.DataSource = dataSource;
        }

        public Control AsControl => this;
    }

    public interface IHistoryLogView
    {
        void SetDataSource(object dataSource);
        Control AsControl { get; }
    }

    public interface IHistoryLogPresenter
    {
        Task InitializeAsync();
    }

    public class HistoryLogPresenter : IHistoryLogPresenter
    {
        private readonly IHistoryLogView _view;
        private readonly DbService _dbService;
        private readonly ILogger _logger;

        public HistoryLogPresenter(IHistoryLogView view, DbService dbService, ILogger logger)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _dbService = dbService ?? throw new ArgumentNullException(nameof(dbService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InitializeAsync()
        {
            try
            {
                // Создаем источник данных и загружаем историю через универсальные методы сервиса БД
                string query = @"
                SELECT 
                    ah.ActionHistoryID,
                    ah.UserID,
                    u.UserName,
                    ah.NameForm,
                    ah.NameObject,
                    ah.Event,
                    ah.EventDate,
                    ah.Komp
                FROM ActionHistory ah
                LEFT JOIN Users u ON u.UserID = ah.UserID
                ORDER BY ah.EventDate DESC";

                var rows = await _dbService.GetListAsync<ActionHistoryRow>(query, new { });
                var binding = new BindingList<ActionHistoryRow>(rows ?? new List<ActionHistoryRow>());
                _view.SetDataSource(binding);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex, "Ошибка загрузки данных для HistoryLog");
                MessageBox.Show($"Ошибка загрузки журнала: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class ActionHistoryRow
    {
        public int ActionHistoryID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string NameForm { get; set; }
        public string NameObject { get; set; }
        public string Event { get; set; }
        public DateTime EventDate { get; set; }
        public string Komp { get; set; }
    }
}
