using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using CathedralKitchen.NewModels;
using CathedralKitchen.Helpers;

namespace CathedralKitchen.ViewModels
{
    public class CartViewModel
    {
        public long Id { get; set; }

        public List<ScheduleConfigViewModel> TodaysSchedule { get; set; }

        public bool IsEmployee { get; set; }

        public bool EmailConsent { get; set; }

        [StringLength(30)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(45)]
        [Required]
        public string LastName { get; set; }

        [StringLength(90)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public Order Order { get; set; }

        public List<CommunityViewModel> Communities { get; set; }
        public List<String> ScheduledCommunities { get; set; }

        public long CommunityId { get; set; }

        [StringLength(255)]
        [RequiredIf("IsEmployee", false)]
        public string AddressLine1 { get; set; }

        [StringLength(255)]
        public string AddressLine2 { get; set; }

        [StringLength(255)]
        [RequiredIf("IsEmployee", false)]
        public string City { get; set; }

        [StringLength(12)]
        [RequiredIf("IsEmployee", false)]
        public string Zipcode { get; set; }



        [StringLength(500)]
        public string Note { get; set; }
       
    }
}
