using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraTab;
using SewingProduction.Features.UserDistribution.Helpers;

namespace SewingProduction.Core.Class
{
    public class CustomTabPage : XtraTabPage, IThemeableControl
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ObjectName { get; set; }

        public void ApplyPermission(UserClass user)
        {
            if (string.IsNullOrEmpty(ObjectName) && !string.IsNullOrEmpty(Name))
                ObjectName = Name;

            if (string.IsNullOrEmpty(ObjectName))
            {
                this.PageVisible = false;
                return;
            }

            bool hasWrite = user.HasPermission(ObjectName, "Редактор");
            bool hasRead = user.HasPermission(ObjectName, "Просмотр");

            // 🔥 Важное отличие от обычных контролов
            this.PageVisible = hasRead || hasWrite;

            // можно логировать при отладке:
            System.Diagnostics.Debug.WriteLine(
                $"[Доступ TabPage] {ObjectName}: Просмотр={hasRead}, Редактор={hasWrite}, PageVisible={this.PageVisible}"
            );
        }
    }

}
