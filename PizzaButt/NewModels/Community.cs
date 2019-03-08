using System;
using System.Collections.Generic;

namespace PizzaButt.NewModels
{
    public partial class Community
    {
        public Community()
        {
            Order = new HashSet<Order>();
            ScheduleConfig = new HashSet<ScheduleConfig>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long? RegionId { get; set; }
        public bool? Active { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        public long? DeleteBy { get; set; }
        public DateTime? DeleteTime { get; set; }

        public virtual Region Region { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<ScheduleConfig> ScheduleConfig { get; set; }
    }
}
