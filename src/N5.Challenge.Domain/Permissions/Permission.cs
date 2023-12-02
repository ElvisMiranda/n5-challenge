using N5.Challenge.Domain.Abstractions;

namespace N5.Challenge.Domain.Permissions
{
    public sealed class Permission : Entity
    {
        private Permission(int id,
            string employeeForename,
            string employeeSurname,
            int permissionTypeId,
            DateOnly permissionDate) : base(id)
        {
            EmployeeForename = employeeForename;
            EmployeeSurname = employeeSurname;
            PermissionTypeId = permissionTypeId;
            PermissionDate = permissionDate;
        }

        public string EmployeeForename { get; }
        public string EmployeeSurname { get; }
        public int PermissionTypeId { get; }
        public DateOnly PermissionDate { get; }
        
        public static Permission Create(string forename, string surname, int permissionType)
        {
            var permissionDate = DateOnly.FromDateTime(DateTime.UtcNow);
            
            return new Permission(
                default,
                forename,
                surname,
                permissionType,
                permissionDate);
        }
    }
}
