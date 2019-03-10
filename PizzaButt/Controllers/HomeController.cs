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
using CathedralKitchen.Helpers;
//using PizzaButt.Models;
using CathedralKitchen.NewModels;
using CathedralKitchen.ViewModels;
using CathedralKitchen.ViewModels.AccountViewModels;

namespace CathedralKitchen.Controllers
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
            var toppings = _ctx.Topping.Include(y => y.ToppingSystemReference).ThenInclude(x => x.ToppingType);
            var filteredPizzaToppings = toppings.Where(x => x.ToppingSystemReference.Any(y => y.ToppingType.Name == "Pizza"));
            var filteredTacoToppings = toppings.Where(x => x.ToppingSystemReference.Any(y => y.ToppingType.Name == "Taco"));
            var todaysSchedule = _ctx.ScheduleConfig.Include(y => y.Community);
            var filteredTodaysSchedule = todaysSchedule.Where(x => x.Date == DateTime.Today).Where(y => y.Active == true);
            bool isAuthenticated = User.Identity.IsAuthenticated;

            var filteredPizzaToppingsViewModel = new List<ToppingsViewModel>();
            var filteredTacoToppingsViewModel = new List<ToppingsViewModel>();
            var allToppingsViewModel = new List<ToppingsViewModel>();
            var filteredScheduleConfigViewModel = new List<ScheduleConfigViewModel>();

            foreach (var config in filteredTodaysSchedule)
            {
                var builderViewModel = new BuilderViewModel
                {
                    Id = config.Builder.Id,
                    Name = config.Builder.Name
                };

                var communityViewModel = new CommunityViewModel
                {
                    Id = config.Community.Id,
                    Name = config.Community.Name
                };

                var scheduleViewModel = new ScheduleConfigViewModel
                {
                    Id = config.Id,
                    Builder = builderViewModel,
                    Community = communityViewModel,
                    Date = config.Date,
                    Active = config.Active
                };

                filteredScheduleConfigViewModel.Add(scheduleViewModel);
            };

            foreach (var topping in filteredPizzaToppings)
            {
                var toppingTypes = new List<SystemReference>();

                foreach(var sysref in topping.ToppingSystemReference)
                {
                    toppingTypes.Add(sysref.ToppingType);
                }
                var toppingViewModel = new ToppingsViewModel
                {
                    Name = topping.ToppingName,
                    ToppingTypes = toppingTypes,
                    Id = topping.Id
                };

                filteredPizzaToppingsViewModel.Add(toppingViewModel);
            };


            foreach (var topping in filteredTacoToppings)
            {
                var toppingTypes = new List<SystemReference>();

                foreach (var sysref in topping.ToppingSystemReference)
                {
                    toppingTypes.Add(sysref.ToppingType);
                }
                var toppingViewModel = new ToppingsViewModel
                {
                    Name = topping.ToppingName,
                    ToppingTypes = toppingTypes,
                    Id = topping.Id
                };

                filteredTacoToppingsViewModel.Add(toppingViewModel);
            };

            foreach (var topping in toppings)
            {
                var toppingTypes = new List<SystemReference>();

                foreach (var sysref in topping.ToppingSystemReference)
                {
                    toppingTypes.Add(sysref.ToppingType);
                }
                var toppingViewModel = new ToppingsViewModel
                {
                    Name = topping.ToppingName,
                    ToppingTypes = toppingTypes,
                    Id = topping.Id
                };

                allToppingsViewModel.Add(toppingViewModel);
            };

            var orderView = new HomePageViewModel
            {
                MenuItems = menuItems,
                PizzaToppings = filteredPizzaToppingsViewModel,
                TacoToppings = filteredTacoToppingsViewModel,
                AllToppings = allToppingsViewModel,
                TodaysSchedule = filteredScheduleConfigViewModel
            };
            return View(orderView);
        }

        public IActionResult ShortendOrderView()
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
            var toppings = _ctx.Topping.Include(y => y.ToppingSystemReference).ThenInclude(x => x.ToppingType);
            var filteredPizzaToppings = toppings.Where(x => x.ToppingSystemReference.Any(y => y.ToppingType.Name == "Pizza"));
            var filteredTacoToppings = toppings.Where(x => x.ToppingSystemReference.Any(y => y.ToppingType.Name == "Taco"));
            var todaysSchedule = _ctx.ScheduleConfig.Include(y => y.Community);
            var filteredTodaysSchedule = todaysSchedule.Where(x => x.Date == DateTime.Today).Where(y => y.Active == true);
            bool isAuthenticated = User.Identity.IsAuthenticated;

            var filteredPizzaToppingsViewModel = new List<ToppingsViewModel>();
            var filteredTacoToppingsViewModel = new List<ToppingsViewModel>();
            var allToppingsViewModel = new List<ToppingsViewModel>();
            var filteredScheduleConfigViewModel = new List<ScheduleConfigViewModel>();

            foreach (var config in filteredTodaysSchedule)
            {
                var builderViewModel = new BuilderViewModel
                {
                    Id = config.Builder.Id,
                    Name = config.Builder.Name
                };

                var communityViewModel = new CommunityViewModel
                {
                    Id = config.Community.Id,
                    Name = config.Community.Name
                };

                var scheduleViewModel = new ScheduleConfigViewModel
                {
                    Id = config.Id,
                    Builder = builderViewModel,
                    Community = communityViewModel,
                    Date = config.Date,
                    Active = config.Active
                };

                filteredScheduleConfigViewModel.Add(scheduleViewModel);
            };

            foreach (var topping in filteredPizzaToppings)
            {
                var toppingTypes = new List<SystemReference>();

                foreach (var sysref in topping.ToppingSystemReference)
                {
                    toppingTypes.Add(sysref.ToppingType);
                }
                var toppingViewModel = new ToppingsViewModel
                {
                    Name = topping.ToppingName,
                    ToppingTypes = toppingTypes,
                    Id = topping.Id
                };

                filteredPizzaToppingsViewModel.Add(toppingViewModel);
            };


            foreach (var topping in filteredTacoToppings)
            {
                var toppingTypes = new List<SystemReference>();

                foreach (var sysref in topping.ToppingSystemReference)
                {
                    toppingTypes.Add(sysref.ToppingType);
                }
                var toppingViewModel = new ToppingsViewModel
                {
                    Name = topping.ToppingName,
                    ToppingTypes = toppingTypes,
                    Id = topping.Id
                };

                filteredTacoToppingsViewModel.Add(toppingViewModel);
            };

            foreach (var topping in toppings)
            {
                var toppingTypes = new List<SystemReference>();

                foreach (var sysref in topping.ToppingSystemReference)
                {
                    toppingTypes.Add(sysref.ToppingType);
                }
                var toppingViewModel = new ToppingsViewModel
                {
                    Name = topping.ToppingName,
                    ToppingTypes = toppingTypes,
                    Id = topping.Id
                };

                allToppingsViewModel.Add(toppingViewModel);
            };

            var orderView = new HomePageViewModel
            {
                MenuItems = menuItems,
                PizzaToppings = filteredPizzaToppingsViewModel,
                TacoToppings = filteredTacoToppingsViewModel,
                AllToppings = allToppingsViewModel,
                TodaysSchedule = filteredScheduleConfigViewModel
            };
            return View(orderView);
        }

        [HttpPost]
        public IActionResult Index(HomePageViewModel request)
        {
            if (!ModelState.IsValid) return RedirectToAction("ShortendOrderView", "Home");
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
                cart.Add(new OrderItemViewModel { MenuItem = selectedItemViewModel, Toppings = selectedToppingsViewModels, Quantity = request.Quantity, SizeId = request.SizeId });
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
                    cart.Add(new OrderItemViewModel { MenuItem = selectedItemViewModel, Toppings = selectedToppingsViewModels, Quantity = request.Quantity, SizeId = request.SizeId });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }

            return RedirectToAction("ShortendOrderView");

        }

        [HttpPost]
        public IActionResult ShortendOrderView(HomePageViewModel request)
        {
            if (!ModelState.IsValid) return RedirectToAction("ShortendOrderView", "Home");
            var menuItems = _cathedralKitchenRepository.GetActiveMenuItems();
            var selectedItem = menuItems.First(x => x.Name == request.ItemName);
            var toppings = _ctx.Topping;
            var selectedToppings = toppings.Where(x => request.Toppings.Contains(x.Id)).ToList();
            var selectedToppingsViewModels = new List<ToppingsViewModel>();

            foreach (var selectedTopping in selectedToppings)
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
                cart.Add(new OrderItemViewModel { MenuItem = selectedItemViewModel, Toppings = selectedToppingsViewModels, Quantity = request.Quantity, SizeId = request.SizeId });
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
                    cart.Add(new OrderItemViewModel { MenuItem = selectedItemViewModel, Toppings = selectedToppingsViewModels, Quantity = request.Quantity, SizeId = request.SizeId });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }

            return RedirectToAction("ShortendOrderView");

        }

        public IActionResult Schedule()
        {
            return View();
        }


        [HttpGet]
        public IActionResult GetCalendarEvents(string start, string end)
        {
            List<ScheduleConfig> events = _ctx.ScheduleConfig.ToList();

            return Json(events);
        }


        [HttpPost]
        public IActionResult AddEvent([FromBody] Event evt)
        {
            var message = "";
            //_ctx.ScheduleConfig.Add(evt);
            _ctx.SaveChanges();
            return Json(new { message, evt.EventId });
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
                    if (cart[i].Toppings.Count() == toppingsViewModels.Count() && !cart[i].Toppings.Except(toppingsViewModels).Any())
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
    }
}