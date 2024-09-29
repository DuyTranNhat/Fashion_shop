using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Product;
using ecommerce_backend.Dtos.Slide;
using ecommerce_backend.Extension;
using ecommerce_backend.Mappers;
using ecommerce_backend.Models;
using ecommerce_backend.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SlideController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ImageService _imageService;

        public SlideController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, ImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _imageService = imageService;
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


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SlideRequestDto slideDto)
        {
            // Validate the incoming model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Handling the image file
            string imageUrl = null;
            if (slideDto.ImageFile != null)
            {
                imageUrl = await _imageService.HandleImageUpload(slideDto.ImageFile, null); // No existing image
            }

            // Create a new slide entity and save
            var newSlide = slideDto.ToSlideFromCreate(imageUrl);
            _unitOfWork.Slide.Add(newSlide);
            _unitOfWork.Save();

            return CreatedAtAction(nameof(GetById), new { id = newSlide.SlideId }, new { message = "Slide created successfully" });
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateSlideDto slideDto)
        {
            // Validate the incoming model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Find the existing slide
            var existingSlide = _unitOfWork.Slide.Get(s => s.SlideId == id);
            if (existingSlide == null)
            {
                return NotFound(new { message = "Slide not found" });
            }

            // Store the current image URL by default
            string imageUrl = existingSlide.Image;

            // Handling the image file if provided
            if (slideDto.ImageFile != null)
            {
                imageUrl = await _imageService.HandleImageUpload(slideDto.ImageFile, existingSlide.Image);
            }



            // Update the existing slide entity with the new data
            _unitOfWork.Slide.Update(id, slideDto, imageUrl);

            // Save the updated slide using the Unit of Work
            _unitOfWork.Save();

            return Ok(new { message = "Slide updated successfully", slideId = existingSlide.SlideId });
        }




        [HttpGet("getById/{id}")]
        public IActionResult GetById(int id)
        {
            // Fetch the slide from the database using the ID
            var slide = _unitOfWork.Slide.Get(s => s.SlideId == id);
            if (slide == null) // Check if slide exists and is active
            {
                return NotFound(new { message = "Slide not found" });
            }

            return Ok(slide.ToSlideDto());
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            // Fetch all slides from the database that are active
            var slides = _unitOfWork.Slide.GetAll().Where(s => s.Status).ToList();

            // Map the Slide entities to SlideDto
            var slideDtos = slides.Select(slide => slide.ToSlideDto()).ToList();

            return Ok(slideDtos);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Find the existing slide
            var existingSlide = _unitOfWork.Slide.Get(s => s.SlideId == id);
            if (existingSlide == null)
            {
                return NotFound(new { message = "Slide not found" });
            }

            // Delete the old image file if it exists
            _imageService.DeleteOldImage(existingSlide.Image);

            // Remove the slide from the database
            _unitOfWork.Slide.Remove(existingSlide);
            _unitOfWork.Save(); // Assuming SaveAsync is an async method in your unit of work

            return NoContent();
        }
    }
}
