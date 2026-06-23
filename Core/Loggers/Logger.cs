using System;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SewingProduction.Helpers;

namespace SewingProduction
{
    public class DatabaseLogger : ILogger
    {
        private string _connectionString;
        private readonly int logStoreDaysCount = 7;
        public DatabaseLogger()
        {
            try
            {
                _connectionString = DatabaseHelperSQL.GetGlobalConnectionString();
                EnsureLogTableExists();
                CleanupOldLogs();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка получения строки подключения: " + ex.Message);
                _connectionString = null;
            }
        }

        private void EnsureLogTableExists()
        {
            string query = "IF OBJECT_ID('dbo.Logs', 'U') IS NULL " +
                           "CREATE TABLE dbo.Logs (Id INT IDENTITY(1,1) PRIMARY KEY, Timestamp DATETIME, Message NVARCHAR(MAX), StackTrace NVARCHAR(MAX), Context NVARCHAR(255))";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void CleanupOldLogs()
        {
            string query = $"DELETE FROM Logs WHERE Timestamp < DATEADD(DAY, -{logStoreDaysCount}, GETDATE())";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public async Task LogErrorAsync(Exception ex, string context = "")
        {
            await LogToDatabaseAsync(ex.Message, ex.StackTrace, context);
        }

        public async Task LogEventAsync(string eventMessage, string context = "")
        {
            await LogToDatabaseAsync(eventMessage, "", context);
        }
        public async Task LogWarningAsync(string eventMessage, string context = "")
        {
            await LogToDatabaseAsync(eventMessage, "", context);
        }

        private async Task LogToDatabaseAsync(string message, string stackTrace, string context)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                Console.WriteLine("Строка подключения не установлена. Лог сохраняется в файл.");
                await new FileLogger().LogErrorAsync(new Exception(message), context);
                return;
            }

            string query = "INSERT INTO Logs (Timestamp, Message, StackTrace, Context) VALUES (@Timestamp, @Message, @StackTrace, @Context)";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Timestamp", DateTime.UtcNow);
                        cmd.Parameters.AddWithValue("@Message", message);
                        cmd.Parameters.AddWithValue("@StackTrace", stackTrace);
                        cmd.Parameters.AddWithValue("@Context", context);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка записи в SQL: " + ex.Message);
                await new FileLogger().LogErrorAsync(ex, "Ошибка SQL логирования"); //throw;
            }
        }

        public Task LogErrorAsync(string v1, string v2)
        {
            throw new NotImplementedException();
        }
    }

    public class FileLogger : ILogger
    {
        private static readonly string logDirectory = "logs";
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        static FileLogger()
        {
            if (!Directory.Exists(logDirectory))
                Directory.CreateDirectory(logDirectory);
        }

        public Task LogErrorAsync(Exception ex, string context = "")
            => WriteLogAsync(new LogEntry
            {
                Timestamp = DateTime.UtcNow.ToString("o"),
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                Context = context
            });

        public Task LogEventAsync(string eventMessage, string context = "")
            => WriteLogAsync(new LogEntry
            {
                Timestamp = DateTime.UtcNow.ToString("o"),
                Message = eventMessage,
                StackTrace = "",
                Context = context
            });

        public Task LogWarningAsync(string eventMessage, string context = "")
            => WriteLogAsync(new LogEntry
            {
                Timestamp = DateTime.UtcNow.ToString("o"),
                Message = "WARNING!!!: " + eventMessage,
                StackTrace = "",
                Context = context
            });

        // Формат: NDJSON — одна запись в строке, O(1) append вместо O(n) read-rewrite.
        private static async Task WriteLogAsync(LogEntry logEntry)
        {
            string logFileName = Path.Combine(logDirectory, $"log_{DateTime.UtcNow:yyyy-MM-dd}.jsonl");
            string line = JsonConvert.SerializeObject(logEntry) + Environment.NewLine;

            await _semaphore.WaitAsync();
            try
            {
                await File.AppendAllTextAsync(logFileName, line);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при записи логов: " + ex.Message);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public Task LogErrorAsync(string v1, string v2)
        {
            throw new NotImplementedException();
        }
    }

    public class HybridLogger : ILogger
    {
        private readonly DatabaseLogger _databaseLogger;
        private readonly FileLogger _fileLogger;

        public HybridLogger()
        {
            _databaseLogger = new DatabaseLogger();
            _fileLogger = new FileLogger();
        }

        public async Task LogErrorAsync(Exception ex, string context = "")
        {
            try
            {
                await _databaseLogger.LogErrorAsync(ex, context);
            }
            catch
            {
                await _fileLogger.LogErrorAsync(ex, context);
            }
        }

        public Task LogErrorAsync(string v1, string v2)
        {
            throw new NotImplementedException();
        }

        public async Task LogEventAsync(string eventMessage, string context = "")
        {
            try
            {
                await _databaseLogger.LogEventAsync(eventMessage, context);
            }
            catch
            {
                await _fileLogger.LogEventAsync(eventMessage, context);
            }
        }

        public async Task LogWarningAsync(string eventMessage, string context = "")
        {
            try
            {
                await _databaseLogger.LogWarningAsync(eventMessage, context);
            }
            catch
            {
                await _fileLogger.LogWarningAsync(eventMessage, context);
            }
        }
    }

    public class LogEntry
    {
        public string Timestamp { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Context { get; set; }
    }
}
