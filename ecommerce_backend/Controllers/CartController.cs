using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Cart;
using ecommerce_backend.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_backend.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Lấy tất cả giỏ hàng
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var carts = _unitOfWork.Cart.GetAll();
            return Ok(carts);
        }

        // Lấy giỏ hàng theo id
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var cartModel = _unitOfWork.Cart.Get(c => c.CartId == id);
            if(cartModel == null) return NotFound("Cart item not found");
            return Ok(cartModel.ToCartDto());
        }

        // Thêm một sản phẩm vào giỏ hàng
        [HttpPost]
        [Authorize(Roles = "customer")]
        public async Task<IActionResult> AddToCart([FromBody] CreateCartDto cartDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if(_unitOfWork.Customer.Get(c => c.CustomerId == cartDto.CustomerId) == null) return NotFound(new {message = "Customer not found"});
            if(_unitOfWork.Variant.Get(v => v.VariantId == cartDto.VariantId) == null) return NotFound(new { message = "variant not found" });
            var cartModel = cartDto.ToCartFromCreate();
            _unitOfWork.Cart.Add(cartModel);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(AddToCart), new { id = cartModel.CartId }, cartModel);
        }

        // cập nhật giỏ hàng
        [HttpPut("{id:int}")]
        [Authorize(Roles = "customer")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCartDto cartDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var cartModel = await _unitOfWork.Cart.UpdateAsync(id, cartDto);
            if(cartModel == null) return NotFound("Cart item not found");
            return Ok(cartModel.ToCartDto());
        }

        // Xóa một sản phẩm trong giỏ hàng
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "customer")]
        public async Task<IActionResult> DeleteItem([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var cartModel = _unitOfWork.Cart.Get(c => c.CartId == id);
            if(cartModel == null) return NotFound("Cart item not found");
            _unitOfWork.Cart.Remove(cartModel);
            _unitOfWork.Save();
            return Ok(new { message = "Cart item deleted successfully." });
        }

        // Xóa toàn bộ giỏ hàng
        [HttpDelete("clearCart/{customerId:int}")]
        [Authorize(Roles = "customer")]
        public async Task<IActionResult> DeleteAll([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var customerModel = _unitOfWork.Customer.Get(c => c.CustomerId == id);
            if(customerModel == null) return NotFound("Customer not found"); 
            var cartItems = _unitOfWork.Cart.GetAll(c => c.CustomerId == id);
            _unitOfWork.Cart.RemoveRange(cartItems);
            _unitOfWork.Save();
            return Ok(new { message = "Cart cleared successfully." });
        }
    }
}