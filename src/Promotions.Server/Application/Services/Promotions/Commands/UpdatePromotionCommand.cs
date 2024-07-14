using AutoMapper;
using Domain.Abstractions;
using Domain.Exeptions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.Promotions.Commands
{
    public class UpdatePromotionCommand : IRequest
    {
        public Guid Id { get; }
        public PromotionCreateOrEdit Promotion { get; }

        public UpdatePromotionCommand(Guid id, PromotionCreateOrEdit promotion)
        {
            Id = id;

            Promotion = promotion;
        }

    }

    public class UpdatePromotionsCommandHandler : IRequestHandler<UpdatePromotionCommand>
    {
        private readonly IMapper _mapper;
        private readonly IEfRepository<Promotion> _promotionRepository;
        private readonly IEfRepository<Picture> _pictureRepository;
        private readonly IEfRepository<Goods> _goodsRepository;

        public UpdatePromotionsCommandHandler(IMapper mapper,
            IEfRepository<Promotion> promotionRepository,
            IEfRepository<Picture> pictureRepository,
            IEfRepository<Goods> goodsRepository
            )
        {
            _mapper = mapper;

            _promotionRepository = promotionRepository;

            _pictureRepository = pictureRepository;

            _goodsRepository = goodsRepository;
        }

        public async Task Handle(UpdatePromotionCommand request, CancellationToken cancellationToken)
        {
            var promotion = await _promotionRepository.GetByIdAsync(request.Id);

            if (promotion is null)
            {
                throw new EntityNotFoundException($"{nameof(Domain.Promotions.Promotion)} with id '{request.Id}' doesn't exist");
            }

            var picture = await _pictureRepository.GetByIdAsync(request.Promotion.LogoPictureId);

            if (picture is null)
            {
                throw new EntityNotFoundException($"{nameof(Domain.Promotions.Picture)} with id '{request.Promotion.LogoPictureId}' doesn't exist");
            }

            promotion.Name = request.Promotion.Name;
            promotion.LogoPicture = picture;
            promotion.Header = request.Promotion.Header;
            promotion.Conditions = request.Promotion.Conditions;
            promotion.SpecialConditions = request.Promotion.SpecialConditions;
            promotion.SpecialConditions2 = request.Promotion.SpecialConditions2;
            promotion.Basement = request.Promotion.Basement;
            promotion.Enable = request.Promotion.Enable;

            promotion.PromotionGoods.Clear();

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

            promotion.Update = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Russian Standard Time");

            await _promotionRepository.UpdateAsync(promotion);
        }
    }
}
