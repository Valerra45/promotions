using AutoMapper;
using Domain.Abstractions;
using Domain.Exeptions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.SendPromotions.Commands
{
    public class CreateSendPromotionCommand : IRequest<SendPromotionResponse>
    {
        public SendPromotionCreateOrEdit SendPromotion { get; }

        public CreateSendPromotionCommand(SendPromotionCreateOrEdit sendPromotion)
        {
            SendPromotion = sendPromotion;
        }
    }

    public class SendPromotionCreateCommandHandler : IRequestHandler<CreateSendPromotionCommand, SendPromotionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEfRepository<SendPromotion> _sendPromotionRepository;
        private readonly IEfRepository<Promotion> _promotionRepository;
        private readonly IEfRepository<Partner> _partnerRepository;
        private readonly IEfRepository<Manager> _managerRepository;

        public SendPromotionCreateCommandHandler(IMapper mapper,
            IEfRepository<SendPromotion> sendPromotionRepository,
            IEfRepository<Promotion> promotionRepository,
            IEfRepository<Partner> partnerRepository,
            IEfRepository<Manager> managerRepository)
        {
            _mapper = mapper;

            _sendPromotionRepository = sendPromotionRepository;

            _promotionRepository = promotionRepository;

            _partnerRepository = partnerRepository;

            _managerRepository = managerRepository;
        }

        public async Task<SendPromotionResponse> Handle(CreateSendPromotionCommand request, CancellationToken cancellationToken)
        {
            var sendPromotion = _mapper.Map<SendPromotion>(request.SendPromotion);

            var promotion = await _promotionRepository.GetByIdAsync(request.SendPromotion.PromotionId);

            if (promotion is null)
            {
                throw new EntityNotFoundException($"{nameof(Promotion)} with id '{request.SendPromotion.PromotionId}' doesn't exist");
            }

            var partner = await _partnerRepository.GetByIdAsync(request.SendPromotion.PartnerId);

            if (partner is null)
            {
                throw new EntityNotFoundException($"{nameof(Partner)} with id '{request.SendPromotion.PartnerId}' doesn't exist");
            }

            var manager = await _managerRepository.GetByIdAsync(request.SendPromotion.ManagerId);

            if (manager is null)
            {
                throw new EntityNotFoundException($"{nameof(Manager)} with id '{request.SendPromotion.ManagerId}' doesn't exist");
            }

            sendPromotion.Promotion = promotion;
            sendPromotion.Manager = manager;
            sendPromotion.Partner = partner;

            await _sendPromotionRepository.AddAsync(sendPromotion);

            return _mapper.Map<SendPromotionResponse>(sendPromotion);
        }
    }
}
