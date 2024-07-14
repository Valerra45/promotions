using Domain.Abstractions;
using Domain.Exeptions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.Managers.Commands
{
    public class DeleteManagerCommand : IRequest
    {
        public Guid Id { get; }

        public DeleteManagerCommand(Guid id)
        {
            Id = id;
        }
    }

    public class DeleteManagerCommandHandler : IRequestHandler<DeleteManagerCommand>
    {
        private readonly IEfRepository<Manager> _managerRepository;

        public DeleteManagerCommandHandler(IEfRepository<Manager> managerRepository)
        {
            _managerRepository = managerRepository;
        }

        public async Task Handle(DeleteManagerCommand request, CancellationToken cancellationToken)
        {
            var manager = await _managerRepository.GetByIdAsync(request.Id);

            if (manager is null)
            {
                throw new EntityNotFoundException($"{nameof(Manager)} with id '{request.Id}' doesn't exist");
            }

            await _managerRepository.DeleteAsync(manager);
        }
    }
}
