using Application.Services.GoodsService;

namespace Application.Services.Orders
{
    public class OrderGoodsResponse
    {
        public Guid Id { get; set; }

        public GoodsResponse? Goods { get; set; }

        public decimal Price { get; set; }

        public decimal Count { get; set; }
    }
}
