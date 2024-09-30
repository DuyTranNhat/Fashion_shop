using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ecommerce_backend.Models;
using ecommerce_backend.Dtos.Product;
using ecommerce_backend.Dtos.Image;

namespace ecommerce_backend.Dtos.Variant
{
    public class GetVariantDto
    {
        public int VariantId { get; set; }

        public int ProductId { get; set; }

        public string VariantName { get; set; } = null!;

        public decimal importPrice { get; set; }
        public decimal salePrice { get; set; }

        public int Quantity { get; set; }

        public string Status { get; set; }

        public virtual  ProductDto Product { get; set; }

        public virtual List<ImageDto> Images { get; set; }
    }
}
