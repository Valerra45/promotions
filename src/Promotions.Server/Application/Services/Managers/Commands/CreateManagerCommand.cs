using AutoMapper;
using Domain.Abstractions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.Managers.Commands
{
    public class CreateManagerCommand : IRequest<ManagerResponse>
    {
        public ManagerCreateOrEdit Manager { get; }

        public CreateManagerCommand(ManagerCreateOrEdit manager)
        {
            Manager = manager;
        }
    }

    public class CreateManagerCommandHandler : IRequestHandler<CreateManagerCommand, ManagerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEfRepository<Manager> _managerRepository;

        public CreateManagerCommandHandler(IMapper mapper,
            IEfRepository<Manager> managerRepository)
        {
            _mapper = mapper;
            _managerRepository = managerRepository;
        }

        public async Task<ManagerResponse> Handle(CreateManagerCommand request, CancellationToken cancellationToken)
        {
            var manager = _mapper.Map<Manager>(request.Manager);

            await _managerRepository.AddAsync(manager);

            return _mapper.Map<ManagerResponse>(manager);
        }
    }
}
