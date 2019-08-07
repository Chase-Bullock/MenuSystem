using System.Collections.Generic;
using CathedralKitchen.NewModels;
using CathedralKitchen.ViewModels;

namespace CathedralKitchen.Service
{
    public interface IMenuService
    {
        List<ToppingsViewModel> ConvertToViewModel(List<Topping> toppings);
        List<MenuItem> GetAllItems();
        MenuItem GetItemById(long id);
        List<ToppingsViewModel> GetToppings(string type = null);
    }
}