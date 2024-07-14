using AutoMapper;
using Domain.Abstractions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.Partners.Queries
{
    public class GetAllPartnersQuery : IRequest<IEnumerable<PartnerResponse>> { }

    public class GetAllPartnersQueryHandler : IRequestHandler<GetAllPartnersQuery, IEnumerable<PartnerResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IEfRepository<Partner> _partnerRepository;

        public GetAllPartnersQueryHandler(IMapper mapper,
            IEfRepository<Partner> partnerRepository)
        {
            _mapper = mapper;
            _partnerRepository = partnerRepository;
        }

        public async Task<IEnumerable<PartnerResponse>> Handle(GetAllPartnersQuery request, CancellationToken cancellationToken)
        {
            var partners = await _partnerRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<PartnerResponse>>(partners);
        }
    }
}
