using Microsoft.AspNetCore.Mvc;
using ecommerce_backend.Models;  // Assume you have a Category model defined here
using ecommerce_backend.DataAccess.Repository.IRepository;  // The IUnitOfWork interface location
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ecommerce_backend.Dtos.Category;
using ecommerce_backend.Mappers;
using System;

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

        [HttpPost] 
        public async Task<IActionResult> AddCategory(CreateCatergoryDto categoryCreate, [FromQuery] int? CategoryParentId = null)
        {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
               
                Category categoryModel = categoryCreate.ToCategoryFromCreate(CategoryParentId);
           
                _unitOfWork.Category.Add(categoryModel);
                _unitOfWork.Save();
                return Ok(categoryModel); 
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {

            // Lấy tất cả các danh mục từ cơ sở dữ liệu
            var categoriesRoot = await _unitOfWork.Category.getAllRecursive();

            // Chuyển đổi các danh mục gốc và danh mục con của chúng sang DTO
            var categoryDtos = categoriesRoot.Select(c => c.ToCategoryDto()).ToList();

            return Ok(categoryDtos);
        }
    }



}
