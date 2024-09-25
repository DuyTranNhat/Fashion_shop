namespace ecommerce_backend.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        ISupplierRepository Supplier { get; }
        IProductRepository Product { get; }
        IVariantRepository Variant { get; }
        IImageRepository Image { get; }
        void Save();
    }
}
