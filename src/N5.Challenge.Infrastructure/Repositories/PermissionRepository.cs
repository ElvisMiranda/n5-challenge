using N5.Challenge.Domain.Permissions;

namespace N5.Challenge.Infrastructure.Repositories;

internal sealed class PermissionRepository(ApplicationDbContext dbContext) 
    : Repository<Permission>(dbContext), IPermissionRepository
{
}