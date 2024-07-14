namespace Application.Services.GoodsService
{
    public class GoodsCreateOrEdit
    {
        public Guid ExternalId { get; set; }

        public string Name { get; set; } = string.Empty;

        public Guid PictureId { get; set; }
    }
}
