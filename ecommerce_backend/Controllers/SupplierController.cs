using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Supplier;
using ecommerce_backend.Mappers;
using ecommerce_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ecommerce_backend.Controllers
{
    [Authorize(Roles = "admin")]
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
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            List<Supplier> SupplierList = _unitOfWork.Supplier.GetAll().ToList();
            return Ok(SupplierList);
        }

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

        [HttpPut("{id:int}")]
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

        [HttpPut("updateStatus/{id:int}")]
        public async Task<IActionResult> UpdateStatus([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var existingSupplier = await _unitOfWork.Supplier.UpdateStatus(id);
            if (existingSupplier == null)
            {
                return NotFound();
            }

            return Ok(existingSupplier.ToSupplierDto());
        }

        [HttpGet]
        [Route("getByID/{id:int}")]
        public async Task<IActionResult> getByID([FromRoute] int id)
        {
            Supplier supplierExisting = _unitOfWork.Supplier.Get(c => c.SupplierId == id);

            if (supplierExisting == null)
            {
                return NotFound();    
            }

            return Ok(supplierExisting.ToSupplierDto());
        }
    }


}
