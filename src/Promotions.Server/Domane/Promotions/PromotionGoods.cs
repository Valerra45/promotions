namespace Domain.Promotions
{
    public class PromotionGoods : BaseEntity
    {
        public virtual Goods? Goods { get; set; }

        public string VendorCode { get; set; } = string.Empty;

        public string GoodsDescription { get; set; } = string.Empty;

        public string PromotionDescription { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}
