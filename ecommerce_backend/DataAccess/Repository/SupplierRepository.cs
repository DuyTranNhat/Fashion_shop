using ecommerce_backend.DataAccess.Data;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Supplier;
using ecommerce_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace ecommerce_backend.DataAccess.Repository
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        private readonly FashionShopContext _db;
        public SupplierRepository(FashionShopContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Supplier> GetById(int id)
        {
            var existingSupplier = await _db.Suppliers.FirstOrDefaultAsync(item => item.SupplierId == id);

            if (existingSupplier == null)
            {
                return null;
            }

            return existingSupplier;
        }

        //Update
        public async Task<Supplier> Update(int id, UpdateSupplierDtos obj)
        {
            var existingSupplier = await _db.Suppliers.FirstOrDefaultAsync(item => item.SupplierId == id);

            if (existingSupplier == null)
            {
                return null;
            }

            existingSupplier.Name = obj.Name;
            existingSupplier.Email = obj.Email;
            existingSupplier.Phone = obj.Phone;
            existingSupplier.Address = obj.Address;
            existingSupplier.Status = obj.Status;
            existingSupplier.Notes = obj.Notes;

            await _db.SaveChangesAsync();

            return existingSupplier;
        }

        public async Task<Supplier> UpdateStatus(int id)
        {
            var existingSupplier = await _db.Suppliers.FirstOrDefaultAsync(item => item.SupplierId == id);

            if (existingSupplier == null)
            {
                return null;
            }

           existingSupplier.Status = false;

            await _db.SaveChangesAsync();

            return existingSupplier;
        }

    }
}
