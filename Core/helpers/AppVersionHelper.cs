using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace SewingProduction.Helpers
{
    public static class AppVersionHelper
    {
        public static string GetDisplayVersion()
        {
            return TryGetVersionFromActivationContext()
                ?? TryGetVersionFromNearbyManifestFiles()
                ?? TryGetVersionFromPublishProfiles()
                ?? typeof(AppVersionHelper).Assembly.GetName().Version?.ToString()
                ?? "1.0.0.0";
        }

        private static string TryGetVersionFromActivationContext()
        {
            try
            {
                var activationContext = typeof(AppDomain)
                    .GetProperty("ActivationContext")
                    ?.GetValue(AppDomain.CurrentDomain);

                if (activationContext == null)
                    return null;

                var activationContextType = activationContext.GetType();
                var deploymentManifestBytes = activationContextType
                    .GetProperty("DeploymentManifestBytes")
                    ?.GetValue(activationContext) as byte[];
                var applicationManifestBytes = activationContextType
                    .GetProperty("ApplicationManifestBytes")
                    ?.GetValue(activationContext) as byte[];
                var identity = activationContextType
                    .GetProperty("Identity")
                    ?.GetValue(activationContext);
                var identityFullName = identity?.GetType()
                    .GetProperty("FullName")
                    ?.GetValue(identity)
                    ?.ToString();

                return TryReadVersionFromManifestBytes(deploymentManifestBytes)
                    ?? TryReadVersionFromManifestBytes(applicationManifestBytes)
                    ?? TryParseVersionFromIdentity(identityFullName);
            }
            catch
            {
                return null;
            }
        }

        private static string TryReadVersionFromManifestBytes(byte[] manifestBytes)
        {
            if (manifestBytes == null || manifestBytes.Length == 0)
                return null;

            try
            {
                using var stream = new MemoryStream(manifestBytes);
                var document = XDocument.Load(stream);
                return TryReadVersionFromManifestDocument(document);
            }
            catch
            {
                return null;
            }
        }

        private static string TryGetVersionFromNearbyManifestFiles()
        {
            var directories = new[]
            {
                AppDomain.CurrentDomain.BaseDirectory,
                Path.GetDirectoryName(typeof(AppVersionHelper).Assembly.Location)
            }
            .Where(path => !string.IsNullOrWhiteSpace(path))
            .Distinct(StringComparer.OrdinalIgnoreCase);

            foreach (var rootDirectory in directories)
            {
                var current = new DirectoryInfo(rootDirectory);

                for (int depth = 0; depth < 3 && current != null; depth++)
                {
                    var deploymentVersion = TryReadVersionFromFirstMatchingFile(current.FullName, "*.application");
                    if (!string.IsNullOrWhiteSpace(deploymentVersion))
                        return deploymentVersion;

                    var appManifestVersion = TryReadVersionFromFirstMatchingFile(current.FullName, "*.dll.manifest")
                        ?? TryReadVersionFromFirstMatchingFile(current.FullName, "*.exe.manifest")
                        ?? TryReadVersionFromFirstMatchingFile(current.FullName, "*.manifest");

                    if (!string.IsNullOrWhiteSpace(appManifestVersion))
                        return appManifestVersion;

                    current = current.Parent;
                }
            }

            return null;
        }

        private static string TryReadVersionFromFirstMatchingFile(string directory, string searchPattern)
        {
            try
            {
                var manifestPath = Directory
                    .EnumerateFiles(directory, searchPattern, SearchOption.TopDirectoryOnly)
                    .OrderBy(path => path, StringComparer.OrdinalIgnoreCase)
                    .FirstOrDefault();

                return string.IsNullOrWhiteSpace(manifestPath)
                    ? null
                    : TryReadVersionFromManifestFile(manifestPath);
            }
            catch
            {
                return null;
            }
        }

        private static string TryReadVersionFromManifestFile(string manifestPath)
        {
            try
            {
                var document = XDocument.Load(manifestPath);
                return TryReadVersionFromManifestDocument(document);
            }
            catch
            {
                return null;
            }
        }

        private static string TryReadVersionFromManifestDocument(XDocument document)
        {
            return document
                .Descendants()
                .FirstOrDefault(node => node.Name.LocalName == "assemblyIdentity")
                ?.Attribute("version")
                ?.Value;
        }

        private static string TryGetVersionFromPublishProfiles()
        {
            string primaryFileName = Environment.Is64BitProcess ? "anyCPU.pubxml" : "ver.x32.pubxml";
            string fallbackFileName = Environment.Is64BitProcess ? "ver.x32.pubxml" : "anyCPU.pubxml";
            string profilePath = FindPublishProfilePath(primaryFileName, fallbackFileName);

            if (string.IsNullOrWhiteSpace(profilePath) || !File.Exists(profilePath))
                return null;

            try
            {
                var document = XDocument.Load(profilePath);
                string revision = document.Descendants("ApplicationRevision").FirstOrDefault()?.Value;
                string versionTemplate = document.Descendants("ApplicationVersion").FirstOrDefault()?.Value;

                if (string.IsNullOrWhiteSpace(versionTemplate) && string.IsNullOrWhiteSpace(revision))
                    return null;

                if (!string.IsNullOrWhiteSpace(versionTemplate))
                {
                    if (versionTemplate.Contains("*"))
                        return versionTemplate.Replace("*", string.IsNullOrWhiteSpace(revision) ? "0" : revision);

                    if (!string.IsNullOrWhiteSpace(revision))
                    {
                        var parts = versionTemplate.Split('.');
                        if (parts.Length == 3)
                            return $"{versionTemplate}.{revision}";
                    }

                    return versionTemplate;
                }

                return revision;
            }
            catch
            {
                return null;
            }
        }

        private static string FindPublishProfilePath(string primaryFileName, string fallbackFileName)
        {
            var directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            for (int depth = 0; depth < 6 && directory != null; depth++)
            {
                string profilesDir = Path.Combine(directory.FullName, "Properties", "PublishProfiles");
                if (Directory.Exists(profilesDir))
                {
                    string primaryPath = Path.Combine(profilesDir, primaryFileName);
                    if (File.Exists(primaryPath))
                        return primaryPath;

                    string fallbackPath = Path.Combine(profilesDir, fallbackFileName);
                    if (File.Exists(fallbackPath))
                        return fallbackPath;
                }

                directory = directory.Parent;
            }

            return null;
        }

        private static string TryParseVersionFromIdentity(string identityFullName)
        {
            if (string.IsNullOrWhiteSpace(identityFullName))
                return null;

            var match = Regex.Match(identityFullName, @"Version=(?<version>\d+(?:\.\d+)+)", RegexOptions.IgnoreCase);
            return match.Success ? match.Groups["version"].Value : null;
        }
    }
}
