namespace ecommerce_backend.Dtos.Category
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public bool? Status { get; set; }
        public List<CategoryDto> SubCategories { get; set; } = new List<CategoryDto>();
    }
}
