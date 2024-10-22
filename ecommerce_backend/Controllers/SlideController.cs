using ecommerce_backend.Dtos.Slide;
using ecommerce_backend.Exceptions;
using ecommerce_backend.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SlideController : Controller
    {
        private readonly ISlideService _slideService;   

        public SlideController(ISlideService slideService)
        {
            _slideService = slideService;
        }

      
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            try
            {
                var slideDtos = await _slideService.SearchAsync(keyword);
                return Ok(slideDtos);
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            } catch (NoContentException)
            {
                return NoContent();
            }
        }
        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] string status)
        {
            try
            {
                var slides = await _slideService.FilterAsync(status);
                return Ok(slides);
            } catch (NoContentException)
            {
                return NoContent();
            }
        }

        [HttpPut]
        [Route("updateStatus/{id:int}")]
        public async Task<IActionResult> UpdateStatus([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var slide = await _slideService.UpdateStatusAsync(id);
                return Ok(slide);
            } catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SlideRequestDto slideDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newSlide = await _slideService.CreateAsync(slideDto);
            return CreatedAtAction(nameof(GetById), new { id = newSlide.SlideId }, new { message = "Slide created successfully" });
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateSlideDto slideDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingSlide = await _slideService.UpdateAsync(id, slideDto);
                return Ok(existingSlide);
            } catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            } 
        }




        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var slide = await _slideService.GetByIdAsync(id);
                return Ok(slide);
            } catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var slides = await _slideService.GetAllAsync();
            return Ok(slides);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _slideService.DeleteAsync(id);
                return NoContent();
            } catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }            
        }
    }
}
