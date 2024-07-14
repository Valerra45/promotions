using AutoMapper;
using Domain.Abstractions;
using Domain.Exeptions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.Partners.Commands
{
    public class UpdatePartnerCommand : IRequest
    {
        public Guid Id { get; }

        public PartnerCreateOrEdit Partner { get; }

        public UpdatePartnerCommand(Guid id, PartnerCreateOrEdit partner)
        {
            Id = id;
            Partner = partner;
        }

        public class UpdatePartnerCommandHandler : IRequestHandler<UpdatePartnerCommand>
        {
            private readonly IMapper _mapper;
            private readonly IEfRepository<Partner> _partnerRepository;

            public UpdatePartnerCommandHandler(IMapper mapper,
                IEfRepository<Partner> partnerRepository)
            {
                _mapper = mapper;
                _partnerRepository = partnerRepository;
            }

            public async Task Handle(UpdatePartnerCommand request, CancellationToken cancellationToken)
            {
                var partner = await _partnerRepository.GetByIdAsync(request.Id);

                if (partner is null)
                {
                    throw new EntityNotFoundException($"{nameof(Domain.Promotions.Partner)} with id '{request.Id}' doesn't exist");
                }

                partner.Name = request.Partner.Name;
                partner.Email = request.Partner.Email;
                partner.Update = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Russian Standard Time");

                await _partnerRepository.UpdateAsync(partner);
            }
        }
    }
}
