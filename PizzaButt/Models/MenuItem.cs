using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace PizzaButt.Models
{
    public class MenuItem
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("ItemId")]
        public int ItemId { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
    }
}