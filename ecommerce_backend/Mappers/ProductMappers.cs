using ecommerce_backend.DataAccess.Repository;
using ecommerce_backend.Dtos.Product;
using ecommerce_backend.Models;

namespace ecommerce_backend.Mappers
{
    public static class ProductMappers
    {
        public static Product ToProductFromCreateDto(this CreateProductDto createProductDto)
        {
            return new Product
            {
                CategoryId = createProductDto.CategoryId,
                SupplierId = createProductDto.SupplierId,
                Name = createProductDto.Name,
                Description = createProductDto.Description,
            };
        }

        public static ProductDto ToProductDto(this Product productModel)
        {
            return new ProductDto
            {
                ProductId = productModel.ProductId,
                CategoryId = productModel.CategoryId,
                SupplierId = productModel.SupplierId,
                Name = productModel.Name,
                Description = productModel.Description,
            };
        }

        public static ProductDto ToGetProductDto(this Product productModel)
        {
            return new ProductDto
            {
                ProductId = productModel.ProductId,
                CategoryId = productModel.CategoryId,
                SupplierId = productModel.SupplierId,
                Name = productModel.Name,
                Description = productModel.Description,
                CategoryDto = productModel.Category?.ToCategoryDto(),
                SupplierDto = productModel.Supplier?.ToSupplierDto(),
                Attributes = productModel.Attributes.Select(a => a.ToAttributeDto()).ToList()
            };
        }


    }
}
