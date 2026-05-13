using System;
using System.Collections.Concurrent;
using System.IO;

namespace SewingProduction.Features.TeamWork.Services
{
    internal sealed class ThreadNormsSqlProvider
    {
        private readonly string _sqlRootPath;
        private readonly ConcurrentDictionary<string, string> _cache = new(StringComparer.OrdinalIgnoreCase);

        public ThreadNormsSqlProvider()
        {
            _sqlRootPath = ResolveSqlRootPath();
        }

        public string Load(string fileName)
        {
            return _cache.GetOrAdd(fileName, static (name, root) =>
            {
                var fullPath = Path.Combine(root, name);
                if (!File.Exists(fullPath))
                {
                    throw new FileNotFoundException($"SQL file not found: {fullPath}", fullPath);
                }

                return File.ReadAllText(fullPath);
            }, _sqlRootPath);
        }

        private static string ResolveSqlRootPath()
        {
            var outputPath = Path.Combine(AppContext.BaseDirectory, "Features", "TeamWork", "Sql", "ThreadNorms");
            if (Directory.Exists(outputPath))
            {
                return outputPath;
            }

            var projectPath = Path.GetFullPath(Path.Combine(
                AppContext.BaseDirectory,
                "..",
                "..",
                "..",
                "Features",
                "TeamWork",
                "Sql",
                "ThreadNorms"));

            if (Directory.Exists(projectPath))
            {
                return projectPath;
            }

            throw new DirectoryNotFoundException("ThreadNorms SQL directory was not found.");
        }
    }
}
