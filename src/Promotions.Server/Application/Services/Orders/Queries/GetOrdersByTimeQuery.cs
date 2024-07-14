using AutoMapper;
using Domain.Abstractions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.Orders.Queries
{
    public class GetOrdersByTimeQuery : IRequest<IEnumerable<OrderShotResponse>>
    {
        public OrdersByTimeRequest Time { get; }

        public GetOrdersByTimeQuery(OrdersByTimeRequest time)
        {
            Time = time;
        }
    }

    public class GetOrdersByTimeQueryHandler : IRequestHandler<GetOrdersByTimeQuery, IEnumerable<OrderShotResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IEfRepository<Order> _orderRepository;

        public GetOrdersByTimeQueryHandler(IMapper mapper,
            IEfRepository<Order> orderRepository)
        {
            _mapper = mapper;

            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderShotResponse>> Handle(GetOrdersByTimeQuery request,
            CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetWhere(x => x.Update >= request.Time.DateStart);

            return _mapper.Map<IEnumerable<OrderShotResponse>>(orders);
        }
    }
}
