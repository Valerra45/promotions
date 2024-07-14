using Microsoft.AspNetCore.Http;

namespace Application.Services.Pictures
{
    public class PictureCreate
    {
        public string Id { get; set; } = string.Empty;

        public IFormFile? File { get; set; }
    }
}
