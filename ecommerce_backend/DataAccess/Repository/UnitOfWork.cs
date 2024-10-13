using ecommerce_backend.DataAccess.Data;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace ecommerce_backend.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FashionShopContext _db;
        private IDbContextTransaction _transaction;

        public IAttributeRepository Attribute { get; set; }
        public ISlideRepository Slide { get; set; }
        public IReceiptRepository Receipt { get; set; }
        public ICategoryRepository Category { get; private set; }
        public ISupplierRepository Supplier { get; private set; }
        public IProductRepository Product { get; private set; }
        public IVariantRepository Variant { get; private set; }
        public IImageRepository Image { get; private set; }
        public ICustomerRepository Customer { get; private set; }
        public IMarketingCampaignRepository MarketingCampaign { get; private set; }
        public IOrderRepository Order { get; private set; }
        public IProductReviewRepository ProductReview { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }
        public ICartRepository Cart { get; private set; }
        public IValueRepository Value { get; set; }
        public IVariantValueRepository VariantValue { get; set; }



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

        public void BeginTransaction()
        {
            _transaction = _db.Database.BeginTransaction(); // Bắt đầu transaction
        }

        public void Commit()
        {
            _transaction?.Commit(); // Commit transaction
            _transaction?.Dispose(); // Giải phóng tài nguyên
        }

        public void Rollback()
        {
            _transaction?.Rollback(); // Rollback transaction
            _transaction?.Dispose(); // Giải phóng tài nguyên
        }
    }
}
