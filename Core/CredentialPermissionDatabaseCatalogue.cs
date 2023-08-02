using KolibSoft.AuthStore.Core.Models;
using KolibSoft.Catalogue.Core;
using Microsoft.EntityFrameworkCore;

namespace KolibSoft.AuthStore.Core;

public class CredentialPermissionDatabaseCatalogue : DatabaseCatalogue<CredentialPermissionModel, CatalogueFilters>
{

    public DbSet<CredentialModel> Credentials { get; }
    public DbSet<PermissionModel> Permissions { get; }

    protected override bool ValidateInsert(CredentialPermissionModel item)
    {
        if (!Credentials.Any(x => x.Id == item.CredentialId))
        {
            Errors?.Add(AuthStoreStatics.InvalidCredential);
            return false;
        }
        if (!Permissions.Any(x => x.Id == item.PermissionId))
        {
            Errors?.Add(AuthStoreStatics.InvalidPermission);
            return false;
        }
        if (DbSet.Any(x => x.CredentialId == item.CredentialId && x.PermissionId == item.PermissionId))
        {
            Errors?.Add(CatalogueStatics.RepeatedItem);
            return false;
        }
        return true;
    }

    protected override bool ValidateUpdate(CredentialPermissionModel item)
    {
        if (!Credentials.Any(x => x.Id == item.CredentialId))
        {
            Errors?.Add(AuthStoreStatics.InvalidCredential);
            return false;
        }
        if (!Permissions.Any(x => x.Id == item.PermissionId))
        {
            Errors?.Add(AuthStoreStatics.InvalidPermission);
            return false;
        }
        if (DbSet.Any(x => x.CredentialId == item.CredentialId && x.PermissionId == item.PermissionId))
        {
            Errors?.Add(CatalogueStatics.RepeatedItem);
            return false;
        }
        return true;
    }

    public CredentialPermissionDatabaseCatalogue(DbContext dbContext) : base(dbContext)
    {
        Credentials = dbContext.Set<CredentialModel>();
        Permissions = dbContext.Set<PermissionModel>();
    }

}