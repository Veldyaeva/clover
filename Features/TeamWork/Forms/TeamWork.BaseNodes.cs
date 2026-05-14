using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors.ButtonsPanelControl;
using SewingProduction.Features.TeamWork.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class TeamWork
    {
        private const string EditBaseNodesButtonTag = "wd:edit-base-nodes";

        //private void InitializeMainBaseNodeActions()
        //{
        //    if (layoutControlGroup8?.CustomHeaderButtons == null)
        //        return;

        //    bool exists = layoutControlGroup8.CustomHeaderButtons
        //        .OfType<GroupBoxButton>()
        //        .Any(button => string.Equals(button.Tag as string, EditBaseNodesButtonTag, StringComparison.OrdinalIgnoreCase)
        //                    || string.Equals(button.Caption, "Редактировать узлы", StringComparison.OrdinalIgnoreCase));

        //    if (exists)
        //        return;

        //    layoutControlGroup8.CustomHeaderButtons.Add(new GroupBoxButton(
        //        "Редактировать узлы",
        //        true,
        //        new ButtonImageOptions(),
        //        ButtonStyle.PushButton,
        //        "Открыть редактор библиотеки базовых узлов",
        //        -1,
        //        true,
        //        null,
        //        true,
        //        false,
        //        true,
        //        EditBaseNodesButtonTag,
        //        -1));
        //}

        private void OpenBaseNodeLibraryEditor()
        {
            try
            {
                var service = new BaseNodeLibraryService(_dbHelper, _logger);
                using var form = new BaseNodeLibraryEditorForm(
                    User,
                    service,
                    openSourceArticle: OpenBaseNodeSourceArticle);
                form.ShowDialog(this);
            }
            catch (Exception ex)
            {
                _ = _logger.LogErrorAsync(ex, "Ошибка при открытии редактора базовых узлов");
                MessageBox.Show(
                    this,
                    $"Не удалось открыть редактор базовых узлов: {ex.Message}",
                    "Базовые узлы",
                    MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }

        private void OpenBaseNodeSourceArticle(int sourceAnnId)
        {
            if (sourceAnnId <= 0)
                return;

            OpenAdvanceFormNonModal(bufferId, (int)Mode.Edit, oldId: sourceAnnId);
        }
    }
}
