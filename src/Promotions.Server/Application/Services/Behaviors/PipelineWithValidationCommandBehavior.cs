using FluentValidation;
using MediatR;

namespace Application.Services.Behaviors
{
    public class PipelineWithValidationCommandBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public PipelineWithValidationCommandBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults.SelectMany(r => r.Errors)
                    .Where(f => f != null).ToList();

                if (failures.Count > 0)
                {
                    throw new ValidationException(string.Join(", ", failures.Select(f => f.ErrorMessage)));
                }

            }

            return await next();
        }
    }
}
