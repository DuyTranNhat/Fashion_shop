﻿using ecommerce_backend.DataAccess.Repository;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.Attribute;
using ecommerce_backend.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ecommerce_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttributeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AttributeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var attributeModels = _unitOfWork.Attribute.GetAll(includeProperties: "Values");
            var attributeDtos = attributeModels.Select(x => x.ToAttributeDto());
            return Ok(attributeDtos);
        }

        [HttpGet("getAllActive")]
        public async Task<IActionResult> GetAllActive()
        {
            var attributeModels = _unitOfWork.Attribute.GetAll(x=>x.Status, includeProperties: "Values");
            attributeModels.ToList().ForEach(x =>
            {
                x.Values = (x.Values.Where(v => v.Status)).ToList();
            });
            var attributeDtos = attributeModels.Select(x => x.ToAttributeDto());
            return Ok(attributeDtos);
        }


        [HttpGet("getByID/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var attributeModel = _unitOfWork.Attribute.Get(x => x.AttributeId == id, "Values");
            if (attributeModel == null) return NotFound();
            return Ok(attributeModel.ToAttributeDto());
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            keyword = keyword.Trim();
            if (string.IsNullOrWhiteSpace(keyword)) return BadRequest();
            var attributeModels = _unitOfWork.Attribute.handleSearch(keyword);
            if (attributeModels == null) return NoContent();
            var attributeDtos = attributeModels.Select(x => x.ToAttributeDto());
            return Ok(attributeDtos);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAttributeDto attributeDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var attributeModel = attributeDto.ToAttributeFromCreateDto();
            _unitOfWork.Attribute.Add(attributeModel);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetById), new { id = attributeModel.AttributeId }, attributeModel.ToAttributeDto());
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAttributeDto attributeDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var attributeModel = _unitOfWork.Attribute.Update(id, attributeDto);
            if (attributeModel == null) return NotFound();
            _unitOfWork.Save();
            return Ok(attributeModel.ToAttributeDto());
        }

        [HttpPut("updateStatus/{id}")]
        public async Task<IActionResult> ChangeStatus([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var attributeModel = _unitOfWork.Attribute.UpdateStaus(id);
            if (attributeModel == null) return NotFound();
            _unitOfWork.Save();
            return Ok(attributeModel.ToAttributeDto());
        }
    }
}
