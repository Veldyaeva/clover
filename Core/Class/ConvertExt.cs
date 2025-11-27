using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SewingProduction.Core.Class
{
    public static class ConvertExt
    {
        public static int? ToIntN(this object value)
        {
            if (value is null || value is DBNull) return null;
            if (value is int i) return i;
            if (value is long l && l >= int.MinValue && l <= int.MaxValue) return (int)l;
            if (value is short s) return (int)s;
            if (value is byte b) return (int)b;
            if (value is string str && int.TryParse(str, out var v)) return v;

            // IConvertible — на случай decimal, double и т.п. (безопасно, без исключений для нечисловых строк)
            if (value is IConvertible conv)
            {
                try { return System.Convert.ToInt32(conv, System.Globalization.CultureInfo.InvariantCulture); }
                catch { /* игнорируем */ }
            }
            return null;
        }
    }

}
