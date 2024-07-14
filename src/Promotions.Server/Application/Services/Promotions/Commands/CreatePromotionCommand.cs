using AutoMapper;
using Domain.Abstractions;
using Domain.Exeptions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.Promotions.Commands
{
    public class CreatePromotionCommand : IRequest<PromotionShotResponse>
    {
        public PromotionCreateOrEdit Promotion { get; }

        public CreatePromotionCommand(PromotionCreateOrEdit promotion)
        {
            Promotion = promotion;
        }

    }

    public class CreatePromotionCommandHandler : IRequestHandler<CreatePromotionCommand, PromotionShotResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEfRepository<Promotion> _promotionRepository;
        private readonly IEfRepository<Picture> _pictureRepository;
        private readonly IEfRepository<Goods> _goodsRepository;

        public CreatePromotionCommandHandler(IMapper mapper,
            IEfRepository<Promotion> promotionRepository,
            IEfRepository<Picture> pictureRepository,
            IEfRepository<Goods> goodsRepository)
        {
            _mapper = mapper;

            _promotionRepository = promotionRepository;

            _pictureRepository = pictureRepository;

            _goodsRepository = goodsRepository;
        }

        public async Task<PromotionShotResponse> Handle(CreatePromotionCommand request, CancellationToken cancellationToken)
        {
            var promotion = _mapper.Map<Promotion>(request.Promotion);

            var picture = await _pictureRepository.GetByIdAsync(request.Promotion.LogoPictureId);

            if (picture is null)
            {
                throw new EntityNotFoundException($"{nameof(Picture)} with id '{request.Promotion.LogoPictureId}' doesn't exist");
            }

            foreach (var goodsRequest in request.Promotion.PromotionGoods)
            {
                var goods = await _goodsRepository.GetByIdAsync(goodsRequest.GoodsId);

                if (goods is null)
                {
                    throw new EntityNotFoundException($"{nameof(Goods)} with id '{goodsRequest.GoodsId}' doesn't exist");
                }

                var promotionGoods = _mapper.Map<PromotionGoods>(goodsRequest);

                promotionGoods.Goods = goods;

                promotion.PromotionGoods.Add(promotionGoods);
            }

            promotion.LogoPicture = picture;

            await _promotionRepository.AddAsync(promotion);

            return _mapper.Map<PromotionShotResponse>(promotion);
        }
    }
}
