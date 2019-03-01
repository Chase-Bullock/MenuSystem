using PizzaButt.Models;
using PizzaButt.NewModels;
using System;
using System.Collections.Generic;

namespace PizzaButt.ViewModels
{
    public partial class OrderItemViewModel
    {
        public List<ToppingsViewModel> Toppings { get; set; }
        public long? SizeId { get; set; }
        public SystemReference Size { get; set; }
        public int Quantity { get; set; }
        public List<OrderItemTopping> OrderItemTopping { get; set; }

        public virtual MenuItemViewModel MenuItem { get; set; }
    }
}
