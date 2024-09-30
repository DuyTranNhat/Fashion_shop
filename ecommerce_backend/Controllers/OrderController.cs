using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Order;
using ecommerce_backend.Mappers;
using ecommerce_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Lấy tất cả hóa đơn
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var orders = _unitOfWork.Order.GetAll().ToList();
            return Ok(orders);
        }

        // Lấy hóa đơn theo Id
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var order = _unitOfWork.Order.Get(order => order.OrderId == id);
            if(order == null) return NotFound();
            return Ok(order.ToOrderDto());
        }

        // Lấy hóa đơn theo id kèm theo chi tiết hóa đơn
        [HttpGet("{id:int}/orderdetail")]
        [Authorize]
        public async Task<IActionResult> GetByIdWithOrderDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var orderModel = _unitOfWork.Order.Get(o => o.OrderId == id);
            if(orderModel == null) return NotFound();
            return Ok(orderModel.ToOrderDto());
        }

        // Tạo một hóa đơn
        [HttpPost]
        [Authorize(Roles = "customer")]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto orderDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            if(_unitOfWork.Customer.Get(c => c.CustomerId == orderDto.CustomerId) == null) return NotFound("Customer not found");
            var orderModel = orderDto.ToOrderFromCreate();
            _unitOfWork.Order.Add(orderModel);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetById), new {id = orderModel.OrderId}, orderModel);
        }
    }
}