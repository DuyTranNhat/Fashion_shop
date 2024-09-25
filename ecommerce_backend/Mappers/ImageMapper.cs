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

        public static ImageDto ToImageDto(this Image image,int idVariant)
        {
            return new ImageDto
            {
                VariantId = idVariant,
                ImageUrl = image.ImageUrl
            };
        }
    }
}
