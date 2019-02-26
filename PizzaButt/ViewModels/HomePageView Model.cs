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
        public IEnumerable<Topping> TacoToppings { get; set; }
        public IEnumerable<Topping> PizzaToppings { get; set; }
        //public IEnumerable<Size> Sizes { get; set; }

        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public Order Order { get; set; }
        
        public string ItemName { get; set; }

        [Required]
        public long Size { get; set; }
        
        public List<long> Toppings { get; set; }
        

        [Range(1, 100)]
        [Required]
        public int Quantity { get; set; }
        
        [StringLength(600)]
        public string SpecialInstructions { get; set; }
        
        public string Status { get; set; }
        
        public DateTime CreateTime { get; set; }
        
        public DateTime CompleteTime { get; set; }
        
        public OrderItem OrderItem { get; set; }
    }
}
