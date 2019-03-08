using System;
using System.Collections.Generic;

namespace PizzaButt.NewModels
{
    public partial class ScheduleConfig
    {
        public long Id { get; set; }
        public long? CommunityId { get; set; }
        public DateTime Date { get; set; }
        public long? BuilderId { get; set; }
        public long? RegionId { get; set; }
        public bool? Active { get; set; }
        public DateTime CreateTime { get; set; }
        public long CreateBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public long UpdateBy { get; set; }
        public DateTime? DeleteTime { get; set; }
        public long? DeleteBy { get; set; }

        public virtual Builder Builder { get; set; }
        public virtual Community Community { get; set; }
        public virtual Region Region { get; set; }
    }
}
