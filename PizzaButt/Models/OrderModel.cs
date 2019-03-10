using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CathedralKitchen.Models
{
    public class OrderModel
    {
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
        
        [Range(1, 100)]
        [BsonElement("Size")]
        public string Size { get; set; }
        
        [BsonElement("Toppings")]
        public List<string> Toppings { get; set; }
        
        [BsonRequired]
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
