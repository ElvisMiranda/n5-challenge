using N5.Challenge.Domain.Abstractions;
using N5.Challenge.Domain.PermissionTypes;

namespace N5.Challenge.Domain.Permissions
{
    public sealed class Permission(
        int id,
        string employeeForename,
        string employeeSurname,
        int permissionTypeId,
        PermissionType permissionType,
        DateOnly permissionDate) : Entity(id)
    {
        public string EmployeeForename { get; } = employeeForename;
        public string EmployeeSurname { get; } = employeeSurname;
        public int PermissionTypeId { get; } = permissionTypeId;
        public DateOnly PermissionDate { get; } = permissionDate;
        public PermissionType PermissionType { get; } = permissionType;
    }
}
