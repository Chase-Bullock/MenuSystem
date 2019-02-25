using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var pizzaToppings = _ctx.Topping.Include(y => y.ToppingType);
            var filteredPizzaToppings = pizzaToppings.Where(x => x.ToppingType.Name == "Pizza");
            var tacoToppings = _ctx.Topping.Include(y => y.ToppingType);
            var filteredTacoToppings = tacoToppings.Where(x => x.ToppingType.Name == "Taco");

            var orderView = new HomePageViewModel
            {
                MenuItems = menuItems,
                PizzaToppings = filteredPizzaToppings,
                TacoToppings = tacoToppings
            };
            return View(orderView);
        }

        [HttpPost]
        public IActionResult Index(HomePageViewModel request)
        {
            var menuItems = _cathedralKitchenRepository.GetActiveMenuItems();
            var pizzaToppings = _ctx.Topping.Include(y => y.ToppingType);
            var filteredPizzaToppings = pizzaToppings.Where(x => x.ToppingType.Name == "Pizza");
            var tacoToppings = _ctx.Topping.Include(y => y.ToppingType);
            var filteredTacoToppings = tacoToppings.Where(x => x.ToppingType.Name == "Taco");

            var orderView = new HomePageViewModel
            {
                MenuItems = menuItems,
                PizzaToppings = filteredPizzaToppings,
                TacoToppings = tacoToppings
            };

            if (orderView.Order == null)
            {
                orderView.Order = new Order() {
                    CreateBy = 1,
                    UpdateBy = 1,
                    CreateTime = DateTime.UtcNow,
                    UpdateTime = DateTime.UtcNow,
                    CustomerName = request.Name,
                    Note = request.SpecialInstructions,
                    OrderStatusId = _ctx.OrderStatus.First(x => x.Status == "Pending").Id
                };
            }

            OrderItem orderItem = new OrderItem
            {
                CreateBy = 1,
                UpdateBy = 1,
                CreateTime = DateTime.UtcNow,
                UpdateTime = DateTime.UtcNow,
                MenuItemId = request.OrderItem.MenuItemId,
                OrderItemTopping = request.Toppings,
                Quantity = request.Quantity,
                Size = request.Size
            };

            orderView.Order.OrderItems.Add(request.OrderItem);
            return View(orderView);

        }

    public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
