using System;
using System.Collections.Generic;

namespace PizzaButt.NewModels
{
    public partial class SystemReference
    {
        public SystemReference()
        {
            Topping = new HashSet<Topping>();
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

        public virtual ICollection<Topping> Topping { get; set; }
    }
}
