using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Customer;
using ecommerce_backend.Exceptions;
using ecommerce_backend.Mappers;
using ecommerce_backend.Service.IService;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ecommerce_backend.Service
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        public AccountService(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<System.Object> Register(CreateCustomerDto customerDto)
        {
            var customerModel = _unitOfWork.Customer.Get(customer => customer.Email == customerDto.Email);
            if (customerModel != null) throw new BadHttpRequestException("User Already Exist");
            customerModel = customerDto.ToCustomerFromCreateDto();
            string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(customerModel.Password, 13);
            customerModel.Password = hashedPassword;
            _unitOfWork.Customer.Add(customerModel);
            _unitOfWork.Save();
            string token = _tokenService.CreateToken(customerModel, customerModel.Role);
            return new
            {
                customerModel.CustomerId,
                customerModel.Email,
                customerModel.Name,
                customerModel.Role,
                token
            };
        }

        public async Task<System.Object> Login(LoginCustomerDto customerDto)
        {
            var customerModel = _unitOfWork.Customer.Get(customer => customer.Email == customerDto.Email);
            if (customerModel == null) throw new NotFoundException("Not Found");
            if (!BCrypt.Net.BCrypt.EnhancedVerify(customerDto.Password, customerModel.Password)) throw new BadHttpRequestException("Invalid Password");
            string token = _tokenService.CreateToken(customerModel, customerModel.Role);
            return new
            {
                customerModel.CustomerId,
                customerModel.Email,
                customerModel.Name,
                customerModel.Role,
                token
            };
        }
    }
}
