namespace Application.Services.Pictures
{
    public class PictureResponse
    {
        public Guid Id { get; set; }

        public string MongoId { get; set; } = string.Empty;
    }
}
