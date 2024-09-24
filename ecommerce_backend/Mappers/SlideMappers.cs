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
                Image = slide.Image,
                Status = slide.Status
            };
        }
        public static Slide ToSlideFromCreate(this CreateSlideDto slidedto)
        {
            return new Slide
            {
                Title = slidedto.Title,
                Link = slidedto.Link,
                Image = slidedto.Image
            };
        }
        public static Slide ToSlideFromUpdate(this UpdateSlideDto slidedto)
        {
            return new Slide
            {
                Title = slidedto.Title,
                Link = slidedto.Link,
                Image = slidedto.Image
            };
        }
    }
}
