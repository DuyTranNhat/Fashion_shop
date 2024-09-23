using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Product;
using ecommerce_backend.Dtos.Slide;
using ecommerce_backend.Mappers;
using ecommerce_backend.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<IActionResult> create([FromBody] CreateSlideDto slideDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var slideModel = slideDto.ToSlideFromCreate();
            _unitOfWork.Slide.Add(slideModel);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetById), new { id = slideModel.SlideId }, slideModel);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSlideDto slideDto)
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
