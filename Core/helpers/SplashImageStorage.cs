#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SewingProduction.Core.interfaces;

namespace SewingProduction.Core.Helpers
{
    internal static class SplashImageStorage
    {
        public const string ShareRoot = @"\\dbfsv\ACE\SewingProduction\Assets\SplashImages";

        private static readonly string[] ImageExtensions = { ".png", ".jpg", ".jpeg", ".bmp", ".gif" };

        public static string GetCacheRoot()
        {
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "SewingProduction",
                "Assets",
                "SplashImages");
        }

        public static IEnumerable<string> GetAvailableSourceRoots()
        {
            if (Directory.Exists(ShareRoot))
            {
                yield return ShareRoot;
            }
        }

        public static bool CacheHasImages()
        {
            string cacheRoot = GetCacheRoot();
            return Directory.Exists(cacheRoot) && GetImageFiles(cacheRoot).Count > 0;
        }

        public static void EnsureCachedBeforeSplash(ILogger? logger = null)
        {
            RemoveLegacyRoamingCache();

            if (CacheHasImages())
            {
                return;
            }

            try
            {
                var syncTask = Task.Run(() => SyncFromSources(logger));
                if (!syncTask.Wait(TimeSpan.FromSeconds(8)))
                {
                    SafeLog(logger, "SplashImages sync timed out before splash");
                }
            }
            catch (Exception ex)
            {
                SafeLogError(logger, ex, "SplashImages pre-splash sync failed");
            }
        }

        public static void StartBackgroundSync(ILogger? logger = null)
        {
            _ = Task.Run(() =>
            {
                try
                {
                    RemoveLegacyRoamingCache();
                    SyncFromSources(logger);
                }
                catch (Exception ex)
                {
                    SafeLogError(logger, ex, "SplashImages background sync failed");
                }
            });
        }

        private static void RemoveLegacyRoamingCache()
        {
            string legacyRoot = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "SewingProduction",
                "SplashImages");

            if (!Directory.Exists(legacyRoot))
            {
                return;
            }

            try
            {
                Directory.Delete(legacyRoot, recursive: true);
            }
            catch
            {
                // ignore cleanup failures
            }
        }

        private static void SyncFromSources(ILogger? logger)
        {
            string destinationRoot = GetCacheRoot();
            Directory.CreateDirectory(destinationRoot);

            foreach (string sourceRoot in GetAvailableSourceRoots())
            {
                int copied = CopyTree(sourceRoot, destinationRoot);
                if (copied > 0)
                {
                    SafeLog(logger, $"SplashImages synced from '{sourceRoot}', files={copied}");
                    return;
                }
            }

            SafeLog(logger, $"SplashImages: нет доступного источника (шара='{ShareRoot}')");
        }

        private static int CopyTree(string sourceRoot, string destinationRoot)
        {
            int copied = 0;

            foreach (string sourceFile in Directory.GetFiles(sourceRoot, "*", SearchOption.AllDirectories)
                         .Where(IsImageFile))
            {
                string relativePath = Path.GetRelativePath(sourceRoot, sourceFile);
                string destinationFile = Path.Combine(destinationRoot, relativePath);
                string? destinationDir = Path.GetDirectoryName(destinationFile);
                if (!string.IsNullOrEmpty(destinationDir))
                {
                    Directory.CreateDirectory(destinationDir);
                }

                if (NeedToCopyFile(sourceFile, destinationFile))
                {
                    File.Copy(sourceFile, destinationFile, true);
                    copied++;
                }
            }

            return copied;
        }

        private static bool NeedToCopyFile(string sourceFile, string destinationFile)
        {
            if (!File.Exists(destinationFile))
            {
                return true;
            }

            var sourceInfo = new FileInfo(sourceFile);
            var destinationInfo = new FileInfo(destinationFile);

            if (sourceInfo.Length != destinationInfo.Length)
            {
                return true;
            }

            return sourceInfo.LastWriteTimeUtc > destinationInfo.LastWriteTimeUtc;
        }

        private static List<string> GetImageFiles(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                return new List<string>();
            }

            return Directory
                .GetFiles(folderPath, "*", SearchOption.AllDirectories)
                .Where(IsImageFile)
                .ToList();
        }

        private static bool IsImageFile(string path)
        {
            string extension = Path.GetExtension(path);
            return ImageExtensions.Any(ext => extension.Equals(ext, StringComparison.OrdinalIgnoreCase));
        }

        private static void SafeLog(ILogger? logger, string message)
        {
            Debug.WriteLine($"[SplashImages] {message}");
            if (logger == null)
            {
                return;
            }

            try
            {
                _ = logger.LogEventAsync(message, "SplashImages");
            }
            catch
            {
                // logging must not break startup
            }
        }

        private static void SafeLogError(ILogger? logger, Exception ex, string message)
        {
            Debug.WriteLine($"[SplashImages] {message}: {ex.Message}");
            if (logger == null)
            {
                return;
            }

            try
            {
                _ = logger.LogErrorAsync(ex, message);
            }
            catch
            {
                // logging must not break startup
            }
        }
    }
}
