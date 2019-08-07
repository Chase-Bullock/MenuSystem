using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CathedralKitchen.NewModels;
using CathedralKitchen.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CathedralKitchen.Service
{
    public class MenuService : IMenuService
    {
        private readonly CathedralKitchenContext _ctx;

        public MenuService(CathedralKitchenContext ctx)
        {
            _ctx = ctx;
        }

        public List<MenuItem> GetActiveItems()
        {
            var data = _ctx.MenuItem.Where(x => x.Active == true);

            return data.ToList();
        }

        public List<MenuItem> GetAllItems()
        {
            var data = _ctx.MenuItem;

            return data.ToList();
        }

        public MenuItem GetItemById(long id)
        {
            var data = _ctx.MenuItem.FirstOrDefault(x => x.Active == true && x.Id == id);

            return data;
        }

        public List<ToppingsViewModel> GetToppings(string type = null)
        {
            var toppings = new List<Topping>();
            var toppingsViewModel = new List<ToppingsViewModel>();

            if (type == null)
            {
                toppings = _ctx.Topping
                    .Where(i => i.Active == true)
                    .Include(y => y.ToppingSystemReference).ThenInclude(x => x.ToppingType).ToList();
            }
            else
            {
                toppings = _ctx.Topping
                    .Where(i => i.Active == true)
                    .Include(y => y.ToppingSystemReference).ThenInclude(x => x.ToppingType)
                    .Where(x => x.ToppingSystemReference.Any(y => y.ToppingType.Name == type)).ToList();
            }

            foreach (var topping in toppings)
            {
                var toppingTypes = new List<SystemReference>();

                foreach (var sysref in topping.ToppingSystemReference)
                {
                    toppingTypes.Add(sysref.ToppingType);
                }
                var toppingViewModel = new ToppingsViewModel
                {
                    Name = topping.ToppingName,
                    ToppingTypes = toppingTypes,
                    Id = topping.Id
                };

                toppingsViewModel.Add(toppingViewModel);
            };

            return toppingsViewModel;

        }

        public List<ToppingsViewModel> ConvertToViewModel(List<Topping> toppings)
        {
            List<ToppingsViewModel> toppingsViewModels = new List<ToppingsViewModel>();
            toppings.ForEach(x =>
            {
                var toppingsViewModel = new ToppingsViewModel
                {
                    Id = x.Id,
                    Name = x.ToppingName,
                };
                toppingsViewModels.Add(toppingsViewModel);
            });

            return toppingsViewModels;
        }


    }
}
