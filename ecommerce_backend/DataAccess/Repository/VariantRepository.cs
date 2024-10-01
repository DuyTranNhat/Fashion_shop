﻿using ecommerce_backend.DataAccess.Data;
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
<<<<<<< HEAD
      
        public async Task<Variant> Edit(int id, UpdateVariantDto obj)
=======
        public async Task<Variant?> Edit(int id, UpdateVariantDto obj, List<UpdateImageDto> listUpdateImageDto)
>>>>>>> 1b74efa36c43880ac7debafbb05d37a3eb0481fb
        {
            var existingVariant = await _db.Variants.Include(variant=>variant.Values).ThenInclude(value=>value.Attribute).FirstOrDefaultAsync(item => item.VariantId == id);
            if (existingVariant == null)
                return null;

            var images = _db.Images.Where(item => item.VariantId == id).ToList();
<<<<<<< HEAD
            var listIFormFile = await ImageMapper.UploadListImages("Assets\\Images\\ProductImage", obj.listFile);
            var updateImageModels = new List<Models.Image>();

            if (listIFormFile == null) ;
            else updateImageModels = listIFormFile.Select(item => item.ToImageModel(id)).ToList();



=======
>>>>>>> 1b74efa36c43880ac7debafbb05d37a3eb0481fb
            if (!images.IsNullOrEmpty())
            {
                foreach (Models.Image item in images)
                {
                    if (File.Exists(item.ImageUrl)) File.Delete(item.ImageUrl);
                    _db.Images.Remove(item);
                    await _db.SaveChangesAsync();
                }
            }
            existingVariant.VariantName = obj.VariantName;
            existingVariant.Status = obj.Status;
            existingVariant.Images = listUpdateImageDto.Select(x=>x.ToModelFromUpdateImage(existingVariant.VariantId)).ToList();
            await _db.SaveChangesAsync();
            return existingVariant;
        }


        public Variant? UpdateStatus(int id, string status)
        {
            var variantModel = _db.Variants.FirstOrDefault(x => x.VariantId == id);
            if (variantModel == null) return null;
            variantModel.Status = status;
            return variantModel;
        }

        public void UpdateVariantQuantity(Receipt receipt)
        {
            
            receipt.ReceiptDetails.ToList().ForEach(rd =>
            {
                var variant = _db.Variants.Include(v=>v.Images).Include(v=>v.Values)
                .ThenInclude(v=>v.Attribute).FirstOrDefault(item => item.VariantId == rd.VariantId);
                if (variant.ImportPrice == rd.UnitPrice)
                {
                    variant.Quantity += rd.Quantity;
                } else
                {
                    var newVariant = new Variant
                    {
                        ProductId = variant.ProductId,
                        VariantName = variant.VariantName,
                        Quantity = rd.Quantity,
                        ImportPrice = rd.UnitPrice,
                        // phan tram hay nhap tay
                        SalePrice = variant.SalePrice,
                        Images = variant.Images,
                        Values = variant.Values,
                        Status = "available"
                    };
                    _db.Variants.Add(newVariant);
                }
            });
        }




    }
}
