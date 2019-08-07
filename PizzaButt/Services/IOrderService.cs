using System.Collections.Generic;
using CathedralKitchen.ViewModels;

namespace CathedralKitchen.Service
{
    public interface IOrderService
    {
        OrderViewModel GetOrderInfoForCustomer(long orderId);
        List<OrderViewModel> GetStatusOfAllOrders();
    }
}