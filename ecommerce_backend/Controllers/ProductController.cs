using Microsoft.AspNetCore.Mvc;
using ecommerce_backend.Models;  // Assume you have a Category model defined here
using ecommerce_backend.DataAccess.Repository.IRepository;  // The IUnitOfWork interface location
using System.Threading.Tasks;
using ecommerce_backend.Dtos;

namespace ecommerce_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //[HttpPost("{CategoryId:int}/{SupplierId:int}")]
        //public async Task<IActionResult> createProduct([FromRoute] int CategoryId, [FromRoute] int SupplierId, CreateProductDto productCreate)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    Category categoryExisting = _unitOfWork.Category.Get();
        //}

    }
}
