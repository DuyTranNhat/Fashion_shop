using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_backend.Dtos.Customer;
using ecommerce_backend.Models;

namespace ecommerce_backend.DataAccess.Repository.IRepository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        public Task<Customer> GetByIdAsync(int id);

        public Task<Customer> UpdateAsync(int id, UpdateCustomerDto customerDto);

        public Task<IEnumerable<Customer>> SearchAsync(string keyword);
        public Task<Customer> AuthenticateAsync(string email, string password);
    }
}