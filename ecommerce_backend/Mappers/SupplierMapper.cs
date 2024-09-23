﻿using ecommerce_backend.Dtos.Supplier;
using ecommerce_backend.Models;

namespace ecommerce_backend.Mappers
{
    public static class SupplierMapper
    {
        public static SupplierDto ToSupplierDto(this Supplier supplierModel)
        {
            return new SupplierDto
            {
                SupplierId = supplierModel.SupplierId,
                Name = supplierModel.Name,
                Email = supplierModel.Email,
                Phone = supplierModel.Phone,
                Address = supplierModel.Address,
                Status = supplierModel.Status,
                Notes = supplierModel.Notes,
            };
        }

        public static Supplier ToSupplierFromCreateDto(this CreateSupplierDto supplierDto)
        {
            return new Supplier
            {
                Name = supplierDto.Name,
                Email = supplierDto.Email,
                Phone = supplierDto.Phone,
                Address = supplierDto.Address,
                Status = supplierDto.Status,
                Notes = supplierDto.Notes,
            };
        }
    }
}
