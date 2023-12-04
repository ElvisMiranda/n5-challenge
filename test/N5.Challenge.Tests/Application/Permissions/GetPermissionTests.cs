using FluentAssertions;
using Moq;

using N5.Challenge.Application.Permissions.GetPermission;
using N5.Challenge.Application.Permissions.GetPermissions;
using N5.Challenge.Domain.Permissions;

namespace N5.Challenge.Tests.Application.Permissions
{
    public partial class PermissionTests
    {
        [Fact]
        public async Task Handle_ShouldReturn_Permission()
        {
            // Arrange
            var mock = PermissionsTests.Setup();

            var permissionId = 1;
            var query = new GetPermissionQuery(permissionId);
            var handler = new GetPermissionQueryHandler(mock.permissionRepositoryMock.Object);

            // Assert
            var result = await handler.Handle(query, default);

            // Act 
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_ShouldReturn_PermissionNotFound()
        {
            // Arrange
            var mock = PermissionsTests.Setup();

            var permissionId = 2;
            var query = new GetPermissionQuery(permissionId);
            var handler = new GetPermissionQueryHandler(mock.permissionRepositoryMock.Object);

            // Assert
            var result = await handler.Handle(query, default);

            // Act 
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(PermissionErrors.PermissionNotFound);
        }
    }
}
