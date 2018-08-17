using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaButt.Models;

namespace PizzaButt.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            var orders = new List<OrderModel>
            {
                new OrderModel
                {
                    Name = "Tyler de Arrigunaga",
                    Order = "Tacos (3)",
                    Quantity = 2,
                    SpecialInstructions = "No Cilantro"
                }
            };
            return View(orders);
        }

        public IActionResult OrderInfo([FromQuery] long orderId)
        {
            var order1 = new OrderModel
            {
                Id = 1,
                Name = "Tyler de Arrigunaga",
                Order = "Tacos (3)",
                Quantity = 2,
                SpecialInstructions = "No Cilantro",
                Status = "Pending"
            };
            var order2 = new OrderModel
            {
                Id = 1,
                Name = "Tyler de Arrigunaga",
                Order = "Tacos (3)",
                Quantity = 2,
                SpecialInstructions = "No Cilantro",
                Status = "Complete"
            };
            var orderDict = new Dictionary<long, OrderModel>();
            orderDict.Add(1, order1);
            orderDict.Add(2, order2);
            var order = orderDict[orderId];
            return View(order);
        }
    }
}