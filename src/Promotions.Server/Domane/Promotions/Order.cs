namespace Domain.Promotions
{
    public class Order : BaseEntity
    {
        public virtual Partner? Partner { get; set; }

        public virtual Promotion? Promotion { get; set; }

        public virtual List<OrderGoods> OrderGoods { get; set; } = new();
    }
}
