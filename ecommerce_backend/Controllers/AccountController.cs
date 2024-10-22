using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Customer;
using ecommerce_backend.Exceptions;
using ecommerce_backend.Mappers;
using ecommerce_backend.Service;
using ecommerce_backend.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ecommerce_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // tạo khách hàng
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateCustomerDto customerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var customerModel = await _accountService.Register(customerDto);
                return Ok(customerModel);
            } catch (BadHttpRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        // Khách hàng đăng nhập
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginCustomerDto customerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var customerModel = await _accountService.Login(customerDto);
                return Ok(customerModel);
            } catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            } catch (BadHttpRequestException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}