namespace ecommerce_backend.Models
{
    public class VariantValue
    {
        public int VariantId { get; set; }
        public int ValueId { get; set; }

        // Navigation properties
        public Variant Variant { get; set; }
        public Value Value { get; set; }
    }
}
