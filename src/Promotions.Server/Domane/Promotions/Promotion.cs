namespace Domain.Promotions
{
    public class Promotion : BaseEntity
    {
        public Guid ExternalId { get; set; }

        public string Name { get; set; } = string.Empty;

        public virtual Picture? LogoPicture { get; set; }

        public string Header { get; set; } = string.Empty;

        public string Conditions { get; set; } = string.Empty;

        public string SpecialConditions { get; set; } = string.Empty;

        public string SpecialConditions2 { get; set; } = string.Empty;

        public string Basement { get; set; } = string.Empty;

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public bool Enable { get; set; }

        public int Type { get; set; }

        public virtual List<PromotionGoods> PromotionGoods { get; set; } = new();
    }
}
