using System;
using System.Collections.Generic;

namespace PizzaButt.NewModels
{
    public partial class City
    {
        public City()
        {
            Order = new HashSet<Order>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        public long? DeleteBy { get; set; }
        public long? DeleteTime { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
