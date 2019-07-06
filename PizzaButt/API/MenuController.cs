using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CathedralKitchen.NewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CathedralKitchen.API
{
    [Route("api/[controller]")]
    public class MenuController : Controller
    {
        private readonly CathedralKitchenContext _ctx;
        private readonly ICathedralKitchenRepository _cathedralKitchenRepository;

        public MenuController(ICathedralKitchenRepository cathedralKitchenRepository, CathedralKitchenContext ctx)
        {
            _ctx = ctx;
            _cathedralKitchenRepository = cathedralKitchenRepository;
        }

        [HttpGet]
        public IActionResult GetAllItems()
        {
            var data = _ctx.MenuItem.Where(x => x.Active == true);

            return Json(data);
        }

        [HttpGet("{id}")]
        public IActionResult GetItemById(long id)
        {
            var data = _ctx.MenuItem.Where(x => x.Active == true && x.Id == id);

            return Json(data);
        }

    }
}
