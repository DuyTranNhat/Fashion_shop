using System;
using System.Collections.Generic;

namespace ecommerce_backend.Models;

public partial class CampaignProduct
{
    public int CampaignProductId { get; set; }

    public int? CampaignId { get; set; }

    public int? ProductId { get; set; }

    public decimal? DiscountPercentage { get; set; }

    public string? SlideTitle { get; set; }

    public string? SlideImage { get; set; }

    public string? SlideDescription { get; set; }

    public string? SlideUrl { get; set; }

    public virtual MarketingCampaign? Campaign { get; set; }

    public virtual Product? Product { get; set; }
}
