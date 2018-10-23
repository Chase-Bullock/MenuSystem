using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaButt.Models
{
  public interface IPizzaRepository
  {
    Task<IEnumerable<MenuItem>> GetMenuItems();
    Task<IEnumerable<MenuItem>> GetActiveMenuItems();
    Task<MenuItem> GetMenuItem(string id);
    Task<string> SendOrder(OrderModel model);
    Task<string> CreateMenuItem(MenuItem model);
    Task<OrderModel> GetOrder(string id);
    Task<List<OrderModel>> GetOrders();
    Task UpdateOrder(OrderModel model);
    Task UpdateMenu(List<MenuItem> model);
  }
}