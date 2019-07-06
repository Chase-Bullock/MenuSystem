using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CathedralKitchen.NewModels;
using CathedralKitchen.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CathedralKitchen.API
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly CathedralKitchenContext _ctx;
        private readonly ICathedralKitchenRepository _cathedralKitchenRepository;

        public PersonController(ICathedralKitchenRepository cathedralKitchenRepository, CathedralKitchenContext ctx)
        {
            _ctx = ctx;
            _cathedralKitchenRepository = cathedralKitchenRepository;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreatePerson([FromBody] PersonViewModel person)
        {
            var personToCreate = new Person
            {
                Email = person.Email,
                Active = true,
                Cell = person.Cell,
                Home = person.Home,
                Work = person.Work,
                SendEmail = person.SendEmail,
                FirstName = person.FirstName,
                LastName = person.LastName,
                CreateBy = 1,
                CreateTime = DateTime.UtcNow,
                UpdateBy = 1,
                UpdateTime = DateTime.UtcNow

            };

            await _ctx.Person.AddAsync(personToCreate);
            await _ctx.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdatePerson([FromBody] PersonViewModel request, long id)
        {

            var person = _ctx.Person.FirstOrDefault(x => x.Id == id);

            person.FirstName = request.FirstName;
            person.LastName = request.LastName;
            person.Email = request.Email;
            person.Cell = request.Cell;
            person.Home = request.Home;
            person.Work = request.Work;
            person.SendEmail = request.SendEmail;

            _ctx.Update(person);
            await _ctx.SaveChangesAsync();

            return Ok();

        }
    }
}
