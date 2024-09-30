using ecommerce_backend.DataAccess.Data;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Attribute;
using ecommerce_backend.Mappers;
using ecommerce_backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

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
            List<Value> tmpList = attributeModel.Values.ToList();
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
                    tmpList.Remove(value);
                }
            });
            if (tmpList.Count > 0)
            {
                tmpList.ForEach(x => {
                    attributeModel.Values.FirstOrDefault(a => a.ValueId == x.ValueId).Status = false;
                });
            }
            return attributeModel;
        }

        public Models.Attribute UpdateStaus(int id)
        {
            var attributeModel = _db.Attributes.Include(a=>a.Values).FirstOrDefault(a => a.AttributeId == id);
            if (attributeModel == null) return null;
            attributeModel.Status = !attributeModel.Status;
            return attributeModel;
        }

        public IEnumerable<Models.Attribute>? handleSearch(string keyword)
        {
            bool isNumeric = int.TryParse(keyword, out int parsedId);
            var attributeModels = GetAll(x =>
                isNumeric && x.AttributeId == parsedId ||
                x.Name.ToLower().Contains(keyword.ToLower())
            , includeProperties: "Values");
            if (attributeModels.IsNullOrEmpty()) return null;
            return attributeModels;
        }
    }
}
