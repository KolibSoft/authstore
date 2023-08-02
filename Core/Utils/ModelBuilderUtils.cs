using KolibSoft.AuthStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace KolibSoft.AuthStore.Core.Utils;

public static class ModelBuilderUtils
{

    public static void BuildAuthStore(this ModelBuilder builder)
    {
        builder.Entity<CredentialModel>(config =>
        {
            config.ToTable("credential");
            config.Property(x => x.Id).HasDefaultValueSql("UUID()");
            config.Property(x => x.Identity).HasMaxLength(32);
            config.Property(x => x.Key).HasMaxLength(64).IsFixedLength();
            config.Property(x => x.Active).HasDefaultValue(true);
            config.Property(x => x.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP()");
            config.HasKey(x => x.Id);
            config.HasIndex(x => x.Identity).IsUnique();
        });
        builder.Entity<PermissionModel>(config =>
        {
            config.ToTable("permission");
            config.Property(x => x.Id).HasDefaultValueSql("UUID()");
            config.Property(x => x.Code).HasMaxLength(32);
            config.Property(x => x.Active).HasDefaultValue(true);
            config.Property(x => x.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP()");
            config.HasKey(x => x.Id);
            config.HasIndex(x => x.Code).IsUnique();
        });
        builder.Entity<CredentialPermissionModel>(config =>
        {
            config.ToTable("credential_permission");
            config.Property(x => x.Id).HasDefaultValueSql("UUID()");
            config.Property(x => x.CredentialId);
            config.Property(x => x.PermissionId);
            config.Property(x => x.Active).HasDefaultValue(true);
            config.Property(x => x.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP()");
            config.HasKey(x => x.Id);
            config.HasOne<CredentialModel>().WithMany().HasForeignKey(x => x.CredentialId);
            config.HasOne<PermissionModel>().WithMany().HasForeignKey(x => x.PermissionId);
        });
    }

}