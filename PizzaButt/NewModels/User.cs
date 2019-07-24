using System;
using System.Collections.Generic;

namespace CathedralKitchen.NewModels
{
    public partial class User
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public bool HasTempPassword { get; set; }
        public long PersonId { get; set; }
        public bool? Active { get; set; }
        public DateTime CreateTime { get; set; }
        public long CreateBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public long UpdateBy { get; set; }
        public DateTime? DeleteTime { get; set; }
        public long? DeleteBy { get; set; }

        public void SetHash(string hash)
        {
            Hash = hash;
        }
    }
}
