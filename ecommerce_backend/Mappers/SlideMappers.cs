using ecommerce_backend.Dtos.Slide;
using ecommerce_backend.Models;
using static ecommerce_backend.PublicClasses.UploadHandler;
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
                Image = slide.Image,
                Status = slide.Status,
                Description = slide.Description
            };
        }
        public static Slide ToSlideFromCreate(this CreateSlideDto slidedto, string path)
        {
            return new Slide
            {
                Title = slidedto.Title,
                Link = slidedto.Link,
                Image = path,
                Description = slidedto.Description
            };
        }

    }
}
