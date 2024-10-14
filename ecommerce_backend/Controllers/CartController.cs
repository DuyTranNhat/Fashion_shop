using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Cart;
using ecommerce_backend.Mappers;
using ecommerce_backend.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_backend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/Cart")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartService _cartService;

        public CartController(IUnitOfWork unitOfWork, ICartService cartService)
        {
            _unitOfWork = unitOfWork;
            _cartService = cartService;
        }

        // Lấy tất cả giỏ hàng
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var carts = _unitOfWork.Cart.GetAll(includeProperties: "Variant.Images,Variant.VariantValues");
            var attributes = _unitOfWork.Attribute.GetAll(includeProperties: "Values");
            var cartsMapper = carts.Select(c => c.ToCartDto(attributes));
            return Ok(cartsMapper);
        }

        // Lấy giỏ hàng theo id
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var cartModel = _unitOfWork.Cart.Get(c => c.CartId == id, includeProperties: "Variant.Images,Variant.VariantValues");

            var attributes = _unitOfWork.Attribute.GetAll(includeProperties: "Values");

            if (cartModel == null) return NotFound("Cart item not found");
            return Ok(cartModel.ToCartDto(attributes));
        }

        // Lấy chi tiết giỏ hàng của một cus
        [HttpGet("getByCusId/{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetByCustomerId([FromRoute] int customerId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var cart = await _cartService.GetByCustomerIdAsync(customerId);
                return Ok(cart);
            }
            catch (BadHttpRequestException ex){
                return BadRequest(ex.Message);
            }
        }




        // Thêm một sản phẩm vào giỏ hàng
        [HttpPost("addtoCart")]
        public async Task<IActionResult> AddToCart([FromBody] CreateCartDto cartDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (_unitOfWork.Customer.Get(c => c.CustomerId == cartDto.CustomerId) == null) return NotFound(new { message = "Customer not found" });
            if (_unitOfWork.Variant.Get(v => v.VariantId == cartDto.VariantId) == null) return NotFound(new { message = "variant not found" });




            var existingCart = await _unitOfWork.Cart.UpdateAsync(cartDto);

            if (existingCart != null)
            {
                return CreatedAtAction(nameof(AddToCart), new { id = existingCart.CartId }, existingCart);
            }

            else
            {
                var cartModel = cartDto.ToCartFromCreate();
                _unitOfWork.Cart.Add(cartModel);
                _unitOfWork.Save();
                return CreatedAtAction(nameof(AddToCart), new { id = cartModel.CartId }, cartModel);
            }
        }

        // Xóa một sản phẩm trong giỏ hàng
        [HttpDelete("removeCartItem/{id:int}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var cartModel = _unitOfWork.Cart.Get(c => c.CartId == id);
            if (cartModel == null) return NotFound("Cart item not found");
            _unitOfWork.Cart.Remove(cartModel);
            _unitOfWork.Save();
            return Ok(new { message = "Remove successfully" });
        }

        [HttpPost("increaseQuantity/{cartId:int}")]
        public async Task<IActionResult> IncreaseQuantity([FromRoute] int cartId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                Cart? existingCart = await _unitOfWork.Cart.increaseQuantity(cartId);
                if (existingCart == null) return BadRequest("Item not found!");
                var attributes = _unitOfWork.Attribute.GetAll(includeProperties: "Values");
                return CreatedAtAction(nameof(GetById), new { id = existingCart.CartId }, existingCart.ToCartDto(attributes));
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }

        }


        [HttpPost("decreaseQuantity/{cartId:int}")]
        public async Task<IActionResult> DecreaseQuantity([FromRoute] int cartId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                Cart? existingCart = await _unitOfWork.Cart.decreaseQuantity(cartId);

                if (existingCart == null) return BadRequest("Item not found!");

                var attributes = _unitOfWork.Attribute.GetAll(includeProperties: "Values");

                return CreatedAtAction(nameof(GetById), new { id = existingCart.CartId }, existingCart.ToCartDto(attributes));

            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }
    }
}