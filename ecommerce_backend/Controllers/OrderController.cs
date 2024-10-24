using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_backend.DataAccess.Repository;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Order;
using ecommerce_backend.Exceptions;
using ecommerce_backend.Mappers;
using ecommerce_backend.Models;
using ecommerce_backend.Service;
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
        private readonly PaypalService _paypalService;
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork, IOrderService orderService, ICartService
            cartService, ICustomerService customerService, PaypalService paypalService)
        {
            _paypalService = paypalService;
            _orderService = orderService;
            _unitOfWork = unitOfWork;
        }

        // Lấy tất cả hóa đơn
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(int customerId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var orders = await _orderService.GetAllOrderAsync(customerId);
            return Ok(orders);
        }


        // Lấy hóa đơn theo id kèm theo chi tiết hóa đơn
        [HttpGet("getByID/{id:int}")]
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


        [Authorize]
        [HttpPost("Checkout/customerID/{idCustomer:int}")]
        public async Task<IActionResult> Checkout([FromRoute] int idCustomer,[FromForm] CreateOrderDto createOrderDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var orders = await _orderService.createOrderAsync(idCustomer, createOrderDto);
                return Ok(orders.OrderId);
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


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

        [HttpPost("/checkout/create-paypal-order")]
        public async Task<IActionResult> CreatePaypalOrder(CancellationToken cancellationToken)
        {
            // Thông tin đơn hàng gửi qua Paypal
            var tongTien = "10"; // thêm ở đây
            var donViTienTe = "USD";
            var maDonHangThamChieu = "DH" + DateTime.Now.Ticks.ToString();

            try
            {
                var response = await _paypalService.CreateOrder(tongTien, donViTienTe, maDonHangThamChieu);

                return Ok(response);
            }
            catch (Exception ex)
            {
                var error = new { ex.GetBaseException().Message };
                return BadRequest(error);
            }
        }
        
        [HttpPost("/checkout/capture-paypal-order/{orderId}")]
        public async Task<IActionResult> CapturePaypalOrder(string orderID, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _paypalService.CaptureOrder(orderID);

                // Lưu database đơn hàng của mình

                return Ok(response);
            }
            catch (Exception ex)
            {
                var error = new { ex.GetBaseException().Message };
                return BadRequest(error);
            }
        }
    }
}