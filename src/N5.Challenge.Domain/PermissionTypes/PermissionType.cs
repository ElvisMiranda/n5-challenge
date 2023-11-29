using N5.Challenge.Domain.Abstractions;

namespace N5.Challenge.Domain.PermissionTypes
{
    public abstract class PermissionType(int id, string description) : Entity(id)
    {
        public string Description { get; private set; } = description;
    }
}
