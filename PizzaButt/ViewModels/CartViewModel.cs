using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using CathedralKitchen.NewModels;

namespace CathedralKitchen.ViewModels
{
    public class CartViewModel
    {
        public long Id { get; set; }

        public IEnumerable<ScheduleConfigViewModel> TodaysSchedule { get; set; }

        [Required]
        [StringLength(75)]
        public string Name { get; set; }

        public Order Order { get; set; }

        public HashSet<CommunityViewModel> Communities { get; set; }

        public long CommunityId { get; set; }

        [StringLength(255)]
        [Required]
        public string AddressLine1 { get; set; }
        [StringLength(255)]
        public string AddressLine2 { get; set; }
        [StringLength(255)]
        [Required]
        public string City { get; set; }
        [StringLength(12)]
        [Required]
        public string Zipcode { get; set; }



        [StringLength(500)]
        public string Note { get; set; }
       
    }
}
