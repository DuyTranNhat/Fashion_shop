using ecommerce_backend.DataAccess.Repository;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Attribute;
using ecommerce_backend.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductAttributeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductAttributeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productAttributeModels = _unitOfWork.ProductAttribute.GetAll();
            var productAttributeDtos = productAttributeModels.Select(x => x.ToProductAttributeDto());
            return Ok(productAttributeDtos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var productAttributeModel = _unitOfWork.ProductAttribute.Get(x => x.AttributeId == id, "AttributeValues,VariantAttributes");
            if (productAttributeModel == null) return NotFound();
            return Ok(productAttributeModel.ToProductAttributeDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAttributeDto productAttributeDto)
        {
            var productAttributeModel = productAttributeDto.ToProductAttributeFromCreateDto();
            _unitOfWork.Attribute.Add(productAttributeModel);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetById), new { id = productAttributeModel.AttributeId }, productAttributeModel.ToProductAttributeDto());
        }
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductAttribute productAttributeDto)
        //{

        //}
    }
}
