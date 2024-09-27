using ecommerce_backend.Dtos.MarketingCampaign;
using ecommerce_backend.Models; 

namespace ecommerce_backend.DataAccess.Repository.IRepository
{
    public interface IMarketingCampaignRepository : IRepository<MarketingCampaign>
    {
        public Task<MarketingCampaign?> GetByIdAsync(int id);
        public Task<IEnumerable<MarketingCampaign>> GetActiveCampaignAsync();
        public Task<MarketingCampaign?> UpdateAsync(int id, UpdateMarketingCampaignDto marketingCampaignDto);
        public Task<MarketingCampaign?> UpdateStatusAsync(int id);
    }
}