namespace Domain.Promotions
{
    public class Manager : BaseEntity
    {
        public Guid ExternalId { get; set; }

        public String Name { get; set; } = string.Empty;

        public String Email { get; set; } = string.Empty;

        public String Phone { get; set; } = string.Empty;
    }
}
