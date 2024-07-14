using AutoMapper;
using Domain.Abstractions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.Orders.Queries
{
    public class GetAllOrdersQuery : IRequest<IEnumerable<OrderShotResponse>> { }

    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderShotResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IEfRepository<Order> _orderRepository;

        public GetAllOrdersQueryHandler(IMapper mapper,
            IEfRepository<Order> orderRepository)
        {
            _mapper = mapper;

            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderShotResponse>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<OrderShotResponse>>(orders);
        }
    }
}
