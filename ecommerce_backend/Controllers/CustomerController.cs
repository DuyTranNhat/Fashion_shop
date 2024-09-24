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
using ecommerce_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ecommerce_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        // private readonly SignInManager

        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private string GenerateJwtToken(Customer customer)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, customer.CustomerId.ToString()),
            new Claim(ClaimTypes.Email, customer.Email)
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Lấy tất cả các khách hàng
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = _unitOfWork.Customer.GetAll().ToList();
            return Ok(customers);
        }

        // lấy khách hàng theo id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var customer = await _unitOfWork.Customer.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer.ToCustomerDto());
        }

        // tìm kiếm khách hàng theo từ khóa 
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            var customers = await _unitOfWork.Customer.SearchAsync(keyword);
            if (customers == null || !customers.Any())
            {
                return NotFound();
            }
            return Ok(customers);
        }

        // tạo khách hàng
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateCustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customerModel = customerDto.ToCustomerFromCreateDto();
            _unitOfWork.Customer.Add(customerModel);
            _unitOfWork.Save();
            return Ok(customerModel);
        }

        // đăng nhập
        // [HttpPost("login")]
        // public async Task<IActionResult> Login(LoginCustomerDto logindto)
        // {
        //     if(!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }
        //     var customerModel = await _unitOfWork.Customer.
        // }

        // cập nhật thông tin khách hàng
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customerModel = await _unitOfWork.Customer.UpdateAsync(id, customerDto);
            if (customerModel == null)
            {
                return NotFound();
            }
            return Ok(customerModel.ToCustomerDto());
        }


    }
}