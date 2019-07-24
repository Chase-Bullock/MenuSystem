using System;
using System.Collections.Generic;

namespace CathedralKitchen.NewModels
{
    public partial class OrderCode
    {
        public long Id { get; set; }
        public string Password { get; set; }
        public bool? Active { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        public long? DeleteBy { get; set; }
        public DateTime? DeleteTime { get; set; }
    }
}
