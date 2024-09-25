using ecommerce_backend.DataAccess.Repository;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.NewFolder;
using ecommerce_backend.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VariantController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VariantController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        //GetAll
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var variants = _unitOfWork.Variant.GetAll(includeProperties: "Product");

            var variantDtos = variants.Select(item => item.ToGetVariantDto());



            return Ok(variantDtos);
        }

        //GetById
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingVariant = _unitOfWork.Variant.Get(item => item.VariantId == id,includeProperties: "Product,Images");

            if(existingVariant == null)
                return NotFound();

            return Ok(existingVariant.ToGetVariantDto());
        }

        //Create
        [HttpPost]        
        public async Task<IActionResult> Create([FromBody] CreateVariantDto obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var variantModel = obj.ToVariantFromCreateDto();

            _unitOfWork.Variant.Add(variantModel);
            _unitOfWork.Save();

            return CreatedAtAction(nameof(GetById), new { id = variantModel.VariantId }, variantModel.ToGetVariantDto());
            // return Ok(variantModel.ToGetVariantDto());
        }

       /* [HttpPut]
        [Route("{variant:int}")]
        public async Task<IActionResult> Edit([FromBody])
        {

        }*/

        
       
    }
}
