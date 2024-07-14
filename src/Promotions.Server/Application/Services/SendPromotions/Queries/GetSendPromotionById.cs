using AutoMapper;
using Domain.Abstractions;
using Domain.Exeptions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.SendPromotions.Queries
{
    public class GetSendPromotionById : IRequest<SendPromotionResponse>
    {
        public Guid Id { get; }

        public GetSendPromotionById(Guid id)
        {
            Id = id;
        }
    }

    public class GetSendPromotionByIdHandler : IRequestHandler<GetSendPromotionById, SendPromotionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEfRepository<SendPromotion> _sendPromotionRepository;

        public GetSendPromotionByIdHandler(IMapper mapper,
            IEfRepository<SendPromotion> sendPromotionRepository)
        {
            _mapper = mapper;

            _sendPromotionRepository = sendPromotionRepository;
        }

        public async Task<SendPromotionResponse> Handle(GetSendPromotionById request, CancellationToken cancellationToken)
        {
            var sendPromotion = await _sendPromotionRepository.GetByIdAsync(request.Id);

            if (sendPromotion is null)
            {
                throw new EntityNotFoundException($"{nameof(SendPromotion)} with id '{request.Id}' doesn't exist");
            }

            return _mapper.Map<SendPromotionResponse>(sendPromotion);
        }
    }
}
