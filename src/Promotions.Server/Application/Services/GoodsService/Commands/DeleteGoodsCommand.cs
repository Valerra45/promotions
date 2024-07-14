using Domain.Abstractions;
using Domain.Exeptions;
using Domain.Promotions;
using MediatR;

namespace Application.Services.GoodsService.Commands
{
    public class DeleteGoodsCommand : IRequest
    {
        public Guid Id { get; }

        public DeleteGoodsCommand(Guid id)
        {
            Id = id;
        }

    }

    public class DeleteGoodsCommandHandler : IRequestHandler<DeleteGoodsCommand>
    {
        private readonly IEfRepository<Goods> _goodsRepository;

        public DeleteGoodsCommandHandler(IEfRepository<Goods> goodsRepository)
        {
            _goodsRepository = goodsRepository;
        }

        public async Task Handle(DeleteGoodsCommand request, CancellationToken cancellationToken)
        {
            var goods = await _goodsRepository.GetByIdAsync(request.Id);

            if (goods is null)
            {
                throw new EntityNotFoundException($"{nameof(Goods)} with id '{request.Id}' doesn't exist");
            }

            await _goodsRepository.DeleteAsync(goods);
        }
    }
}
