namespace Domain.Promotions
{
    public class OrderGoods : BaseEntity
    {
        public virtual Goods? Goods { get; set; }

        public decimal Price { get; set; }

        public decimal Count { get; set; }
    }
}
