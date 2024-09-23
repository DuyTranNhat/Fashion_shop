using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Receipt;
using ecommerce_backend.Dtos.Slide;
using ecommerce_backend.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceiptController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReceiptController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var receipts = _unitOfWork.Receipt.GetAll();
            var receiptDtos = receipts.Select(item => item.ToReceiptDto());
            return Ok(receiptDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var receiptModel = _unitOfWork.Receipt.Get(item => item.ReceiptId == id);
            if (receiptModel == null) return NotFound();
            return Ok(receiptModel.ToReceiptDto());
        }

        [HttpPost]
        public async Task<IActionResult> create([FromBody] CreateReceiptDto receiptDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var receiptModel = receiptDto.ToReceiptFromCreate();
            _unitOfWork.Receipt.Add(receiptModel);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetById), new { id = receiptModel.ReceiptId }, receiptDto);
        }

    }
}
