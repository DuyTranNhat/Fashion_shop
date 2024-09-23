using ecommerce_backend.Models;

namespace ecommerce_backend.DataAccess.Repository.IRepository
{
    public interface IAttributeRepository : IRepository<Attribute>
    {
        void Update(Attribute obj);
    }
}
