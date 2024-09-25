using ecommerce_backend.Dtos.Attribute;
using ecommerce_backend.Models;
using System;

namespace ecommerce_backend.Mappers
{
    public static class AttributeMapper
    {
        public static AttributeDto ToAttributeDto(this Models.Attribute attributeModel)
        {
            return new AttributeDto
            {
                AttributeId = attributeModel.AttributeId,
                Name = attributeModel.Name,
                Status = attributeModel.Status,
                Values = (attributeModel.Values.Select(v => v.ToValueDto())).ToList()
            };
        }
        public static Models.Attribute ToAttributeFromCreateDto(this CreateAttributeDto attributeDto)
        {
            var attributeModel =  new Models.Attribute{ Name = attributeDto.Name, Status = true };
            Console.WriteLine(attributeModel.AttributeId);
            attributeModel.Values = (attributeDto.Values.Select(v => v.ToValueModelFromCreate(attributeModel.AttributeId))).ToList();
            return attributeModel;
        }
        
    }
}
