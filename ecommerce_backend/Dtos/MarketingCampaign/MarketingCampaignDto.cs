using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_backend.Dtos.CampaignVariant
{
    public class MarketingCampaignDto
    {
        public int CampaignId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool Status { get; set; }
    }
}