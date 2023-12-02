using Moq;
using N5.Challenge.Domain.Abstractions;
using N5.Challenge.Domain.Permissions;
using N5.Challenge.Domain.PermissionTypes;

namespace N5.Challenge.Tests.Application.Permissions;

public class PermissionsTests
{
    public static (
        Mock<IPermissionRepository> permissionRepositoryMock,
        Mock<IPermissionTypeRepository> permissionTypeRepositoryMock,
        Mock<IUnitOfWork> unitOfWorkMock
        ) Setup()
    {
        var permissionTypeRepositoryMock = new Mock<IPermissionTypeRepository>();
        var permissionRepositoryMock = new Mock<IPermissionRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        const int permissionId = 1;
        const int permissionTypeId = 1;
        
        var permission = Permission.Create(It.IsAny<string>(), It.IsAny<string>(), permissionTypeId);
        var permissionType = new PermissionType(permissionTypeId, It.IsAny<string>());
        
        permissionRepositoryMock
            .Setup(p => p.Add(permission));
            
        permissionRepositoryMock
            .Setup(p => p.GetByIdAsync(permissionId, default))
            .ReturnsAsync(permission);
        
        permissionTypeRepositoryMock
            .Setup(pt => pt.GetByIdAsync(permissionTypeId, default))
            .ReturnsAsync(permissionType);

        unitOfWorkMock
            .Setup(uof => uof.SaveChangesAsync(default))
            .ReturnsAsync(It.IsAny<int>());
        
        return (
            permissionRepositoryMock, 
            permissionTypeRepositoryMock, 
            unitOfWorkMock);
    }
}