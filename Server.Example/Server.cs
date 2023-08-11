using KolibSoft.AuthStore.Core;
using KolibSoft.AuthStore.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KolibSoft.AuthStore.Core.Utils;
using KolibSoft.AuthStore.Core.Catalogues;
using Microsoft.AspNetCore.Cors;

namespace KolibSoft.AuthStore.Server.Example;

[Route("auth")]
[EnableCors("Allow-All")]
public class TestAuthController : AuthController
{
    public TestAuthController(AuthStoreContext context, TokenGenerator generator) : base(new DatabaseAuth(context, generator)) { }
}

[Route("credential")]
[EnableCors("Allow-All")]
public class TestCredentialController : CredentialController
{
    public TestCredentialController(AuthStoreContext context) : base(new CredentialDatabaseCatalogue(context)) { }
}

[Route("permission")]
[EnableCors("Allow-All")]
public class TestPermissionController : PermissionController
{
    public TestPermissionController(AuthStoreContext context) : base(new PermissionDatabaseCatalogue(context)) { }
}

[Route("credential-permission")]
[Route("permission-credential")]
[EnableCors("Allow-All")]
public class TestCredentialPermissionController : CredentialPermissionController
{
    public TestCredentialPermissionController(AuthStoreContext context) : base(new CredentialPermissionDatabaseCatalogue(context)) { }
}

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