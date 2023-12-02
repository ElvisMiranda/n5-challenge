using N5.Challenge.Domain.PermissionTypes;

namespace N5.Challenge.Infrastructure.Repositories;

internal sealed class PermissionTypeRepository(ApplicationDbContext dbContext)
: Repository<PermissionType>(dbContext), IPermissionTypeRepository
{
    
}