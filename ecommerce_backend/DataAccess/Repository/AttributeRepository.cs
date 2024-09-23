using ecommerce_backend.DataAccess.Data;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Attribute;
using ecommerce_backend.Mappers;
using ecommerce_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_backend.DataAccess.Repository
{
    public class AttributeRepository : Repository<Models.Attribute>, IAttributeRepository
    {
        private readonly FashionShopContext _db;
        public AttributeRepository(FashionShopContext db) : base(db)
        {
            _db = db;
        }



        public Models.Attribute Update(int id, UpdateAttributeDto obj)
        {
            var attributeModel = _db.Attributes.Include(x=>x.Values).FirstOrDefault(a => a.AttributeId == id);
            if (attributeModel == null) return null;
            attributeModel.Name = obj.Name;
            obj.Values.ToList().ForEach(v =>
            {
                var value = attributeModel.Values.FirstOrDefault(x => x.ValueId == v.ValueId);
                if (value == null)
                {
                    attributeModel.Values.Add(new Value()
                    {
                        AttributeId = attributeModel.AttributeId,
                        Value1 = v.Value1,
                        Status = v.Status
                    });

                } else
                {
                    value.Value1 = v.Value1;
                    value.Status = v.Status;
                }
            });
            return attributeModel;
        }

        public Models.Attribute UpdateStaus(int id)
        {
            var attributeModel = _db.Attributes.Include(a=>a.Values).FirstOrDefault(a => a.AttributeId == id);
            if (attributeModel == null) return null;
            attributeModel.Status = !attributeModel.Status;
            return attributeModel;
        }
    }
}
