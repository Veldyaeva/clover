using System;
using System.Windows.Forms;

namespace SewingProduction.Features.Articul.Helpers
{
    public static class ArticulControlBindingHelper
    {
        public static void SetDetails<TDetails>(BindingSource detailsSource, TDetails details)
        {
            if (detailsSource == null) throw new ArgumentNullException(nameof(detailsSource));

            detailsSource.DataSource = details;
            detailsSource.ResetBindings(false);
        }

        public static void ClearDetails(BindingSource detailsSource)
        {
            if (detailsSource == null) throw new ArgumentNullException(nameof(detailsSource));

            detailsSource.DataSource = null;
            detailsSource.ResetBindings(false);
        }
    }
}
