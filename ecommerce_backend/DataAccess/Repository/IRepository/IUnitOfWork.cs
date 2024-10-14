using Microsoft.EntityFrameworkCore.Storage;

namespace ecommerce_backend.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        ISupplierRepository Supplier { get; }
        IProductRepository Product { get; }
        IVariantRepository Variant { get; }
        IImageRepository Image { get; }
        IAttributeRepository Attribute { get; }
        ISlideRepository Slide { get; }
        IReceiptRepository Receipt { get; }
        ICustomerRepository Customer { get; }
        IMarketingCampaignRepository MarketingCampaign { get; }
        IOrderRepository Order { get; }
        IProductReviewRepository ProductReview { get; }
        IOrderDetailRepository OrderDetail { get; }
        ICartRepository Cart { get; }
        IValueRepository Value { get; }
        IVariantValueRepository VariantValue { get; }  // Không cần `public`

        // Sync transaction methods
        void Save();
        void BeginTransaction();
        void Commit();
        void Rollback();

        // Async transaction methods
        Task SaveAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
