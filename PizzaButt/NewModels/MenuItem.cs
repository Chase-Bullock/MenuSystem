using System;
using System.Collections.Generic;

namespace PizzaButt.NewModels
{
    public partial class MenuItem
    {
        public MenuItem()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public long CreateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public long UpdateBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public long? DeleteBy { get; set; }
        public long? DeleteTime { get; set; }

        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
