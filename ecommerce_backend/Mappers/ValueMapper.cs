using ecommerce_backend.Dtos.Value;
using ecommerce_backend.Models;

namespace ecommerce_backend.Mappers
{
    public static class ValueMapper
    {
        public static Value ToValueModelFromCreate(this CreateValueDto valueDto, int AttributeId)
        {
            return new Value
            {
                Value1 = valueDto.Value1,
                AttributeId = AttributeId,
                Status = true
            };
        }
        public static ValueDto ToValueDto(this Value value)
        {
            return new ValueDto
            {
                ValueId = value.ValueId,
                Value1 = value.Value1,
                Status = value.Status
            };
        }

        public static VariantValueDto ToVariantValueDto(this Value value)
        {
            return new VariantValueDto
            {
                ValueId = value.ValueId,
                Value1 = value.Value1,
                Status = value.Status,
                Attribute = value.Attribute.ToAttributeVariantValueDto()
            };
        }
    }
}
