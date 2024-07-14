namespace Domain.Promotions
{
    public class Picture : BaseEntity
    {
        public Guid ExternalId { get; set; }

        public string MongoId { get; set; } = string.Empty;
    }
}
