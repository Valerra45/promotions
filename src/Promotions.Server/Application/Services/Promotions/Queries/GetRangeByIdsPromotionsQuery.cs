using AutoMapper;
using Domain.Abstractions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.Promotions.Queries
{
    public class GetRangeByIdsPromotionsQuery : IRequest<IEnumerable<PromotionShotResponse>>
    {
        public List<Guid> Ids { get; }

        public GetRangeByIdsPromotionsQuery(List<Guid> ids)
        {
            Ids = ids;
        }
    }

    public class GetAllPromotionsQueryHandler : IRequestHandler<GetRangeByIdsPromotionsQuery, IEnumerable<PromotionShotResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IEfRepository<Promotion> _promotionRepository;

        public GetAllPromotionsQueryHandler(IMapper mapper,
            IEfRepository<Promotion> promotionRepository)
        {
            _mapper = mapper;

            _promotionRepository = promotionRepository;
        }

        public async Task<IEnumerable<PromotionShotResponse>> Handle(GetRangeByIdsPromotionsQuery request, CancellationToken cancellationToken)
        {
            var promotions = await _promotionRepository
                .GetWhere(x => request.Ids.Contains(x.Id) && x.Enable);

            return _mapper.Map<IEnumerable<PromotionShotResponse>>(promotions);
        }
    }
}
