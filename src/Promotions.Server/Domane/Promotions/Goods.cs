namespace Domain.Promotions
{
    public class Goods : BaseEntity
    {
        public Guid ExternalId { get; set; }

        public string Name { get; set; } = string.Empty;

        public virtual Picture? Picture { get; set; }
    }
}
