namespace N5.Challenge.Domain.PermissionTypes;

public interface IPermissionTypeRepository
{
    IQueryable<PermissionType> All();
    Task<PermissionType?> GetByIdAsync(int permissionTypeId, CancellationToken cancellationToken);
}