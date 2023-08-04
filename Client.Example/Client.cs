using Microsoft.EntityFrameworkCore;
using KolibSoft.Catalogue.Core.Abstractions;
using KolibSoft.Catalogue.Core;
using KolibSoft.AuthStore.Core.Abstractions;
using KolibSoft.AuthStore.Core;
using KolibSoft.AuthStore.Core.Models;
using KolibSoft.AuthStore.Core.Utils;

namespace KolibSoft.AuthStore.Client.Example;

public class AuthStoreClient
{

    public HttpClient HttpClient { get; } = new HttpClient();
    public string Uri { get; }
    public DbContext DbContext { get; }
    public AuthStoreChanges Changes { get; }

    public IAuthConnector Auth { get; }
    public ICatalogueConnector<CredentialModel, CredentialFilters> Credentials { get; }
    public ICatalogueConnector<PermissionModel, PermissionFilters> Permissions { get; }
    public ICatalogueConnector<CredentialPermissionModel, CredentialPermissionFilters> CredentialPermissions { get; }

    public AuthStoreClient(string uri, DbContext dbContext, AuthStoreChanges changes)
    {
        Uri = uri;
        DbContext = new AuthStoreContext();
        Auth = new AuthService(HttpClient, $"{uri}/auth");
        Changes = changes;
        Credentials = new ServiceCatalogue<CredentialModel, CredentialFilters>(
            new CredentialDatabaseCatalogue(dbContext),
            new CredentialService(HttpClient, $"{Uri}/credential"),
            changes.Credentials
        );
        Permissions = new ServiceCatalogue<PermissionModel, PermissionFilters>(
            new PermissionDatabaseCatalogue(dbContext),
            new PermissionService(HttpClient, $"{Uri}/permission"),
            changes.Permissions
        );
        CredentialPermissions = new ServiceCatalogue<CredentialPermissionModel, CredentialPermissionFilters>(
            new CredentialPermissionDatabaseCatalogue(dbContext),
            new CredentialPermissionService(HttpClient, $"{Uri}/credential-permission"),
            changes.CredentialPermissions
        );
    }

}

public class AuthStoreChanges
{
    public Dictionary<Guid, string[]?> Credentials { get; init; } = new();
    public Dictionary<Guid, string[]?> Permissions { get; init; } = new();
    public Dictionary<Guid, string[]?> CredentialPermissions { get; init; } = new();
}

public class AuthStoreContext : DbContext
{

    public DbSet<CredentialModel> Credentials { get; init; } = null!;
    public DbSet<PermissionModel> Permissions { get; init; } = null!;
    public DbSet<CredentialPermissionModel> CredentialPermissions { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.BuildAuthStore();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=database.db");
    }

    public AuthStoreContext() : base()
    {
        if (Database.EnsureCreated()) this.CreateAuthStore();
    }

    public AuthStoreContext(DbContextOptions<AuthStoreContext> options) : base(options)
    {
        if (Database.EnsureCreated()) this.CreateAuthStore();
    }

}