using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaButt.Models
{
    public class PizzaRepository : IPizzaRepository
    {
        private readonly PizzaContext _context = null;

        public PizzaRepository(IOptions<Settings> settings)
        {
            _context = new PizzaContext(settings);
        }

        public async Task<IEnumerable<MenuItem>> GetMenuItems()
        {
            try
            {
                return await _context.MenuItems
                        .Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MenuItem> GetMenuItem(int id)
        {
            try
            {
                return await _context.MenuItems
                                .Find(menuItem => menuItem.ItemId == id)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
