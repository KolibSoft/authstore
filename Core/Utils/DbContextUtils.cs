using KolibSoft.AuthStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace KolibSoft.AuthStore.Core.Utils;

public static class DbContextUtils
{

    public static void CreateAuthStore(this DbContext dbContext)
    {
        var credentials = dbContext.Set<CredentialModel>();
        var permissions = dbContext.Set<PermissionModel>();
        var credentialPermissions = dbContext.Set<CredentialPermissionModel>();
        var rootCredential = new CredentialModel
        {
            Id = Guid.NewGuid(),
            Identity = "ROOT",
            Key = "ROOT".GetHashString(),
            Active = true,
            UpdatedAt = DateTime.UtcNow
        };
        credentials.Add(rootCredential);
        var authStorePermissions = new PermissionModel[] { };
        permissions.AddRange(authStorePermissions);
        var rootPermissions = authStorePermissions.Select(x => new CredentialPermissionModel
        {
            Id = Guid.NewGuid(),
            CredentialId = rootCredential.Id,
            PermissionId = x.Id,
            Active = true,
            UpdatedAt = DateTime.UtcNow
        }).ToArray();
        credentialPermissions.AddRange(rootPermissions);
        dbContext.SaveChanges();
    }

}