using System.Runtime.InteropServices;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Customer;
using ecommerce_backend.Dtos.Slide;
using ecommerce_backend.Exceptions;
using ecommerce_backend.Mappers;
using ecommerce_backend.Service;
using ecommerce_backend.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerService _customerService;

        public CustomerController(IUnitOfWork unitOfWork, ICustomerService customerService)
        {
            _unitOfWork = unitOfWork;
            _customerService = customerService;
        }

        // Lấy tất cả các khách hàng
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var customers = await _customerService.GetAllAsync();
            return Ok(customers);
        }

        // lấy khách hàng theo id
        [HttpGet("GetById/{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var customer =  await _customerService.GetByIdAsync(id);
                return Ok(customer);
            } catch (BadHttpRequestException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("updateProfile/{id:int}")]
        public async Task<IActionResult> updateProfile([FromRoute] int id,[FromForm] UpdateCustomerDto customerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var customerExisting = await _customerService.updateProfileAsync(id, customerDto);
                return Ok(customerExisting);
            } catch (BadHttpRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }



        // lấy khách hàng theo id kèm theo lịch sử mua hàng
        [Authorize]
        [HttpGet("orders/{id:int}")]
        public async Task<IActionResult> GetByIdWithOrders([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var customerModel = _unitOfWork.Customer.Get(customer => customer.CustomerId == id, includeProperties: "Orders");
            if (customerModel == null) return NotFound("Customer not found");
            return Ok(customerModel.ToCustomerDtoWithOrders());
        }

        // lấy khách hàng theo id kèm theo lịch sử preview
        [Authorize]
        [HttpGet("previews/{id:int}")]
        public async Task<IActionResult> GetByIdWithPreviews([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); 
            var customerModel = _unitOfWork.Customer.Get(customer => customer.CustomerId == id, includeProperties: "ProductReviews");
            if (customerModel == null) return NotFound("Customer not found");
            return Ok(customerModel.ToCustomerDtoWithPreviews());
        }

        // Lấy khách hàng theo id kèm theo giỏ hàng
        [Authorize]
        [HttpGet("carts/{id:int}")]
        public async Task<IActionResult> GetByIdWithCarts([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var customerModel = _unitOfWork.Customer.Get(customerModel => customerModel.CustomerId == id, includeProperties: "Carts");
            if (customerModel == null) return NotFound("Customer not found");
            return Ok(customerModel.ToCustomerDtoWithCarts());
        }

        // tìm kiếm khách hàng theo từ khóa 
        [Authorize(Roles = "admin")]
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            try
            {
                var customers = await _customerService.SearchAsync(keyword);
                return Ok(customers);
            } catch (NoContentException ex)
            {
                return NoContent();
            }
            
        }
    }
}