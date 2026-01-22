using DevExpress.XtraEditors;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SewingProduction.Core.Class.Settings;

namespace SewingProduction.HelpAdmin.Forms
{
    public partial class AdminHelpEditorForm : XtraForm
    {
        private readonly FormManager _formManager;
        private readonly Timer _previewTimer;
        public string _filePathOverride;

        public AdminHelpEditorForm(FormManager formManager, string filePathOverride = null)
        {
            InitializeComponent();

            _formManager = formManager ?? throw new ArgumentNullException(nameof(formManager));
            _filePathOverride = filePathOverride;

            _previewTimer = new Timer { Interval = 400 };
            _previewTimer.Tick += (s, e) =>
            {
                _previewTimer.Stop();
                RenderPreview();
            };

            memoHtml.TextChanged += (s, e) =>
            {
                if (checkAutoPreview.Checked)
                {
                    _previewTimer.Stop();
                    _previewTimer.Start();
                }
            };
        }

        private void AdminHelpEditorForm_Load(object sender, EventArgs e)
        {
            UpdateHelpPathLabel();
            LoadOrCreateHelp();
        }

        private void buttonUseActiveForm_Click(object sender, EventArgs e)
        {
            _filePathOverride = null; // если хотите строго по активной форме
            UpdateHelpPathLabel();
            LoadOrCreateHelp();
        }

        private void buttonLoad_Click(object sender, EventArgs e) => LoadOrCreateHelp();

        private void buttonTemplate_Click(object sender, EventArgs e)
        {
            var helpPath = ResolveHelpPathLikeHelpForm_ProjectRoot();
            if (helpPath == null)
            {
                XtraMessageBox.Show("Не удалось определить путь инструкции.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var title = Path.GetFileNameWithoutExtension(helpPath);
            memoHtml.Text = BuildDefaultTemplateHtml(title);
            RenderPreview();
        }

        private void buttonSave_Click(object sender, EventArgs e) => SaveHelp();

        private void buttonOpenFolder_Click(object sender, EventArgs e)
        {
            var helpPath = ResolveHelpPathLikeHelpForm_ProjectRoot();
            if (helpPath == null) return;

            var dir = Path.GetDirectoryName(helpPath);
            if (dir == null) return;

            Directory.CreateDirectory(dir);
            Process.Start(new ProcessStartInfo("explorer.exe", dir) { UseShellExecute = true });
        }

        private void buttonRefreshPreview_Click(object sender, EventArgs e) => RenderPreview();

        private void UpdateHelpPathLabel()
        {
            labelHelpPath.Text = ResolveHelpPathLikeHelpForm_ProjectRoot() ?? "(не удалось определить путь)";
        }

        /// <summary>
        /// ВАЖНО: как HelpForm, но root = корень проекта (перед bin), а не AppContext.BaseDirectory
        /// </summary>
        private string ResolveHelpPathLikeHelpForm_ProjectRoot()
        {
            // 1) если явно передали путь
            if (!string.IsNullOrWhiteSpace(_filePathOverride))
                return _filePathOverride;

            // 2) иначе берём активную форму, как HelpForm :contentReference[oaicite:2]{index=2}
            var activeForm = _formManager.GetActiveForm(); // :contentReference[oaicite:3]{index=3}
            if (activeForm == null)
                return null;

            var type = activeForm.GetType();
            var ns = type.Namespace ?? "";
            var className = type.Name;

            // Убираем только "SewingProduction." в начале, оставляем Features (как у вас) :contentReference[oaicite:4]{index=4}
            string relativeNamespace = ns.StartsWith("SewingProduction.", StringComparison.OrdinalIgnoreCase)
                ? ns.Substring("SewingProduction.".Length)
                : ns;

            string relativePath = Path.Combine(
                relativeNamespace.Replace('.', Path.DirectorySeparatorChar),
                className + ".html"
            );

            // !!! вот тут отличие: projectRoot не BaseDirectory, а корень проекта
            string projectRoot = GetProjectRootFromBaseDirectory();

            return Path.Combine(projectRoot, "Help", relativePath);
        }

        /// <summary>
        /// AppContext.BaseDirectory обычно: ...\bin\Debug\net8.0-windows7.0\
        /// Поднимаемся вверх, пока не найдём папку "bin", и берём её Parent.
        /// </summary>
        public static string GetProjectRootFromBaseDirectory()
        {
            var dir = new DirectoryInfo(AppContext.BaseDirectory);

            while (dir != null)
            {
                if (dir.Name.Equals("bin", StringComparison.OrdinalIgnoreCase))
                {
                    // parent of bin => D:\SewingProduction
                    return dir.Parent?.FullName ?? AppContext.BaseDirectory;
                }
                dir = dir.Parent;
            }

            // fallback (если вдруг приложение запущено не из bin)
            return AppContext.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar);
        }

        private void LoadOrCreateHelp()
        {
            var helpPath = ResolveHelpPathLikeHelpForm_ProjectRoot();
            if (helpPath == null)
            {
                XtraMessageBox.Show("Активная форма не найдена. Откройте нужную форму и нажмите 'По активной форме'.",
                    "Не найдено", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(helpPath)!);

                if (File.Exists(helpPath))
                    memoHtml.Text = File.ReadAllText(helpPath, Encoding.UTF8);
                else
                    memoHtml.Text = BuildDefaultTemplateHtml(Path.GetFileNameWithoutExtension(helpPath));

                labelStatus.Text = "Загружено";
                RenderPreview();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Ошибка загрузки/создания:\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveHelp()
        {
            var helpPath = ResolveHelpPathLikeHelpForm_ProjectRoot();
            if (helpPath == null) return;

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(helpPath)!);

                var utf8NoBom = new UTF8Encoding(false);
                File.WriteAllText(helpPath, memoHtml.Text ?? "", utf8NoBom);

                labelStatus.Text = $"Сохранено: {DateTime.Now:dd.MM.yyyy HH:mm:ss}";
                RenderPreview();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Ошибка сохранения:\n{ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RenderPreview()
        {
            try
            {
                var helpPath = ResolveHelpPathLikeHelpForm_ProjectRoot();
                if (helpPath == null)
                {
                    webBrowserPreview.DocumentText = memoHtml.Text ?? "";
                    return;
                }

                var dir = Path.GetDirectoryName(helpPath)!;
                Directory.CreateDirectory(dir);

                // Чтобы относительные img/... работали: preview лежит рядом с реальным help
                var previewPath = Path.Combine(dir, Path.GetFileNameWithoutExtension(helpPath) + ".preview.html");

                var utf8NoBom = new UTF8Encoding(false);
                File.WriteAllText(previewPath, memoHtml.Text ?? "", utf8NoBom);

                webBrowserPreview.Navigate(previewPath);
            }
            catch
            {
                webBrowserPreview.DocumentText = memoHtml.Text ?? "";
            }
        }

        private static string BuildDefaultTemplateHtml(string title)
        {
            return
$@"<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"">
    <title>{EscapeHtml(title)}</title>
    <style>
        body {{
            font-family: Arial;
            font-size: 12pt;
            padding: 20px;
        }}
        h1 {{
            color: #336699;
        }}
        p {{
            margin: 10px 0;
        }}
    </style>
</head>
<body>
    <h1>{EscapeHtml(title)}</h1>
    <p>Описание формы (для чего нужна, где используется).</p>

    <img src=""img/{EscapeHtml(title)}.png"" alt=""{EscapeHtml(title)}"" width=""800"">
</body>
</html>";
        }

        private static string EscapeHtml(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            return s.Replace("&", "&amp;")
                    .Replace("<", "&lt;")
                    .Replace(">", "&gt;")
                    .Replace("\"", "&quot;");
        }
    }
}
