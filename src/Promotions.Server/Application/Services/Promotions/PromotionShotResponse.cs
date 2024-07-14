namespace Application.Services.Promotions
{
    public class PromotionShotResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool Enable { get; set; }
    }
}
