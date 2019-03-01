using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaButt.NewModels
{
    public interface ICathedralKitchenRepository
    {
        IEnumerable<MenuItem> GetMenuItems();
        IEnumerable<MenuItem> GetActiveMenuItems();
        MenuItem GetMenuItem(long id);
        string SendOrder(long orderId);
        string StartOrder(long orderId);
        string CancelOrder(long orderId);
        string CreateMenuItem(MenuItem model);
        Order GetOrder(long id);
        IEnumerable<Order> GetOrders();
        void CompleteOrder(Order model);
        void UpdateMenu(List<MenuItem> model);
    }
}