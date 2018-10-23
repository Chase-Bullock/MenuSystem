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

    public async Task<IEnumerable<MenuItem>> GetActiveMenuItems()
    {
      try
      {
        return await context.MenuItems
                .Find(_ => _.Active == true).ToListAsync();
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

    public async Task<string> CreateMenuItem(MenuItem model)
    {
      try
      {
        await context.MenuItems.InsertOneAsync(model);
        return model.Name.ToString();
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
      catch (Exception ex)
      {

        throw ex;
      }
    }

    public async Task<List<OrderModel>> GetOrders()
    {
      try
      {
        return (await context.Orders.FindAsync(o => o.CompleteTime > (DateTime.Now.Date.AddDays(-1)) || o.CompleteTime == DateTime.Parse("01/01/0001")
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
            Builders<OrderModel>.Update
                .Set("Status", model.Status)
                .Set("CompleteTime", model.CompleteTime)
        );
      }
      catch (Exception ex)
      {

        throw ex;
      }
    }

    public async Task UpdateMenu(List<MenuItem> model)
    {
      try
      {
        foreach (var x in model)
        {
          await context.MenuItems.UpdateOneAsync(
              Builders<MenuItem>.Filter.Eq("Id", x.Id),
              Builders<MenuItem>.Update
                  .Set("Active", x.Active)
          );
        }
      }
      catch (Exception ex)
      {

        throw ex;
      }
    }
  }
}
