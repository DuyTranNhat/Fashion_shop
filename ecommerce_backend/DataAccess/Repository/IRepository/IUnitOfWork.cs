namespace ecommerce_backend.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        ISupplierRepository Supplier { get; }
        //IProductRepository Category { get; }
        ICustomerRepository Customer { get; }
        IMarketingCampaignRepository MarketingCampaign { get; }
        void Save();
    }
}
