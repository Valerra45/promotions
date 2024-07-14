using Microsoft.AspNetCore.Http;
using MongoDB.Bson;

namespace Domain.Abstractions
{
    public interface IMongoRepository
    {
        Task<ObjectId> UploadPictureAsync(string filename, Stream source);

        Task GetPictureByIdAsync(HttpContext context, string id);

        Task DeletePictureAsync(string id);
    }
}
