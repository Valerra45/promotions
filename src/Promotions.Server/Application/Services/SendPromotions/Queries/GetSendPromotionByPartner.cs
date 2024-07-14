using Application.Services.Orders;
using AutoMapper;
using Domain.Abstractions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.SendPromotions.Queries
{
    public class GetSendPromotionByPartner : IRequest<IEnumerable<SendPromotionShotResponse>>
    {
        public ByPartnerRequest Partner { get; }

        public GetSendPromotionByPartner(ByPartnerRequest partner)
        {
            Partner = partner;
        }
    }

    public class GetSendPromotionByPartnerHandler
        : IRequestHandler<GetSendPromotionByPartner, IEnumerable<SendPromotionShotResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IEfRepository<SendPromotion> _sendPromotionRepository;

        public GetSendPromotionByPartnerHandler(IMapper mapper,
            IEfRepository<SendPromotion> sendPromotionRepository)
        {
            _mapper = mapper;

            _sendPromotionRepository = sendPromotionRepository;
        }

        public async Task<IEnumerable<SendPromotionShotResponse>> Handle(GetSendPromotionByPartner request,
            CancellationToken cancellationToken)
        {
            var sendPromotions = await _sendPromotionRepository
                .GetWhere(x => x.Partner!.Name.Equals(request.Partner.UserName));

            return _mapper.Map<IEnumerable<SendPromotionShotResponse>>(sendPromotions);
        }
    }
}
