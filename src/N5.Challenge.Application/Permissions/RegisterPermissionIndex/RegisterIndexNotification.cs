using MediatR;

namespace N5.Challenge.Application.Permissions.RegisterPermissionIndex;

public record RegisterIndexNotification(int PermissionId) : INotification;