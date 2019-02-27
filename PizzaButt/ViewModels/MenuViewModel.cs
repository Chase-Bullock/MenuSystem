using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PizzaButt.NewModels;

namespace PizzaButt.ViewModels
{
    public class MenuViewModel
    {
        public List<string> MenuItemNames { get; set; }
        
        public IEnumerable<MenuItem> MenuItems { get; set; }
    }
}
