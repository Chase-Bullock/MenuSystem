using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PizzaButt.Models;

namespace PizzaButt.ViewModels
{
    public class CreateMenuItemViewModel
    {
        public string Active { get; set; }
        
        public string Name { get; set; }

        public string Success { get; set; }
    }
}
