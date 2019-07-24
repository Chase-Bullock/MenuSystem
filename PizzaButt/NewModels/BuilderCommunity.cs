using System;
using System.Collections.Generic;

namespace CathedralKitchen.NewModels
{
    public partial class BuilderCommunity
    {
        public long Id { get; set; }
        public long BuilderId { get; set; }
        public long CommunityId { get; set; }
        public bool? Active { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
        public long? DeleteBy { get; set; }
        public long? DeleteTime { get; set; }
    }
}
