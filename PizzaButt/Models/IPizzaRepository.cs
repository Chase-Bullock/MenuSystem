using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaButt.Models
{
    public interface IPizzaRepository
    {
        Task<IEnumerable<MenuItem>> GetMenuItems();
        Task<MenuItem> GetMenuItem(int id);
        Task<string> SendOrder(OrderModel model);
        Task<OrderModel> GetOrder(string id);
        Task<List<OrderModel>> GetOrders();
        Task UpdateOrder(OrderModel model);
    }
}