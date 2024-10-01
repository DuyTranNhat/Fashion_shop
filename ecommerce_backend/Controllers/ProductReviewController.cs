using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.ProductReview;
using ecommerce_backend.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductReviewController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductReviewController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // lấy tất cả các preview
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var productReviews = _unitOfWork.ProductReview.GetAll().ToList();
            return Ok(productReviews);
        }

        // lấy preview theo id
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var productReviewModel = _unitOfWork.ProductReview.Get(p => p.ReviewId == id);
            if (productReviewModel == null) return NotFound();
            return Ok(productReviewModel.ToProductReviewDto());
        }

        // tạo một preview
        [Authorize(Roles = "customer")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductReviewDto productReviewDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (_unitOfWork.Customer.Get(c => c.CustomerId == productReviewDto.CustomerId) == null) return NotFound("Customer not found");
            if (_unitOfWork.Product.Get(p => p.ProductId == productReviewDto.ProductId) == null) return NotFound("Product not found");
            var productReviewModel = productReviewDto.ToProductReviewFromCreateDto();
            _unitOfWork.ProductReview.Add(productReviewModel);
            _unitOfWork.Save();
            return Ok(productReviewModel);
        }

        // cập nhật preview
        [Authorize(Roles = "customer")]
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductReviewDto productReviewDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var productReviewModel = await _unitOfWork.ProductReview.UpdateAsync(id, productReviewDto);
            if (productReviewModel == null) return NotFound();
            return Ok(productReviewModel.ToProductReviewDto());
        }

        // xóa preview
        [Authorize(Roles = "customer")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var productReviewModel = _unitOfWork.ProductReview.Get(p => p.ReviewId == id);
            if (productReviewModel == null) return NotFound();
            _unitOfWork.ProductReview.Remove(productReviewModel);
            _unitOfWork.Save();
            return Ok(new {message = "Product review deleted successfully."});
        }
    }
}