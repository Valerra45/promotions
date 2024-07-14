namespace Application.Services.Orders
{
    public class OrderGoodsCreateOrEdit
    {
        public Guid GoodsId { get; set; }

        public decimal Price { get; set; }

        public decimal Count { get; set; }
    }
}
