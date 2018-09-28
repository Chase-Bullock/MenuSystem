using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaButt.Models
{
    public class PizzaContext
    {
        private readonly IMongoDatabase _database = null;

        public PizzaContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<MenuItem> MenuItems => _database.GetCollection<MenuItem>("MenuItem");

        public IMongoCollection<OrderModel> Orders => _database.GetCollection<OrderModel>("Order");
    }
}
