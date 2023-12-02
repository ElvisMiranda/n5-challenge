using N5.Challenge.Application.Abstractions.Messaging;
using N5.Challenge.Domain.Abstractions;
using N5.Challenge.Domain.Permissions;
using N5.Challenge.Domain.PermissionTypes;

namespace N5.Challenge.Application.Permissions.RequestPermission;

public class RequestPermissionCommandHandler : ICommandHandler<RequestPermissionCommand, int>
{
    private readonly IPermissionTypeRepository _permissionTypeRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RequestPermissionCommandHandler(
        IPermissionTypeRepository permissionTypeRepository, 
        IPermissionRepository permissionRepository, 
        IUnitOfWork unitOfWork)
    {
        _permissionTypeRepository = permissionTypeRepository;
        _permissionRepository = permissionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int>> Handle(RequestPermissionCommand request, CancellationToken cancellationToken)
    {
        var permissionType = await _permissionTypeRepository.GetByIdAsync(request.PermissionType, cancellationToken);
        
        if (permissionType is null)
        {
            return Result.Failure<int>(PermissionErrors.PermissionTypeNotFound);
        }

        var permission = Permission.Create(request.Name, request.Surname, request.PermissionType);

        _permissionRepository.Add(permission);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return permission.Id;
    }
}