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
        public Task<Customer> UpdateAsync(int id, UpdateCustomerDto customerDto);
        public Task<IEnumerable<Customer>> SearchAsync(string keyword);
    }
}