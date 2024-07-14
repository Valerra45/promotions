using Application.Services.GoodsService;

namespace Application.Services.Promotions
{
    public class PromotionGoodsResponse
    {
        public Guid Id { get; set; }

        public GoodsResponse Goods { get; set; } = new();

        public string VendorCode { get; set; } = string.Empty;

        public string GoodsDescription { get; set; } = string.Empty;

        public string PromotionDescription { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}
