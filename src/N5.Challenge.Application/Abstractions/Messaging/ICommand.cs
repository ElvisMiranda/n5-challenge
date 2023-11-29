using MediatR;

using N5.Challenge.Domain.Abstractions;

namespace N5.Challenge.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
}