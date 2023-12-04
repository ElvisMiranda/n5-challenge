namespace N5.Challenge.Application.Permissions.Shared;

public sealed class PermissionResponse
{
    public int Id { get; init; }
    public string EmployeeForename { get; init; }
    public string EmployeeSurname { get; init; }
    public int PermissionType { get; init; }
    public DateOnly PermissionDate { get; init; }
}