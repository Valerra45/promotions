using Domain.Exeptions;
using System.Net;

namespace WebApi.Helpers
{
    public static class ExceptionsHelper
    {
        public static Dictionary<Type, HttpStatusCode> ExceptionsHttpStatusCodes => new()
        {
            [typeof(ValidationException)] = HttpStatusCode.UnprocessableEntity,
            [typeof(EntityNotFoundException)] = HttpStatusCode.NotFound
        };
    }
}
