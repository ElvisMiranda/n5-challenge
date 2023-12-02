using N5.Challenge.Application.Abstractions.Messaging;

namespace N5.Challenge.Application.Permissions.ModifyPermission;

public sealed record ModifyPermissionCommand(
    int Id,
    string Forename,
    string Surname,
    int PermissionType
    ) : ICommand;