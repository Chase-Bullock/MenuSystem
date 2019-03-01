using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PizzaButt.NewModels;

namespace PizzaButt.ViewModels
{
    public class OrderViewModel
    {
        public long Id { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }

        public string Name { get; set; }
        
        public string Size { get; set; }

        public int Quantity { get; set; }
        
        public string Note { get; set; }
        
        public string Status { get; set; }
        
        public DateTime CreateTime { get; set; }
        
        public DateTime CompleteTime { get; set; }
        
    }
}
