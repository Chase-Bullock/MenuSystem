using System;
using System.Collections.Generic;

namespace CathedralKitchen.NewModels
{
    public partial class OrderItem
    {
        public OrderItem()
        {
            OrderItemTopping = new HashSet<OrderItemTopping>();
        }

        public long Id { get; set; }
        public long MenuItemId { get; set; }
        public long? OrderId { get; set; }
        public long? SizeId { get; set; }
        public int Quantity { get; set; }
        public DateTime UpdateTime { get; set; }
        public long UpdateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public long CreateBy { get; set; }
        public DateTime? DeleteTime { get; set; }
        public long? DeleteBy { get; set; }
        public bool? Active { get; set; }

        public virtual MenuItem MenuItem { get; set; }
        public virtual Order Order { get; set; }
        public virtual ICollection<OrderItemTopping> OrderItemTopping { get; set; }
    }
}
