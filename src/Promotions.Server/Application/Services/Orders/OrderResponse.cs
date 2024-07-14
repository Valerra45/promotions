using Application.Services.Partners;
using Application.Services.Promotions;

namespace Application.Services.Orders
{
    public class OrderResponse
    {
        public Guid Id { get; set; }

        public PartnerResponse? Partner { get; set; }

        public PromotionShotResponse? Promotion { get; set; }

        public List<OrderGoodsResponse> Goods { get; set; } = new();
    }
}
