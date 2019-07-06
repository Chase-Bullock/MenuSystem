using System;
using System.Collections.Generic;

namespace CathedralKitchen.ViewModels
{
    public partial class PersonViewModel
    {
        public long Id { get; set; }
        public string Home { get; set; }
        public string Work { get; set; }
        public string Cell { get; set; }
        public string Email { get; set; }
        public bool? SendEmail { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public bool? Active { get; set; }
    }
}
