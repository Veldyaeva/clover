using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SewingProduction.Features.UserDistribution.Helpers;

namespace SewingProduction.Features.UserDistribution.Class
{
    public static class CurrentUser
    {
        public static UserClass User { get; private set; }

        public static void SetUser(UserClass user)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
        }

        public static void Clear()
        {
            User = null;
        }
    }
}
