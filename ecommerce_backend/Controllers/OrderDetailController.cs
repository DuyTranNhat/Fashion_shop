using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.OrderDetail;
using ecommerce_backend.Mappers;
using ecommerce_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderDetailController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Lấy tất cả chi tiết hóa đơn
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var orderdetails = _unitOfWork.OrderDetail.GetAll().ToList();
            return Ok(orderdetails);
        }

        // Lấy chi tiết hóa đơn theo id
        // tạo chi tiết hóa đơn
        [HttpPost]
        [Authorize(Roles = "customer")]
        public async Task<IActionResult> Create([FromBody] CreateOrderDetailDto orderDetailDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (_unitOfWork.Order.Get(o => o.OrderId == orderDetailDto.OrderId) == null) return NotFound(new {message = "Order not found"});
            if (_unitOfWork.Variant.Get(v => v.VariantId == orderDetailDto.VariantId) == null) return NotFound(new { message = "Variant not found" });
            var orderdetailModel = orderDetailDto.ToOrderDetailFromCreate();
            _unitOfWork.OrderDetail.Add(orderdetailModel);
            _unitOfWork.Save();
            return Ok(orderdetailModel);
        }

        // Xóa chi tiết hóa đơn
        [HttpDelete]
        [Authorize(Roles = "customer")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var orderdetailModel = _unitOfWork.OrderDetail.Get(o => o.OrderDetailId == id);
            if (orderdetailModel == null) return NotFound();
            _unitOfWork.OrderDetail.Remove(orderdetailModel);
            _unitOfWork.Save();
            return Ok(new { message = "Order detail deleted successfully." });
        }
    }
}