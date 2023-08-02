using KolibSoft.AuthStore.Core.Models;
using KolibSoft.AuthStore.Core.Utils;
using Microsoft.EntityFrameworkCore;

namespace KolibSoft.AuthStore.Core;

public class AuthStoreContext : DbContext
{

    public DbSet<CredentialModel> Credentials { get; init; } = null!;
    public DbSet<PermissionModel> Permissions { get; init; } = null!;
    public DbSet<CredentialPermissionModel> CredentialPermissions { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.BuildAuthStore();

    public AuthStoreContext() : base()
    {
        if (Database.EnsureCreated()) this.CreateAuthStore();
    }

    public AuthStoreContext(DbContextOptions<AuthStoreContext> options) : base(options)
    {
        if (Database.EnsureCreated()) this.CreateAuthStore();
    }

}