using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaButt.ViewModels
{
    public class ScheduleConfigViewModel
    {            
        public long Id { get; set; }
        public long? CommunityId { get; set; }
        public DateTime Date { get; set; }
        public long? BuilderId { get; set; }
        public long? RegionId { get; set; }
        public bool? Active { get; set; }

        public virtual BuilderViewModel Builder { get; set; }
        public virtual CommunityViewModel Community { get; set; }
        //public virtual Region Region { get; set; }
    }
}
