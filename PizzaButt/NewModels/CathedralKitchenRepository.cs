using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaButt.NewModels
{
    public class CathedralKitchenRepository : ICathedralKitchenRepository
    {
        private readonly CathedralKitchenContext context;

        public CathedralKitchenRepository()
        {
            context = new CathedralKitchenContext();
        }

        public IEnumerable<MenuItem> GetMenuItems()
        {
            try
            {
                var result = (from r in context.MenuItem select r);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<MenuItem> GetActiveMenuItems()
        {
            try
            {
                var result = (from r in context.MenuItem where r.Active == true select r);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MenuItem GetMenuItem(long id)
        {
            try
            {
                var result = (from r in context.MenuItem where r.Id == id select r).FirstOrDefault();
                return result;
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

        public void CreateMenuItem(MenuItem model)
        {
            try
            {
                context.MenuItem.Add(model);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string SendOrder(Order model)
        {
            try
            {
                var CreateTime = DateTime.Now;
                model.CreateTime = CreateTime;
                model.OrderStatusId = context.OrderStatus.SingleOrDefault(x => x.Status == "Pending").Id;
                context.Order.Add(model);
                return model.Id.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Order GetOrder(long id)
        {
            //ObjectId internalId = GetInternalId(id);
            try
            {
                var result = (from r in context.Order where r.Id == id select r).FirstOrDefault();
                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Order> GetOrders()
        {
            try
            {
                return context.Order;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void CompleteOrder(Order model)
        {
            try
            {
                var result = context.Order.SingleOrDefault(x => x.Id == model.Id);
                {
                    result.OrderStatusId = context.OrderStatus.SingleOrDefault(x => x.Status == "Complete").Id;
                    result.CompleteTime = DateTime.UtcNow;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void UpdateMenu(List<MenuItem> model)
        {
            try
            {
                foreach (var x in model)
                {
                    var result = context.MenuItem.SingleOrDefault(i => i.Id == x.Id);
                    result.Active = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
