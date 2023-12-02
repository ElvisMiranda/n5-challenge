using N5.Challenge.Application.Abstractions.Messaging;

namespace N5.Challenge.Application.Permissions.RequestPermission;

public sealed record RequestPermissionCommand(
    string Name,
    string Surname,
    int PermissionType
    ) : ICommand<int>;