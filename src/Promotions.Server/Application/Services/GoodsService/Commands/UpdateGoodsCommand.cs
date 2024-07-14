using AutoMapper;
using Domain.Abstractions;
using Domain.Exeptions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.GoodsService.Commands
{
    public class UpdateGoodsCommand : IRequest
    {
        public Guid Id { get; }
        public GoodsCreateOrEdit Goods { get; }

        public UpdateGoodsCommand(Guid id, GoodsCreateOrEdit goods)
        {
            Id = id;

            Goods = goods;
        }
    }

    public class UpdateGoodsCommandHandler : IRequestHandler<UpdateGoodsCommand>
    {
        private readonly IMapper _mapper;
        private readonly IEfRepository<Goods> _goodsRepository;
        private readonly IEfRepository<Picture> _pictureRepository;

        public UpdateGoodsCommandHandler(IMapper mapper,
            IEfRepository<Goods> goodsRepository,
            IEfRepository<Picture> pictureRepository)
        {
            _mapper = mapper;

            _goodsRepository = goodsRepository;

            _pictureRepository = pictureRepository;
        }

        public async Task Handle(UpdateGoodsCommand request, CancellationToken cancellationToken)
        {
            var goods = await _goodsRepository.GetByIdAsync(request.Id);

            if (goods == null)
            {
                throw new EntityNotFoundException($"{nameof(Domain.Promotions.Goods)} with id '{request.Id}' doesn't exist");
            }

            var picture = await _pictureRepository.GetByIdAsync(request.Goods.PictureId);

            if (picture is null)
            {
                throw new EntityNotFoundException($"{nameof(Picture)} with id '{request.Goods.PictureId}' doesn't exist");
            }

            goods.Name = request.Goods.Name;
            goods.Picture = picture;
            goods.ExternalId = request.Goods.ExternalId;

            goods.Update = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Russian Standard Time");

            await _goodsRepository.UpdateAsync(goods);
        }
    }
}
