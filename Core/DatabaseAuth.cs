using System.Security.Claims;
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
    public ITokenGenerator TokenGenerator { get; }
    public ICollection<string> Errors { get; } = new List<string>();

    public virtual bool Available => DbContext.Database.CanConnect();

    public TimeSpan AccessTime { get; set; } = TimeSpan.FromMinutes(20);
    public TimeSpan RefreshTime { get; set; } = TimeSpan.FromDays(1);

    protected virtual AuthModel Login(CredentialModel credential, string? refreshToken)
    {
        var permissionIds = CredentialPermissions.Where(x => x.CredentialId == credential.Id).Select(x => x.PermissionId).ToArray();
        var permissions = Permissions.Where(x => permissionIds.Contains(x.Id)).ToArray();

        var accessClaims = new List<Claim>
        {
            new Claim(AuthStoreStatics.Id, credential.Id.ToString()),
            new Claim(AuthStoreStatics.Access, AuthStoreStatics.Permitted),
            new Claim(AuthStoreStatics.Refresh, AuthStoreStatics.Denied)
        };
        accessClaims.AddRange(permissions.Select(x => new Claim(AuthStoreStatics.Permissions, x.Code)));
        var accessToken = TokenGenerator.Generate(accessClaims, AccessTime);

        if (refreshToken == null)
        {
            var refreshClaims = new List<Claim>
            {
                new Claim(AuthStoreStatics.Id, credential.Id.ToString()),
                new Claim(AuthStoreStatics.Access, AuthStoreStatics.Denied),
                new Claim(AuthStoreStatics.Refresh, AuthStoreStatics.Permitted)
            };
            refreshToken = TokenGenerator.Generate(refreshClaims, RefreshTime);
        }

        return new AuthModel
        {
            Credential = credential.ToPublic(),
            Permissions = permissions,
            AccessToken = accessToken,
            RefreshToken = refreshToken
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

    public DatabaseAuth(DbContext dbContext, ITokenGenerator tokenGenerator)
    {
        DbContext = dbContext;
        Credentials = dbContext.Set<CredentialModel>();
        Permissions = dbContext.Set<PermissionModel>();
        CredentialPermissions = dbContext.Set<CredentialPermissionModel>();
        TokenGenerator = tokenGenerator;
    }

}