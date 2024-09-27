using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ecommerce_backend.DataAccess.Data;
using ecommerce_backend.DataAccess.Repository.IRepository;
using ecommerce_backend.Dtos.MarketingCampaign;
using ecommerce_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_backend.DataAccess.Repository
{
    public class MarketingCampaignRepository : Repository<MarketingCampaign>, IMarketingCampaignRepository
    {
        private readonly FashionShopContext _db;

        public MarketingCampaignRepository(FashionShopContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<MarketingCampaign>> GetActiveCampaignAsync()
        {
            return await _db.MarketingCampaigns.Where(c => c.Status == true).ToListAsync();
        }

        public async Task<MarketingCampaign?> GetByIdAsync(int id)
        {
            return await _db.MarketingCampaigns.FirstOrDefaultAsync(m => m.CampaignId == id);   
        }

        public async Task<MarketingCampaign?> UpdateAsync(int id, UpdateMarketingCampaignDto marketingCampaignDto)
        {
            var existingMarketingCampaign = await _db.MarketingCampaigns.FirstOrDefaultAsync(m => m.CampaignId == id);
            if(existingMarketingCampaign == null) 
            {
                return null;
            }
            existingMarketingCampaign.Name = marketingCampaignDto.Name;
            existingMarketingCampaign.Description = marketingCampaignDto.Description;
            existingMarketingCampaign.StartDate = marketingCampaignDto.StartDate;
            existingMarketingCampaign.EndDate = marketingCampaignDto.EndDate;   
            existingMarketingCampaign.Status = marketingCampaignDto.Status;
            await _db.SaveChangesAsync();
            return existingMarketingCampaign;
        }

        public async Task<MarketingCampaign?> UpdateStatusAsync(int id)
        {
            var existingMarketingCampaign = await _db.MarketingCampaigns.FirstOrDefaultAsync(m => m.CampaignId == id);
            if(existingMarketingCampaign == null)
            {
                return null;
            }
            existingMarketingCampaign.Status = !existingMarketingCampaign.Status;
            await _db.SaveChangesAsync();
            return existingMarketingCampaign;
        }
    }
}