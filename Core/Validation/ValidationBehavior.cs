using FluentValidation;
using MediatR;

namespace Core.Validation
{
    public class ValidationBehavior<TRequest, TResult> : IPipelineBehavior<TRequest, TResult>
    {

       private readonly IValidator<TRequest> _validator;

        public ValidationBehavior(IValidator<TRequest> validator)
        {
            _validator = validator;
        }
        
        public async Task<TResult> Handle(TRequest request, 
            RequestHandlerDelegate<TResult> next, 
            CancellationToken cancellationToken)
        {
            await _validator.ValidateAndThrowAsync(request);
            return await next();
        }
    }
}
