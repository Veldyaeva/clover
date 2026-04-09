using SewingProduction.Features.TeamWork.Models;
using System.IO;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Helpers
{
    internal sealed class BaseNodePreviewPanel
    {
        public Panel Root { get; init; }
        public Label SourceLabel { get; init; }
        public Label ImageStatusLabel { get; init; }
        public PictureBox ImageBox { get; init; }
    }

    internal static class BaseNodePreviewHelper
    {
        public static BaseNodePreviewPanel AttachToForm(Form form, string title = "Источник РТ")
        {
            var root = new Panel
            {
                Dock = DockStyle.Right,
                Width = 220,
                Padding = new Padding(10, 12, 10, 12),
                BorderStyle = BorderStyle.FixedSingle
            };

            var titleLabel = new Label
            {
                Dock = DockStyle.Top,
                Height = 20,
                Text = title,
                Font = new System.Drawing.Font(form.Font, System.Drawing.FontStyle.Bold)
            };

            var sourceLabel = new Label
            {
                Dock = DockStyle.Top,
                Height = 36,
                Padding = new Padding(0, 6, 0, 6),
                Text = "РТ: -"
            };

            var imageStatusLabel = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 24,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Text = "Изображение не задано"
            };

            var imageBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            root.Controls.Add(imageBox);
            root.Controls.Add(imageStatusLabel);
            root.Controls.Add(sourceLabel);
            root.Controls.Add(titleLabel);

            form.Controls.Add(root);
            form.Controls.SetChildIndex(root, 0);

            return new BaseNodePreviewPanel
            {
                Root = root,
                SourceLabel = sourceLabel,
                ImageStatusLabel = imageStatusLabel,
                ImageBox = imageBox
            };
        }

        public static void Update(BaseNodePreviewPanel preview, BaseNodeDefinition node)
        {
            if (preview == null)
                return;

            string sourceRtCode = node?.SourceRtCode;
            string imagePath = node?.SourceImagePath;

            preview.SourceLabel.Text = string.IsNullOrWhiteSpace(sourceRtCode)
                ? "РТ: -"
                : $"РТ: {sourceRtCode}";

            if (!string.IsNullOrWhiteSpace(imagePath) && File.Exists(imagePath))
            {
                preview.ImageBox.ImageLocation = imagePath;
                preview.ImageStatusLabel.Text = Path.GetFileName(imagePath);
            }
            else
            {
                preview.ImageBox.ImageLocation = null;
                preview.ImageBox.Image = null;
                preview.ImageStatusLabel.Text = "Изображение не задано";
            }
        }

        public static void Update(BaseNodePreviewPanel preview, BaseNodeSaveDefaults defaults)
        {
            if (preview == null)
                return;

            preview.SourceLabel.Text = string.IsNullOrWhiteSpace(defaults?.SourceRtCode)
                ? "РТ: -"
                : $"РТ: {defaults.SourceRtCode}";

            if (!string.IsNullOrWhiteSpace(defaults?.SourceImagePath) && File.Exists(defaults.SourceImagePath))
            {
                preview.ImageBox.ImageLocation = defaults.SourceImagePath;
                preview.ImageStatusLabel.Text = Path.GetFileName(defaults.SourceImagePath);
            }
            else
            {
                preview.ImageBox.ImageLocation = null;
                preview.ImageBox.Image = null;
                preview.ImageStatusLabel.Text = "Изображение не задано";
            }
        }
    }
}
