using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaButt.Models
{
    public class OrderModel
    {
        public string Name { get; set; }
        public string Order { get; set; }
        public int Quantity { get; set; }
        public string SpecialInstructions { get; set; }
    }
}
