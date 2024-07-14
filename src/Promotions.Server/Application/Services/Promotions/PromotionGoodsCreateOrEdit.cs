namespace Application.Services.Promotions
{
    public class PromotionGoodsCreateOrEdit
    {
        public Guid GoodsId { get; set; }

        public string VendorCode { get; set; } = string.Empty;

        public string GoodsDescription { get; set; } = string.Empty;

        public string PromotionDescription { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}
