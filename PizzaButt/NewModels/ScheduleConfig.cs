using System;
using System.Collections.Generic;

namespace CathedralKitchen.NewModels
{
    public partial class ScheduleConfig
    {
        public ScheduleConfig()
        {
            InverseParent = new HashSet<ScheduleConfig>();
        }

        public long Id { get; set; }
        public long? CommunityId { get; set; }
        public DateTime Date { get; set; }
        public bool? Active { get; set; }
        public DateTime CreateTime { get; set; }
        public long CreateBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public long UpdateBy { get; set; }
        public DateTime? DeleteTime { get; set; }
        public long? DeleteBy { get; set; }
        public long? ParentId { get; set; }

        public virtual Community Community { get; set; }
        public virtual ScheduleConfig Parent { get; set; }
        public virtual ICollection<ScheduleConfig> InverseParent { get; set; }
    }
}
