using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using CathedralKitchen.NewModels;

namespace CathedralKitchen.ViewModels
{
    public class OrderViewModel
    {
        public long Id { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }

        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Zipcode { get; set; }

        public long CommunityId { get; set; }

        public CommunityViewModel Community { get; set; }
        
        public string Note { get; set; }
        
        public string Status { get; set; }
        
        public DateTime CreateTime { get; set; }
        
        public DateTime? CompleteTime { get; set; }
        
    }
}
