using N5.Challenge.Application.Abstractions.Messaging;
using N5.Challenge.Application.Permissions.Shared;
using N5.Challenge.Domain.Abstractions;
using N5.Challenge.Domain.Permissions;

namespace N5.Challenge.Application.Permissions.GetPermission;

public sealed class GetPermissionQueryHandler : IQueryHandler<GetPermissionQuery, PermissionResponse>
{
    private readonly IPermissionRepository _permissionRepository;

    public GetPermissionQueryHandler(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    public async Task<Result<PermissionResponse>> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
    {
        var permission = await _permissionRepository.GetByIdAsync(request.PermissionId, cancellationToken);

        if (permission is null)
        {
            return Result.Failure<PermissionResponse>(PermissionErrors.PermissionNotFound);
        }

        return ToResponse(permission);
    }

    private static PermissionResponse ToResponse(Permission? permission)
        => new()
        {
            Id = permission!.Id,
            EmployeeForename = permission.EmployeeForename,
            EmployeeSurname = permission.EmployeeSurname,
            PermissionType = permission.Id,
            PermissionDate = permission.PermissionDate
        };
}