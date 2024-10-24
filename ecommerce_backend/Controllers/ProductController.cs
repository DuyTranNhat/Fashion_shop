﻿using Microsoft.AspNetCore.Mvc;
using ecommerce_backend.Models;  // Assume you have a Category model defined here
using ecommerce_backend.DataAccess.Repository.IRepository;  // The IUnitOfWork interface location
using System.Threading.Tasks;
using ecommerce_backend.Dtos;
using ecommerce_backend.Dtos.Product;
using ecommerce_backend.Mappers;
using System.Net.WebSockets;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ecommerce_backend.DataAccess.Repository;

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

        //GetAll
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var products = _unitOfWork.Product.GetAll(includeProperties: "Category,Supplier,Attributes");

            var productDtos = products.Select(item => item.ToGetProductDto());

            return Ok(productDtos);
        }

        //GetById
        [HttpGet("getByID/{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)    
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productModel = _unitOfWork.Product.Get(item => item.ProductId == id, includeProperties: "Category,Supplier,Attributes.Values,Variants.VariantValues.Value,Variants.Images");
            var attributes = _unitOfWork.Attribute.GetAll(includeProperties: "Values");
                                                                                      
            if (productModel == null)
                return NotFound();

            return Ok(productModel.ToGetProductDto(attributes));
        }

        //create product
        [HttpPost]
        public async Task<IActionResult> create([FromBody] CreateProductDto productCreate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Product productModel = productCreate.ToProductFromCreateDto();
            _unitOfWork.Product.Add(productModel);
            _unitOfWork.Attribute.CreateProductAttribute(productModel,productCreate.Attributes);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetById), new { id = productModel.ProductId }, productModel.ToGetProductDto());
        }

        [HttpPut]
        [Route("update/{id:int}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] updateProductDto updateProduct)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingProduct = await _unitOfWork.Product.Edit(id, updateProduct);


            if (existingProduct == null)
                return NotFound();

            return Ok(existingProduct.ToGetProductDto());
        }


    }
}
