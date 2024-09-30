using ecommerce_backend.DataAccess.Data;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Models;

namespace ecommerce_backend.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FashionShopContext _db;
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
        }


        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
