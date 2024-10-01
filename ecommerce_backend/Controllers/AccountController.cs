using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Customer;
using ecommerce_backend.Mappers;
using ecommerce_backend.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ecommerce_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public AccountController(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        // tạo khách hàng
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateCustomerDto customerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var customerModel = _unitOfWork.Customer.Get(customer => customer.Email == customerDto.Email);
            if(customerModel != null) return BadRequest("User already exists");
            customerModel = customerDto.ToCustomerFromCreateDto();
            string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(customerModel.Password, 13);
            customerModel.Password = hashedPassword;
            _unitOfWork.Customer.Add(customerModel);
            _unitOfWork.Save();
            return Ok(customerModel);
        }

        // Khách hàng đăng nhập
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginCustomerDto customerDto)
        {
            if (customerDto == null) return BadRequest("Invalid login data.");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var customerModel = _unitOfWork.Customer.Get(customer => customer.Email == customerDto.Email);
            if (customerModel == null) return NotFound();
            if (!BCrypt.Net.BCrypt.EnhancedVerify(customerDto.Password, customerModel.Password)) return Unauthorized("Invalid password.");
            string token = _tokenService.CreateToken(customerModel, customerDto.Role);
            return Ok(new
            {
                customerDto.Email,
                customerDto.Role,
                token
            });
        }
    }
}