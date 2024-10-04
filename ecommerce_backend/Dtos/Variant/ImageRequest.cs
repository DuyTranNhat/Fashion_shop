namespace ecommerce_backend.Dtos.Variant
{
        public class ImageRequest
        {
            public int VariantId { get; set; }

            public List<IFormFile>? fileImages { get; set; }
        }
}
