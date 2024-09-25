using ecommerce_backend.DataAccess.Data;
using ecommerce_backend.DataAccess.Repository.IRepository;

namespace ecommerce_backend.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FashionShopContext _db;
        public ICategoryRepository Category { get; private set; }
        public ISupplierRepository Supplier { get; private set; }
        public IProductRepository Product { get; private set; }
        public IVariantRepository Variant { get; private set; }

        public IImageRepository Image { get; private set; }

        

        public UnitOfWork(FashionShopContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Supplier = new SupplierRepository(_db);
            Product = new ProductRepository(_db);
            Variant = new VariantRepository(_db);
            Image = new ImageRepository(_db);
        }


        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
