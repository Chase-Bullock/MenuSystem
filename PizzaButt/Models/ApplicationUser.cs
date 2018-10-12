using AspNetCore.Identity.Mongo;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaButt.Models
{
    public class ApplicationUser : MongoIdentityUser
    {
    }
}
