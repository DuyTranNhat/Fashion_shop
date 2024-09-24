﻿namespace ecommerce_backend.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        ISupplierRepository Supplier { get; }
        IProductRepository Product { get; }
        IAttributeRepository Attribute { get; }
        ISlideRepository Slide { get; }
        IReceiptRepository Receipt { get; }
        void Save();
    }
}
