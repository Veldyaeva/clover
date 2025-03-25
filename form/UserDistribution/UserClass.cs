using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SewingProduction.Helpers;

namespace SewingProduction.form.UserDistribution
{
    internal class UserClass
    {
    }
    public class UserClassDataService
    {
        private readonly DatabaseHelper _dbHelper;
        public UserClassDataService(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
    }
}
