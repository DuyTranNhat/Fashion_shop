using ecommerce_backend.Dtos.Customer;

namespace ecommerce_backend.Service.IService
{
    public interface ICustomerService
    {
        public Task<CustomerDto> GetByIdAsync(int id);
    }
}
