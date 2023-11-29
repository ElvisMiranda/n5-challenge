using N5.Challenge.Application.Abstractions.Messaging;

namespace N5.Challenge.Application.Permissions.GetPermission;

public sealed record GetPermissionQuery(int PermissionId) : IQuery<PermissionResponse>;