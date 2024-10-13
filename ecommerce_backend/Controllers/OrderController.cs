using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_backend.DataAccess.Repository;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Order;
using ecommerce_backend.Mappers;
using ecommerce_backend.Models;
using ecommerce_backend.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork, IOrderService orderService, ICartService cartService, ICustomerService customerService)
        {
            _orderService = orderService;
            _unitOfWork = unitOfWork;
        }

        // Lấy tất cả hóa đơn
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var orders = await _orderService.GetAllOrderAsync();
            return Ok(orders);
        }


        // Lấy hóa đơn theo id kèm theo chi tiết hóa đơn
        [HttpGet("orderdetails/{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetByIdWithOrderDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var order = await _orderService.GetByIdWithOrderDetailAsync(id);
                return Ok(order);
            }
            catch (BadHttpRequestException ex) {
                return BadRequest(ex.Message);
            }
        }

        // checkout
        [HttpGet("checkout/{customerId:int}")]
        [Authorize(Roles = "customer")]
        public async Task<IActionResult> CheckOut([FromRoute] int customerId)
        {
            try
            {
                var checkoutDto = _orderService.CheckOut(customerId);
                return Ok(checkoutDto);
            }
            catch (BadHttpRequestException ex) {
                return BadRequest(ex.Message);
            }

        }


        //// Tạo một hóa đơn
        //[HttpPost]
        //[Authorize(Roles = "customer")]
        //public async Task<IActionResult> Create([FromBody] CreateOrderDto orderDto)
        //{
        //    if(!ModelState.IsValid) return BadRequest(ModelState);
        //    try
        //    {
        //        var order = _orderService.CreateOrderAsync(orderDto);
        //        return CreatedAtAction(nameof(GetAll), new { id = order.OrderId }, orderModel);
        //    } catch (BadHttpRequestException ex) {
        //        return BadRequest(ex.Message);
        //    }
        //}

        // Cập nhật trạng thái đơn hàng
        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> PayOrder([FromRoute] int id, [FromBody] UpdateOrderDto orderDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var orderModel = _unitOfWork.Order.UpdateStatusAsync(id, orderDto);
            if( orderModel == null) return NotFound("Order not found");
            return Ok(orderModel);
        }
    }
}