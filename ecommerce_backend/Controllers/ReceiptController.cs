using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Receipt;
using ecommerce_backend.Dtos.Slide;
using ecommerce_backend.Mappers;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

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
            var receipts = _unitOfWork.Receipt.GetAll(includeProperties: "ReceiptDetails");
            var receiptDtos = receipts.Select(item => item.ToReceiptDto());
            return Ok(receiptDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var receiptModel = _unitOfWork.Receipt.Get(item => item.ReceiptId == id, "ReceiptDetails");
            if (receiptModel == null) return NotFound();
            return Ok(receiptModel.ToReceiptDto());
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            keyword = keyword.Trim();
            if (string.IsNullOrWhiteSpace(keyword)) return BadRequest();
            var receiptModels = _unitOfWork.Receipt.handleSearch(keyword);
            if (receiptModels == null) return NoContent();
            var receiptDtos = receiptModels.Select(item => item.ToReceiptDto());
            return Ok(receiptDtos);
        }
        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] string status)
        {
            var receipts = _unitOfWork.Receipt.GetAll(x=>x.Status == status, includeProperties: "ReceiptDetails");
            var receiptDtos = receipts.Select(item => item.ToReceiptDto());
            return Ok(receiptDtos);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReceiptDto receiptDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var receiptModel = receiptDto.ToReceiptFromCreate();
            _unitOfWork.Receipt.Add(receiptModel);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetById), new { id = receiptModel.ReceiptId }, receiptModel.ToReceiptDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateReceiptDto receiptDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var receiptModel = _unitOfWork.Receipt.Update(id, receiptDto);
            if (receiptModel == null) return NotFound("Không tìm thấy receipt hoặc receipt không thể update");
            _unitOfWork.Save();
            return Ok(receiptModel.ToReceiptDto());
        }

        [HttpPut("updateStatus/{id:int}")]
        public async Task<IActionResult> ChangeStauts([FromRoute] int id,[FromBody] string status)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var receiptModel = _unitOfWork.Receipt.UpdateStatus(id, status);
            if (receiptModel == null) return NotFound("Không tìm thấy receipt hoặc không thể cập nhật trạng thái receipt");
            _unitOfWork.Save();
            return Ok(receiptModel.ToReceiptDto());
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var receiptModel = _unitOfWork.Receipt.Get(r => r.ReceiptId == id);
            if (receiptModel == null) return NotFound();
            if (receiptModel.Status != "Pending" && receiptModel.Status != "Expired") return BadRequest("Chỉ xóa được receipt đang chờ hoặc là hết hạn");
            _unitOfWork.Receipt.Remove(receiptModel);
            _unitOfWork.Save();
            return NoContent();
        }

    }
}
