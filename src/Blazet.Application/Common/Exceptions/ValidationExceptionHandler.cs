using Blazet.Application.Common.Models;
using MediatR.Pipeline;
using MediatR;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Blazet.Application.Common.Exceptions
{
    public class ValidationExceptionHandler<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse,
    TException>
    where TRequest : IRequest<Result<int>>
    where TResponse : Result<int>
    where TException : ValidationException
    {
    private readonly ILogger<ValidationExceptionHandler<TRequest, TResponse, TException>> _logger;

        public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state,
    CancellationToken cancellationToken)
        {
            state.SetHandled(
                (TResponse)Result<int>.Failure(exception.Errors.Select(x => x.ErrorMessage).Distinct().ToArray()));
            return Task.CompletedTask;
        }
    }
}
