using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using CathedralKitchen.NewModels;

namespace CathedralKitchen.ViewModels
{
    public class SelectToppingViewModel
    {
        public string Active { get; set; }

        public string Name { get; set; }

        public long ToppingTypeId { get; set; }

        public string Success { get; set; }

        public List<string> ToppingNames { get; set; }

        public List<Topping> Toppings {get; set; }

        public List<SystemReference> ToppingTypes { get; set; }
    }
}
