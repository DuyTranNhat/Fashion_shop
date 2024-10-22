using ecommerce_backend.DataAccess.Data;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Slide;
using ecommerce_backend.Models;
using Microsoft.IdentityModel.Tokens;

namespace ecommerce_backend.DataAccess.Repository
{
    public class SlideRepository : Repository<Slide>, ISlideRepository
    {
        private readonly FashionShopContext _db;
        public SlideRepository(FashionShopContext db) : base(db)
        {
            _db = db;
        }


        public Slide? Update(int id, UpdateSlideDto obj, string result)
        {
            var slide = _db.Slides.FirstOrDefault(s => s.SlideId == id);
            if (slide == null) return null;

            string newPath = result.Replace(@"..\ecommerce_frontend\public", "");


            slide.Image = newPath;
            slide.Title = obj.Title;
            slide.Link = obj.Link;
            slide.Description = obj.Description;

            return slide;
        }

        public Slide? UpdateStatus(int id)
        {
            var slide = _db.Slides.FirstOrDefault(s => s.SlideId == id);
            if (slide == null) return null;
            slide.Status = !slide.Status;
            return slide;
        }
        public IEnumerable<Models.Slide>? handleSearch(string keyword)
        {
            bool isNumeric = int.TryParse(keyword, out int parsedId);
            var slideModels = GetAll(x =>
                isNumeric && x.SlideId == parsedId ||
                x.Title.ToLower().Contains(keyword.ToLower())
            );
            if (slideModels.IsNullOrEmpty()) return null;
            return slideModels;
        }
    }
}
