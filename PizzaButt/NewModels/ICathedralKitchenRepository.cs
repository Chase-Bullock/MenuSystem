using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaButt.NewModels
{
    public interface ICathedralKitchenRepository
    {
        IEnumerable<MenuItem> GetMenuItems();
        IEnumerable<MenuItem> GetActiveMenuItems();
        MenuItem GetMenuItem(long id);
        string SendOrder(Order model);
        string StartOrder(Order model);
        string CancelOrder(Order model);
        string CreateMenuItem(MenuItem model);
        Order GetOrder(long id);
        IEnumerable<Order> GetOrders();
        void CompleteOrder(Order model);
        void UpdateMenu(List<MenuItem> model);
    }
}