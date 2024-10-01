using ecommerce_backend.DataAccess.Repository;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Image;
using ecommerce_backend.Dtos.NewFolder;
using ecommerce_backend.Dtos.Slide;
using ecommerce_backend.Dtos.Variant;
using ecommerce_backend.Mappers;
using ecommerce_backend.Models;
using ecommerce_backend.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VariantController : Controller
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ImageService _imageService;

        public VariantController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, ImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _imageService = imageService;
        }

        //GetAll
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var variants = _unitOfWork.Variant.GetAll(includeProperties: "Images,Values.Attribute");

            var variantDtos = variants.Select(item => item.ToGetVariantDto());

            return Ok(variantDtos);
        }

        //GetAllByStatus
        [HttpGet("status")]
        public async Task<IActionResult> GetAllByStatus(string status)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var variants = _unitOfWork.Variant.GetAll(item => item.Status == status, includeProperties: "Images,Values.Attribute").ToList();

            var variantDtos = variants.Select(item => item.ToGetVariantDto());
            if (variantDtos == null)
                return NotFound();

            return Ok(variantDtos);
        }

        //GetById
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingVariant = _unitOfWork.Variant.Get(item => item.VariantId == id, includeProperties: "Images,Values.Attribute");

            if (existingVariant == null)
                return NotFound();

            return Ok(existingVariant.ToGetVariantDto());
        }

        //Create
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]CreateVariantDto obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            /*string path = Path.Combine("..", "ecommerce_frontend", "public", "assets", "imgs", "sliders");*/
            var listImage = await ImageMapper.UploadListImages("Assets\\Images\\ProductImage",obj.listFile);

            List<CreateImageDto> listCreateImageDto = new List<CreateImageDto>();
            if (obj.listFile!=null)
            {
                _imageService.setDirect("images/variants");
                var listImagePath = obj.listFile.Select(async file =>
                {
                    var filePath = await _imageService.HandleImageUpload(file, null);
                    return new CreateImageDto { ImageUrl = filePath };
                });
                var tmp = await Task.WhenAll(listImagePath);
                listCreateImageDto = tmp.Select(x => new CreateImageDto { ImageUrl = x.ImageUrl }).ToList();
            }

            Variant variantModel = obj.ToVariantFromCreateDto(listCreateImageDto);
            _unitOfWork.Variant.Add(variantModel);
            _unitOfWork.Value.CreateVariantValue(variantModel, obj.Values);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetById), new { id = variantModel.VariantId }, variantModel.ToGetVariantDto());
        }

        //Edit variant 
        [HttpPut("/edit/{id:int}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] UpdateVariantDto updateVariant)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            List<UpdateImageDto> listCreateImageDto = new List<UpdateImageDto>();
            if (updateVariant.listFile != null)
            {
                _imageService.setDirect("images/variants");
                var listImagePath = updateVariant.listFile.Select(async file =>
                {
                    var filePath = await _imageService.HandleImageUpload(file, null);
                    return new CreateImageDto { ImageUrl = filePath };
                });
                var tmp = await Task.WhenAll(listImagePath);
                listCreateImageDto = tmp.Select(x => new UpdateImageDto { ImageUrl = x.ImageUrl }).ToList();
            }

            var variantModel = await _unitOfWork.Variant.Edit(id, updateVariant, listCreateImageDto);
            if (variantModel == null)
                return NotFound();
            return Ok(variantModel.ToGetVariantDto());
        }

        [HttpPut("updateStatus/{id:int}")]
        public async Task<IActionResult> ChangeStauts([FromRoute] int id, [FromBody] string status)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var variantModel = _unitOfWork.Variant.UpdateStatus(id, status);
            if (variantModel == null) return NotFound("Không tìm thấy Variant");
            _unitOfWork.Save();
            return Ok(variantModel.ToGetVariantDto());
        }

    }
}
