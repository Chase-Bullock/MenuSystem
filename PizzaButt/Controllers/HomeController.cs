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
using PizzaButt.Helpers;
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
            if (SessionHelper.GetObjectFromJson<List<OrderItemViewModel>>(HttpContext.Session, "cart") == null)
            {
                List<OrderItemViewModel> newCart = new List<OrderItemViewModel>();
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", newCart);
            }
            var cart = SessionHelper.GetObjectFromJson<List<OrderItemViewModel>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
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
            var selectedItem = menuItems.First(x => x.Name == request.ItemName);
            var toppings = _ctx.Topping;
            var selectedToppings = toppings.Where(x => request.Toppings.Contains(x.Id)).ToList();

            var selectedToppingsViewModels = new List<ToppingsViewModel>();

            foreach(var selectedTopping in selectedToppings)
            {
                ToppingsViewModel topping = new ToppingsViewModel
                {
                    Id = selectedTopping.Id,
                    Name = selectedTopping.ToppingName
                };

                selectedToppingsViewModels.Add(topping);
            }

            MenuItemViewModel selectedItemViewModel = new MenuItemViewModel
            {
                Id = selectedItem.Id,
                Name = selectedItem.Name
            };
            
            if (SessionHelper.GetObjectFromJson<List<OrderItemViewModel>>(HttpContext.Session, "cart") == null)
            {
                List<OrderItemViewModel> cart = new List<OrderItemViewModel>();
                cart.Add(new OrderItemViewModel { MenuItem = selectedItemViewModel, Toppings = selectedToppingsViewModels, Quantity = request.Quantity });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<OrderItemViewModel> cart = SessionHelper.GetObjectFromJson<List<OrderItemViewModel>>(HttpContext.Session, "cart");
                long index = isExist(selectedItemViewModel.Id, selectedToppingsViewModels);
                if (index != -1)
                {
                    cart[(int)index].Quantity += request.Quantity;
                }
                else
                {
                    cart.Add(new OrderItemViewModel { MenuItem = selectedItemViewModel, Toppings = selectedToppingsViewModels, Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }

            return RedirectToAction("Index");

        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private int isExist(long id, List<ToppingsViewModel> toppingsViewModels)
        {
            List<OrderItemViewModel> cart = SessionHelper.GetObjectFromJson<List<OrderItemViewModel>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].MenuItem.Id.Equals(id))
                {
                    if (cart[i].Toppings.Count() == toppingsViewModels.Count() && cart[i].Toppings.Except(toppingsViewModels).Any())
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
    }
}

//[HttpPost]
//public IActionResult Index(HomePageViewModel request)
//{
//    var menuItems = _cathedralKitchenRepository.GetActiveMenuItems();
//    var pizzaToppings = _ctx.Topping.Include(y => y.ToppingType);
//    var filteredPizzaToppings = pizzaToppings.Where(x => x.ToppingType.Name == "Pizza");
//    var tacoToppings = _ctx.Topping.Include(y => y.ToppingType);
//    var filteredTacoToppings = tacoToppings.Where(x => x.ToppingType.Name == "Taco");

//    var orderView = new HomePageViewModel
//    {
//        MenuItems = menuItems,
//        PizzaToppings = filteredPizzaToppings,
//        TacoToppings = tacoToppings
//    };

//    if (orderView.Order == null)
//    {
//        orderView.Order = new Order() {
//            CreateBy = 1,
//            UpdateBy = 1,
//            CreateTime = DateTime.UtcNow,
//            UpdateTime = DateTime.UtcNow,
//            CustomerName = request.Name,
//            Note = request.SpecialInstructions,
//            OrderStatusId = _ctx.OrderStatus.First(x => x.Status == "Pending").Id
//        };
//    }

//    OrderItem orderItem = new OrderItem
//    {
//        CreateBy = 1,
//        UpdateBy = 1,
//        CreateTime = DateTime.UtcNow,
//        UpdateTime = DateTime.UtcNow,
//        MenuItemId = request.OrderItem.MenuItemId,
//        OrderItemTopping = request.Toppings,
//        Quantity = request.Quantity,
//        Size = request.Size
//    };

//   orderView.OrderItems.Add(request.OrderItem);
//    return View(orderView);

//}