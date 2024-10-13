using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ecommerce_backend.DataAccess.Data;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Customer;
using ecommerce_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_backend.DataAccess.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly FashionShopContext _db;

        public CustomerRepository(FashionShopContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Customer>> SearchAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return await _db.Customers.ToListAsync();
            return await _db.Customers
                .Where(c =>
                    c.Name.Contains(keyword) ||
                    c.Email.Contains(keyword) ||
                    (c.Phone != null && c.Phone.Contains(keyword)) ||
                    (c.Address != null && c.Address.Contains(keyword))
                ).ToListAsync();
        }


        public async Task<Customer> UpdateAsync(int id, UpdateCustomerDto customerDto, string urlImage)
        {
            // Retrieve the existing customer by ID
            var existingCustomer = await _db.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);

            // Check if the customer exists
            if (existingCustomer == null)
                return null;

            // Update the customer's properties
            existingCustomer.Name = customerDto.Name ?? existingCustomer.Name;  // Only update if not null
            existingCustomer.Email = customerDto.Email ?? existingCustomer.Email;
            existingCustomer.Phone = customerDto.Phone ?? existingCustomer.Phone;
            existingCustomer.Address = customerDto.Address ?? existingCustomer.Address;

            // Only update the image URL if a new image is provided
            if (!string.IsNullOrEmpty(urlImage))
            {
                existingCustomer.ImageUrl = urlImage;
            }

            // Save the changes to the database
            await _db.SaveChangesAsync();

            // Return the updated customer
            return existingCustomer;
        }

    }
}