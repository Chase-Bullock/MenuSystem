using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaButt.Helpers;
using PizzaButt.NewModels;
using PizzaButt.ViewModels;

namespace PizzaButt.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {

        private readonly ICathedralKitchenRepository _cathedralKitchenRepository;
        private readonly CathedralKitchenContext _ctx;

        public CartController(ICathedralKitchenRepository cathedralKitchenRepository, CathedralKitchenContext ctx)
        {
            _cathedralKitchenRepository = cathedralKitchenRepository;
            _ctx = ctx;
        }

        [Route("checkout")]
        public IActionResult Checkout()
        {
            var cart = SessionHelper.GetObjectFromJson<List<OrderItemViewModel>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;

            //return RedirectToAction("OrderInfoForCustomer", "Orders");
            return View();
        }

        [Route("checkout")]
        [HttpPost]
        public IActionResult Checkout(CartViewModel cartViewModel)
        {
            if (!ModelState.IsValid) return View(cartViewModel);

            var cart = SessionHelper.GetObjectFromJson<List<OrderItemViewModel>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;

            var order = new Order
            {
                OrderStatusId = _ctx.OrderStatus.SingleOrDefault(x => x.Status == "Pending").Id,
                CustomerName = cartViewModel.Name,
                Note = cartViewModel.Note,
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
                    OrderId = order.Id

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
                _ctx.SaveChanges();
            }
            if (SessionHelper.GetObjectFromJson<List<OrderItemViewModel>>(HttpContext.Session, "orderId") == null)
            {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "orderId", order.Id);
            }


            return RedirectToAction("OrderInfoForCustomer", "Orders");
               
        }

        [Route("buy/{id}")]
        public IActionResult Buy(long id)
        {
            var MenuItems = _cathedralKitchenRepository.GetActiveMenuItems();
                if (SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart") == null)
            {
                List<OrderItem> cart = new List<OrderItem>();
                cart.Add(new OrderItem { MenuItem = MenuItems.FirstOrDefault(x => x.Id == id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<OrderItem> cart = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart");
                long index = isExist(id);
                if (index != -1)
                {
                    cart[(int)index].Quantity++;
                }
                else
                {
                    cart.Add(new OrderItem { MenuItem = MenuItems.FirstOrDefault(x => x.Id == id), Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        [Route("remove/{id}")]
        public IActionResult Remove(long id)
        {
            List<OrderItem> cart = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index", "Home");
        }

        private int isExist(long id)
        {
            List<OrderItem> cart = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].MenuItem.Id.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
