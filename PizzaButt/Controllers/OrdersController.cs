using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CathedralKitchen.Helpers;
using CathedralKitchen.NewModels;
using CathedralKitchen.Services;
using CathedralKitchen.ViewModels;

namespace CathedralKitchen.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ICathedralKitchenRepository _cathedralKitchenRepository;
        private readonly CathedralKitchenContext _ctx;
        private readonly IEmailNotificationService _emailNotificationService;

        public OrdersController(ICathedralKitchenRepository cathedralKitchenRepository, CathedralKitchenContext ctx, IEmailNotificationService emailNotificationService)
        {
            _emailNotificationService = emailNotificationService;
            _cathedralKitchenRepository = cathedralKitchenRepository;
            _ctx = ctx;
        }
        [Authorize]
        public IActionResult StatusOfAllOrders()
        {
            var orders = _ctx.Order.Where(x => x.OrderStatusId != 20002).Include(y => y.OrderItem).ThenInclude(z => z.OrderItemTopping).ThenInclude(v => v.Topping).Include(c => c.OrderItem).ThenInclude(w => w.MenuItem).Include(y => y.OrderStatus).Include(c => c.Community);
            //var orderItems = _ctx.OrderItem.Include(y => y.MenuItem).Include(y => y.OrderItemTopping).ThenInclude(y => y.Topping).ToList();
            var orderItemsViewModel = new Dictionary<long, List<OrderItemViewModel>>();
            var ordersViewModel = new List<OrderViewModel>();

            foreach (var order in orders)
            {
                foreach (var orderItem in order.OrderItem)
                {
                    var orderItemViewModel = new OrderItemViewModel
                    {
                        MenuItem = new MenuItemViewModel
                        {
                            Id = orderItem.MenuItem.Id,
                            Name = orderItem.MenuItem.Name
                        },
                        OrderItemTopping = orderItem.OrderItemTopping.ToList(),
                        Quantity = orderItem.Quantity,
                        Size = _ctx.SystemReference.FirstOrDefault(x => x.Id == orderItem.SizeId)
                    };
                    if (!orderItemsViewModel.ContainsKey(order.Id))
                    {
                        orderItemsViewModel[order.Id] = new List<OrderItemViewModel>();
                        orderItemsViewModel[order.Id].Add(orderItemViewModel);
                    } else
                    {
                        orderItemsViewModel[order.Id].Add(orderItemViewModel);
                    };
                }

                var communityViewModel = new CommunityViewModel
                {
                    Id = order.Community.Id,
                    Name = order.Community.Name,
                    Active = order.Community.Active
                };

                var orderViewModel = new OrderViewModel
                {
                    Id = order.Id,
                    Status = order.OrderStatus.Status,
                    Note = order.Note,
                    Community = communityViewModel,
                    OrderItems = orderItemsViewModel[order.Id] != null ? orderItemsViewModel.First(x => x.Key == order.Id).Value : new List<OrderItemViewModel>(),
                    Name = order.CustomerFirstName + " " + order.CustomerLastName,
                    City = order.City,
                    Zipcode = order.ZipCode,
                    Address = order.AddressLine1 + " " + order.AddressLine2,
                    CreateTime = order.CreateTime,
                    CompleteTime = order.CompleteTime
                };
                ordersViewModel.Add(orderViewModel);

            }
            return View(ordersViewModel);
        }

        public IActionResult OrderInfoForCustomer()
        {
            SessionHelper.Remove(HttpContext.Session, "cart");
            var orderId = SessionHelper.GetObjectFromJson<long>(HttpContext.Session, "orderId");

            if (orderId == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var order = _ctx.Order.Include(y => y.OrderItem).ThenInclude(z => z.OrderItemTopping).ThenInclude(v => v.Topping).Include(c => c.OrderItem).ThenInclude(w => w.MenuItem).Include(y => y.OrderStatus).Include(c => c.Community).FirstOrDefault(x => x.Id == orderId);
            var orderItemsViewModel = new List<OrderItemViewModel>();

            foreach (var orderItem in order.OrderItem)
            {
                var orderItemViewModel = new OrderItemViewModel
                {
                    MenuItem = new MenuItemViewModel
                    {
                        Id = orderItem.MenuItem.Id,
                        Name = orderItem.MenuItem.Name
                    },
                    OrderItemTopping = orderItem.OrderItemTopping.ToList(),
                    Quantity = orderItem.Quantity,
                    Size = _ctx.SystemReference.FirstOrDefault(x => x.Id == orderItem.SizeId)
                };
                orderItemsViewModel.Add(orderItemViewModel);
            }

            var communityViewModel = new CommunityViewModel
            {
                Id = order.Community.Id,
                Name = order.Community.Name,
                Active = order.Community.Active
            };

            var orderViewModel = new OrderViewModel
            {
                Id = order.Id,
                Status = order.OrderStatus.Status,
                Community = communityViewModel,
                Note = order.Note,
                OrderItems = orderItemsViewModel,
                Name = order.CustomerFirstName + " " + order.CustomerLastName,
                CreateTime = order.CreateTime,
                CompleteTime = order.CompleteTime
            };

            return View(orderViewModel);
        }

        [Authorize]
        public IActionResult SelectActiveToppings()
        {
            var toppings = _ctx.Topping;
            var distinctToppings = toppings.DistinctBy(x => x.ToppingName).ToList();

            var toppingsViewModel = new SelectToppingViewModel
            {
                Toppings = distinctToppings,
            };
            return View(toppingsViewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult SelectActiveToppings(SelectToppingViewModel selectedItems)
        {
            var items = _ctx.Topping;
            List<Topping> allItems = items.ToList();

            foreach (var x in allItems)
            {
                if (selectedItems.ToppingNames.Contains(x.ToppingName))
                {
                    x.Active = true;
                }
                else
                {
                    x.Active = false;
                }
            };

            _cathedralKitchenRepository.UpdateToppings(allItems);
            return Redirect("SelectActiveToppings");
        }

        [Authorize]
        public IActionResult SelectActiveMenuItems()
        {
            var allItems = _cathedralKitchenRepository.GetMenuItems();
            var Menu = new MenuViewModel
            {
                MenuItems = allItems
            };
            return View(Menu);
        }

        [HttpPost]
        public IActionResult SelectActiveMenuItems(MenuViewModel selectedItems)
        {
            var items = _cathedralKitchenRepository.GetMenuItems();
            List<MenuItem> allItems = items.ToList();

            foreach (var x in allItems)
            {
                if (selectedItems.MenuItemNames.Contains(x.Name))
                {
                    x.Active = true;
                }
                else
                {
                    x.Active = false;
                }
            };

            _cathedralKitchenRepository.UpdateMenu(allItems);
            return Redirect("SelectActiveMenuItems");
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreateItem()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateItem(CreateMenuItemViewModel newItem)
        {
            var newCreatedItem = new MenuItem
            {
                Active = newItem.Active == "true" ? true : false,
                Name = newItem.Name,
                CreateBy = 1,
                CreateTime = DateTime.UtcNow,
                UpdateBy = 1,
                UpdateTime = DateTime.UtcNow
            };
            var success = _cathedralKitchenRepository.CreateMenuItem(newCreatedItem);
            ViewBag.Create = success;
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreateTopping()
        {
            var toppingTypes = _ctx.SystemReference.Where(x => x.AltValue == "Topping").ToList();

            var toppingTypesViewModel = new CreateToppingViewModel
            {
                ToppingTypes = toppingTypes
            };

            return View(toppingTypesViewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateTopping(CreateToppingViewModel newItem)
        {
            var newCreatedItem = new Topping
            {
                Active = newItem.Active == "true" ? true : false,
                ToppingName = newItem.Name,
                CreateBy = 1,
                CreateTime = DateTime.UtcNow,
                UpdateBy = 1,
                UpdateTime = DateTime.UtcNow
            };
            _ctx.Topping.Add(newCreatedItem);
            _ctx.SaveChanges();

            foreach (var toppingTypeId in newItem.ToppingTypeIds)
            {
                var toppingSysRef = new ToppingSystemReference
                {
                    ToppingId = newCreatedItem.Id,
                    ToppingTypeId = toppingTypeId,
                    CreateBy = 1,
                    CreateTime = DateTime.UtcNow,
                    UpdateBy = 1,
                    UpdateTime = DateTime.UtcNow
                };
                _ctx.ToppingSystemReference.Add(toppingSysRef);
            }
            _ctx.SaveChanges();
            var success = newItem.Name.ToString();

            ViewBag.Create = success;
            return Redirect("CreateTopping");
        }

        public IActionResult Complete([FromQuery] long orderId)
        {
            var order = _ctx.Order.FirstOrDefault(x => x.Id == orderId);
            var person = _ctx.Person.FirstOrDefault(x => x.Email == order.CustomerEmail);
            if (person.SendEmail == true)
            {
                _emailNotificationService.SendMail(person, 1);
            }
            _cathedralKitchenRepository.CompleteOrder(order);
            return Redirect("StatusOfAllOrders");
        }

        public IActionResult Start([FromQuery] long orderId)
        {
            _cathedralKitchenRepository.StartOrder(orderId);
            var order = _ctx.Order.FirstOrDefault(x => x.Id == orderId);
            var person = _ctx.Person.FirstOrDefault(x => x.Email == order.CustomerEmail);
            if (person.SendEmail == true)
            {
                _emailNotificationService.SendMail(person, 0);
            }
            return Redirect("StatusOfAllOrders");
        }

        public IActionResult ReOpen([FromQuery] long orderId)
        {
            _cathedralKitchenRepository.StartOrder(orderId);
            return Redirect("StatusOfAllOrders");
        }


        public IActionResult Cancel([FromQuery] long orderId)
        {
            _cathedralKitchenRepository.CancelOrder(orderId);
            SessionHelper.Clear(HttpContext.Session);
            return RedirectToAction("Index", "Home");
        }
    }
}