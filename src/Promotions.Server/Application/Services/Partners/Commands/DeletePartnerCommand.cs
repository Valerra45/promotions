using Domain.Abstractions;
using Domain.Exeptions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.Partners.Commands
{
    public class DeletePartnerCommand : IRequest
    {
        public Guid Id { get; }

        public DeletePartnerCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeletePartnerCommandHandler : IRequestHandler<DeletePartnerCommand>
    {
        private readonly IEfRepository<Partner> _partnerRepository;

        public DeletePartnerCommandHandler(IEfRepository<Partner> partnerRepository)
        {
            _partnerRepository = partnerRepository;
        }

        public async Task Handle(DeletePartnerCommand request, CancellationToken cancellationToken)
        {
            var partner = await _partnerRepository.GetByIdAsync(request.Id);

            if (partner is null)
            {
                throw new EntityNotFoundException($"{nameof(Partner)} with id '{request.Id}' doesn't exist");
            }

            await _partnerRepository.DeleteAsync(partner);
        }

    }
}
