using System;
using System.Collections.Generic;

namespace CathedralKitchen.NewModels
{
    public partial class Region
    {
        public Region()
        {
            Community = new HashSet<Community>();
            ScheduleConfig = new HashSet<ScheduleConfig>();
        }

        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string RegionName { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime? DeleteTime { get; set; }
        public long? DeleteBy { get; set; }

        public virtual ICollection<Community> Community { get; set; }
        public virtual ICollection<ScheduleConfig> ScheduleConfig { get; set; }
    }
}
