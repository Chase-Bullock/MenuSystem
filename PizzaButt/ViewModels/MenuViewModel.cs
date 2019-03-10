using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using CathedralKitchen.NewModels;

namespace CathedralKitchen.ViewModels
{
    public class MenuViewModel
    {
        public List<string> MenuItemNames { get; set; }
        
        public IEnumerable<MenuItem> MenuItems { get; set; }
    }
}
