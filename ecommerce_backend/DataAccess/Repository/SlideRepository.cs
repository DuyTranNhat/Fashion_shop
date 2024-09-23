using ecommerce_backend.DataAccess.Data;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Slide;
using ecommerce_backend.Models;

namespace ecommerce_backend.DataAccess.Repository
{
    public class SlideRepository : Repository<Slide>, ISlideRepository
    {
        private readonly FashionShopContext _db;
        public SlideRepository(FashionShopContext db) : base(db)
        {
            _db = db;
        }
        public Slide? Update(int id, UpdateSlideDto obj)
        {
            var slide = _db.Slides.FirstOrDefault(s => s.SlideId == id);
            if (slide == null) return null;
            slide.Title = obj.Title;
            slide.Link = obj.Link;
            slide.Image = obj.Image;
            return slide;
        }
        public Slide? UpdateStatus(int id)
        {
            var slide = _db.Slides.FirstOrDefault(s => s.SlideId == id);
            if (slide == null) return null;
            slide.Status = !slide.Status;
            return slide;

        }
    }
}
