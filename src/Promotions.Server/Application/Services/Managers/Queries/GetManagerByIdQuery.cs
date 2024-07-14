using AutoMapper;
using Domain.Abstractions;
using Domain.Exeptions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.Managers.Queries
{
    public class GetManagerByIdQuery : IRequest<ManagerResponse>
    {
        public Guid Id { get; }

        public GetManagerByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetManagerByIdQueryHandler : IRequestHandler<GetManagerByIdQuery, ManagerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEfRepository<Manager> _managerRepository;

        public GetManagerByIdQueryHandler(IMapper mapper,
            IEfRepository<Manager> managerRepository)
        {
            _mapper = mapper;
            _managerRepository = managerRepository;
        }

        public async Task<ManagerResponse> Handle(GetManagerByIdQuery request, CancellationToken cancellationToken)
        {
            var manager = await _managerRepository.GetByIdAsync(request.Id);

            if (manager is null)
            {
                throw new EntityNotFoundException($"{nameof(Manager)} with id '{request.Id}' doesn't exist");
            }

            return _mapper.Map<ManagerResponse>(manager);
        }
    }
}
