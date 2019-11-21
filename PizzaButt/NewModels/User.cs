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
        public long? PersonId { get; set; }
        public long? BuilderId { get; set; }
        public bool? Active { get; set; }
        public DateTime CreateTime { get; set; }
        public long CreateBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public long UpdateBy { get; set; }
        public DateTime? DeleteTime { get; set; }
        public long? DeleteBy { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public long? CityId { get; set; }
        public string Zipcode { get; set; }
        public string Number { get; set; }

        public virtual Builder Builder { get; set; }
        public virtual City City { get; set; }
    }
}
