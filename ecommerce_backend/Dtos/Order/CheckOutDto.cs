using ecommerce_backend.Dtos.Cart;
using ecommerce_backend.Dtos.Customer;

namespace ecommerce_backend.Dtos.Order
{
    public class CheckOutDto
    {
        public CustomerDto Customer { get; set; }
        public CartDto Cart { get; set; }
       
    }
}
