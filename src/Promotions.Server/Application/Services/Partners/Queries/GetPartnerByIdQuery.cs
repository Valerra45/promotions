using AutoMapper;
using Domain.Abstractions;
using Domain.Exeptions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.Partners.Queries
{
    public class GetPartnerByIdQuery : IRequest<PartnerResponse>
    {
        public Guid Id { get; }

        public GetPartnerByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetPartnerByIdQueryHandler : IRequestHandler<GetPartnerByIdQuery, PartnerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEfRepository<Partner> _partnerRepository;

        public GetPartnerByIdQueryHandler(IMapper mapper,
            IEfRepository<Partner> partnerRepository)
        {
            _mapper = mapper;
            _partnerRepository = partnerRepository;
        }

        public async Task<PartnerResponse> Handle(GetPartnerByIdQuery request, CancellationToken cancellationToken)
        {
            var partner = await _partnerRepository.GetByIdAsync(request.Id);

            if (partner is null)
            {
                throw new EntityNotFoundException($"{nameof(Partner)} with id '{request.Id}' doesn't exist");
            }

            return _mapper.Map<PartnerResponse>(partner);
        }
    }
}
