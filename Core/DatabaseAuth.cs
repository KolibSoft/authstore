using KolibSoft.AuthStore.Core.Abstractions;
using KolibSoft.AuthStore.Core.Models;
using KolibSoft.Catalogue.Core;
using Microsoft.EntityFrameworkCore;

namespace KolibSoft.AuthStore.Core;

public class DatabaseAuth : IAuthConnector
{

    public DbContext DbContext { get; }
    public DbSet<CredentialModel> Credentials { get; }
    public DbSet<PermissionModel> Permissions { get; }
    public DbSet<CredentialPermissionModel> CredentialPermissions { get; }
    public ICollection<string> Errors { get; } = new List<string>();

    protected virtual AuthModel Login(CredentialModel credential, string? refreshToken)
    {
        var permissionIds = CredentialPermissions.Where(x => x.CredentialId == credential.Id).Select(x => x.PermissionId).ToArray();
        var permissions = Permissions.Where(x => permissionIds.Contains(x.Id));
        return new AuthModel
        {
            Credential = credential,
            Permissions = permissions
        };
    }

    public virtual Task<Result<AuthModel?>> AccessAsync(LoginModel login) => Task.Run<Result<AuthModel?>>(() =>
    {
        Errors.Clear();
        var credential = Credentials.FirstOrDefault(x => x.Identity == login.Identity && x.Active && x.Key == login.Key);
        if (credential == null)
        {
            Errors.Add(CatalogueStatics.NoItem);
            return Errors.ToArray();
        }
        var auth = Login(credential, null);
        return auth;
    });

    public virtual Task<Result<AuthModel?>> RefreshAsync(Guid id, string refreshToken) => Task.Run<Result<AuthModel?>>(() =>
    {
        Errors.Clear();
        var credential = Credentials.FirstOrDefault(x => x.Id == id && x.Active);
        if (credential == null)
        {
            Errors.Add(CatalogueStatics.NoItem);
            return Errors.ToArray();
        }
        var auth = Login(credential, refreshToken);
        return auth;
    });

    public DatabaseAuth(DbContext dbContext)
    {
        DbContext = dbContext;
        Credentials = dbContext.Set<CredentialModel>();
        Permissions = dbContext.Set<PermissionModel>();
        CredentialPermissions = dbContext.Set<CredentialPermissionModel>();
    }

}