using MediatR;
using N5.Challenge.Domain.Abstractions;

namespace N5.Challenge.Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> 
    where TQuery : IRequest<Result<TResponse>>
{

}