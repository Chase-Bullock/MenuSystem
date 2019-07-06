using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CathedralKitchen.ExtendedModels
{
    public class SettingsModel
    {
        public string DbConnection { get; set; }
        public string Email { get; set; }
        public string SMTPPort { get; set; }
        public string WebApiBaseUrl { get; set; }
    }
}
