using Domain.Abstractions;
using Domain.Exeptions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.Promotions.Commands
{
    public class DeletePromotionCommand : IRequest
    {
        public Guid Id { get; }

        public DeletePromotionCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeletePromotionCommandHandler : IRequestHandler<DeletePromotionCommand>
    {
        private readonly IEfRepository<Promotion> _promotionRepository;

        public DeletePromotionCommandHandler(IEfRepository<Promotion> promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        public async Task Handle(DeletePromotionCommand request, CancellationToken cancellationToken)
        {
            var promotion = await _promotionRepository.GetByIdAsync(request.Id);

            if (promotion is null)
            {
                throw new EntityNotFoundException($"{nameof(Promotion)} with id '{request.Id}' doesn't exist");
            }

            await _promotionRepository.DeleteAsync(promotion);
        }
    }
}
