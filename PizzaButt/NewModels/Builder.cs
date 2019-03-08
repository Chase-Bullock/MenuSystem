using System;
using System.Collections.Generic;

namespace PizzaButt.NewModels
{
    public partial class Builder
    {
        public Builder()
        {
            ScheduleConfig = new HashSet<ScheduleConfig>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }

        public virtual ICollection<ScheduleConfig> ScheduleConfig { get; set; }
    }
}
