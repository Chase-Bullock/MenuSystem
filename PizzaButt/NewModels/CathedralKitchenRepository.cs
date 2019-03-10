using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using CathedralKitchen.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CathedralKitchen.NewModels
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

        public string CreateMenuItem(MenuItem model)
        {
            try
            {
                context.MenuItem.Add(model);
                context.SaveChanges();
                return model.Name.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public string SendOrder(long orderId)
        {
            try
            {
                var result = context.Order.SingleOrDefault(x => x.Id == orderId);
                var createTime = DateTime.UtcNow;
                result.CreateTime = createTime;
                result.UpdateTime = createTime;
                result.OrderStatusId = context.OrderStatus.SingleOrDefault(x => x.Status == "Pending").Id;
                context.Order.Add(result);
                context.SaveChanges();
                return orderId.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string StartOrder(long orderId)
        {
            try
            {
                var updateTime = DateTime.UtcNow;
                var result = context.Order.SingleOrDefault(x => x.Id == orderId);
                result.UpdateTime = updateTime;
                result.OrderStatusId = context.OrderStatus.SingleOrDefault(x => x.Status == "Started").Id;
                context.SaveChanges();
                return orderId.ToString();
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
                    var UpdateTime = DateTime.UtcNow;
                    result.OrderStatusId = context.OrderStatus.SingleOrDefault(x => x.Status == "Complete").Id;
                    result.CompleteTime = UpdateTime;
                    result.UpdateTime = UpdateTime;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string CancelOrder(long orderId)
        {
            try
            {
                var result = context.Order.SingleOrDefault(x => x.Id == orderId);
                {
                    var UpdateTime = DateTime.UtcNow;
                    result.OrderStatusId = context.OrderStatus.SingleOrDefault(x => x.Status == "Canceled").Id;
                    result.UpdateTime = UpdateTime;
                    context.SaveChanges();
                    return orderId.ToString();
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
                    result.Active = x.Active;
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void UpdateToppings(List<Topping> model)
        {
            try
            {
                foreach (var x in model)
                {
                    var result = context.Topping.SingleOrDefault(i => i.Id == x.Id);
                    result.Active = x.Active;
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
