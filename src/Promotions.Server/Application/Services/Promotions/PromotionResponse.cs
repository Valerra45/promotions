using Application.Services.Pictures;

namespace Application.Services.Promotions
{
    public class PromotionResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public PictureResponse Picture { get; set; } = new();

        public string Header { get; set; } = string.Empty;

        public string Conditions { get; set; } = string.Empty;

        public string SpecialConditions { get; set; } = string.Empty;

        public string SpecialConditions2 { get; set; } = string.Empty;

        public string Basement { get; set; } = string.Empty;

        public virtual List<PromotionGoodsResponse> Goods { get; set; } = new();
    }
}
