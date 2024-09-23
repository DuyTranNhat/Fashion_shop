using ecommerce_backend.Dtos.Slide;
using ecommerce_backend.Models;

namespace ecommerce_backend.DataAccess.Repository.IRepository
{
    public interface ISlideRepository : IRepository<Slide>
    {
        Slide? Update(int id, UpdateSlideDto obj);
        Slide? UpdateStatus(int id);
    }
}
