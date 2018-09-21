using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public async Task<MenuItem> GetMenuItem(string id)
        {
            ObjectId internalId = GetInternalId(id);
            try
            {
                return (await context.MenuItems.FindAsync(o => o.Id == internalId)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private ObjectId GetInternalId(string id)
        {
            ObjectId internalId;
            if (!ObjectId.TryParse(id, out internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }

        public async Task<string> SendOrder(OrderModel model)
        {
            try
            {
                const string FMT = "O";
                var CreateTime = DateTime.Now;
                model.CreateTime = CreateTime;
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
            ObjectId internalId = GetInternalId(id);
            try
            {
                return (await context.Orders.FindAsync(o => o.Id == internalId)).FirstOrDefault();
  
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
                return (await context.Orders.FindAsync(o => o.CompleteTime > (DateTime.Now.Date.AddDays(-1)) || o.CompleteTime ==  DateTime.Parse("01/01/0001")
                                                            )).ToList();
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
