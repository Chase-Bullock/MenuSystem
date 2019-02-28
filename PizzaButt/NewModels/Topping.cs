using System;
using System.Collections.Generic;

namespace PizzaButt.NewModels
{
    public partial class Topping
    {
        public Topping()
        {
            OrderItemTopping = new HashSet<OrderItemTopping>();
        }

        public long Id { get; set; }
        public string ToppingName { get; set; }
        public long? ToppingTypeId { get; set; }
        public bool? Active { get; set; }
        public DateTime CreateTime { get; set; }
        public long CreateBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public long UpdateBy { get; set; }
        public DateTime? DeleteTime { get; set; }
        public long? DeleteBy { get; set; }

        public virtual SystemReference ToppingType { get; set; }
        public virtual ICollection<OrderItemTopping> OrderItemTopping { get; set; }
    }
}
