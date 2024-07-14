using AutoMapper;
using Domain.Abstractions;
using Domain.Exeptions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.Pictures.Queries
{
    public class GetPictureByIdQuery : IRequest<PictureResponse>
    {
        public Guid Id { get; set; }

        public GetPictureByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetPictureByIdQueryHandler : IRequestHandler<GetPictureByIdQuery, PictureResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEfRepository<Picture> _pictureRepository;

        public GetPictureByIdQueryHandler(IMapper mapper,
            IEfRepository<Picture> pictureRepository)
        {
            _mapper = mapper;
            _pictureRepository = pictureRepository;
        }

        public async Task<PictureResponse> Handle(GetPictureByIdQuery request, CancellationToken cancellationToken)
        {
            var picture = await _pictureRepository.GetByIdAsync(request.Id);

            if (picture is null)
            {
                throw new EntityNotFoundException($"{nameof(Picture)} with id '{request.Id}' doesn't exist");
            }

            return _mapper.Map<PictureResponse>(picture);
        }
    }
}
