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
        private const string EmptySourceLabel = "Источник РТ: -";
        private const string MissingImageText = "Изображение не найдено";
        public static BaseNodePreviewPanel Create(Panel root, Label sourceLabel, Label imageStatusLabel, PictureBox imageBox)
        {
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
                ? EmptySourceLabel
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
                preview.ImageStatusLabel.Text = MissingImageText;
            }
        }
        public static void Update(BaseNodePreviewPanel preview, BaseNodeSaveDefaults defaults)
        {
            if (preview == null)
                return;
            preview.SourceLabel.Text = string.IsNullOrWhiteSpace(defaults?.SourceRtCode)
                ? EmptySourceLabel
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
                preview.ImageStatusLabel.Text = MissingImageText;
            }
        }
    }
}
