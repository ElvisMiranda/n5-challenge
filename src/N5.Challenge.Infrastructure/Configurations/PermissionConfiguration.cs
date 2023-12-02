using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using N5.Challenge.Domain.Permissions;
using N5.Challenge.Domain.PermissionTypes;

namespace N5.Challenge.Infrastructure.Configurations;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");

        builder.HasKey(permission => permission.Id);

        builder.Property(permission => permission.Id)
            .HasComment("Unique ID");

        builder.Property(permission => permission.PermissionTypeId)
            .HasColumnName("PermissionType");

        builder.Property(permission => permission.EmployeeForename)
            .IsRequired()
            .HasMaxLength(200)
            .HasComment("Employee Forename");

        builder.Property(permission => permission.EmployeeSurname)
            .IsRequired()
            .HasMaxLength(200)
            .HasComment("Employee Surname");

        builder.Property(permission => permission.PermissionTypeId)
            .HasComment("Permission Type");

        builder.Property(permission => permission.PermissionDate)
            .IsRequired()
            .HasComment("Permission granted on Date");

        builder.HasOne<PermissionType>()
            .WithMany()
            .HasForeignKey(permission => permission.PermissionTypeId);
    }
}