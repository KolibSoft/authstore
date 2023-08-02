using KolibSoft.AuthStore.Core.Models;
using KolibSoft.Catalogue.Core;
using Microsoft.EntityFrameworkCore;

namespace KolibSoft.AuthStore.Core;

public class PermissionDatabaseCatalogue : DatabaseCatalogue<PermissionModel, CatalogueFilters>
{

    public DbSet<CredentialPermissionModel> CredentialPermissions { get; }

    protected override bool ValidateInsert(PermissionModel item)
    {
        var valid = true;
        if (DbSet.Any(x => x.Code == item.Code))
        {
            Errors?.Add(AuthStoreStatics.RepeatedCode);
            valid = false;
        }
        return valid;
    }

    protected override bool ValidateUpdate(PermissionModel item)
    {
        var valid = true;
        if (DbSet.Any(x => x.Code == item.Code && x.Id != item.Id))
        {
            Errors?.Add(AuthStoreStatics.RepeatedCode);
            valid = false;
        }
        return valid;
    }

    protected override bool ValidateDelete(PermissionModel item)
    {
        var valid = true;
        if (CredentialPermissions.Any(x => x.PermissionId == item.Id))
        {
            Errors?.Add(AuthStoreStatics.UsedItem);
            valid = false;
        }
        return valid;
    }

    public PermissionDatabaseCatalogue(DbContext dbContext) : base(dbContext)
    {
        CredentialPermissions = dbContext.Set<CredentialPermissionModel>();
    }

}