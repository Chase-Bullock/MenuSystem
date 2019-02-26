using System;
using System.Collections.Generic;

namespace PizzaButt.NewModels
{
    public partial class Role
    {
        public long Id { get; set; }
        public string RoleName { get; set; }
        public bool? Active { get; set; }
        public long CreateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public long UpdateBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public long? DeleteBy { get; set; }
        public DateTime? DeleteTime { get; set; }
    }
}
