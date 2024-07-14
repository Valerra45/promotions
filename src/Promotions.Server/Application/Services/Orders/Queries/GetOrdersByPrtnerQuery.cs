using AutoMapper;
using Domain.Abstractions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.Orders.Queries
{
    public class GetOrdersByPartnerQuery : IRequest<IEnumerable<OrderShotResponse>>
    {
        public ByPartnerRequest Partner { get; }

        public GetOrdersByPartnerQuery(ByPartnerRequest partner)
        {
            Partner = partner;
        }
    }

    public class GetOrdersByPartnerQueryHandler
        : IRequestHandler<GetOrdersByPartnerQuery, IEnumerable<OrderShotResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IEfRepository<Order> _orderRepository;

        public GetOrdersByPartnerQueryHandler(IMapper mapper,
            IEfRepository<Order> orderRepository)
        {
            _mapper = mapper;

            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderShotResponse>> Handle(GetOrdersByPartnerQuery request,
            CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetWhere(x => x.Partner!.Name.Equals(request.Partner.UserName));

            return _mapper.Map<IEnumerable<OrderShotResponse>>(orders);
        }
    }
}
