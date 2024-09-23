using ecommerce_backend.Dtos.Receipt;
using ecommerce_backend.Dtos.Slide;
using ecommerce_backend.Models;

namespace ecommerce_backend.Mappers
{
    public static class ReceiptMappers
    {
        public static ReceiptDto ToReceiptDto(this Receipt receipt)
        {
            return new ReceiptDto
            {
                ReceiptId = receipt.ReceiptId,
                ReceiptDate = receipt.ReceiptDate,
                TotalAmount = receipt.TotalAmount,
                TotalPrice = receipt.TotalPrice,
                ReceiptDetails = receipt.ReceiptDetails
            };
        }
        public static Receipt ToReceiptFromCreate(this CreateReceiptDto receiptdto)
        {
            return new Receipt
            {
                ReceiptDate = receiptdto.ReceiptDate,
                TotalAmount = receiptdto.TotalAmount,
                TotalPrice = receiptdto.TotalPrice,
                ReceiptDetails = receiptdto.ReceiptDetails
            };
        }
    }
}
