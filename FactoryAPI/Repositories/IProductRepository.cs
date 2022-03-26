using FactoryAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FactoryAPI.Repositories
{
    public interface IProductRepository
    {
        Task Create(Product product);
        Task<List<Product>> Get();
        Task<Product> Get(string id);
        Task Remove(string id);
        Task Update(string id, Product product);
    }
}