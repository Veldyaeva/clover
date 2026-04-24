using SewingProduction.Helpers;
using SewingProduction.Services;

namespace SewingProduction.Features.TeamWork.Services
{
    /// <summary>
    /// Централизованная фабрика инфраструктурных зависимостей TeamWork внутри хост-проекта.
    /// Убирает ручную сборку DatabaseHelper/DbService/репозитория из форм.
    /// </summary>
    public static class TeamWorkDependencyFactory
    {
        public static TeamWorkDatabaseServices CreateDatabaseServices()
        {
            var dbHelper = new DatabaseHelperSQL();
            var dbService = new DbService(dbHelper);
            var artNormRepository = new ArtNormRepository(dbHelper);

            return new TeamWorkDatabaseServices(dbHelper, dbService, artNormRepository);
        }

        public static TeamWorkCoreServices CreateCoreServices(ILogger logger)
        {
            var databaseServices = CreateDatabaseServices();
            var jabberSender = new JabberSender(databaseServices.DbHelper);
            var repository = new TeamWorkRepositoryAdapter(
                databaseServices.ArtNormRepository,
                databaseServices.DbService,
                databaseServices.DbHelper,
                logger);
            var unitOfWork = new TeamWorkUnitOfWorkAdapter(
                databaseServices.ArtNormRepository,
                databaseServices.DbService,
                databaseServices.DbHelper);
            var notificationService = new TeamWorkNotificationServiceAdapter(jabberSender);
            var transactionBoundary = new TeamWorkTransactionBoundaryAdapter(databaseServices.DbHelper);
            var orchestrator = new TeamWorkOrchestrator(
                repository,
                unitOfWork,
                transactionBoundary,
                notificationService,
                logger);

            return new TeamWorkCoreServices(
                databaseServices.DbHelper,
                databaseServices.DbService,
                databaseServices.ArtNormRepository,
                jabberSender,
                orchestrator);
        }
    }

    public class TeamWorkDatabaseServices
    {
        public TeamWorkDatabaseServices(
            DatabaseHelperSQL dbHelper,
            DbService dbService,
            ArtNormRepository artNormRepository)
        {
            DbHelper = dbHelper;
            DbService = dbService;
            ArtNormRepository = artNormRepository;
        }

        public DatabaseHelperSQL DbHelper { get; }
        public DbService DbService { get; }
        public ArtNormRepository ArtNormRepository { get; }
    }

    public sealed class TeamWorkCoreServices : TeamWorkDatabaseServices
    {
        public TeamWorkCoreServices(
            DatabaseHelperSQL dbHelper,
            DbService dbService,
            ArtNormRepository artNormRepository,
            IJabberSender jabberSender,
            ITeamWorkOrchestrator orchestrator)
            : base(dbHelper, dbService, artNormRepository)
        {
            JabberSender = jabberSender;
            Orchestrator = orchestrator;
        }

        public IJabberSender JabberSender { get; }
        public ITeamWorkOrchestrator Orchestrator { get; }
    }
}
