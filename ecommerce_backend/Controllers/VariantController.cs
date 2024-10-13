using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Image;
using ecommerce_backend.Dtos.NewFolder;
using ecommerce_backend.Dtos.Variant;
using ecommerce_backend.Mappers;
using ecommerce_backend.Models;
using ecommerce_backend.Service;
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

          [HttpGet("getByID/{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingVariant = _unitOfWork.Variant.Get(item => item.VariantId == id, includeProperties: "Images,VariantValues.Value");


            if (existingVariant == null)
                return NotFound();

            //return Ok(existingVariant.ToGetVariantDto(existingValue));
            return Ok(existingVariant.ToGetVariantDto());
        }

        [HttpPost("upload-images-variant")]
        public async Task<IActionResult> UploadImages([FromForm] ImageRequest imageRequest)
        {
            if (imageRequest.fileImages == null || imageRequest.fileImages.Count == 0)
            {
                return BadRequest("No images uploaded.");
            }


            foreach (var file in imageRequest.fileImages)
            {
                // Upload each image and get the URL
                var imageUrl = await _imageService.HandleImageUpload(file);

                _unitOfWork.Image.Add(new Image
                {
                    ImageUrl = imageUrl,
                    VariantId = imageRequest.VariantId
                });
            }
            _unitOfWork.Save();
            var reponseData = _unitOfWork.Image.GetAll(i => i.VariantId == imageRequest.VariantId);
            

            return Ok(reponseData);
        }


        [HttpDelete("delete-image/{imageId}/variant/{variantID}")]
        public async Task<IActionResult> DeleteImage([FromRoute] int imageId,[FromRoute] int variantID)
        {
            var image = _unitOfWork.Image.Get(i => i.ImageId == imageId); // Fetch the image by ID
            if (image == null)
            {
                return NotFound("Image not found.");
            }

            _imageService.setDirect("images/variants");
            _imageService.DeleteOldImage(image.ImageUrl); // Make sure to implement this method

            _unitOfWork.Image.Remove(image); // Remove the image from the database
            _unitOfWork.Save(); // Save changes to the database

            var reponseData = _unitOfWork.Image.GetAll(i => i.VariantId == variantID);

            return Ok(reponseData);
        }


        [HttpPost("create-variant")]
        public async Task<IActionResult> CreateVariant([FromBody] CreateVariantDto createVariantDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _unitOfWork.BeginTransaction(); // Bắt đầu transaction

                // Tạo một biến thể từ DTO
                Variant variantModel = createVariantDto.ToVariantFromCreateDto();
                _unitOfWork.Variant.Add(variantModel);
                _unitOfWork.Save();



                _imageService.setDirect("images/variants");

                // Liên kết giá trị với biến thể
                foreach (var value in createVariantDto.Values)
                {
                    _unitOfWork.VariantValue.Add(new VariantValue
                    {
                        ValueId = value.ValueId,
                        VariantId = variantModel.VariantId,
                    });
                }

                _unitOfWork.Save();

                _unitOfWork.Commit(); // Commit transaction
                return Ok(variantModel.VariantId);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback(); // Rollback transaction nếu có lỗi xảy ra
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("update-variant/{id:int}")]
        public async Task<IActionResult> UpdateVariant([FromRoute] int id, [FromBody] UpdateVariantDto updateVariantDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Lấy biến thể hiện tại từ cơ sở dữ liệu
            var existingVariant = _unitOfWork.Variant.Get(item => item.VariantId == id);

            if (existingVariant == null)
            {
                return NotFound();
            }

            var variant = await _unitOfWork.Variant.Edit(id, updateVariantDto);

            _unitOfWork.Save(); // Lưu các thay đổi

            _unitOfWork.Commit(); // Commit transaction

            return Ok(existingVariant.ToGetVariantDto()); // Trả về biến thể đã được cập nhật
        }

        [HttpGet("get-images-by-variant/{id:int}")]
        public async Task<IActionResult> GetImagesByVariantId([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Lấy tất cả hình ảnh của biến thể từ cơ sở dữ liệu
            var images = _unitOfWork.Image.GetAll(i => i.VariantId == id);

            if (images == null || !images.Any())
            {
                return NotFound("No images found for the given variant.");
            }

            // Chuyển đổi danh sách hình ảnh sang danh sách ImageDto
            var imageDtos = images.Select(img => img.ToVariantImageDto()).ToList();

            return Ok(imageDtos);
        }


        [HttpGet("get-all-variants")]
        public async Task<IActionResult> GetAllVariants()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Lấy tất cả các biến thể từ cơ sở dữ liệuf b
            var variants = _unitOfWork.Variant.GetAll(includeProperties: "Images,VariantValues.Value");

            if (variants == null || !variants.Any())
            {
                return NotFound("No variants found.");
            }

            // Chuyển đổi danh sách biến thể sang danh sách VariantDto
            var variantDtos = variants.Select(variant => variant.ToGetVariantDto()).ToList();

            return Ok(variantDtos);
        }

    }
}
