using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using CathedralKitchen.NewModels;
using System.ComponentModel;

namespace CathedralKitchen.ViewModels
{
    public class CustomerOrderViewModel
    {

        public IEnumerable<MenuItem> MenuItems { get; set; }
        public IEnumerable<ToppingsViewModel> TacoToppings { get; set; }
        public IEnumerable<ToppingsViewModel> PizzaToppings { get; set; }
        public IEnumerable<ToppingsViewModel> AllToppings { get; set; }
        public IEnumerable<ScheduleConfigViewModel> TodaysSchedule { get; set; }
        //public IEnumerable<Size> Sizes { get; set; }
        
        [Required]
        public string ItemName { get; set; }

        [Required]
        public long SizeId { get; set; }
        
        public List<long> Toppings { get; set; }

        public bool IsEmployee { get; set; }

        [Range(1, 100)]
        [Required]
        public int Quantity { get; set; }

        public OrderItem OrderItem { get; set; }
    }
}
