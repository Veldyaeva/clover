using System;

namespace SewingProduction.Features.TeamWork.Services
{
    /// <summary>
    /// Адаптер для работы с UI формой TeamWork_AdvanceTW (вызовы должны выполняться в UI-потоке).
    /// Не содержит бизнес-логики, только координация обновлений UI.
    /// </summary>
    public class TeamWorkUIService : ITeamWorkUIService
    {
        private readonly TeamWork_AdvanceTW _form;

        public TeamWorkUIService(TeamWork_AdvanceTW form)
        {
            _form = form;
        }

        public void RefreshAllGrids()
        {
            _form?.Invoke(new Action(() =>
            {
                _form.RefreshAllGridsForService();
            }));
        }

        public void HighlightChangedControls() { }

        public void UpdateFormTitle(string title)
        {
            if (_form != null && !_form.IsDisposed)
            {
                _form.Text = title;
            }
        }

        public void ShowStatus(string message, int delayMs = 3000)
        {
            if (_form == null || _form.IsDisposed) return;
            _ = _form.ShowStatusForService(message, delayMs);
        }

        public void HighlightControl(System.Windows.Forms.Control control, bool on)
        {
            if (_form == null || _form.IsDisposed || control == null) return;
            _form.Invoke(new Action(() =>
            {
                if (on) _form.HighlightControlForService(control);
                else _form.UnhighlightControlForService(control);
            }));
        }

        public void RestoreFocusRow(DevExpress.XtraGrid.Views.Grid.GridView view, int rowHandle)
        {
            if (_form == null || _form.IsDisposed || view == null) return;
            _form.Invoke(new Action(() =>
            {
                try
                {
                    if (view.IsValidRowHandle(rowHandle))
                    {
                        view.FocusedRowHandle = rowHandle;
                        view.MakeRowVisible(rowHandle);
                    }
                }
                catch { }
            }));
        }
    }
}


