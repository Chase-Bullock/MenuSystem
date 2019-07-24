using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using CathedralKitchen.NewModels;

namespace CathedralKitchen.ViewModels
{
    public class CodeViewModel
    {
     
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
    }
}
