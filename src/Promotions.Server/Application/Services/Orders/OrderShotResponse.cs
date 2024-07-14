using Application.Services.Partners;
using Application.Services.Promotions;

namespace Application.Services.Orders
{
    public class OrderShotResponse
    {
        public Guid Id { get; set; }

        public PartnerResponse? Partner { get; set; }

        public PromotionShotResponse? Promotion { get; set; }
    }
}
