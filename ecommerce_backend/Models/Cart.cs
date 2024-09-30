using ecommerce_backend.Models;

public class Cart
{
    public int CartId { get; set; }
    public int CustomerId { get; set; }
    public int VariantId { get; set; }
    public int Quantity { get; set; }
    public DateTime DateAdded { get; set; }

    public virtual Customer Customer { get; set; }
    public virtual Variant Variant { get; set; }
}
