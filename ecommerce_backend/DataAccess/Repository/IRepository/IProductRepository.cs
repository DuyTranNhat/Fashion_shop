using ecommerce_backend.Dtos.Product;
using ecommerce_backend.Models;

namespace ecommerce_backend.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> Edit(int id,updateProductDto obj);
    }
}
