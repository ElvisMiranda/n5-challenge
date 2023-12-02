namespace N5.Challenge.Domain.Permissions
{
    public interface IPermissionRepository
    {
        IQueryable<Permission> All();
        void Add(Permission permission);
        void Update(Permission permission);
        Task<Permission?> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}
