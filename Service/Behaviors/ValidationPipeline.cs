using MediatR;
using Service.Command;
using Service.Responses;
using ErrorOr;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Exceptions;

namespace Service.Behaviors
{
    
    public class ValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<IRequest?>> _validators;

        public ValidationPipeline(IEnumerable<IValidator<IRequest?>> validators)
        {
            _validators = validators;
        }
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var validationFailures = _validators
                .Select(validator => validator.Validate((IRequest)request))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validationFailures => validationFailures != null)
                .ToList();
            if(validationFailures.Any())
            {
                throw new DomainException(
                    $"Command validation Error for type {typeof(TRequest).Name}",
                    new ValidationException("Validation exception", validationFailures)
                    );
            }
            return next();
        }
    }
}
