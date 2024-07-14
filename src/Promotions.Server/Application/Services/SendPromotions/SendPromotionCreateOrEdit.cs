namespace Application.Services.SendPromotions
{
    public class SendPromotionCreateOrEdit
    {
        public Guid PromotionId { get; set; }

        public Guid PartnerId { get; set; }

        public Guid ManagerId { get; set; }
    }
}
