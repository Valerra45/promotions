namespace Application.Services.Promotions
{
    public class PromotionCreateOrEdit
    {
        public Guid ExternalId { get; set; }

        public string Name { get; set; } = string.Empty;

        public Guid LogoPictureId { get; set; }

        public string Header { get; set; } = string.Empty;

        public string Conditions { get; set; } = string.Empty;

        public string SpecialConditions { get; set; } = string.Empty;

        public string SpecialConditions2 { get; set; } = string.Empty;

        public string Basement { get; set; } = string.Empty;

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public bool Enable { get; set; }

        public int Type { get; set; }

        public virtual List<PromotionGoodsCreateOrEdit> PromotionGoods { get; set; } = new();
    }
}
