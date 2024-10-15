using ecommerce_backend.Dtos.Customer;
using ecommerce_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_backend.Service.IService
{
    public interface ICustomerService
    {
        public Task<CustomerDto> GetByIdAsync(int id);

        public Task<IEnumerable<CustomerDto>> GetAllAsync();

        public Task<IEnumerable<CustomerDto>> SearchAsync(string keyword);

        public Task<Customer> updateProfileAsync(int id, UpdateCustomerDto customerDto);
    }
}
