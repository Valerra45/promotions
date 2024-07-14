using Domain.Abstractions;
using Domain.Exeptions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.SendPromotions.Commands
{
    public class DeleteSendPromotionCommand : IRequest
    {
        public Guid Id { get; }

        public DeleteSendPromotionCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteSendPromotionCommandHandler : IRequestHandler<DeleteSendPromotionCommand>
    {
        private readonly IEfRepository<SendPromotion> _sendPromotionRepository;

        public DeleteSendPromotionCommandHandler(IEfRepository<SendPromotion> sendPromotionRepository)
        {
            _sendPromotionRepository = sendPromotionRepository;
        }

        public async Task Handle(DeleteSendPromotionCommand request, CancellationToken cancellationToken)
        {
            var sendPromotion = await _sendPromotionRepository.GetByIdAsync(request.Id);

            if (sendPromotion is null)
            {
                throw new EntityNotFoundException($"{nameof(SendPromotion)} with id '{request.Id}' doesn't exist");
            }

            await _sendPromotionRepository.DeleteAsync(sendPromotion);
        }
    }
}
