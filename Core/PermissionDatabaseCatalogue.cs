using KolibSoft.AuthStore.Core.Models;
using KolibSoft.Catalogue.Core;
using Microsoft.EntityFrameworkCore;

namespace KolibSoft.AuthStore.Core;

public class PermissionDatabaseCatalogue : DatabaseCatalogue<PermissionModel, CatalogueFilters>
{

    public DbSet<CredentialPermissionModel> CredentialPermissions { get; }

    protected override bool ValidateInsert(PermissionModel item)
    {
        if (DbSet.Any(x => x.Code == item.Code))
        {
            Errors?.Add(AuthStoreStatics.RepeatedCode);
            return false;
        }
        return true;
    }

    protected override bool ValidateUpdate(PermissionModel item)
    {
        if (DbSet.Any(x => x.Code == item.Code && x.Id != item.Id))
        {
            Errors?.Add(AuthStoreStatics.RepeatedCode);
            return false;
        }
        return true;
    }

    protected override bool ValidateDelete(PermissionModel item)
    {
        if (CredentialPermissions.Any(x => x.PermissionId == item.Id))
        {
            Errors?.Add(AuthStoreStatics.UsedItem);
            return false;
        }
        return true;
    }

    public PermissionDatabaseCatalogue(DbContext dbContext) : base(dbContext)
    {
        CredentialPermissions = dbContext.Set<CredentialPermissionModel>();
    }

}