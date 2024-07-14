using Domain.Abstractions;
using Infrastructure.Environment;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace Infrastructure.Repositories
{
    public class MongoRepository : IMongoRepository
    {
        private readonly IGridFSBucket _gridFS;

        public MongoRepository(IPicturesDatabaseSettings settings)
        {
            var mongoDatabase = new MongoClient(settings.ConnectionString)
              .GetDatabase(settings.DatabaseName);

            _gridFS = new GridFSBucket(mongoDatabase);
        }

        public async Task<ObjectId> UploadPictureAsync(string filename, Stream source)
        {
            return await _gridFS.UploadFromStreamAsync(filename, source);
        }

        public async Task GetPictureByIdAsync(HttpContext context, string id)
        {
            await _gridFS.DownloadToStreamAsync(new ObjectId(id), context.Response.Body);
        }

        public async Task DeletePictureAsync(string id)
        {
            await _gridFS.DeleteAsync(new ObjectId(id));
        }
    }
}
