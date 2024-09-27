﻿using ecommerce_backend.Dtos.Receipt;
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
                CreateDate = receipt.CreateDate,
                ReceiptDate = receipt.ReceiptDate,
                TotalPrice = receipt.TotalPrice,
                Status = receipt.Status,
                ReceiptDetails = receipt.ReceiptDetails.Select(x=>x.ToReceiptDetailDto()).ToList()
            };
        }
        public static Receipt ToReceiptFromCreate(this CreateReceiptDto receiptdto)
        {
            var receipt = new Receipt { 
                ReceiptDate = receiptdto.ReceiptDate,
                TotalPrice = receiptdto.TotalPrice
            };
            receipt.ReceiptDetails = receiptdto.ReceiptDetails.Select(x => x.ToReceiptDetail(receipt.ReceiptId)).ToList();
            return receipt;
        }
    }
}
