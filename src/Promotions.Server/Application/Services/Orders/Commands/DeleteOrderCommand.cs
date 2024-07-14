using Domain.Abstractions;
using Domain.Exeptions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.Orders.Commands
{
    public class DeleteOrderCommand : IRequest
    {
        public Guid Id { get; }

        public DeleteOrderCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IEfRepository<Order> _orderRepository;

        public DeleteOrderCommandHandler(IEfRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id);

            if (order is null)
            {
                throw new EntityNotFoundException($"{nameof(Order)} with id '{request.Id}' doesn't exist");
            }

            await _orderRepository.DeleteAsync(order);
        }
    }
}
