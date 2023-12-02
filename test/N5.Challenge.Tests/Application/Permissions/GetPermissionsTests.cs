using FluentAssertions;
using Moq;
using N5.Challenge.Application.Permissions.GetPermissions;
using N5.Challenge.Domain.Permissions;

namespace N5.Challenge.Tests.Application.Permissions;

public partial class PermissionTests
{
    [Fact]
    public async Task Handle_ShouldReturn_Permissions()
    {
        // Arrange
        const int expectedCount = 3;
        var permissionRepository = new Mock<IPermissionRepository>();
        permissionRepository
            .Setup(p => p.All())
            .Returns(Permissions());
        
        var query = new GetPermissionsQuery();
        var handler = new GetPermissionQueryHandler(permissionRepository.Object);

        // Assert
        var result = await handler.Handle(query, default);

        // Act 
        result.IsSuccess.Should().BeTrue();
        result.Value.Count.Should().Be(expectedCount);
    }

    private static IQueryable<Permission> Permissions()
        => new List<Permission>
        {
            Permission.Create("forename1", "surname1", 1),
            Permission.Create("forename2", "surname2", 2),
            Permission.Create("forename3", "surname3", 1)
        }.AsQueryable();
}