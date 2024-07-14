using AutoMapper;
using Domain.Abstractions;
using Domain.Exeptions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.GoodsService.Queries
{
    public class GetGoodsByIdQuery : IRequest<GoodsResponse>
    {
        public Guid Id { get; }

        public GetGoodsByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetGoodsByIdQueryHandler : IRequestHandler<GetGoodsByIdQuery, GoodsResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEfRepository<Goods> _goodsRepository;

        public GetGoodsByIdQueryHandler(IMapper mapper,
            IEfRepository<Goods> goodsRepository)
        {
            _mapper = mapper;

            _goodsRepository = goodsRepository;
        }

        public async Task<GoodsResponse> Handle(GetGoodsByIdQuery request, CancellationToken cancellationToken)
        {
            var goods = await _goodsRepository.GetByIdAsync(request.Id);

            if (goods is null)
            {
                throw new EntityNotFoundException($"{nameof(Goods)} with id '{request.Id}' doesn't exist");
            }

            return _mapper.Map<GoodsResponse>(goods);
        }
    }
}
