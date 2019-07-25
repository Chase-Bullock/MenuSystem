using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CathedralKitchen.Helpers;
using CathedralKitchen.NewModels;
using CathedralKitchen.ViewModels;
using CathedralKitchen.Service;

namespace CathedralKitchen.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {

        private readonly CathedralKitchenContext _ctx;
        private readonly IScheduleService _scheduleService;

        public CartController(CathedralKitchenContext ctx, IScheduleService scheduleService)
        {
            _ctx = ctx;
            _scheduleService = scheduleService;
        }

        [Route("OrderMenu")]
        public IActionResult OrderMenu()
        {
            if (SessionHelper.GetObjectFromJson<long>(HttpContext.Session, "orderId") == 0)
            {
                return RedirectToAction("ShortendOrderView", "Home");
            }
                if (SessionHelper.GetObjectFromJson<List<OrderItemViewModel>>(HttpContext.Session, "cart") == null)
            {
                List<OrderItemViewModel> newCart = new List<OrderItemViewModel>();
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", newCart);
            }
            var isEmployee = SessionHelper.GetObjectFromJson<bool>(HttpContext.Session, "isEmployee");

            var cart = SessionHelper.GetObjectFromJson<List<OrderItemViewModel>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            //FIX ORDER VIEW MODEL
            var menuItems = _ctx.MenuItem.Where(x => x.Active == true);
            var toppings = _ctx.Topping.Where(i => i.Active == true).Include(y => y.ToppingSystemReference).ThenInclude(x => x.ToppingType);
            var filteredPizzaToppings = toppings.Where(x => x.Active == true && x.ToppingSystemReference.Any(y => y.ToppingType.Name == "Pizza"));
            var filteredTacoToppings = toppings.Where(x => x.Active == true && x.ToppingSystemReference.Any(y => y.ToppingType.Name == "Taco"));

            bool isAuthenticated = User.Identity.IsAuthenticated;

            var filteredPizzaToppingsViewModel = new List<ToppingsViewModel>();
            var filteredTacoToppingsViewModel = new List<ToppingsViewModel>();
            var allToppingsViewModel = new List<ToppingsViewModel>();

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

            var orderView = new CustomerOrderViewModel
            {
                MenuItems = menuItems,
                PizzaToppings = filteredPizzaToppingsViewModel,
                TacoToppings = filteredTacoToppingsViewModel,
                AllToppings = allToppingsViewModel,
                IsEmployee = isEmployee
            };
            return View(orderView);
        }


        [Route("OrderMenu")]
        [HttpPost]
        public IActionResult OrderMenu(CustomerOrderViewModel request)
        {
            if(request.ItemName != "Pizza")
            {
                request.SizeId = 0;
            }
            if (!ModelState.IsValid) return View();
            var menuItems = _ctx.MenuItem.Where(x => x.Active == true);
            var selectedItem = menuItems.First(x => x.Name == request.ItemName);
            var toppings = _ctx.Topping;
            var selectedToppingsViewModels = new List<ToppingsViewModel>();
            if (request.Toppings != null)
            {
                var selectedToppings = toppings?.Where(x => request.Toppings.Contains(x.Id)).ToList();


                foreach (var selectedTopping in selectedToppings)
                {
                    ToppingsViewModel topping = new ToppingsViewModel
                    {
                        Id = selectedTopping.Id,
                        Name = selectedTopping.ToppingName
                    };

                    selectedToppingsViewModels.Add(topping);
                }
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
                if(!request.IsEmployee && SessionHelper.GetObjectFromJson<List<OrderItemViewModel>>(HttpContext.Session, "cart").Count > 0)
                {
                    TempData["ErrorMessage"] = "You may only select one item, please remove your previous item to add a new one.";
                    return RedirectToAction("OrderMenu", "Cart");
                }
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

            return RedirectToAction("OrderMenu", "Cart");

        }

        [Route("remove/{id}/{toppingIds}")]
        [Route("remove/{id}")]
        public IActionResult Remove(long id, string toppingIds)
        {
            var ids = new List<long>();

            if (toppingIds != null)
            {
                string[] splitToppingIds = toppingIds.Split(",");
                splitToppingIds = splitToppingIds.Where(x => x != "").ToArray();
                ids = splitToppingIds.Select(long.Parse).ToList();
            }
            List<OrderItem> cart = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart");
            var toppings = _ctx.Topping.Where(r => ids.Contains(r.Id)).ToList();
            var toppingsViewModels = new List<ToppingsViewModel>();
            toppings.ForEach(x =>
            {
                var toppingsViewModel = new ToppingsViewModel
                {
                    Id = x.Id,
                    Name = x.ToppingName,
                };
                toppingsViewModels.Add(toppingsViewModel);
            });
            int index = isExist(id, toppingsViewModels); //returns index in cart where item id = id and item toppings = toppings
            if (index >= 0)
            {
                cart.RemoveAt(index);
            }
            else
            {
                TempData["ErrorMessage"] = "Something went wrong, please try again.";
                return RedirectToAction("OrderMenu", "Cart");
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("OrderMenu", "Cart");
        }

        //TODO CHECK ORDERSFORCUSTOMERTODAY
        public IActionResult Checkout(string note)
        {
            var cart = SessionHelper.GetObjectFromJson<List<OrderItemViewModel>>(HttpContext.Session, "cart");
            var orderId = SessionHelper.GetObjectFromJson<long>(HttpContext.Session, "orderId");
            var order = _ctx.Order.FirstOrDefault(x => x.Id == orderId);
            var ordersForCustomerToday = _ctx.Order.Where(x => x.CustomerEmail == order.CustomerEmail
                                                            && x.CreateTime.ToLocalTime().Day == DateTime.Today.Day
                                                            && x.OrderStatusId != 2
                                                            && x.CommunityId != 1);
            if (ordersForCustomerToday.Count() > 1)
            {
                TempData["ErrorMessage"] = "Orders are limited to only one per day, cancel your last order by clicking the cancel button below.";
                return RedirectToAction("ShortendOrderView", "Home");
            }

            ViewBag.cart = cart;

            order.Note = note;
            _ctx.SaveChanges();
            

            foreach (var item in cart)
            {
                var orderItem = new OrderItem
                {
                    MenuItemId = item.MenuItem.Id,
                    Quantity = item.Quantity,
                    SizeId = item.SizeId,
                    CreateBy = 1,
                    UpdateBy = 1,
                    CreateTime = DateTime.UtcNow,
                    UpdateTime = DateTime.UtcNow,
                    OrderId = orderId

                };
                _ctx.OrderItem.Add(orderItem);
                _ctx.SaveChanges();

                foreach (var topping in item.Toppings)
                {
                    var toppingItem = new OrderItemTopping
                    {
                        OrderItemId = orderItem.Id,
                        ToppingId = topping.Id,
                        CreateBy = 1,
                        UpdateBy = 1,
                        CreateTime = DateTime.UtcNow,
                        UpdateTime = DateTime.UtcNow
                    };
                    _ctx.OrderItemTopping.Add(toppingItem);
                }

                var orderToUpdate = _ctx.Order.First(x => x.Id == orderId);
                orderToUpdate.OrderStatusId = _ctx.OrderStatus.SingleOrDefault(x => x.Status == "Pending").Id;

                _ctx.SaveChanges();
            }
            return RedirectToAction("OrderInfoForCustomer", "Orders");
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
