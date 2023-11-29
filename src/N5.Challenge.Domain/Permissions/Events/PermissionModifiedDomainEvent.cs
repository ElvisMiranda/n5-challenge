using N5.Challenge.Domain.Abstractions;

namespace N5.Challenge.Domain.Permissions.Events;

public sealed record PermissionModifiedDomainEvent(int id) : IDomainEvent;