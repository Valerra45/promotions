namespace Application.Services.Orders
{
    public class OrderCreateOrEdit
    {
        public Guid PartnerId { get; set; }

        public Guid PromotionId { get; set; }

        public List<OrderGoodsCreateOrEdit> OrderGoods { get; set; } = new();
    }
}
