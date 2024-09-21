using ecommerce_backend.Models;

namespace ecommerce_backend.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Category>
    {
        void Update(Category obj);
    }
}
