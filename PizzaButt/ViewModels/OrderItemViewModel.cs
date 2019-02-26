using PizzaButt.Models;
using System;
using System.Collections.Generic;

namespace PizzaButt.ViewModels
{
    public partial class OrderItemViewModel
    {
        public List<ToppingsViewModel> Toppings { get; set; }
        public long? Size { get; set; }
        public int Quantity { get; set; }

        public virtual MenuItemViewModel MenuItem { get; set; }
    }
}
