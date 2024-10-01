using ecommerce_backend.Dtos.Slide;
using ecommerce_backend.Models;
namespace ecommerce_backend.Mappers
{
    public static class SlideMappers
    {
        public static SlideDto ToSlideDto(this Slide slide)
        {
            return new SlideDto
            {
                SlideId = slide.SlideId,
                Title = slide.Title,
                Link = slide.Link,
                ImageUrl = slide.Image,  // Assuming this stores the image URL
                Description = slide.Description,
                Status = slide.Status
            };
        }
        public static Slide ToSlideFromCreate(this SlideRequestDto slideDto, string imageUrl)
        {
            return new Slide
            {
                Title = slideDto.Title,
                Link = slideDto.Link,
                Image = imageUrl,
                Description = slideDto.Description,
                Status = slideDto.Status // Assuming status is part of the DTO
            };
        }

    }
}
