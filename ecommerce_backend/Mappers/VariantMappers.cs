using ecommerce_backend.Dtos.NewFolder;
using ecommerce_backend.Dtos.Variant;
using ecommerce_backend.Models;

namespace ecommerce_backend.Mappers
{
    public static class VariantMappers
    {

        public static GetVariantDto ToGetVariantDto(this Variant variant)
        {
            return new GetVariantDto
            {
                VariantId = variant.VariantId,
                ProductId = variant.ProductId,
                VariantName = variant.VariantName,
                importPrice = variant.importPrice,
                salePrice = variant.salePrice,
                Quantity = variant.Quantity,
                Status = variant.Status,
                Images = variant.Images.Select(item => item.ToImageDto(variant.VariantId)).ToList(),
            };
        }
        public static Variant ToVariantFromCreateDto(this CreateVariantDto createVariant)
        {

            var variantModel = new Variant
            {
                
                ProductId = createVariant.ProductId,
                VariantName = createVariant.VariantName,
                importPrice = createVariant.importPrice,
                salePrice = createVariant.salePrice,
                Quantity = 0,
                Status = "out_of_stock",
            };

            variantModel.Images =
                createVariant.createImageDtos.Select(item => item.ToImageModel(variantModel.VariantId)).ToList(); ;


            return variantModel;
        }
    }
}
