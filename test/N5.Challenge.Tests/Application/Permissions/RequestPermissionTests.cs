using FluentAssertions;
using Moq;
using N5.Challenge.Application.Permissions.RequestPermission;
using N5.Challenge.Domain.Abstractions;
using N5.Challenge.Domain.Permissions;
using N5.Challenge.Domain.PermissionTypes;

namespace N5.Challenge.Tests.Application.Permissions;

public partial class PermissionTests
{
    [Fact]
    public async Task Handle_ShouldFail_WithInvalidPermissionType()
    {
        // Arrange
        var mock = PermissionsTests.Setup();
        
        mock.permissionTypeRepositoryMock
            .Setup(pt => pt.GetByIdAsync(It.IsAny<int>(), default))
            .ReturnsAsync((PermissionType)null!);

        var handler = new RequestPermissionCommandHandler(
            mock.permissionTypeRepositoryMock.Object,
            mock.permissionRepositoryMock.Object,
            mock.unitOfWorkMock.Object
            );

        var request = new RequestPermissionCommand(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<int>()
            );

        // Act
        var result = await handler.Handle(request, default);

        // Assert
        result.Error.Should().Be(PermissionErrors.PermissionTypeNotFound);
    }

    [Fact]
    public async Task Handle_ShouldSuccess_WithValidParameters()
    {
        // Arrange
        var mock = PermissionsTests.Setup();

        mock.permissionTypeRepositoryMock
            .Setup(pt => pt.GetByIdAsync(It.IsAny<int>(), default))
            .ReturnsAsync(new PermissionType(It.IsAny<int>(), It.IsAny<string>()));
        
        var command = new RequestPermissionCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>());
        
        var handler = new RequestPermissionCommandHandler(
            mock.permissionTypeRepositoryMock.Object, 
            mock.permissionRepositoryMock.Object,
            mock.unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        mock.permissionTypeRepositoryMock.Verify(pt => pt.GetByIdAsync(It.IsAny<int>(), default), Times.Once);
        mock.permissionRepositoryMock.Verify(x => x.Add(It.IsAny<Permission>()), Times.Once);
        mock.unitOfWorkMock.Verify(uof => uof.SaveChangesAsync(default), Times.Once);
        
        result.IsSuccess.Should().BeTrue();
    }

    
}