using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Customer;
using ecommerce_backend.Exceptions;
using ecommerce_backend.Mappers;
using ecommerce_backend.Models;
using ecommerce_backend.Service.IService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_backend.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ImageService _imageService;

        public CustomerService(IUnitOfWork unitOfWork, ImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
        }
        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customerModel = _unitOfWork.Customer.GetAll();
            return customerModel.Select(customer => customer.ToCustomerDto());
        }
        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            var customerModel = _unitOfWork.Customer.Get(customer => customer.CustomerId == id) 
                ?? throw new BadHttpRequestException("Not found a customer id");
            return customerModel.ToCustomerDto();
        }

        public async Task<IEnumerable<CustomerDto>> SearchAsync(string keyword)
        {
            var customers = await _unitOfWork.Customer.SearchAsync(keyword);
            if (customers == null || customers.Count() ==0)
                 throw new NoContentException("No content");
            return customers.Select(customer=>customer.ToCustomerDto());
        }

        public async Task<Customer> updateProfileAsync(int id, UpdateCustomerDto customerDto)
        {
            var customerExisting = _unitOfWork.Customer.Get(customer => customer.CustomerId == id)
                ?? throw new BadHttpRequestException("Customer not found");
            string imageUrl = customerExisting.ImageUrl;
            if (customerDto.Image != null)
            {
                _imageService.setDirect("images/customerAvar");
                imageUrl = await _imageService.HandleImageUpload(customerDto.Image);
                _imageService.DeleteOldImage(customerExisting.ImageUrl);
            }


            await _unitOfWork.Customer.UpdateAsync(id, customerDto, imageUrl);
            return customerExisting;
        }
    }
}
