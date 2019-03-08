using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PizzaButt.NewModels;

namespace PizzaButt.ViewModels
{
    public class HomePageViewModel
    {

        public IEnumerable<MenuItem> MenuItems { get; set; }
        public IEnumerable<ToppingsViewModel> TacoToppings { get; set; }
        public IEnumerable<ToppingsViewModel> PizzaToppings { get; set; }
        public IEnumerable<ToppingsViewModel> AllToppings { get; set; }
        public IEnumerable<ScheduleConfigViewModel> TodaysSchedule { get; set; }
        //public IEnumerable<Size> Sizes { get; set; }

        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public Order Order { get; set; }
        
        [Required]
        public string ItemName { get; set; }

        [Required]
        public long SizeId { get; set; }
        
        public List<long> Toppings { get; set; }

        public bool UserLoggedIn { get; set; }
        

        [Range(1, 100)]
        [Required]
        public int Quantity { get; set; }
        
        public string Status { get; set; }
        
        public DateTime CreateTime { get; set; }
        
        public DateTime CompleteTime { get; set; }
        
        public OrderItem OrderItem { get; set; }
    }
}
