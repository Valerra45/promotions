using AutoMapper;
using Domain.Abstractions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.Partners.Commands
{
    public class CreatePartnerCommand : IRequest<PartnerResponse>
    {
        public PartnerCreateOrEdit Partner { get; }

        public CreatePartnerCommand(PartnerCreateOrEdit partner)
        {
            Partner = partner;
        }
    }

    public class CreatePartnerCommandHandler : IRequestHandler<CreatePartnerCommand, PartnerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEfRepository<Partner> _partnerRepository;

        public CreatePartnerCommandHandler(IMapper mapper,
            IEfRepository<Partner> partnerRepository)
        {
            _mapper = mapper;
            _partnerRepository = partnerRepository;
        }

        public async Task<PartnerResponse> Handle(CreatePartnerCommand request, CancellationToken cancellationToken)
        {
            var partner = _mapper.Map<Partner>(request.Partner);

            await _partnerRepository.AddAsync(partner);

            return _mapper.Map<PartnerResponse>(partner);
        }
    }
}
