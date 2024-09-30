﻿using ecommerce_backend.DataAccess.Repository;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Image;
using ecommerce_backend.Dtos.NewFolder;
using ecommerce_backend.Dtos.Variant;
using ecommerce_backend.Mappers;
using ecommerce_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VariantController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public VariantController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //GetAll
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var variants = _unitOfWork.Variant.GetAll(includeProperties: "Images,Values.Attribute");

            var variantDtos = variants.Select(item => item.ToGetVariantDto());

            return Ok(variantDtos);
        }

        //GetAllByStatus
        [HttpGet("status")]
        public async Task<IActionResult> GetAllByStatus(string status)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var variants = _unitOfWork.Variant.GetAll(item => item.Status == status, includeProperties: "Images,Values.Attribute").ToList();

            var variantDtos = variants.Select(item => item.ToGetVariantDto());
            if (variantDtos == null)
                return NotFound();

            return Ok(variantDtos);
        }

        //GetById
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingVariant = _unitOfWork.Variant.Get(item => item.VariantId == id, includeProperties: "Images,Values.Attribute");

            if (existingVariant == null)
                return NotFound();

            return Ok(existingVariant.ToGetVariantDto());
        }

        //Create
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]CreateVariantDto obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var listImage = await ImageMapper.UploadImages("Assets\\Images\\",obj.listFile);
            Variant variantModel = obj.ToVariantFromCreateDto(listImage);
            _unitOfWork.Variant.Add(variantModel);
            _unitOfWork.Value.CreateVariantValue(variantModel, obj.Values);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetById), new { id = variantModel.VariantId }, variantModel.ToGetVariantDto());
        }

        //Edit variant 
        [HttpPut("/edit/{id:int}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromForm] UpdateVariantDto updateVariant)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var variantModel = await _unitOfWork.Variant.Edit(id, updateVariant);
            if (variantModel == null)
                return NotFound();

            return Ok(variantModel.ToGetVariantDto());
        }

        [HttpPut("updateStatus/{id:int}")]
        public async Task<IActionResult> ChangeStauts([FromRoute] int id, [FromBody] string status)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var variantModel = _unitOfWork.Variant.UpdateStatus(id, status);
            if (variantModel == null) return NotFound("Không tìm thấy Variant");
            _unitOfWork.Save();
            return Ok(variantModel.ToGetVariantDto());
        }

    }
}