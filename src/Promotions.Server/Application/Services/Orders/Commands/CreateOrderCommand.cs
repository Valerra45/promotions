using AutoMapper;
using Domain.Abstractions;
using Domain.Exeptions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.Orders.Commands
{
    public class CreateOrderCommand : IRequest<OrderShotResponse>
    {
        public OrderCreateOrEdit Order { get; }

        public CreateOrderCommand(OrderCreateOrEdit order)
        {
            Order = order;
        }
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderShotResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEfRepository<Order> _orderRepository;
        private readonly IEfRepository<Partner> _partnerRepository;
        private readonly IEfRepository<Goods> _goodsRepository;
        private readonly IEfRepository<Promotion> _promotionRepository;

        public CreateOrderCommandHandler(IMapper mapper,
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

        public async Task<OrderShotResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
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

            var order = new Order();

            order.Partner = partner;
            order.Promotion = promotion;

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

            await _orderRepository.AddAsync(order);

            return _mapper.Map<OrderShotResponse>(order);
        }
    }
}
