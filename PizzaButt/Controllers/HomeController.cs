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
using CathedralKitchen.Services;
using CathedralKitchen.API;
using CathedralKitchen.Utility;
using CathedralKitchen.ExtendedModels;
using CathedralKitchen.Factory;

namespace CathedralKitchen.Controllers
{
    public class HomeController : Controller
    {
        //private readonly IPizzaRepository pizzaRepository;
        private readonly ICathedralKitchenRepository _cathedralKitchenRepository;
        private readonly CathedralKitchenContext _ctx;
        private readonly IEmailNotificationService _emailNotificationService;
        private readonly ScheduleController _scheduleController;
        private readonly IOptions<SettingsModel> _options;

        public HomeController(ICathedralKitchenRepository cathedralKitchenRepository, CathedralKitchenContext ctx, IEmailNotificationService emailNotificationService, IOptions<SettingsModel> options)
        {
            _emailNotificationService = emailNotificationService;
            _cathedralKitchenRepository = cathedralKitchenRepository;
            _ctx = ctx;
            _options = options;
            ApplicationSettings.WebApiUrl = _options.Value.WebApiBaseUrl;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ShortendOrderView()
        {
            if (!ModelState.IsValid) return RedirectToAction("ShortendOrderView", "Home");
            var validated = SessionHelper.GetObjectFromJson<string>(HttpContext.Session, "validated");
            if (validated != _ctx.OrderCode.First(x => x.Active == true).Password) return RedirectToAction("Index", "Home");
            var cart = SessionHelper.GetObjectFromJson<List<OrderItemViewModel>>(HttpContext.Session, "cart");
            var orderId = SessionHelper.GetObjectFromJson<long>(HttpContext.Session, "orderId");
            if(orderId > 0)
            {
                TempData["ErrorMessage"] = "Orders are limited to only one per day, cancel your last order to create a new one.";
                return RedirectToAction("OrderInfoForCustomer", "Orders");
            }
            ViewBag.cart = cart;

            var todaysSchedule = _ctx.ScheduleConfig.Include(y => y.Community);
            var filteredTodaysSchedule = todaysSchedule.Where(x => x.Date.Date == DateTime.Today);

            var allCommunities = new List<CommunityViewModel>();
            var communities = _ctx.Community.Where(x => x.Active == true).ToList();

            foreach(var community in communities)
            {
                var communityViewModel = new CommunityViewModel
                {
                    Id = community.Id,
                    Name = community.Name
                };
                allCommunities.Add(communityViewModel);
            }

            var scheduleData = await ApiClientFactory.Instance.GetSchedule();
            var scheduledCommunityData = await ApiClientFactory.Instance.GetScheduledCommunities();

            var cartViewModel = new CartViewModel
            {
                TodaysSchedule = scheduleData,
                ScheduledCommunities = scheduledCommunityData,
                Communities = allCommunities
            };

            //return RedirectToAction("OrderInfoForCustomer", "Orders");
            return View(cartViewModel);
        }

        [HttpPost]
        public IActionResult Index(CodeViewModel codeViewModel)
        {
            if (!ModelState.IsValid) return View();
            if (codeViewModel.Code != _ctx.OrderCode.First(x => x.Active == true).Password)
            {
                ModelState.AddModelError("error", "Code is invalid");
                return View();
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "validated", _ctx.OrderCode.First(x => x.Active == true).Password);
            return RedirectToAction("ShortendOrderView", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> ShortendOrderView(CartViewModel cartViewModel)
        {
            if (!ModelState.IsValid) return RedirectToAction("ShortendOrderView", "Home");

            if (!cartViewModel.IsEmployee)
            {
                TimeSpan now = DateTime.Now.TimeOfDay;
                TimeSpan start = new TimeSpan(06, 0, 0);
                TimeSpan end = new TimeSpan(10, 0, 0);
                if (now < start || now > end)
                {
                    TempData["ErrorMessage"] = "Delivery orders must be placed between 6 am and 10 am!";
                    return RedirectToAction("ShortendOrderView", "Home");
                }
            }

            var cart = SessionHelper.GetObjectFromJson<List<OrderItemViewModel>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;

            var person = _ctx.Person.FirstOrDefault(x => x.Email == cartViewModel.Email);

            if (person == null) {

                var personToCreate = new PersonViewModel
                {
                    FirstName = cartViewModel.FirstName,
                    LastName = cartViewModel.LastName,
                    SendEmail = cartViewModel.EmailConsent == true ? true : false,
                    Email = cartViewModel.Email,
                };
                await ApiClientFactory.Instance.CreatePerson(personToCreate);
            } else
            {
                var personToUpdate = new PersonViewModel
                {
                    FirstName = cartViewModel.FirstName,
                    LastName = cartViewModel.LastName,
                    SendEmail = cartViewModel.EmailConsent == true ? true : false,
                    Email = cartViewModel.Email,
                };

                await ApiClientFactory.Instance.UpdatePerson(person.Id, personToUpdate);

            }

            var order = new Order
            {
                OrderStatusId = _ctx.OrderStatus.SingleOrDefault(x => x.Status == "InProgress").Id,
                CustomerEmail = cartViewModel.Email,
                CustomerFirstName = cartViewModel.FirstName,
                CustomerLastName = cartViewModel.LastName,
                Note = cartViewModel.Note,
                CommunityId = cartViewModel.CommunityId > 0 ? cartViewModel.CommunityId : 1,
                AddressLine1 = cartViewModel.AddressLine1,
                AddressLine2 = cartViewModel.AddressLine2,
                City = cartViewModel.City,
                ZipCode = cartViewModel.Zipcode,
                CreateBy = 1,
                UpdateBy = 1,
                CreateTime = DateTime.UtcNow,
                UpdateTime = DateTime.UtcNow
            };

            _ctx.Order.Add(order);
            _ctx.SaveChanges();
        
            if (SessionHelper.GetObjectFromJson<long>(HttpContext.Session, "orderId") == 0)
            {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "orderId", order.Id);
            }

            SessionHelper.SetObjectAsJson(HttpContext.Session, "isEmployee", cartViewModel.IsEmployee);


            return RedirectToAction("OrderMenu", "Cart");


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

        public IActionResult Calendar()
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
            var events = new Dictionary<long, dynamic>();
            var parentIds = new List<long>();
            for (int i = 0; i < (int)evt.Cycle; i++)
            {
                DateTime date = evt.Start;
                for (int y = 0; y < cycle; y++)
                {
                    date = date.AddDays(7);
                }
                if (i == 0)
                {
                    foreach (var y in evt.CommunityId)
                    {
                        var parentScheduleConfig = new ScheduleConfig
                        {
                            CommunityId = y,
                            Date = date,
                            Active = true,
                            CreateBy = 2,
                            CreateTime = DateTime.UtcNow,
                            UpdateBy = 2,
                            UpdateTime = DateTime.UtcNow
                        };
                        _ctx.ScheduleConfig.Add(parentScheduleConfig);
                        _ctx.SaveChanges();
                        parentIds.Add(parentScheduleConfig.Id);
                        var community = _ctx.Community.FirstOrDefault(x => x.Id == parentScheduleConfig.CommunityId);
                        events.Add(parentScheduleConfig.Id, new { parentScheduleConfig.Date, community });

                    }
                }
                else
                {
                    var index = 0;
                    foreach (var y in evt.CommunityId)
                    {
                        
                        var scheduleConfig = new ScheduleConfig
                        {
                            ParentId = parentIds[index],
                            CommunityId = y,
                            Date = date,
                            Active = true,
                            CreateBy = 2,
                            CreateTime = DateTime.UtcNow,
                            UpdateBy = 2,
                            UpdateTime = DateTime.UtcNow

                        };
                        _ctx.ScheduleConfig.Add(scheduleConfig);
                        _ctx.SaveChanges();
                        var community = _ctx.Community.FirstOrDefault(x => x.Id == scheduleConfig.CommunityId);
                        events.Add(scheduleConfig.Id, new { scheduleConfig.Date, community });
                        index++;
                    }
                }
                cycle += 1;

            }

            var message = "";
            return Json(new { message, events });
        }


        [HttpPost]
        public IActionResult DeleteEvent([FromBody] dynamic evt)
        {
            long eventId = (long)evt.EventId;
            var eventToDelete = _ctx.ScheduleConfig.First(x => x.Id == eventId);
            var message = "";

            eventToDelete.Active = false;
            _ctx.SaveChanges();
            return Json(new { message });
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
            if (parentSchedule != null)
            {
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