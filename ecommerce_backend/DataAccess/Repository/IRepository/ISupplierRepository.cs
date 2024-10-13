using ecommerce_backend.Dtos.Supplier;
using ecommerce_backend.Models;

namespace ecommerce_backend.DataAccess.Repository.IRepository
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        public Task<Supplier> Update(int id, UpdateSupplierDtos obj);
        public  Task<Supplier> UpdateStatus(int id);
        public Task<Supplier> GetById(int id);
    }
}
