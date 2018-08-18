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
        private readonly PizzaContext context = null;

        public PizzaRepository(IOptions<Settings> settings)
        {
            context = new PizzaContext(settings);
        }

        public async Task<IEnumerable<MenuItem>> GetMenuItems()
        {
            try
            {
                return await context.MenuItems
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
                return await context.MenuItems
                                .Find(menuItem => menuItem.ItemId == id)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> SendOrder(OrderModel model)
        {
            try
            {
                model.Status = "Pending";
                await context.Orders.InsertOneAsync(model);
                return model.Id.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<OrderModel> GetOrder(string id)
        {
            var objId = new ObjectId(id);
            try
            {
                return (await context.Orders.FindAsync(o => o.Id == objId)).FirstOrDefault();
            }
            catch(Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<OrderModel>> GetOrders()
        {
            try
            {
                return (await context.Orders.FindAsync(o => o.Status != "Complete")).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task UpdateOrder(OrderModel model)
        {
            try
            {
                await context.Orders.UpdateOneAsync(
                    Builders<OrderModel>.Filter.Eq("Id", model.Id),
                    Builders<OrderModel>.Update.Set("Status", model.Status)
                );
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
