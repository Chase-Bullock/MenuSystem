using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
//using PizzaButt.Models;
using PizzaButt.NewModels;
using PizzaButt.ViewModels;
using PizzaButt.ViewModels.AccountViewModels;

namespace PizzaButt.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IPizzaRepository pizzaRepository;
        private readonly ICathedralKitchenRepository _cathedralKitchenRepository;
        private readonly CathedralKitchenContext _ctx;

        public HomeController(ICathedralKitchenRepository cathedralKitchenRepository, CathedralKitchenContext ctx)
        {
            _cathedralKitchenRepository = cathedralKitchenRepository;
            _ctx = ctx;
        }

        [HttpGet]
        public IEnumerable<MenuItem> GetAllItems()
        {
            return _cathedralKitchenRepository.GetMenuItems();
        }

        [HttpGet("{id}")]
        public MenuItem GetItemById(long id)
        {
            return _cathedralKitchenRepository.GetMenuItem(id) ?? new MenuItem();
        }

        public IActionResult Index()
        {
            //FIX ORDER VIEW MODEL
            var menuItems = _cathedralKitchenRepository.GetActiveMenuItems();
            var Toppings = _ctx.Topping.ToList();

            var orderView = new HomePageViewModel
            {
                MenuItems = menuItems,
                PizzaToppings = Toppings
            };
            return View(orderView);
        }

        [HttpPost]
        public IActionResult Index(Order request)
        {
            if (!ModelState.IsValid)
            {
                var options = _cathedralKitchenRepository.GetActiveMenuItems();
                var orderView = new OrderViewModel
                {
                    MenuItems = options
                };
                return View(orderView);
            }
            
            var orderId = _cathedralKitchenRepository.SendOrder(request);
            return RedirectToAction("OrderInfo", "Orders", new {orderId = orderId});

        }

    public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
