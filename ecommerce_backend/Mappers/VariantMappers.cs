using ecommerce_backend.Dtos.Image;
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
                importPrice = variant.ImportPrice,
                salePrice = variant.SalePrice,
                Quantity = variant.Quantity,
                Status = variant.Status,
                Images = variant.Images.Select(item => item.ToImageDto(variant.VariantId)).ToList(),
                Values = variant.VariantValues.Select(v => v.Value.ToValueDto()).ToList(),
            };
        }
        public static GetVariantDto ToGetVariantDto(this Variant variant, IEnumerable<Models.Attribute> attributes)
        {

            return new GetVariantDto
            {
                VariantId = variant.VariantId,
                ProductId = variant.ProductId,
                VariantName = variant.VariantName,
                importPrice = variant.ImportPrice,
                salePrice = variant.SalePrice,
                Quantity = variant.Quantity,
                Status = variant.Status,
                Images = variant.Images.Select(item => item.ToImageDto(variant.VariantId)).ToList(),
                Values = variant.VariantValues.Select(v => v.Value.ToValueDto(attributes)).ToList(),
            };
        }

        public static GetVariantDto ToVariantDtoWithoutImages(this Variant variant, IEnumerable<Models.Attribute> attributes)
        {

            return new GetVariantDto
            {
                VariantId = variant.VariantId,
                ProductId = variant.ProductId,
                VariantName = variant.VariantName,
                importPrice = variant.ImportPrice,
                salePrice = variant.SalePrice,
                Quantity = variant.Quantity,
                Status = variant.Status,
                Values = variant.VariantValues.Select(v => v.Value.ToValueDto(attributes)).ToList(),
            };
        }


        public static GetVariantDto ToGetVariantDto(this Variant variant, bool primaryStatus)
        {

            return new GetVariantDto
            {
                VariantId = variant.VariantId,
                ProductId = variant.ProductId,
                VariantName = variant.VariantName,
                importPrice = variant.ImportPrice,
                salePrice = variant.SalePrice,
                Quantity = variant.Quantity,
                Status = variant.Status,
                Images = variant.Images.Select(item => item.ToImageDto(variant.VariantId)).ToList(),
                Values = variant.VariantValues.Select(v => v.Value.ToValueDto()).ToList(),
            };
        }



        public static Variant ToVariantFromCreateDto(this CreateVariantDto createVariant)
        {

            var variantModel = new Variant
            {
                ProductId = createVariant.ProductId,
                VariantName = createVariant.VariantName,
                ImportPrice = createVariant.importPrice,
                SalePrice = createVariant.salePrice,
                Quantity = 0,
                Status = "out_of_stock"
                
            };


            return variantModel;
        }

        public static ImageDto ToVariantImageDto(this Image img)
        {

            return new ImageDto
            {
                ImageId = img.ImageId,
                VariantId = img.VariantId,
                ImageUrl = img.ImageUrl
            };
        }





    }
}
