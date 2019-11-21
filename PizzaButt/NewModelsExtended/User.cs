using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CathedralKitchen.NewModels
{
    public partial class User
    {
        [NotMapped]
        public string Token { get; set; }

        public void SetHash(string hash)
        {
            Hash = hash;
        }
    }
}
