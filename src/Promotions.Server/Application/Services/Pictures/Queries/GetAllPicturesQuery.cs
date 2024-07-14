using AutoMapper;
using Domain.Abstractions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.Pictures.Queries
{
    public class GetAllPicturesQuery : IRequest<IEnumerable<PictureResponse>> { }

    public class GetAllPicturesQueryHandler : IRequestHandler<GetAllPicturesQuery, IEnumerable<PictureResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IEfRepository<Picture> _pictureRepository;

        public GetAllPicturesQueryHandler(IMapper mapper,
            IEfRepository<Picture> pictureRepository)
        {
            _mapper = mapper;

            _pictureRepository = pictureRepository;
        }

        public async Task<IEnumerable<PictureResponse>> Handle(GetAllPicturesQuery request, CancellationToken cancellationToken)
        {
            var pictures = await _pictureRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<PictureResponse>>(pictures);
        }
    }
}
