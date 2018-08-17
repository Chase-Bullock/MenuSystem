using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaButt.Models
{
    public interface IPizzaRepository
    {
        Task<IEnumerable<MenuItem>> GetMenuItems();
        Task<MenuItem> GetMenuItem(int id);
    }
}