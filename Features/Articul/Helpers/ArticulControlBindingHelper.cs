using System;
using System.Windows.Forms;
using SewingProduction.Features.Articul.Models;

namespace SewingProduction.Features.Articul.Helpers
{
    public static class ArticulControlBindingHelper
    {
        public static void SetDetails(BindingSource detailsSource, SpArticulPreviewModel details)
        {
            if (detailsSource == null) throw new ArgumentNullException(nameof(detailsSource));

            detailsSource.DataSource = details ?? new SpArticulPreviewModel();
            detailsSource.ResetBindings(false);
        }

        public static void ClearDetails(BindingSource detailsSource)
        {
            SetDetails(detailsSource, null);
        }
    }
}
