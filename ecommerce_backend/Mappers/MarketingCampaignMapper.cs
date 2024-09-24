using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_backend.Dtos.CampaignVariant;
using ecommerce_backend.Dtos.MarketingCampaign;
using ecommerce_backend.Models;

namespace ecommerce_backend.Mappers
{
    public static class MarketingCampaignMapper
    {
        public static MarketingCampaignDto ToMarketingCampaignDto(this MarketingCampaign marketingCampaignModel)
        {
            return new MarketingCampaignDto 
            {
                CampaignId = marketingCampaignModel.CampaignId,
                Name = marketingCampaignModel.Name,
                Description = marketingCampaignModel.Description,
                StartDate = marketingCampaignModel.StartDate,
                EndDate = marketingCampaignModel.EndDate,
                Status = marketingCampaignModel.Status
            };
        }

        public static MarketingCampaign ToMarketingcampaignFromCreateDto(this CreateMarketingCampaignDto marketingCampaignDto)
        {
            return new MarketingCampaign
            {
                Name = marketingCampaignDto.Name,
                Description = marketingCampaignDto.Description,
                StartDate = marketingCampaignDto.StartDate,
                EndDate = marketingCampaignDto.EndDate,
                Status = marketingCampaignDto.Status
            };
        }
    }
}