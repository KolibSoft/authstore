using KolibSoft.AuthStore.Core.Models;
using KolibSoft.Catalogue.Core;
using Microsoft.EntityFrameworkCore;

namespace KolibSoft.AuthStore.Core;

public class CredentialDatabaseCatalogue : DatabaseCatalogue<CredentialModel, CatalogueFilters>
{

    public DbSet<CredentialPermissionModel> CredentialPermissions { get; }

    protected override bool ValidateInsert(CredentialModel item)
    {
        if (DbSet.Any(x => x.Identity == item.Identity))
        {
            Errors.Add(AuthStoreStatics.RepeatedIdentity);
            return false;
        }
        return true;
    }

    protected override bool ValidateUpdate(CredentialModel item)
    {
        if (DbSet.Any(x => x.Identity == item.Identity && x.Id != item.Id))
        {
            Errors.Add(AuthStoreStatics.RepeatedIdentity);
            return false;
        }
        return true;
    }

    protected override bool ValidateDelete(CredentialModel item)
    {
        if (CredentialPermissions.Any(x => x.CredentialId == item.Id))
        {
            Errors.Add(AuthStoreStatics.UsedItem);
            return false;
        }
        return true;
    }

    public override async Task<Result<Page<CredentialModel>?>> PageAsync(CatalogueFilters? filters = null)
    {
        var result = await base.PageAsync(filters);
        if (result.Data != null)
        {
            result = new Page<CredentialModel>
            {
                Items = result.Data.Items.Select(x => x.ToPublic()),
                PageIndex = result.Data.PageIndex,
                PageCount = result.Data.PageCount
            };
        }
        return result;
    }

    public override async Task<Result<CredentialModel?>> GetAsync(Guid id)
    {
        var result = await base.GetAsync(id);
        if (result.Data != null)
        {
            result = result.Data.ToPublic();
        }
        return result;
    }

    public CredentialDatabaseCatalogue(DbContext dbContext) : base(dbContext)
    {
        CredentialPermissions = dbContext.Set<CredentialPermissionModel>();
    }

}