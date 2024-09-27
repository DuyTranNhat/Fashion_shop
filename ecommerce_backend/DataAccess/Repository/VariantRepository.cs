using ecommerce_backend.DataAccess.Data;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Image;
using ecommerce_backend.Dtos.NewFolder;
using ecommerce_backend.Dtos.Variant;
using ecommerce_backend.Mappers;
using ecommerce_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static System.Net.Mime.MediaTypeNames;

namespace ecommerce_backend.DataAccess.Repository
{
    public class VariantRepository : Repository<Variant>, IVariantRepository
    {
        private readonly FashionShopContext _db;

        public VariantRepository(FashionShopContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Variant> Edit(int id, UpdateVariantDto obj)
        {
            var existingVariant = await _db.Variants.FirstOrDefaultAsync(item => item.VariantId == id);
            if (existingVariant == null)
                return null;

            var images = _db.Images.Where(item => item.VariantId == id).ToList();
            var listIFormFile = await ImageMapper.UploadImages("Assets\\Images\\", obj.listFile);
            var updateImageModels = new List<Models.Image>();

            if (listIFormFile == null) ;
            else updateImageModels = listIFormFile.Select(item => item.ToImageModel(id)).ToList();



            if (!images.IsNullOrEmpty())
            {
                foreach (Models.Image item in images)
                {
                    _db.Images.Remove(item);
                    await _db.SaveChangesAsync();
                }
            }
            existingVariant.VariantName = obj.VariantName;
            existingVariant.Quantity = obj.Quantity;
            existingVariant.Status = obj.Status;
            existingVariant.Images = updateImageModels;

            await _db.SaveChangesAsync();

            return existingVariant;
        }



    }
}
