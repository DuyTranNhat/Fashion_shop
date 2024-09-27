using ecommerce_backend.Dtos.Image;
using ecommerce_backend.Dtos.NewFolder;
using ecommerce_backend.Models;

namespace ecommerce_backend.Mappers
{
    public static class ImageMapper
    {
        public static Image ToImageModel(this CreateImageDto createImageDto,int VariantId)
        {
            
            return new Image
            {   
                VariantId = VariantId,
                ImageUrl = createImageDto.ImageUrl,
            };
        }
        public static Image ToModelFromUpdateImage(this UpdateImageDto updateImageDto,int variantId)
        {
            return new Image
            {
                VariantId = variantId,
                ImageUrl = updateImageDto.ImageUrl
            };
        }



        public static ImageDto ToImageDto(this Image image,int idVariant)
        {
            return new ImageDto
            {
                ImageId = image.ImageId,
                VariantId = idVariant,
                ImageUrl = image.ImageUrl
            };
        }

        public static UpdateImageDto ToUpdateImageDtoFromModel(this Image image)
        {
            return new UpdateImageDto
            {
                ImageUrl = image.ImageUrl
            };
        }

        public static async Task<List<CreateImageDto>> UploadImages(string folderPath, List<IFormFile> files)
        {
            List<CreateImageDto> listImage = new List<CreateImageDto>();
            if (files == null || files.Count == 0)
            {
                return listImage;

            }

            var uploadPath = Path.Combine(folderPath, "ProductImage");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var filePath = Path.Combine(uploadPath, file.FileName);

                    listImage.Add(new CreateImageDto { ImageUrl = filePath });
                    // Lưu file vào thư mục 'uploads'
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }

            return listImage;
        }
    }
}
