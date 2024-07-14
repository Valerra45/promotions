using AutoMapper;
using Domain.Abstractions;
using Domain.Exeptions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.GoodsService.Commands
{
    public class CreateGoodsCommand : IRequest<GoodsResponse>
    {
        public GoodsCreateOrEdit Goods { get; }

        public CreateGoodsCommand(GoodsCreateOrEdit goods)
        {
            Goods = goods;
        }
    }

    public class CreateGoodsCommandHandler : IRequestHandler<CreateGoodsCommand, GoodsResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEfRepository<Goods> _goodsRepository;
        private readonly IEfRepository<Picture> _pictureRepository;

        public CreateGoodsCommandHandler(IMapper mapper,
            IEfRepository<Goods> goodsRepository,
            IEfRepository<Picture> pictureRepository)
        {
            _mapper = mapper;

            _goodsRepository = goodsRepository;

            _pictureRepository = pictureRepository;
        }

        public async Task<GoodsResponse> Handle(CreateGoodsCommand request, CancellationToken cancellationToken)
        {
            var goods = _mapper.Map<Goods>(request.Goods);

            var picture = await _pictureRepository.GetByIdAsync(request.Goods.PictureId);

            if (picture is null)
            {
                throw new EntityNotFoundException($"{nameof(Picture)} with id '{request.Goods.PictureId}' doesn't exist");
            }

            goods.Picture = picture;

            await _goodsRepository.AddAsync(goods);

            return _mapper.Map<GoodsResponse>(goods);
        }
    }
}
