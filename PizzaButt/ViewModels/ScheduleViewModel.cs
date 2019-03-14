using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using CathedralKitchen.NewModels;

namespace CathedralKitchen.ViewModels
{
    public class ScheduleViewModel
    {
        public IEnumerable<CommunityViewModel> Communities { get; set; }
        public IEnumerable<ScheduleConfigViewModel> ScheduleConfigViewModels { get; set; }
        public long CommunityId { get; set; }
        public string StartDate { get; set; }
        public bool Cycle { get; set; }
    }
}
