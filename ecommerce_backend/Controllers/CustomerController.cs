using System.Runtime.InteropServices;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Customer;
using ecommerce_backend.Dtos.Slide;
using ecommerce_backend.Mappers;
using ecommerce_backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ImageService _imageService;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerController(IUnitOfWork unitOfWork, ImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
        }

        // Lấy tất cả các khách hàng
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var customers = _unitOfWork.Customer.GetAll().ToList();
            return Ok(customers);
        }

        // lấy khách hàng theo id
        [HttpGet("GetById/{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var customerModel = _unitOfWork.Customer.Get(customer => customer.CustomerId == id);
            if (customerModel == null) return NotFound("Customer not found");
            return Ok(customerModel.ToCustomerDto());
        }

        [HttpPut("updateProfile/{id:int}")]
        public async Task<IActionResult> updateProfile([FromRoute] int id,[FromForm] UpdateCustomerDto customerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var customerExisting = _unitOfWork.Customer.Get(customer => customer.CustomerId == id);

            if (customerExisting == null) return NotFound("Customer not found");


            string imageUrl = customerExisting.ImageUrl;

            if (customerDto.Image != null)
            {
                _imageService.setDirect("images/customerAvar");
                imageUrl = await _imageService.HandleImageUpload(customerDto.Image); 
                _imageService.DeleteOldImage(customerExisting.ImageUrl);
            }


            await _unitOfWork.Customer.UpdateAsync(id, customerDto, imageUrl);

            return Ok(customerExisting);
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
            var customers = await _unitOfWork.Customer.SearchAsync(keyword);
            if (customers == null || !customers.Any()) return NotFound("Customer not found");
            return Ok(customers);
        }
    }
}