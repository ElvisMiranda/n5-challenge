using Elastic.Clients.Elasticsearch;
using MediatR;
using Microsoft.Extensions.Logging;
using N5.Challenge.Domain.Permissions;

namespace N5.Challenge.Application.Permissions.RegisterPermissionIndex;

public class RegisterPermissionIndexEventHandler : INotificationHandler<RegisterIndexNotification>
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly ILogger<RegisterPermissionIndexEventHandler> _logger;
    private readonly ElasticsearchClient _elasticsearchClient;

    public RegisterPermissionIndexEventHandler(
        IPermissionRepository permissionRepository, 
        ILogger<RegisterPermissionIndexEventHandler> logger, 
        ElasticsearchClient elasticsearchClient)
    {
        _permissionRepository = permissionRepository;
        _logger = logger;
        _elasticsearchClient = elasticsearchClient;
    }

    public async Task Handle(RegisterIndexNotification notification, CancellationToken cancellationToken)
    {
        var permission = await _permissionRepository.GetByIdAsync(notification.PermissionId, cancellationToken);
        if (permission is null)
        {
            _logger.LogError($"Permission with Id: {notification.PermissionId} was not found.");
            return;
        }

        var response = await _elasticsearchClient.IndexAsync(permission, "permissions", cancellationToken);

        if (!response.IsValidResponse)
        {
            _logger.LogError($"Error trying to create an ES index {response.DebugInformation}");
            return;
        }
        
        _logger.LogInformation("Index created successfully");
    }
}