using System;
using System.Collections.Generic;

namespace PizzaButt
{
    public partial class Person
    {
        public Person()
        {
            User = new HashSet<User>();
        }

        public long Id { get; set; }
        public string Home { get; set; }
        public string Work { get; set; }
        public string Cell { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public bool Active { get; set; }
        public DateTime CreateTime { get; set; }
        public long CreateBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public long UpdateBy { get; set; }
        public DateTime? DeleteTime { get; set; }
        public long? DeleteBy { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
