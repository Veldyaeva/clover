using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace SewingProduction.Helpers
{
    public class FormSettingsHelper
    {
        private readonly FileLogger _logger = new FileLogger();

        /// <summary>
        /// Сохраняет настройки формы: размер, положение и состояние окна
        /// </summary>
        /// <param name="form">Форма для сохранения настроек</param>
        /// <param name="fileName">Имя файла для сохранения</param>
        public void SaveFormSettings(Form form, string fileName)
        {
            try
            {
                string appPath = Application.StartupPath;
                string settingsPath = Path.Combine(appPath, "Settings");

                // Создаем папку, если не существует
                if (!Directory.Exists(settingsPath))
                    Directory.CreateDirectory(settingsPath);

                string fullPath = Path.Combine(settingsPath, fileName);

                // Создаем XML документ с настройками формы
                using (XmlWriter writer = XmlWriter.Create(fullPath))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("FormLayout");

                    // Сохраняем состояние окна
                    writer.WriteStartElement("WindowState");
                    writer.WriteAttributeString("State", form.WindowState.ToString());
                    writer.WriteEndElement();

                    // Сохраняем размер и положение (только если не максимизировано)
                    if (form.WindowState == FormWindowState.Normal)
                    {
                        writer.WriteStartElement("Bounds");
                        writer.WriteAttributeString("X", form.Location.X.ToString());
                        writer.WriteAttributeString("Y", form.Location.Y.ToString());
                        writer.WriteAttributeString("Width", form.Size.Width.ToString());
                        writer.WriteAttributeString("Height", form.Size.Height.ToString());
                        writer.WriteEndElement();
                    }
                    else if (form.RestoreBounds != default)
                    {
                        // Если окно максимизировано, сохраняем RestoreBounds
                        writer.WriteStartElement("Bounds");
                        writer.WriteAttributeString("X", form.RestoreBounds.X.ToString());
                        writer.WriteAttributeString("Y", form.RestoreBounds.Y.ToString());
                        writer.WriteAttributeString("Width", form.RestoreBounds.Width.ToString());
                        writer.WriteAttributeString("Height", form.RestoreBounds.Height.ToString());
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement(); // FormLayout
                    writer.WriteEndDocument();
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при сохранении настроек формы");
            }
        }

        /// <summary>
        /// Загружает настройки формы: размер, положение и состояние окна
        /// </summary>
        /// <param name="form">Форма для загрузки настроек</param>
        /// <param name="fileName">Имя файла с настройками</param>
        public void LoadFormSettings(Form form, string fileName)
        {
            try
            {
                string appPath = Application.StartupPath;
                string settingsPath = Path.Combine(appPath, "Settings");
                string fullPath = Path.Combine(settingsPath, fileName);

                if (!File.Exists(fullPath))
                    return;

                using (XmlReader reader = XmlReader.Create(fullPath))
                {
                    FormWindowState windowState = FormWindowState.Normal;
                    int x = -1, y = -1, width = -1, height = -1;

                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            if (reader.Name == "WindowState")
                            {
                                string stateStr = reader.GetAttribute("State");
                                if (Enum.TryParse<FormWindowState>(stateStr, out var state))
                                {
                                    windowState = state;
                                }
                            }
                            else if (reader.Name == "Bounds")
                            {
                                int.TryParse(reader.GetAttribute("X"), out x);
                                int.TryParse(reader.GetAttribute("Y"), out y);
                                int.TryParse(reader.GetAttribute("Width"), out width);
                                int.TryParse(reader.GetAttribute("Height"), out height);
                            }
                        }
                    }

                    // Применяем настройки к форме
                    if (width > 0 && height > 0)
                    {
                        // Проверяем, что координаты находятся в пределах экрана
                        if (x >= 0 && y >= 0 &&
                            x < Screen.PrimaryScreen.WorkingArea.Width &&
                            y < Screen.PrimaryScreen.WorkingArea.Height)
                        {
                            form.StartPosition = FormStartPosition.Manual;
                            form.Location = new System.Drawing.Point(x, y);
                        }

                        form.Size = new System.Drawing.Size(width, height);
                    }

                    // Устанавливаем состояние окна
                    form.WindowState = windowState;
                }
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex, "Ошибка при загрузке настроек формы");
            }
        }
    }
}