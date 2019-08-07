using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CathedralKitchen.NewModels;
using CathedralKitchen.Service;
using CathedralKitchen.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CathedralKitchen.API
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly CathedralKitchenContext _ctx;
        private readonly IOrderService _orderService;
        private readonly ICathedralKitchenRepository _cathedralKitchenRepository;

        public OrdersController(ICathedralKitchenRepository cathedralKitchenRepository, CathedralKitchenContext ctx, IOrderService orderService)
        {
            _ctx = ctx;
            _orderService = orderService;
            _cathedralKitchenRepository = cathedralKitchenRepository;
        }


        [HttpGet]
        public IActionResult GetStatusOfAllOrders()
        {

            var ordersViewModel = _orderService.GetStatusOfAllOrders();

            return Ok(ordersViewModel);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderInfoForCustomer(long orderId)
        {
            var orderViewModel = _orderService.GetOrderInfoForCustomer(orderId);

            return Ok(orderViewModel);
        }

    }
}
