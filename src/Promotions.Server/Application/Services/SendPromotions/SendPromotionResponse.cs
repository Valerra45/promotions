using Application.Services.Managers;
using Application.Services.Partners;
using Application.Services.Promotions;

namespace Application.Services.SendPromotions
{
    public class SendPromotionResponse
    {
        public Guid Id { get; set; }

        public PromotionShotResponse? Promotion { get; set; }

        public PartnerResponse? Partner { get; set; }

        public ManagerResponse? Manager { get; set; }
    }
}
