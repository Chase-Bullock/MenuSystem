using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CathedralKitchen.NewModels;
using CathedralKitchen.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CathedralKitchen.API
{
    [Route("api/[controller]")]
    public class MenuController : Controller
    {
        private readonly CathedralKitchenContext _ctx;
        private readonly IMenuService _menuService;
        private readonly ICathedralKitchenRepository _cathedralKitchenRepository;

        public MenuController(ICathedralKitchenRepository cathedralKitchenRepository, CathedralKitchenContext ctx, IMenuService menuService)
        {
            _ctx = ctx;
            _menuService = menuService;
            _cathedralKitchenRepository = cathedralKitchenRepository;
        }

        [HttpGet("Active")]
        public IActionResult GeActiveItems()
        {
            var data = _menuService.GetActiveItems();

            return Json(data);
        }

        [HttpGet]
        public IActionResult GetAllItems()
        {
            var data = _menuService.GetAllItems();

            return Json(data);
        }

        [HttpGet("{menuItem}/toppings/{toppingsType}")]
        public IActionResult GetToppingsForMenuItem(string menuItem, string toppingsType)
        {
            var data = _menuService.GetTopping(menuItem, toppingsType);

            return Json(data);
        }

        [HttpGet("{id}")]
        public IActionResult GetItemById(long id)
        {
            var data = _menuService.GetItemById(id);

            return Json(data);
        }

    }
}
