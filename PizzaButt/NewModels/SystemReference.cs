using System;
using System.Collections.Generic;

namespace CathedralKitchen.NewModels
{
    public partial class SystemReference
    {
        public SystemReference()
        {
            ToppingSystemReference = new HashSet<ToppingSystemReference>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string MainValue { get; set; }
        public string AltValue { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreateTime { get; set; }
        public long? CreateBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public long? UpdateBy { get; set; }
        public DateTime? DeleteTime { get; set; }
        public long? DeleteBy { get; set; }

        public virtual ICollection<ToppingSystemReference> ToppingSystemReference { get; set; }
    }
}
