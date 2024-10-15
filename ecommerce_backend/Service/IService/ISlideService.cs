using ecommerce_backend.Dtos.Slide;
using ecommerce_backend.Models;

namespace ecommerce_backend.Service.IService
{
    public interface ISlideService
    {
        public Task<IEnumerable<SlideDto>> SearchAsync(string keyword);
        public Task<IEnumerable<SlideDto>> FilterAsync(string status);
        public Task<SlideDto> UpdateStatusAsync(int id);
        public Task<Slide> CreateAsync(SlideRequestDto slideDto);
        public Task<Object> UpdateAsync(int id, UpdateSlideDto slideDto);
        public Task<SlideDto> GetByIdAsync(int id);
        public Task<IEnumerable<SlideDto>> GetAllAsync();
        public Task DeleteAsync(int id);
    }
}
