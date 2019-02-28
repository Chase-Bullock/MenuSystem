﻿using System;
using System.Collections.Generic;

namespace PizzaButt.NewModels
{
    public partial class Order
    {
        public Order()
        {
            OrderOrderItem = new HashSet<OrderOrderItem>();
        }

        public long Id { get; set; }
        public string Note { get; set; }
        public long OrderStatusId { get; set; }
        public DateTime? CompleteTime { get; set; }
        public string CustomerName { get; set; }
        public DateTime CreateTime { get; set; }
        public long CreateBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public long UpdateBy { get; set; }
        public DateTime? DeleteTime { get; set; }
        public long? DeleteBy { get; set; }
        public bool? Active { get; set; }

        public virtual OrderStatus OrderStatus { get; set; }
        public virtual ICollection<OrderOrderItem> OrderOrderItem { get; set; }
    }
}