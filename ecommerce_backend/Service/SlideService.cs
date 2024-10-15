using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Slide;
using ecommerce_backend.Exceptions;
using ecommerce_backend.Mappers;
using ecommerce_backend.Models;
using ecommerce_backend.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_backend.Service
{
    public class SlideService : ISlideService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ImageService _imageService;
        public SlideService(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, ImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _imageService = imageService;
        }
        public async Task<IEnumerable<SlideDto>> SearchAsync(string keyword)
        {
            keyword = keyword.Trim();
            if (string.IsNullOrWhiteSpace(keyword)) throw new BadHttpRequestException("Invalid keyword");
            var slideModels = _unitOfWork.Slide.handleSearch(keyword);
            if (slideModels == null) throw new NoContentException("No Content");
            var slideDtos = slideModels.Select(item => item.ToSlideDto());
            return slideDtos;
        }
        public async Task<IEnumerable<SlideDto>> FilterAsync(string status)
        {
            var slideModels = _unitOfWork.Slide.GetAll(x => x.Status == Boolean.Parse(status));
            if (slideModels.Count() == 0)
                throw new NoContentException("No Content");
            var slideDtos = slideModels.Select(item => item.ToSlideDto());
            return slideDtos;
        }

        public async Task<SlideDto> UpdateStatusAsync(int id)
        {
            var slide = _unitOfWork.Slide.UpdateStatus(id);
            if (slide == null) throw new NotFoundException("Not found a slide id");
            _unitOfWork.Save();
            return slide.ToSlideDto();
        }

        public async Task<Slide> CreateAsync(SlideRequestDto slideDto)
        {
            string imageUrl = null;
            if (slideDto.ImageFile != null)
            {
                _imageService.setDirect("images/slides");
                imageUrl = await _imageService.HandleImageUpload(slideDto.ImageFile);
            }
            var newSlide = slideDto.ToSlideFromCreate(imageUrl);
            _unitOfWork.Slide.Add(newSlide);
            _unitOfWork.Save();
            return newSlide;
        }

        public async Task<Object> UpdateAsync(int id, UpdateSlideDto slideDto)
        {
            var existingSlide = _unitOfWork.Slide.Get(s => s.SlideId == id);
            if (existingSlide == null) throw new NotFoundException("Not found a slide id");

            string imageUrl = existingSlide.Image;
            if (slideDto.ImageFile != null)
            {
                _imageService.setDirect("images/slides");
                imageUrl = await _imageService.HandleImageUpload(slideDto.ImageFile);
                _imageService.DeleteOldImage(existingSlide.Image);
            }

            _unitOfWork.Slide.Update(id, slideDto, imageUrl);
            _unitOfWork.Save();

            return new { message = "Slide updated successfully", slideId = existingSlide.SlideId };
        }

        public async Task<SlideDto> GetByIdAsync(int id)
        {
            var slide = _unitOfWork.Slide.Get(s => s.SlideId == id);
            if (slide == null) throw new NotFoundException("Slide not found");
            return slide.ToSlideDto();
        }

        public async Task<IEnumerable<SlideDto>> GetAllAsync()
        {
            var slides = _unitOfWork.Slide.GetAll().Where(s => s.Status).ToList();
            var slideDtos = slides.Select(slide => slide.ToSlideDto()).ToList();
            return slideDtos;
        }

        public async Task DeleteAsync(int id)
        {
            var existingSlide = _unitOfWork.Slide.Get(s => s.SlideId == id);
            if (existingSlide == null) throw new NotFoundException("Slide not found");
            _imageService.DeleteOldImage(existingSlide.Image);
            _unitOfWork.Slide.Remove(existingSlide);
            _unitOfWork.Save();
        }
    }
}
