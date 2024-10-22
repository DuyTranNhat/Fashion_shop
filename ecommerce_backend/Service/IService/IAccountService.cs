using ecommerce_backend.Dtos.Customer;

namespace ecommerce_backend.Service.IService
{
    public interface IAccountService
    {
        public Task<Object> Register(CreateCustomerDto customerDto);
        public Task<Object> Login(LoginCustomerDto customerDto);
    }
}
