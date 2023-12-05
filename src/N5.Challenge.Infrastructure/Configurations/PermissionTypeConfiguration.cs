using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N5.Challenge.Domain.PermissionTypes;

namespace N5.Challenge.Infrastructure.Configurations;

internal sealed class PermissionTypeConfiguration : IEntityTypeConfiguration<PermissionType>
{
    public void Configure(EntityTypeBuilder<PermissionType> builder)
    {
        builder.ToTable("PermissionTypes");

        builder.HasKey(permissionType => permissionType.Id);

        builder.Property(permissionType => permissionType.Id)
            .HasComment("Unique ID");

        builder.Property(permissionType => permissionType.Description)
            .HasComment("Permission Description")
            .IsRequired();

        builder.HasData(
            new PermissionType(1, "Admin"),
            new PermissionType(2, "Manager")
        );
    }
}