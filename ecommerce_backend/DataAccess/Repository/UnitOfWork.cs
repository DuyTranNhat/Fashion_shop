using ecommerce_backend.DataAccess.Data;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace ecommerce_backend.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly FashionShopContext _db;
        private IDbContextTransaction _transaction;

        public IAttributeRepository Attribute { get; }
        public ISlideRepository Slide { get; }
        public IReceiptRepository Receipt { get; }
        public ICategoryRepository Category { get; }
        public ISupplierRepository Supplier { get; }
        public IProductRepository Product { get; }
        public IVariantRepository Variant { get; }
        public IImageRepository Image { get; }
        public ICustomerRepository Customer { get; }
        public IMarketingCampaignRepository MarketingCampaign { get; }
        public IOrderRepository Order { get; }
        public IProductReviewRepository ProductReview { get; }
        public IOrderDetailRepository OrderDetail { get; }
        public ICartRepository Cart { get; }
        public IValueRepository Value { get; }
        public IVariantValueRepository VariantValue { get; }

        public UnitOfWork(FashionShopContext db)
        {
            _db = db;
            Attribute = new AttributeRepository(_db);
            Slide = new SlideRepository(_db);
            Receipt = new ReceiptRepository(_db);
            Category = new CategoryRepository(_db);
            Supplier = new SupplierRepository(_db);
            Product = new ProductRepository(_db);
            Variant = new VariantRepository(_db);
            Image = new ImageRepository(_db);
            Customer = new CustomerRepository(_db);
            MarketingCampaign = new MarketingCampaignRepository(_db);
            Order = new OrderRepository(_db);
            ProductReview = new ProductReviewRepository(_db);
            OrderDetail = new OrderDetailRepository(_db);
            Cart = new CartRepository(_db);
            Value = new ValueRepository(_db);
            VariantValue = new VariantValueRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void BeginTransaction()
        {
            _transaction = _db.Database.BeginTransaction();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            _transaction = await _db.Database.BeginTransactionAsync();
            return _transaction;
        }


        public void Commit()
        {
            _transaction?.Commit();
            _transaction?.Dispose();
        }

        public async Task CommitAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
            }
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _db.Dispose();
        }
    }
}
