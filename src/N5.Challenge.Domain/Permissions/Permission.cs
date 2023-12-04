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

        public string EmployeeForename { get; private set; }
        public string EmployeeSurname { get; private set; }
        public int PermissionTypeId { get; private set; }
        public DateOnly PermissionDate { get; private set; }
        
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

        public void Update(string forename, string surname, int permissionType)
        {
            EmployeeForename = forename;
            EmployeeSurname = surname;
            PermissionTypeId = permissionType;

            PermissionDate = DateOnly.FromDateTime(DateTime.UtcNow);
        }
    }
}
