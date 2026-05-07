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
        private const string EmptySourceLabel = "Источник: -";
        private const string MissingImageText = "Изображение не найдено";
        private const string EmptyImageText = "Изображение не задано";
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
            string sourceArticul = node?.SourceArticul;
            string sourceRtCode = node?.SourceRtCode;
            string imagePath = node?.SourceImagePath;
            preview.SourceLabel.Text = BuildSourceLabel(sourceArticul, sourceRtCode);
            if (!string.IsNullOrWhiteSpace(imagePath) && File.Exists(imagePath))
            {
                preview.ImageBox.ImageLocation = imagePath;
                preview.ImageStatusLabel.Text = Path.GetFileName(imagePath);
            }
            else
            {
                preview.ImageBox.ImageLocation = null;
                preview.ImageBox.Image = null;
                preview.ImageStatusLabel.Text = string.IsNullOrWhiteSpace(imagePath)
                    ? EmptyImageText
                    : $"{MissingImageText}: {Path.GetFileName(imagePath)}";
            }
        }
        public static void Update(BaseNodePreviewPanel preview, BaseNodeSaveDefaults defaults)
        {
            if (preview == null)
                return;
            preview.SourceLabel.Text = BuildSourceLabel(defaults?.SourceArticul, defaults?.SourceRtCode);
            if (!string.IsNullOrWhiteSpace(defaults?.SourceImagePath) && File.Exists(defaults.SourceImagePath))
            {
                preview.ImageBox.ImageLocation = defaults.SourceImagePath;
                preview.ImageStatusLabel.Text = Path.GetFileName(defaults.SourceImagePath);
            }
            else
            {
                preview.ImageBox.ImageLocation = null;
                preview.ImageBox.Image = null;
                preview.ImageStatusLabel.Text = string.IsNullOrWhiteSpace(defaults?.SourceImagePath)
                    ? EmptyImageText
                    : $"{MissingImageText}: {Path.GetFileName(defaults.SourceImagePath)}";
            }
        }

        private static string BuildSourceLabel(string sourceArticul, string sourceRtCode)
        {
            bool hasArticul = !string.IsNullOrWhiteSpace(sourceArticul);
            bool hasRtCode = !string.IsNullOrWhiteSpace(sourceRtCode);

            if (!hasArticul && !hasRtCode)
            {
                return EmptySourceLabel;
            }

            if (hasArticul && hasRtCode)
            {
                return $"Артикул: {sourceArticul} | РТ: {sourceRtCode}";
            }

            if (hasArticul)
            {
                return $"Артикул: {sourceArticul}";
            }

            return $"РТ: {sourceRtCode}";
        }
    }
}
