using ecommerce_backend.Dtos.Category;
using ecommerce_backend.Models;

namespace ecommerce_backend.Mappers
{
    public static class CategoryMappers
    {
        public static Category ToCategoryFromCreate(this CreateCatergoryDto categoryCreate, int? parentCategory)
        {
            return new Category
            {
                Name = categoryCreate.Name,
                Status = categoryCreate.Status,
                ParentCategoryId = parentCategory,
            };
        }

        public static CategoryDto ToCategoryDto(this Category category)
        {
            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Status = category.Status,
                SubCategories = category.InverseParentCategory
                                    .Select(c => ToCategoryDto(c))
                                    .ToList()
            };
        }
    }

}
