using AutoMapper;
using Domain.Abstractions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.Pictures.Commands
{
    public class CreatePictureCommand : IRequest<PictureResponse>
    {
        public PictureCreate PictureCreate { get; set; }

        public CreatePictureCommand(PictureCreate pictureCreate)
        {
            PictureCreate = pictureCreate;
        }
    }

    public class CreatePictureCommandHandler : IRequestHandler<CreatePictureCommand, PictureResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEfRepository<Picture> _pictureRepositiry;
        private readonly IMongoRepository _mongoRepository;

        public CreatePictureCommandHandler(IMapper mapper,
            IEfRepository<Picture> pictureRepositiry,
            IMongoRepository mongoRepository)
        {
            _mapper = mapper;

            _pictureRepositiry = pictureRepositiry;

            _mongoRepository = mongoRepository;
        }

        public async Task<PictureResponse> Handle(CreatePictureCommand request, CancellationToken cancellationToken)
        {
            var id = request.PictureCreate.Id;
            var file = request.PictureCreate.File;

            var picture = new Picture();

            picture.ExternalId = new Guid(id);

            using (var stream = file!.OpenReadStream())
            {
                var fileId = await _mongoRepository.UploadPictureAsync(file.FileName, stream);

                picture.MongoId = fileId.ToString();
            }

            await _pictureRepositiry.AddAsync(picture);

            return _mapper.Map<PictureResponse>(picture);
        }
    }
}
