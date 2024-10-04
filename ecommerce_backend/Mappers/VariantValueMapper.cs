using ecommerce_backend.Dtos.Value;
using ecommerce_backend.Models;

namespace ecommerce_backend.Mappers
{
    public static class VariantValueMapper
    {
        public static VariantValueDto ToValueModelFromCreate(this VariantValue variantValue)
        {
            return new VariantValueDto
            {
                ValueId = variantValue.ValueId,
                //Value1 = value.Value.,
                Status = true
            };
        }
    }
}
