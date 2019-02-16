using System;
using System.Collections.Generic;

namespace PizzaButt.NewModels
{
    public partial class OrderItem
    {
        public OrderItem()
        {
            OrderItemTopping = new HashSet<OrderItemTopping>();
        }

        public long Id { get; set; }
        public long MenuItemId { get; set; }
        public int? Size { get; set; }
        public int Quantity { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime UpdateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CreateBy { get; set; }
        public DateTime? DeleteTime { get; set; }
        public DateTime? DeleteBy { get; set; }

        public virtual MenuItem MenuItem { get; set; }
        public virtual ICollection<OrderItemTopping> OrderItemTopping { get; set; }
    }
}
