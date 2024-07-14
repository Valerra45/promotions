namespace Domain.Promotions
{
    public class Partner : BaseEntity
    {
        public Guid ExternalId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
