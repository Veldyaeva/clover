using System;
using System.IO;
using System.Xml;
using DevExpress.XtraEditors;
using SewingProduction.Interfaces;

namespace SewingProduction.Helpers
{
    public class SplitContainerHelper
    {
        private readonly FileLogger _logger = new FileLogger();

        public void SaveSplitContainerSettings(SplitContainerControl splitContainer, string fileName)
        {
            try
            {
                string appPath = System.Windows.Forms.Application.StartupPath;
                string settingsPath = Path.Combine(appPath, "Settings");

                // Create directory if it doesn't exist
                if (!Directory.Exists(settingsPath))
                    Directory.CreateDirectory(settingsPath);

                string fullPath = Path.Combine(settingsPath, fileName);

                // Create XML document with splitter position
                using (XmlWriter writer = XmlWriter.Create(fullPath))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("SplitContainerLayout");
                    
                    writer.WriteStartElement("Splitter");
                    writer.WriteAttributeString("Position", splitContainer.SplitterPosition.ToString());
                    writer.WriteEndElement(); // Splitter
                    
                    writer.WriteEndElement(); // SplitContainerLayout
                    writer.WriteEndDocument();
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Error saving split container settings");
            }
        }

        public void LoadSplitContainerSettings(SplitContainerControl splitContainer, string fileName)
        {
            try
            {
                string appPath = System.Windows.Forms.Application.StartupPath;
                string settingsPath = Path.Combine(appPath, "Settings");
                string fullPath = Path.Combine(settingsPath, fileName);

                if (!File.Exists(fullPath))
                    return;

                using (XmlReader reader = XmlReader.Create(fullPath))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Splitter")
                        {
                            string positionStr = reader.GetAttribute("Position");
                            if (int.TryParse(positionStr, out int position))
                            {
                                splitContainer.SplitterPosition = position;
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Error loading split container settings");
            }
        }
    }
} 