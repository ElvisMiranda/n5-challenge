namespace N5.Challenge.Domain.Permissions
{
    public interface IPermissionRepository
    {
        Task<Permission?> GetByIdAsync(int id, CancellationToken cancellationToken);
        void Add(Permission permission);
        void Update(Permission permission);
    }
}
