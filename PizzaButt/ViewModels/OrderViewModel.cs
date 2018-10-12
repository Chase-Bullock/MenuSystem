using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PizzaButt.Models;

namespace PizzaButt.ViewModels
{
    public class OrderViewModel
    {

        public IEnumerable<MenuItem> MenuItems { get; set; }

        [BsonId]
        public ObjectId Id { get; set; }
        
        [BsonRequired]
        [Required]
        [BsonElement("Name")]
        public string Name { get; set; }
        
        [BsonRequired]
        [Required]
        [BsonElement("Order")]
        public string Order { get; set; }
        

        [BsonElement("Size")]
        [Required]
        public string Size { get; set; }
        
        [BsonElement("Toppings")]
        public List<string> Toppings { get; set; }
        
        [BsonRequired]
        [Range(1, 100)]
        [Required]
        [BsonElement("Quantity")]
        public int Quantity { get; set; }
        
        [StringLength(355)]
        [BsonElement("SpecialInstructions")]
        public string SpecialInstructions { get; set; }
        
        [BsonElement("Status")]
        public string Status { get; set; }
        
        [BsonElement("CreateTime")]
        public DateTime CreateTime { get; set; }
        
        [BsonElement("CompleteTime")]
        public DateTime CompleteTime { get; set; }
        
    }
}
