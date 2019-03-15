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

                var communityViewModel = new CommunityViewModel
                {
                    Id = config.Community.Id,
                    Name = config.Community.Name
                };

                var scheduleViewModel = new ScheduleConfigViewModel
                {
                    Id = config.Id,
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

                var communityViewModel = new CommunityViewModel
                {
                    Id = config.Community.Id,
                    Name = config.Community.Name
                };

                var scheduleViewModel = new ScheduleConfigViewModel
                {
                    Id = config.Id,
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

            var communities = _ctx.Community.Where(x => x.Active == true).ToList();

            var communityViewModels = new List<CommunityViewModel>();

            foreach (var community in communities)
            {
                var communityViewModel = new CommunityViewModel
                {
                    Id = community.Id,
                    Name = community.Name,
                    Active = community.Active
                };

                communityViewModels.Add(communityViewModel);
            }

            ScheduleViewModel scheduleViewModel = new ScheduleViewModel
            {
                Communities = communityViewModels
                //ScheduleConfigViewModels = events
            };

            return View(scheduleViewModel);
        }


        [HttpGet]
        public IActionResult GetCalendarEvents(string start)
        {
            List<ScheduleConfig> events = _ctx.ScheduleConfig.Where(x => x.Active == true).Include(y => y.Community).ToList();

            return Json(events);
        }


        [HttpPost]
        public IActionResult AddEvent([FromBody] dynamic evt)
        {
            var cycle = 0;
            var events = new Dictionary<long, DateTime>();
            long parentId = 0;
            for (int i = 0; i < (int)evt.Cycle; i++)
            {
                DateTime date = evt.Start;
                for (int y = 0; y < cycle; y++)
                {
                    date = date.AddDays(7);
                }
                if (i == 0)
                {
                    var parentScheduleConfig = new ScheduleConfig
                    {
                        CommunityId = evt.CommunityId,
                        Date = date,
                        Active = true,
                        CreateBy = 2,
                        CreateTime = DateTime.UtcNow,
                        UpdateBy = 2,
                        UpdateTime = DateTime.UtcNow
                    };
                    _ctx.ScheduleConfig.Add(parentScheduleConfig);
                    _ctx.SaveChanges();
                    parentId = parentScheduleConfig.Id;
                    events.Add(parentScheduleConfig.Id, parentScheduleConfig.Date);

                }
                else
                {
                    var scheduleConfig = new ScheduleConfig
                    {
                        ParentId = parentId,
                        CommunityId = evt.CommunityId,
                        Date = date,
                        Active = true,
                        CreateBy = 2,
                        CreateTime = DateTime.UtcNow,
                        UpdateBy = 2,
                        UpdateTime = DateTime.UtcNow

                    };
                    _ctx.ScheduleConfig.Add(scheduleConfig);
                    _ctx.SaveChanges();
                    events.Add(scheduleConfig.Id, scheduleConfig.Date);
                }
                cycle += 1;

            }

            var message = "";
            return Json(new { message, events });
        }

        [HttpPost]
        public IActionResult UpdateEvent([FromBody] dynamic evt)
        {
            DateTime InitialDate = evt.Start;
            long eventId = (long)evt.EventId;
            TimeSpan dateDifference;
            var events = new Dictionary<long, DateTime>();
            var eventSchedule = _ctx.ScheduleConfig.First(x => x.Id == eventId);
            ScheduleConfig parentSchedule = _ctx.ScheduleConfig.FirstOrDefault(x => x.Id == eventSchedule.ParentId);
            if(parentSchedule != null) {
                dateDifference = parentSchedule.Date.Subtract(eventSchedule.Date);
                eventSchedule = parentSchedule;
                InitialDate = InitialDate.Add(dateDifference);
            }
            long parentId = eventSchedule.ParentId ?? eventSchedule.Id;
            var schedules = _ctx.ScheduleConfig.Where(x => x.ParentId == parentId || x.Id == parentId).ToList();
            var cycle = 0;

            foreach (var schedule in schedules)
            {
                schedule.Active = false;
                _ctx.SaveChanges();
            }
            for (int i = 0; i < (int)evt.Cycle; i++)
            {
                DateTime oldDate = eventSchedule.Date;
                DateTime newDate = InitialDate;
                for (int y = 0; y < cycle; y++)
                {
                    oldDate = oldDate.AddDays(7);
                    newDate = newDate.AddDays(7);
                }
                ScheduleConfig scheduledDate = schedules.Where(x => x.Date == oldDate).FirstOrDefault(x => x.CommunityId == eventSchedule.CommunityId);
                if (scheduledDate != null)
                {
                    scheduledDate.ParentId = parentId;
                    scheduledDate.CommunityId = evt.CommunityId;
                    scheduledDate.Date = newDate;
                    scheduledDate.Active = true;
                    scheduledDate.UpdateBy = 2;
                    scheduledDate.UpdateTime = DateTime.UtcNow;
                    events.Add(scheduledDate.Id, scheduledDate.Date);
                    _ctx.SaveChanges();
                }
                else
                {
                    var scheduleConfig = new ScheduleConfig
                    {
                        ParentId = parentId,
                        CommunityId = evt.CommunityId,
                        Date = newDate,
                        Active = true,
                        CreateBy = 2,
                        CreateTime = DateTime.UtcNow,
                        UpdateBy = 2,
                        UpdateTime = DateTime.UtcNow

                    };
                    _ctx.ScheduleConfig.Add(scheduleConfig);
                    _ctx.SaveChanges();
                    events.Add(scheduleConfig.Id, scheduleConfig.Date);
                }
                cycle += 1;
            }

            var message = "";
            return Json(new { message, events });
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