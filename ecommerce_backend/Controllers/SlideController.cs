using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Product;
using ecommerce_backend.Dtos.Slide;
using ecommerce_backend.Mappers;
using ecommerce_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using static ecommerce_backend.PublicClasses.UploadHandler;

namespace ecommerce_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SlideController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SlideController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var slides = _unitOfWork.Slide.GetAll();
            var slideDtos = slides.Select(item => item.ToSlideDto());
            return Ok(slideDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var slideModel = _unitOfWork.Slide.Get(item => item.SlideId == id);
            if (slideModel == null) return NotFound();
            return Ok(slideModel.ToSlideDto());
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            keyword = keyword.Trim();
            if (string.IsNullOrWhiteSpace(keyword)) return BadRequest();
            var slideModels = _unitOfWork.Slide.handleSearch(keyword);
            if (slideModels == null) return NoContent();
            var slideDtos = slideModels.Select(item => item.ToSlideDto());
            return Ok(slideDtos);
        }
        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] string status)
        {
            var slideModels = _unitOfWork.Slide.GetAll(x => x.Status == Boolean.Parse(status));
            var slideDtos = slideModels.Select(item => item.ToSlideDto());
            return Ok(slideDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateSlideDto slideDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = handleUpload(slideDto.Image);
            if (!Path.Exists(result)) return BadRequest(result);
            var slideModel = slideDto.ToSlideFromCreate(result);
            _unitOfWork.Slide.Add(slideModel);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetById), new { id = slideModel.SlideId }, slideModel);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] UpdateSlideDto slideDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var slideModel = _unitOfWork.Slide.Update(id, slideDto);
            if (slideModel == null) return NotFound();
            _unitOfWork.Save();
            return Ok(slideModel.ToSlideDto());
        }

        [HttpPut]
        [Route("updateStatus/{id:int}")]
        public async Task<IActionResult> UpdateStatus([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var slide = _unitOfWork.Slide.UpdateStatus(id);
            if (slide == null) return NotFound();
            _unitOfWork.Save();
            return Ok(slide.ToSlideDto());
        }
    }
}
