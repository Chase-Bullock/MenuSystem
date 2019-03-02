using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PizzaButt.Models;
using PizzaButt.NewModels;

namespace PizzaButt.ViewModels
{
    public class CreateToppingViewModel
    {
        public string Active { get; set; }
        
        public string Name { get; set; }

        public string Success { get; set; }

        public List<long> ToppingTypeIds { get; set; }

        public List<SystemReference> ToppingTypes { get; set; }
    }
}
