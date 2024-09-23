using ecommerce_backend.Dtos.Attribute;
using ecommerce_backend.Dtos.ProductAttribute;
using ecommerce_backend.Models;
using System;

namespace ecommerce_backend.Mappers
{
    public static class ProductAttributeMapper
    {
        public static ProductAttributeDto ToProductAttributeDto(this ProductAttribute productAttributeModel)
        {
            return new ProductAttributeDto
            {
                AttributeId = productAttributeModel.AttributeId,
                Name = productAttributeModel.Name,
                AttributeValues = productAttributeModel.AttributeValues,
                VariantAttributes = productAttributeModel.VariantAttributes
            };
        }
        public static ProductAttribute ToProductAttributeFromCreateDto(this CreateAttributeDto productAttriDto)
        {
            return new ProductAttribute
            {
                Name = productAttriDto.Name,
            };
        }
    }
}
