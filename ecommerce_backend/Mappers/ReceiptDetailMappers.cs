using ecommerce_backend.Dtos.Receipt;
using ecommerce_backend.Dtos.ReceiptDetail;
using ecommerce_backend.Models;

namespace ecommerce_backend.Mappers
{
    public static class ReceiptDetailMappers
    {
        public static ReceiptDetail ToReceiptDetail(this ReceiptDetailDto receiptDto, int receiptId)
        {
            return new ReceiptDetail
            {
                ReceiptId = receiptId,
                VariantId = receiptDto.VariantId,
                Quantity = receiptDto.Quantity,
                UnitPrice = receiptDto.UnitPrice
            };
        }

        public static ReceiptDetailDto ToReceiptDetailDto(this ReceiptDetail receipt)
        {
            return new ReceiptDetailDto
            {
                VariantId = receipt.VariantId,
                Quantity = receipt.Quantity,
                UnitPrice = receipt.UnitPrice
            };
        }
    }
}
