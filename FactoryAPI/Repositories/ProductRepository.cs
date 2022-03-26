using FactoryAPI.Database;
using FactoryAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FactoryAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly IMongoCollection<Product> _productsCollection;

        public ProductRepository(IOptions<DbConfig> dbConfig)
        {
            MongoClient mongoClient = new MongoClient(dbConfig.Value.ConnectionString);
            IMongoDatabase database = mongoClient.GetDatabase(dbConfig.Value.DatabaseName);
            _productsCollection = database.GetCollection<Product>(dbConfig.Value.CollectionName);
        }


        public async Task<List<Product>> Get()
        {
            return await _productsCollection.Find(x => true).ToListAsync();
        }

        public async Task<Product> Get(string id)
        {
            return await _productsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }


        public async Task Create(Product product)
        {
            await _productsCollection.InsertOneAsync(product);
            return;
        }


        public async Task Update(string id, Product product)
        {
            await _productsCollection.ReplaceOneAsync(x => x.Id == id, product);
            return;
        }


        public async Task Remove(string id)
        {
            await _productsCollection.DeleteOneAsync(x => x.Id == id);
            return;
        }



    }

}
