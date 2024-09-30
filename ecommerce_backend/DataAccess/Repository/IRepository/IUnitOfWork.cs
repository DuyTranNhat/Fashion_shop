namespace ecommerce_backend.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        ISupplierRepository Supplier { get; }

        IProductRepository Product { get; }
        IVariantRepository Variant { get; }
        IImageRepository Image { get; }
        IAttributeRepository Attribute { get; }
        ISlideRepository Slide { get; }
        IReceiptRepository Receipt { get; }
        //IProductRepository Category { get; }
        ICustomerRepository Customer { get; }
        IMarketingCampaignRepository MarketingCampaign { get; }
        IOrderRepository Order { get; }
        IProductReviewRepository ProductReview { get; }
        IOrderDetailRepository OrderDetail { get; }
        void Save();
    }
}
