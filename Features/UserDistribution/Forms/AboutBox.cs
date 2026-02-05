using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Windows.Forms;

namespace SewingProduction.Features.UserDistribution.Forms
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            this.Text = String.Format("О программе {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Версия {0}", GetDisplayVersion());
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            this.textBoxDescription.Text = AssemblyDescription;
        }

        #region Методы доступа к атрибутам сборки

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private static string GetDisplayVersion()
        {
            return GetClickOnceProfileVersion() ?? Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private static string GetClickOnceProfileVersion()
        {
            string profileFileName = Environment.Is64BitProcess ? "anyCPU.pubxml.user" : "ver.x32.pubxml.user";
            string fallbackFileName = Environment.Is64BitProcess ? "anyCPU.pubxml" : "ver.x32.pubxml";

            string profilePath = FindPublishProfilePath(profileFileName, fallbackFileName);
            if (string.IsNullOrWhiteSpace(profilePath) || !File.Exists(profilePath))
            {
                return null;
            }

            try
            {
                var document = XDocument.Load(profilePath);
                string revision = document.Descendants("ApplicationRevision").FirstOrDefault()?.Value;
                string versionTemplate = document.Descendants("ApplicationVersion").FirstOrDefault()?.Value;

                if (string.IsNullOrWhiteSpace(versionTemplate) && string.IsNullOrWhiteSpace(revision))
                {
                    return null;
                }

                if (!string.IsNullOrWhiteSpace(versionTemplate))
                {
                    if (versionTemplate.Contains("*"))
                    {
                        return versionTemplate.Replace("*", string.IsNullOrWhiteSpace(revision) ? "0" : revision);
                    }

                    if (!string.IsNullOrWhiteSpace(revision))
                    {
                        var parts = versionTemplate.Split('.');
                        if (parts.Length == 3)
                        {
                            return $"{versionTemplate}.{revision}";
                        }
                    }

                    return versionTemplate;
                }

                var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
                if (assemblyVersion == null)
                {
                    return null;
                }

                return $"{assemblyVersion.Major}.{assemblyVersion.Minor}.{assemblyVersion.Build}.{revision}";
            }
            catch
            {
                return null;
            }
        }

        private static string FindPublishProfilePath(string primaryFileName, string fallbackFileName)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var directory = new DirectoryInfo(baseDir);

            for (int i = 0; i < 6 && directory != null; i++)
            {
                string profilesDir = Path.Combine(directory.FullName, "Properties", "PublishProfiles");
                if (Directory.Exists(profilesDir))
                {
                    string primaryPath = Path.Combine(profilesDir, primaryFileName);
                    if (File.Exists(primaryPath))
                    {
                        return primaryPath;
                    }

                    string fallbackPath = Path.Combine(profilesDir, fallbackFileName);
                    if (File.Exists(fallbackPath))
                    {
                        return fallbackPath;
                    }
                }

                directory = directory.Parent;
            }

            return null;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
