using ErrorOr;

namespace N5.Challenge.Domain.Permissions;

public static class PermissionErrors
{
    public static Error NotFound => Error.NotFound("Permission.NotFound", "The permission with the specified identifier was not found");
}