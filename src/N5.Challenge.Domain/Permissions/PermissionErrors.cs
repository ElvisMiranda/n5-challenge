using ErrorOr;

namespace N5.Challenge.Domain.Permissions;

public static class PermissionErrors
{
    public static Error PermissionNotFound => Error.NotFound("Permission.NotFound", "The permission with the specified identifier was not found");

    public static Error PermissionTypeNotFound =>
        Error.NotFound("Permission.PermissionTypeNotFound", "Permission Type was not found");
}