using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using ecommerce_backend.Dtos.CampaignVariant;
using ecommerce_backend.Dtos.MarketingCampaign;
using ecommerce_backend.Mappers;
using Microsoft.AspNetCore.Authorization;


namespace ecommerce_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarketingCampaignController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MarketingCampaignController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Lấy tất cả các chương trình khuyến mãi
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var CampaignVariants = _unitOfWork.MarketingCampaign.GetAll().ToList();
            return Ok(CampaignVariants);
        }

        // Lấy chương trình khuyến mãi theo id
        [HttpGet("{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var marketingCampaign = await _unitOfWork.MarketingCampaign.GetByIdAsync(id);
            if (marketingCampaign == null) return NotFound();
            return Ok(marketingCampaign.ToMarketingCampaignDto());
        }

        // Lấy chương trình khuyến mãi đang hoạt động 
        [HttpGet("active")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetOnAvailable()
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var marketingCampaigns = await _unitOfWork.MarketingCampaign.GetActiveCampaignAsync();
            return Ok(marketingCampaigns.ToList());
        }

        // tạo chương trình khuyến mãi
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] CreateMarketingCampaignDto marketingCampaignDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var marketingCampaignModel = marketingCampaignDto.ToMarketingcampaignFromCreateDto();
            _unitOfWork.MarketingCampaign.Add(marketingCampaignModel);
            _unitOfWork.Save();
            return Ok(marketingCampaignModel);
        }

        // cập nhật chương trình khuyến mãi
        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateMarketingCampaignDto marketingCampaignDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var marketingCampaignModel = await _unitOfWork.MarketingCampaign.UpdateAsync(id, marketingCampaignDto);
            if (marketingCampaignModel == null) return NotFound();
            return Ok(marketingCampaignModel.ToMarketingCampaignDto());
        }

        // tắt chương trình khuyến mãi
        [HttpPut]
        [Route("UpdateStatus/{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateStatus([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var marketingCampaignModel = await _unitOfWork.MarketingCampaign.UpdateStatusAsync(id);
            if (marketingCampaignModel == null) return NotFound();
            return Ok(marketingCampaignModel);
        }
    }
}