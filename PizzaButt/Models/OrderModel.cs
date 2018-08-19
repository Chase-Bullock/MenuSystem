using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PizzaButt.Models
{
    public class OrderModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Order")]
        public string Order { get; set; }
        [BsonElement("Size")]
        public string Size { get; set; }
        [BsonElement("Toppings")]
        public List<string> Toppings { get; set; }
        [BsonElement("Quantity")]
        public int Quantity { get; set; }
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
