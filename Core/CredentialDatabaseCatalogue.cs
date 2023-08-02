using KolibSoft.AuthStore.Core.Models;
using KolibSoft.Catalogue.Core;
using Microsoft.EntityFrameworkCore;

namespace KolibSoft.AuthStore.Core;

public class CredentialDatabaseCatalogue : DatabaseCatalogue<CredentialModel, CredentialFilters>
{

    public DbSet<CredentialPermissionModel> CredentialPermissions { get; }

    protected override IQueryable<CredentialModel> QueryItems(IQueryable<CredentialModel> items, CredentialFilters filters)
    {
        if (filters.Clean ?? true) items = items.Where(x => x.Active);
        if (filters.PermissionId != null)
        {
            var credentialIds = CredentialPermissions.Where(x => x.PermissionId == filters.PermissionId).Select(x => x.CredentialId).ToArray();
            items = items.Where(x => credentialIds.Contains(x.Id));
        }
        if (filters.Hint != null) items = items.Where(x => EF.Functions.Like(x.Identity, $"%{filters.Hint}%"));
        return items;
    }

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

    public override async Task<Result<Page<CredentialModel>?>> PageAsync(CredentialFilters? filters = null)
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