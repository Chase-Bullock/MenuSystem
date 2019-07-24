using System;
using System.Collections.Generic;

namespace CathedralKitchen.NewModels
{
    public partial class OrderOrderItem
    {
        public long Id { get; set; }
        public long OrderItemId { get; set; }
        public long OrderId { get; set; }
        public bool? Active { get; set; }
        public DateTime CreateTime { get; set; }
        public long CreateBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public long UpdateBy { get; set; }
        public DateTime? DeleteTime { get; set; }
        public long? DeleteBy { get; set; }

        public virtual Order Order { get; set; }
        public virtual OrderItem OrderItem { get; set; }
    }
}
