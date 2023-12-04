using N5.Challenge.Application.Abstractions.Messaging;
using N5.Challenge.Domain.Abstractions;
using N5.Challenge.Domain.Permissions;
using N5.Challenge.Domain.PermissionTypes;

namespace N5.Challenge.Application.Permissions.ModifyPermission;

public class ModifyPermissionCommandHandler : ICommandHandler<ModifyPermissionCommand>
{
    private readonly IPermissionTypeRepository _permissionTypeRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ModifyPermissionCommandHandler(
        IPermissionTypeRepository permissionTypeRepository, 
        IPermissionRepository permissionRepository, 
        IUnitOfWork unitOfWork)
    {
        _permissionTypeRepository = permissionTypeRepository;
        _permissionRepository = permissionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ModifyPermissionCommand request, CancellationToken cancellationToken)
    {
        var permissionType = await _permissionTypeRepository.GetByIdAsync(request.PermissionType, cancellationToken);

        if (permissionType is null)
        {
            return Result.Failure(PermissionErrors.PermissionTypeNotFound);
        }

        var permission = await _permissionRepository.GetByIdAsync(request.Id, cancellationToken);

        if (permission is null)
        {
            return Result.Failure(PermissionErrors.PermissionNotFound);
        }

        permission.Update(request.Forename, request.Surname, request.PermissionType);

        _permissionRepository.Update(permission);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}