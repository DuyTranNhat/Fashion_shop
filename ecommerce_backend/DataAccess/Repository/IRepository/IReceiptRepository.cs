using ecommerce_backend.Dtos.Receipt;
using ecommerce_backend.Dtos.Slide;
using ecommerce_backend.Models;

namespace ecommerce_backend.DataAccess.Repository.IRepository
{
    public interface IReceiptRepository : IRepository<Receipt>
    {
        Receipt? Update(int id, UpdateReceiptDto updateReceiptDto);
        Receipt? UpdateStatus(int id, string status);
        IEnumerable<Models.Receipt>? handleSearch(string keyword);
    }
}
