using AutoMapper;
using Domain.Abstractions;
using Domain.Exeptions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.Orders.Commands
{
    public class UpdateOrderCommand : IRequest
    {
        public Guid Id { get; }
        public OrderCreateOrEdit Order { get; }

        public UpdateOrderCommand(Guid id, OrderCreateOrEdit order)
        {
            Id = id;

            Order = order;
        }
    }

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IMapper _mapper;
        private readonly IEfRepository<Order> _orderRepository;
        private readonly IEfRepository<Partner> _partnerRepository;
        private readonly IEfRepository<Goods> _goodsRepository;
        private readonly IEfRepository<Promotion> _promotionRepository;

        public UpdateOrderCommandHandler(IMapper mapper,
            IEfRepository<Order> orderRepository,
            IEfRepository<Partner> partnerRepository,
            IEfRepository<Goods> goodsRepository,
            IEfRepository<Promotion> promotionRepository)
        {
            _mapper = mapper;

            _orderRepository = orderRepository;

            _partnerRepository = partnerRepository;

            _goodsRepository = goodsRepository;

            _promotionRepository = promotionRepository;
        }

        public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id);

            if (order is null)
            {
                throw new EntityNotFoundException($"{nameof(Order)} with id '{request.Id}' doesn't exist");
            }

            var partner = await _partnerRepository.GetByIdAsync(request.Order.PartnerId);

            if (partner is null)
            {
                throw new EntityNotFoundException($"{nameof(Partner)} with id '{request.Order.PartnerId}' doesn't exist");
            }

            var promotion = await _promotionRepository.GetByIdAsync(request.Order.PromotionId);

            if (promotion is null)
            {
                throw new EntityNotFoundException($"{nameof(Promotion)} with id '{request.Order.PromotionId}' doesn't exist");
            }

            order.Partner = partner;
            order.Promotion = promotion;

            order.OrderGoods.Clear();

            foreach (var orderGoodsRequest in request.Order.OrderGoods)
            {
                var goods = await _goodsRepository.GetByIdAsync(orderGoodsRequest.GoodsId);

                if (goods is null)
                {
                    throw new EntityNotFoundException($"{nameof(Goods)} with id '{orderGoodsRequest.GoodsId}' doesn't exist");
                }

                var orderGoods = _mapper.Map<OrderGoods>(orderGoodsRequest);
                orderGoods.Goods = goods;

                order.OrderGoods.Add(orderGoods);
            }

            order.Update = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Russian Standard Time");

            await _orderRepository.UpdateAsync(order);
        }
    }
}
