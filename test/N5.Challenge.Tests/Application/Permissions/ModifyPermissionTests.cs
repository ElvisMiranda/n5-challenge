using FluentAssertions;
using Moq;
using N5.Challenge.Application.Permissions.ModifyPermission;
using N5.Challenge.Domain.Permissions;

namespace N5.Challenge.Tests.Application.Permissions;

public partial class PermissionTests
{
    [Fact]
    public async Task Handle_Should_ReturnPermissionTypeNotFound_WhenPermissionType_IsInvalid()
    {
        // Arrange
        const int permissionTypeId = 2;
        var mock = PermissionsTests.Setup();

        var command = new ModifyPermissionCommand(
            It.IsAny<int>(), 
            It.IsAny<string>(), 
            It.IsAny<string>(), 
            permissionTypeId);
        
        var handler = new ModifyPermissionCommandHandler(
            mock.permissionTypeRepositoryMock.Object,
            mock.permissionRepositoryMock.Object,
            mock.unitOfWorkMock.Object
            );

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(PermissionErrors.PermissionTypeNotFound);
    }

    [Fact]
    public async Task Handle_Should_ReturnPermissionNotFound_WhenPermissionId_NotRegistered()
    {
        // Arrange
        const int permissionTypeId = 1;
        var mock = PermissionsTests.Setup();
        
        var command = new ModifyPermissionCommand(
            It.IsAny<int>(), 
            It.IsAny<string>(), 
            It.IsAny<string>(), 
            permissionTypeId);
        
        var handler = new ModifyPermissionCommandHandler(
            mock.permissionTypeRepositoryMock.Object,
            mock.permissionRepositoryMock.Object,
            mock.unitOfWorkMock.Object
        );
        
        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(PermissionErrors.PermissionNotFound);
    }

    [Fact]
    public async Task Handle_ShouldReturnSucess_WhenValidParameters()
    {
        // Arrange
        const int permissionTypeId = 1;
        const int permissionId = 1;
        
        var mock = PermissionsTests.Setup();
        
        var command = new ModifyPermissionCommand(permissionId, It.IsAny<string>(), It.IsAny<string>(), permissionTypeId);
        var handler = new ModifyPermissionCommandHandler(
            mock.permissionTypeRepositoryMock.Object,
            mock.permissionRepositoryMock.Object,
            mock.unitOfWorkMock.Object
            );
        
        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        mock.permissionTypeRepositoryMock
            .Verify(pt => pt.GetByIdAsync(permissionTypeId, default), Times.Once);
        mock.permissionRepositoryMock
            .Verify(p => p.GetByIdAsync(It.IsAny<int>(), default), Times.Once);
        mock.unitOfWorkMock.Verify(uof => uof.SaveChangesAsync(default), Times.Once);
        
    }
}