using AutoMapper;
using Domain.Abstractions;
using Domain.Exeptions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.Promotions.Queries
{
    public class GetPromotionByIdQuery : IRequest<PromotionResponse>
    {
        public Guid Id { get; }

        public GetPromotionByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetPromotionByIdQueryHandler : IRequestHandler<GetPromotionByIdQuery, PromotionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEfRepository<Promotion> _promotionRepository;

        public GetPromotionByIdQueryHandler(IMapper mapper,
            IEfRepository<Promotion> promotionRepository)
        {
            _mapper = mapper;

            _promotionRepository = promotionRepository;
        }

        public async Task<PromotionResponse> Handle(GetPromotionByIdQuery request, CancellationToken cancellationToken)
        {
            var promotion = await _promotionRepository.GetByIdAsync(request.Id);

            if (promotion is null)
            {
                throw new EntityNotFoundException($"{nameof(Promotion)} with id '{request.Id}' doesn't exist");
            }

            return _mapper.Map<PromotionResponse>(promotion);
        }
    }
}
