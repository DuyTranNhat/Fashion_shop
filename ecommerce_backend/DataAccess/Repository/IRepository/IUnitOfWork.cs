namespace ecommerce_backend.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        ISupplierRepository Supplier { get; }
        IProductRepository Product { get; }
        ISlideRepository Slide { get; }
        void Save();
    }
}
