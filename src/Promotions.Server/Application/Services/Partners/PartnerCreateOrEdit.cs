namespace Application.Services.Partners
{
    public class PartnerCreateOrEdit
    {
        public Guid ExternalId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
    }
}
