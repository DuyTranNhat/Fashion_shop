using ecommerce_backend.Dtos.Attribute;
using ecommerce_backend.Models;

namespace ecommerce_backend.DataAccess.Repository.IRepository
{
    public interface IAttributeRepository : IRepository<Models.Attribute>
    {
        Models.Attribute Update(int id, UpdateAttributeDto obj);
        Models.Attribute UpdateStaus(int id);
        IEnumerable<Models.Attribute>? handleSearch(string keyword);

        void CreateProductAttribute(Product product, ICollection<CreateProuctAttributeDto> productAttributes);
    }
}
