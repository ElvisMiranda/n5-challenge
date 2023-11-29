using Microsoft.EntityFrameworkCore;

using N5.Challenge.Domain.Abstractions;

namespace N5.Challenge.Infrastructure.Repositories
{
    internal abstract class Repository<TEntity>(ApplicationDbContext dbContext)
        where TEntity : Entity
    {
        protected ApplicationDbContext DbContext { get; } = dbContext;

        public void Add(TEntity entity) => DbContext.Add(entity);

        public void Update(TEntity entity) => DbContext.Set<TEntity>().Attach(entity).State = EntityState.Modified;

        public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
                => await DbContext
                    .Set<TEntity>()
                    .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }
}
