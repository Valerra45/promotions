using AutoMapper;
using Domain.Abstractions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.GoodsService.Queries
{
    public class GetAllGoodsQuery : IRequest<IEnumerable<GoodsResponse>> { }

    public class GetAllGoodsQueryHandler : IRequestHandler<GetAllGoodsQuery, IEnumerable<GoodsResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IEfRepository<Goods> _goodsRepository;

        public GetAllGoodsQueryHandler(IMapper mapper,
            IEfRepository<Goods> goodsRepository)
        {
            _mapper = mapper;

            _goodsRepository = goodsRepository;
        }

        public async Task<IEnumerable<GoodsResponse>> Handle(GetAllGoodsQuery request, CancellationToken cancellationToken)
        {
            var goods = await _goodsRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<GoodsResponse>>(goods);
        }
    }
}
