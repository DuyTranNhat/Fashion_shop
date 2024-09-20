using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Supplier;
using ecommerce_backend.Mappers;
using ecommerce_backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ecommerce_backend.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SupplierController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Get all Supplier 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Supplier> SupplierList = _unitOfWork.Supplier.GetAll(
          /*  c=> c.SupplierId == 10, includeProperties:"products"*/
            ).ToList();
            return Ok(SupplierList);
        }

        //Create Supplier
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSupplierDto supplierDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Supplier supplierModel = supplierDto.ToSupplierFromCreateDto();

            _unitOfWork.Supplier.Add(supplierModel);
            _unitOfWork.Save();

            return Ok(supplierModel);
        }

        //Update Supplier 
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSupplierDtos supplierDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // check containt Supplier 
            var supplierModel = await _unitOfWork.Supplier.Update(id, supplierDto);
            if (supplierModel == null)
            {
                return NotFound();
            }

            return Ok(supplierModel.ToSupplierDto());
        }

        //Hide Supplier(update Status of Supplier)
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateStatus([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            // check containt Supplier 
            var existingSupplier = await  _unitOfWork.Supplier.UpdateStatus(id);
            if(existingSupplier == null)
            {
                return NotFound();
            }

            return Ok(existingSupplier);
        }
    }


}
