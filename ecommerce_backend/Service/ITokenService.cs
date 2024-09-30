using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_backend.Dtos.Customer;
using ecommerce_backend.Models;

namespace ecommerce_backend.Service
{
    public interface ITokenService
    {
        public string CreateToken(Customer customerModel, string role);
    }
}