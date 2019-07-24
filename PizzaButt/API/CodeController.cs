using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CathedralKitchen.NewModels;
using CathedralKitchen.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CathedralKitchen.API
{
    [Route("api/[controller]")]
    public class CodeController : Controller
    {
        private readonly CathedralKitchenContext _ctx;
        private readonly ICathedralKitchenRepository _cathedralKitchenRepository;

        public CodeController(ICathedralKitchenRepository cathedralKitchenRepository, CathedralKitchenContext ctx)
        {
            _ctx = ctx;
            _cathedralKitchenRepository = cathedralKitchenRepository;
        }

        [HttpPost]
        public IActionResult Index([FromBody]CodeViewModel codeViewModel)
        {
            if (!ModelState.IsValid) return BadRequest();
            if (codeViewModel.Code != _ctx.OrderCode.First(x => x.Active == true).Password)
            {
                ModelState.AddModelError("error", "Code is invalid");
                return BadRequest();
            }
            return Ok();
        }

    }
}
