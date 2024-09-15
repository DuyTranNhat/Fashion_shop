using Microsoft.AspNetCore.Mvc;
using ecommerce_backend.Models;  // Assume you have a Category model defined here
using ecommerce_backend.DataAccess.Repository.IRepository;  // The IUnitOfWork interface location
using System.Threading.Tasks;

namespace ecommerce_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        
    }
}
