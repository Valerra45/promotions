using AutoMapper;
using Domain.Abstractions;
using Domain.Exeptions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.Managers.Commands
{
    public class UpdateManagerCommand : IRequest
    {
        public Guid Id { get; }
        public ManagerCreateOrEdit Manager { get; }

        public UpdateManagerCommand(Guid id, ManagerCreateOrEdit manager)
        {
            Id = id;

            Manager = manager;
        }

        public class UpdateManagerCommandHandler : IRequestHandler<UpdateManagerCommand>
        {
            private readonly IMapper _mapper;
            private readonly IEfRepository<Manager> _managerRepository;

            public UpdateManagerCommandHandler(IMapper mapper,
                IEfRepository<Manager> managerRepository)
            {
                _mapper = mapper;

                _managerRepository = managerRepository;
            }

            public async Task Handle(UpdateManagerCommand request, CancellationToken cancellationToken)
            {
                var manager = await _managerRepository.GetByIdAsync(request.Id);

                if (manager == null)
                {
                    throw new EntityNotFoundException($"{nameof(Domain.Promotions.Manager)} with id '{request.Id}' doesn't exist");
                }

                manager.Name = request.Manager.Name;
                manager.Email = request.Manager.Email;
                manager.Phone = request.Manager.Phone;

                manager.Update = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Russian Standard Time");

                await _managerRepository.UpdateAsync(manager);
            }
        }
    }
}
