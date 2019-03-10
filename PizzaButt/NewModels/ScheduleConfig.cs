using System;
using System.Collections.Generic;

namespace CathedralKitchen.NewModels
{
    public class ScheduleConfig
    {
        public long Id { get; set; }
        public long? CommunityId { get; set; }
        public DateTime Date { get; set; }
        public string DateString { get; set; }
        public long? BuilderId { get; set; }
        public long? RegionId { get; set; }
        public bool? Active { get; set; }
        public DateTime CreateTime { get; set; }
        public long CreateBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public long UpdateBy { get; set; }
        public DateTime? DeleteTime { get; set; }
        public long? DeleteBy { get; set; }
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public bool AllDay { get; set; }

        public virtual Builder Builder { get; set; }
        public virtual Community Community { get; set; }
        public virtual Region Region { get; set; }
    }
}
