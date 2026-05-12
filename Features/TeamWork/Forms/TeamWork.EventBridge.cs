using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SewingProduction.Models;
using System;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class TeamWork
    {
        private void ButtonEditWd_Click(object sender, EventArgs e)
        {
            EditWd_Internal2(ANNgridView, _bindingList, _bindingSource, Editing: false);
        }

        private void ButtonEditOnlyAdv_Click(object sender, EventArgs e)
        {
            EditWd_Internal2(ANNgridView, _bindingList, _bindingSource, Editing: true);
        }

        private async void ButtonArchAndCopyWd_Click(object sender, EventArgs e)
        {
            await ArchAndCopy(ANNgridView, _bindingList, _bindingSource, false);
        }

        private async void ButtonPreliminaryWd_Click(object sender, EventArgs e)
        {
            ButtonPreliminaryWd_Click_Internal(sender, e);
        }

        private async void ButtonCopyWd_Click(object sender, EventArgs e)
        {
            ButtonCopyWd_Click_Internal(sender, e);
        }

        private async void gridViewNZP_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            gridViewNZP_FocusedRowChanged_Internal(sender, e);
        }

        private void ApplyAnnGridKitSearchFilter(GridView gridView)
        {
            if (gridView == null || _isUpdatingAnnGridKitFilter)
            {
                return;
            }

            string searchText = StringNormalizer.TrimOrEmpty(gridView.FindFilterText);
            bool shouldUseKitFilter = toggleSwitchKit?.IsOn == true && !string.IsNullOrEmpty(searchText);

            string targetFilter = string.Empty;
            if (shouldUseKitFilter)
            {
                var parsedArticle = ParseKitArticle(searchText);
                if (parsedArticle.HasValidComponents)
                {
                    targetFilter = BuildKitArticleFilter(parsedArticle.Component1, parsedArticle.Component2);
                }
            }

            if (string.IsNullOrEmpty(targetFilter))
            {
                if (!_annGridKitFilterActive)
                {
                    return;
                }

                _isUpdatingAnnGridKitFilter = true;
                try
                {
                    _annGridKitFilterActive = false;
                    gridView.ActiveFilterString = string.Empty;
                }
                finally
                {
                    _isUpdatingAnnGridKitFilter = false;
                }

                return;
            }

            if (_annGridKitFilterActive && string.Equals(gridView.ActiveFilterString, targetFilter, StringComparison.Ordinal))
            {
                return;
            }

            _isUpdatingAnnGridKitFilter = true;
            try
            {
                _annGridKitFilterActive = true;
                gridView.ActiveFilterString = targetFilter;
            }
            finally
            {
                _isUpdatingAnnGridKitFilter = false;
            }
        }

        private static string BuildKitArticleFilter(string component1, string component2)
        {
            string escapedComponent1 = EscapeGridFilterValue(component1);
            string escapedComponent2 = EscapeGridFilterValue(component2);

            return $"([Articul] LIKE '%{escapedComponent1}%') OR ([Articul] LIKE '%{escapedComponent2}%')";
        }

        private static string EscapeGridFilterValue(string value)
        {
            return StringNormalizer.TrimOrEmpty(value).Replace("'", "''");
        }

        /// <summary>
        /// Обрабатывает смену строки в неувязанных артикулах
        /// </summary>
        private async void gridView_unboundArts_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (_isRestoringGridState)
            {
                return;
            }

            gridView_unboundArts_FocusedRowChanged_Internal(sender, e);
        }

        private void Filter_CheckedChanged(object sender, EventArgs e)
        {
            Filter_CheckedChanged_Internal(sender, e);
        }

        private void ANNgridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (_isRestoringGridState)
            {
                return;
            }
            try
            {
                if (e.FocusedRowHandle >= 0 && ANNgridView.GetRow(e.FocusedRowHandle) is ArtNormN row)
                {
                    _lastFocusedAnnId = row.AnnID;
                }
            }
            catch (Exception ex)
            {
                _ = _logger.LogErrorAsync(ex, "ANNgridView_FocusedRowChanged: не удалось сохранить _lastFocusedAnnId");
            }
            ANNgridView_FocusedRowChanged_Internal(sender, e);
        }

        /// <summary>
        /// Обрабатывает изменение фильтра поиска в ANNgridView с поддержкой комплектных артикулов
        /// </summary>
        private void ANNgridView_ActiveFilterChanged(object sender, EventArgs e)
        {
            try
            {
                var gridView = sender as GridView;
                if (gridView == null) return;

                if (!IsHandleCreated) return;
                BeginInvoke((MethodInvoker)(() =>
                {
                    ApplyAnnGridKitSearchFilter(gridView);
                    RefreshAnnGridSearchVisualState();
                }));
            }
            catch (Exception ex)
            {
                _logger?.LogErrorAsync(ex, "Ошибка в обработчике изменения фильтра поиска");
            }
        }

        /// <summary>
        /// Разбирает комплектный артикул на компоненты
        /// </summary>
        private (bool HasValidComponents, string Component1, string Component2) ParseKitArticle(string articleText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(articleText))
                    return (false, string.Empty, string.Empty);

                string cleanText = StringNormalizer.NormalizeUpperInvariant(articleText);

                var patterns = new[]
                {
                    @"^(\d+[А-ЯЁ]+\d+)([А-ЯЁ]+\d+)$",
                    @"^(\d+[A-Za-z]*[А-ЯЁ]+\d+)([А-ЯЁ]+\d+)$",
                    @"^(\d+[A-Za-zА-ЯЁ]+\d+)([А-ЯЁ]+\d+)$"
                };

                foreach (var pattern in patterns)
                {
                    var match = System.Text.RegularExpressions.Regex.Match(cleanText, pattern);
                    if (match.Success && match.Groups.Count == 3)
                    {
                        string component1 = match.Groups[1].Value;
                        string component2 = match.Groups[2].Value;

                        if (component1.Length >= 3 && component2.Length >= 2)
                        {
                            return (true, component1, component2);
                        }
                    }
                }

                for (int i = cleanText.Length - 1; i > 2; i--)
                {
                    char c = cleanText[i];
                    if (char.IsLetter(c) && char.IsUpper(c))
                    {
                        bool hasDigitsAfter = false;
                        for (int j = i + 1; j < cleanText.Length; j++)
                        {
                            if (char.IsDigit(cleanText[j]))
                            {
                                hasDigitsAfter = true;
                                break;
                            }
                        }

                        if (hasDigitsAfter)
                        {
                            string component1 = cleanText.Substring(0, i);
                            string component2 = cleanText.Substring(i);

                            if (component1.Length >= 3 && component2.Length >= 2)
                            {
                                return (true, component1, component2);
                            }
                        }
                    }
                }

                return (false, string.Empty, string.Empty);
            }
            catch (Exception ex)
            {
                _logger?.LogErrorAsync(ex, $"Ошибка при разборе комплектного артикула: {articleText}");
                return (false, string.Empty, string.Empty);
            }
        }

        private async void TeamWork_FormClosing(object sender, FormClosingEventArgs e)
        {
            CancelCurrentLoad();
            if (ANNgridView != null)
            {
                ANNgridView.FocusedRowChanged -= ANNgridView_FocusedRowChanged;
                ANNgridView.ColumnFilterChanged -= ANNgridView_ActiveFilterChanged;
            }

            _secondsUpdateManager?.CancelUpdate();
            _secondsUpdateManager?.Dispose();
            SaveGridSettings();

            await _logger.LogEventAsync("Форма TeamWork закрыта", "FormClosing");
            GC.Collect();
        }

        private async void customButton2_Click(object sender, EventArgs e)
        {
            await Arch(sender, e);
        }

        private async void BindButton_Click(object sender, EventArgs e)
        {
            await BindButton_Click_Internal(sender, e);
        }

        private async void ButtonDouble_Click(object sender, EventArgs e)
        {
            await DuplicateWorkDivision_Click_Internal(ANNgridView, _bindingList, _bindingSource, false);
        }

        private async void layoutControlGroup2_CustomButtonClick(object sender, BaseButtonEventArgs e)
        {
            var tag = (e.Button as DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton)?.Tag as string;

            switch (tag)
            {
                case "btnAdd":
                case "rt:add":
                    ButtonPreliminaryWd_Click_Internal(sender, e);
                    break;
                case "btnEdit":
                case "rt:duplicate":
                    await DuplicateWorkDivision_Click_Internal(gridView_wdToBind, _myDataAnnList, _myDataAnnBindingSource, forMyDataAnnView: true);
                    break;
                case "btnArch":
                case "rt:arch-and-copy":
                    await ArchAndCopy(gridView_wdToBind, _myDataAnnList, _myDataAnnBindingSource, true);
                    break;
            }
        }

        private void ButtonUnboundWd_Click(object sender, EventArgs e)
        {
            UnbindWD(sender, e);
        }

        private async void ANNgridView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Upd")
            {
                GridView view = sender as GridView;
                if (view != null)
                {
                    ArtNormN row = view.GetRow(e.RowHandle) as ArtNormN;
                    if (row != null)
                    {
                        if (e.Value is bool val && val)
                        {
                            row.dateUpdate = DateTime.Now;
                            _hasUnsavedChanges = true;

                            try
                            {
                                decimal updatedSeb = await _artNormService.getArtNormnSeb(row.AnnID);
                                row.Seb = updatedSeb;
                                row.dateUpdate = DateTime.Now;
                            }
                            catch (Exception ex)
                            {
                                await _logger.LogErrorAsync(ex, $"Ошибка при вызове getArtNormnSeb для AnnID: {row.AnnID}");
                                MessageBox.Show("Ошибка при обновлении данных после вызова процедуры: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            int handle = e.RowHandle;
                            if (view.IsValidRowHandle(handle))
                            {
                                view.RefreshRow(handle);
                            }
                        }
                    }
                }
            }
        }

        private async void layoutControlGroup8_CustomButtonClick(object sender, BaseButtonEventArgs e)
        {
            var tag = (e.Button as DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton)?.Tag as string;

            switch (tag)
            {
                case "wd:add-prelim":
                    if (ButtonPreliminaryWd.Enabled && ButtonPreliminaryWd.Visible)
                        ButtonPreliminaryWd_Click_Internal(sender, e);
                    break;
                case "wd:edit":
                    if (ButtonEditOnlyAdv.Enabled && ButtonEditOnlyAdv.Visible)
                        await EditWd_Internal2(ANNgridView, _bindingList, _bindingSource, Editing: true);
                    else if (ButtonEditWd.Enabled && ButtonEditWd.Visible)
                        await EditWd_Internal2(ANNgridView, _bindingList, _bindingSource, Editing: false);
                    break;
                case "wd:clone":
                    if (ButtonDouble.Enabled && ButtonDouble.Visible)
                        await DuplicateWorkDivision_Click_Internal(ANNgridView, _bindingList, _bindingSource);
                    break;
                case "wd:archive":
                    if (ButtonArchAndCopyWd.Enabled && ButtonArchAndCopyWd.Visible)
                        await SetArchiveStatus_Internal(sender, e);
                    break;
                case "wd:print":
                    if (PrintButton.Enabled && PrintButton.Visible)
                        PrintWorkDivisionScheme_Click(null, null);
                    break;
                case "wd:print-plus":
                    if (printButtonPlus.Enabled && printButtonPlus.Visible)
                        printButtonPlus_Click(null, null);
                    break;
                case "wd:edit-base-nodes":
                    OpenBaseNodeLibraryEditor();
                    break;
                case "wd:thread-norms":
                    OpenThreadNormsForm();
                    break;
            }
        }

        private void ANNgridView_CalcPreviewText(object sender, CalcPreviewTextEventArgs e)
        {
            if (e.RowHandle >= 0 && ANNgridView.GetRow(e.RowHandle) is ArtNormN row)
            {
                e.PreviewText = $"Дизайнер: {row.Diz}, Конструктор: {row.Constr}, Особенности: {row.Komment}, Рекомендации: {row.Reco}";
            }
        }

        private void layoutControlGroup6_CustomButtonClick(object sender, BaseButtonEventArgs e)
        {
            simpleButton2_Click_Internal(sender, e);
        }

        private async void layoutControlGroup14_CustomButtonClick(object sender, BaseButtonEventArgs e)
        {
            var tag = (e.Button as DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton)?.Tag as string;

            switch (tag)
            {
                case "bind:link":
                    await BindButton_Click_Internal(sender, e);
                    break;
                case "bind:unlink":
                    await UnbindWD(sender, e);
                    break;
                case "bind:touch-update-date":
                    await SetUpdateDate_Internal(sender, e);
                    break;
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопок в группе архива (layoutControlGroup19)
        /// </summary>
        private async void layoutControlGroup19_CustomButtonClick(object sender, BaseButtonEventArgs e)
        {
            var tag = (e.Button as DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton)?.Tag as string;

            switch (tag)
            {
                case "arch:restore":
                    await RestoreFromArchive_Internal(sender, e);
                    break;
            }
        }

        private void gridView_unboundArts_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            _gridHelper.popUpMenuCopy(sender, e);
        }

        private void ANNgridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            _gridHelper.popUpMenuCopy(sender, e);
        }

        private void gridView_binded_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            _gridHelper.popUpMenuCopy(sender, e);
        }

        private void gridViewRaskrTW_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            _gridHelper.popUpMenuCopy(sender, e);
        }

        private void gridViewRaszTW_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            _gridHelper.popUpMenuCopy(sender, e);
        }

        private async void customSimpleButtonArch_Click(object sender, EventArgs e)
        {
            await SetArchiveStatus_Internal(sender, e);
        }

        private async void customSimpleButtonArchArt_Click(object sender, EventArgs e)
        {
            await UpdateSpArticulArch_Internal(sender, e);
        }

        private async void customSimpleButtonUpd_Click(object sender, EventArgs e)
        {
            await SetUpdateDate_Internal(sender, e);
        }

        private async void customSimpleButton3_Click(object sender, EventArgs e)
        {
            await UpdateSpArticulArch_Internal(sender, e);
        }

        private async void customSimpleButton4_Click(object sender, EventArgs e)
        {
            await SetUpdateDate_Internal(sender, e);
        }

        private async void customSimpleButton5_Click(object sender, EventArgs e)
        {
            await UnbindArticulesFromWorkDivision_Internal(sender, e);
        }

        private async void customSimpleButtonDel_Click(object sender, EventArgs e)
        {
            await MarkWorkDivisionForDeletion_Internal(sender, e);
        }
    }
}
