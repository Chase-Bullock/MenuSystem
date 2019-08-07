using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CathedralKitchen.NewModels;
using CathedralKitchen.Service;
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
        private readonly IPersonService _personService;

        public PersonController(CathedralKitchenContext ctx, IPersonService personService)
        {
            _ctx = ctx;
            _personService = personService;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreatePerson([FromBody] PersonViewModel person)
        {
            var personId = await _personService.CreatePerson(person);

            return Ok(personId);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdatePerson([FromBody] PersonViewModel request, long id)
        {

            await _personService.UpdatePerson(request, id);

            return Ok();

        }
    }
}
