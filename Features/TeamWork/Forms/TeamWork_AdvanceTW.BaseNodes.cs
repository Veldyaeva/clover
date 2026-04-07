using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors.ButtonsPanelControl;
using SewingProduction.Features.TeamWork.Helpers;
using SewingProduction.Features.TeamWork.Models;
using SewingProduction.Features.TeamWork.Services;
using SewingProduction.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SewingProduction.Features.TeamWork.Forms
{
    public partial class TeamWork_AdvanceTW
    {
        private BaseNodeLibraryService _baseNodeLibraryService;

        private void InitializeBaseNodeActions()
        {
            _baseNodeLibraryService ??= new BaseNodeLibraryService(_dbHelper, _logger);

            if (layoutControlGroup10?.CustomHeaderButtons == null)
            {
                return;
            }

            EnsureNormRaszHeaderButton("op:add-operation", "Добавить операцию", null, "Добавить новую операцию в текущий RT");
            EnsureNormRaszHeaderButton("op:add-base-node", "Добавить базовый узел", null, "Вставить сохраненный базовый узел в операции NormRasz");
            EnsureNormRaszHeaderButton("op:save-base-node", "Сохранить как базовый узел", Properties.Resources.save_16x16, "Сохранить выбранные операции как базовый узел");
        }

        private void EnsureNormRaszHeaderButton(string tag, string caption, Image image, string hint)
        {
            bool exists = layoutControlGroup10.CustomHeaderButtons
                .OfType<GroupBoxButton>()
                .Any(button => string.Equals(button.Tag as string, tag, StringComparison.Ordinal));

            if (exists)
            {
                return;
            }

            var imageOptions = new ButtonImageOptions();
            if (image != null)
            {
                imageOptions.Image = image;
            }

            layoutControlGroup10.CustomHeaderButtons.Add(new GroupBoxButton(
                caption,
                true,
                imageOptions,
                ButtonStyle.PushButton,
                hint,
                -1,
                true,
                null,
                true,
                false,
                true,
                tag,
                -1));
        }

        private async void AddBaseNodeFromLibrary()
        {
            try
            {
                var nodes = (await _baseNodeLibraryService.GetAllAsync()).ToList();
                if (nodes.Count == 0)
                {
                    MessageBox.Show(this, "Библиотека базовых узлов пока пустая. Сначала сохраните узел из текущего RT.", "Базовые узлы", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var insertionPoints = BuildBaseNodeInsertionPoints();
                using var form = new BaseNodeInsertForm(User, nodes, insertionPoints, _lastFocusedRaszOperation?.N, _baseNodeLibraryService);
                if (form.ShowDialog(this) != DialogResult.OK || form.SelectedNode == null || form.SelectedInsertionPoint == null)
                {
                    return;
                }

                var operations = BaseNodeMapper.CreateOperations(form.SelectedNode, _selectedAnnId);
                if (operations.Count == 0)
                {
                    MessageBox.Show(this, "В выбранном базовом узле нет операций для вставки.", "Базовые узлы", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                InsertBaseNodeOperations(operations, form.SelectedInsertionPoint, form.SelectedNode.Name);
            }
            catch (Exception ex)
            {
                _ = _logger.LogErrorAsync(ex, "Ошибка при добавлении базового узла");
                MessageBox.Show(this, $"Не удалось добавить базовый узел: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void SaveSelectionAsBaseNode()
        {
            try
            {
                var operations = GetOperationsForBaseNodeSave();
                if (operations.Count == 0)
                {
                    MessageBox.Show(this, "Выберите операции в NormRasz или установите фокус на операции, которую нужно сохранить как базовый узел.", "Базовые узлы", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string defaultName = BuildDefaultBaseNodeName(operations);
                var defaults = BaseNodeMetadataSuggester.Suggest(_currentAnnData ?? CreatedAnn, operations);
                using var form = new BaseNodeSaveForm(User, operations, defaultName, defaults, _baseNodeLibraryService);
                if (form.ShowDialog(this) != DialogResult.OK || form.ResultNode == null)
                {
                    return;
                }

                var existing = (await _baseNodeLibraryService
                    .GetAllAsync())
                    .FirstOrDefault(node => string.Equals(node.Name, form.ResultNode.Name, StringComparison.CurrentCultureIgnoreCase));

                if (existing != null)
                {
                    var overwriteResult = MessageBox.Show(
                        this,
                        $"Базовый узел \"{existing.Name}\" уже существует. Перезаписать его?",
                        "Базовые узлы",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2);

                    if (overwriteResult != DialogResult.Yes)
                    {
                        return;
                    }

                    form.ResultNode.BaseNodeId = existing.BaseNodeId;
                    form.ResultNode.Id = existing.Id;
                    form.ResultNode.CreatedAtUtc = existing.CreatedAtUtc;
                }

                var savedNode = await _baseNodeLibraryService.SaveAsync(form.ResultNode);
                _ = _logger.LogEventAsync($"Сохранен базовый узел \"{savedNode.Name}\" ({savedNode.Operations.Count} операций)", "SaveSelectionAsBaseNode");
                _ = ShowStatusMessage($"Базовый узел \"{savedNode.Name}\" сохранен", 3000, Color.DarkGreen);
            }
            catch (Exception ex)
            {
                _ = _logger.LogErrorAsync(ex, "Ошибка при сохранении базового узла");
                MessageBox.Show(this, $"Не удалось сохранить базовый узел: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<NormRasz> GetOperationsForBaseNodeSave()
        {
            var selected = gridViewRasz.GetSelectedRows()
                .Distinct()
                .Where(gridViewRasz.IsValidRowHandle)
                .Select(handle => gridViewRasz.GetRow(handle) as NormRasz)
                .Where(row => row != null)
                .Distinct()
                .OrderBy(row => row.N)
                .ThenBy(row => row.N1)
                .ToList();

            if (selected.Count > 0)
            {
                return selected;
            }

            var focused = GetSelectedOperation();
            if (focused == null)
            {
                return new List<NormRasz>();
            }

            return _normRaszList?
                .Where(row => row.N == focused.N)
                .OrderBy(row => row.N)
                .ThenBy(row => row.N1)
                .ToList() ?? new List<NormRasz>();
        }

        private List<BaseNodeInsertionPoint> BuildBaseNodeInsertionPoints()
        {
            var points = new List<BaseNodeInsertionPoint>
            {
                new BaseNodeInsertionPoint
                {
                    Key = "start",
                    Label = "В начало списка"
                }
            };

            var chapters = (_normRaszList ?? new System.ComponentModel.BindingList<NormRasz>())
                .GroupBy(row => row.N)
                .OrderBy(group => group.Key)
                .Select(group =>
                {
                    var first = group.OrderBy(row => row.N1).FirstOrDefault();
                    string text = first?.Text ?? string.Empty;
                    if (text.Length > 60)
                    {
                        text = text.Substring(0, 60) + "...";
                    }

                    return new BaseNodeInsertionPoint
                    {
                        Key = $"after:{group.Key}",
                        AfterN = group.Key,
                        Label = string.IsNullOrWhiteSpace(text) ? $"После главы №{group.Key}" : $"После главы №{group.Key} - {text}"
                    };
                });

            points.AddRange(chapters);
            points.Add(new BaseNodeInsertionPoint
            {
                Key = "end",
                Label = "В конец списка",
                AppendToEnd = true
            });

            return points;
        }

        private void InsertBaseNodeOperations(List<NormRasz> nodeOperations, BaseNodeInsertionPoint insertionPoint, string nodeName)
        {
            if (nodeOperations == null || nodeOperations.Count == 0)
            {
                return;
            }

            int chapterCount = nodeOperations.Select(row => row.N).DefaultIfEmpty(0).Max();
            int insertAfterN = insertionPoint.AppendToEnd
                ? (_normRaszList?.Select(row => row.N).DefaultIfEmpty(0).Max() ?? 0)
                : insertionPoint.AfterN.GetValueOrDefault();

            gridViewRasz.BeginDataUpdate();
            try
            {
                ShiftOperationsForBaseNodeInsert(insertAfterN, chapterCount, insertionPoint.AppendToEnd);

                foreach (var operation in nodeOperations)
                {
                    operation.N = insertAfterN + operation.N;
                    _normRaszList.Add(operation);
                }

                OperationNumberingService.RecalculateAllOperationNumbers(_normRaszList);
                FinalizeRaszBatch(nodeOperations.FirstOrDefault(), false);
            }
            finally
            {
                try
                {
                    gridViewRasz.EndDataUpdate();
                }
                catch (Exception ex)
                {
                    LogSuppressedException("InsertBaseNodeOperations: EndDataUpdate", ex);
                }
            }

            _ = _logger.LogEventAsync($"Добавлен базовый узел \"{nodeName}\" ({nodeOperations.Count} операций)", "InsertBaseNodeOperations");
            _ = ShowStatusMessage($"Добавлен базовый узел \"{nodeName}\"", 3000, Color.DarkGreen);
        }

        private void ShiftOperationsForBaseNodeInsert(int insertAfterN, int chapterCount, bool appendToEnd)
        {
            if (appendToEnd || chapterCount <= 0 || _normRaszList == null)
            {
                return;
            }

            foreach (var operation in _normRaszList)
            {
                if (operation.N > insertAfterN)
                {
                    int oldN = operation.N;
                    operation.N += chapterCount;
                    if (!operation.IsNew && operation.N != oldN)
                    {
                        operation.IsModified = true;
                    }
                }
            }
        }

        private string BuildDefaultBaseNodeName(IReadOnlyList<NormRasz> operations)
        {
            if (operations == null || operations.Count == 0)
            {
                return "Новый базовый узел";
            }

            if (operations.Count == 1)
            {
                return $"Узел {operations[0].DisplayNumber}";
            }

            return $"Узел {operations.First().DisplayNumber}-{operations.Last().DisplayNumber}";
        }
    }
}
