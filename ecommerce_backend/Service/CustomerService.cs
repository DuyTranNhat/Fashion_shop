using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Customer;
using ecommerce_backend.Mappers;
using ecommerce_backend.Models;
using ecommerce_backend.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_backend.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            var customerModel = _unitOfWork.Customer.Get(customer => customer.CustomerId == id);
            if (customerModel == null) throw new BadHttpRequestException("Not found a customer id");
            return customerModel.ToCustomerDto();
        }
    }
}
