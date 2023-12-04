using N5.Challenge.Application.Abstractions.Messaging;
using N5.Challenge.Application.Permissions.Shared;
using N5.Challenge.Domain.Abstractions;
using N5.Challenge.Domain.Permissions;

namespace N5.Challenge.Application.Permissions.GetPermissions;

public sealed class GetPermissionsQueryHandler(IPermissionRepository permissionRepository) 
    : IQueryHandler<GetPermissionsQuery, IReadOnlyList<PermissionResponse>>
{
    public async Task<Result<IReadOnlyList<PermissionResponse>>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
    {
        var permissions = permissionRepository.All();

        return permissions.Select(p => ToResponse(p)).ToList();
    }

    private static PermissionResponse ToResponse(Permission? permission)
        => new()
        {
            Id = permission!.Id,
            EmployeeForename = permission.EmployeeForename,
            EmployeeSurname = permission.EmployeeSurname,
            PermissionType = permission.PermissionTypeId,
            PermissionDate = permission.PermissionDate
        };
}