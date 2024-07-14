using Domain.Abstractions;
using Domain.Exeptions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.Pictures.Commands
{
    public class DeletePictureCommand : IRequest
    {
        public Guid Id { get; }

        public DeletePictureCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeletePictureCommandHandler : IRequestHandler<DeletePictureCommand>
    {
        private readonly IEfRepository<Picture> _pictureRepository;
        private readonly IMongoRepository _mongoRepository;

        public DeletePictureCommandHandler(IEfRepository<Picture> pictureRepository,
            IMongoRepository mongoRepository)
        {
            _pictureRepository = pictureRepository;

            _mongoRepository = mongoRepository;
        }

        public async Task Handle(DeletePictureCommand request, CancellationToken cancellationToken)
        {
            var picture = await _pictureRepository.GetByIdAsync(request.Id);

            if (picture == null)
            {
                throw new EntityNotFoundException($"{nameof(Picture)} with id '{request.Id}' doesn't exist");
            }

            await _mongoRepository.DeletePictureAsync(picture.MongoId);

            await _pictureRepository.DeleteAsync(picture);
        }
    }
}
