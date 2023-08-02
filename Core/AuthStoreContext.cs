using KolibSoft.AuthStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace KolibSoft.AuthStore.Core;

public class AuthStoreContext : DbContext
{

    public DbSet<CredentialModel> Credentials { get; init; } = null!;
    public DbSet<PermissionModel> Permissions { get; init; } = null!;
    public DbSet<CredentialPermissionModel> CredentialPermissions { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CredentialModel>(config =>
        {
            config.ToTable("credential");
            config.Property(x => x.Id).HasDefaultValueSql("UUID()");
            config.Property(x => x.Identity).HasMaxLength(32);
            config.Property(x => x.Key).HasMaxLength(64).IsFixedLength();
            config.Property(x => x.Active).HasDefaultValue(true);
            config.Property(x => x.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP()");
            config.HasKey(x => x.Id);
        });
        modelBuilder.Entity<PermissionModel>(config =>
        {
            config.ToTable("permission");
            config.Property(x => x.Id).HasDefaultValueSql("UUID()");
            config.Property(x => x.Code).HasMaxLength(32);
            config.Property(x => x.Active).HasDefaultValue(true);
            config.Property(x => x.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP()");
            config.HasKey(x => x.Id);
        });
        modelBuilder.Entity<CredentialPermissionModel>(config =>
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

    protected virtual void OnCreate()
    {

    }

    public AuthStoreContext() : base()
    {
        if (Database.EnsureCreated()) OnCreate();
    }

    public AuthStoreContext(DbContextOptions<AuthStoreContext> options) : base(options)
    {
        if (Database.EnsureCreated()) OnCreate();
    }

}