using Application.Services.Pictures;

namespace Application.Services.GoodsService
{
    public class GoodsResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public PictureResponse? Picture { get; set; }
    }
}
