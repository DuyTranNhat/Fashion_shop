using System;
using System.Collections.Generic;

namespace ecommerce_backend.Models;

public partial class MarketingCampaign
{
    public int CampaignId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<CampaignProduct> CampaignProducts { get; set; } = new List<CampaignProduct>();
}
