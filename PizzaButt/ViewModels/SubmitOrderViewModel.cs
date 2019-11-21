using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using CathedralKitchen.NewModels;
using CathedralKitchen.Helpers;

namespace CathedralKitchen.ViewModels
{
    public class SubmitOrderViewModel
    {
        public List<OrderItemViewModel> OrderItems { get; set; }
        public CartViewModel Order { get; set; }

    }
}
