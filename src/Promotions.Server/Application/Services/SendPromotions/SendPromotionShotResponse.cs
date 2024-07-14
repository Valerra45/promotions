using Application.Services.Managers;
using Application.Services.Promotions;

namespace Application.Services.SendPromotions
{
    public class SendPromotionShotResponse
    {
        public PromotionShotResponse? Promotion { get; set; }

        public ManagerResponse? Manager { get; set; }
    }
}
