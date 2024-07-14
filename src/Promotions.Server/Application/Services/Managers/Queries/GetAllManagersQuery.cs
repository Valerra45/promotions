using AutoMapper;
using Domain.Abstractions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.Managers.Queries
{
    public class GetAllManagersQuery : IRequest<IEnumerable<ManagerResponse>> { }

    public class GetAllManagersQueryHandler : IRequestHandler<GetAllManagersQuery, IEnumerable<ManagerResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IEfRepository<Manager> _managerRepository;

        public GetAllManagersQueryHandler(IMapper mapper,
            IEfRepository<Manager> managerRepository)
        {
            _mapper = mapper;
            _managerRepository = managerRepository;
        }

        public async Task<IEnumerable<ManagerResponse>> Handle(GetAllManagersQuery request, CancellationToken cancellationToken)
        {
            var managers = await _managerRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<ManagerResponse>>(managers);
        }
    }
}
