using System;
using System.Collections.Generic;

namespace CathedralKitchen.NewModels
{
    public partial class OrderCode
    {
        public long Id { get; set; }
        public string Password { get; set; }
        public bool? Active { get; set; }
    }
}
