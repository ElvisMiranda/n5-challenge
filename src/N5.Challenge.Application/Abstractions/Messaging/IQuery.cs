using MediatR;
using N5.Challenge.Domain.Abstractions;

namespace N5.Challenge.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}