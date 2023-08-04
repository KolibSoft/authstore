using KolibSoft.AuthStore.Core.Models;
using KolibSoft.AuthStore.Core.Utils;
using KolibSoft.Catalogue.Core;
using Microsoft.EntityFrameworkCore;

namespace KolibSoft.AuthStore.Core;

public class CredentialDatabaseCatalogue : DatabaseCatalogue<CredentialModel, CredentialFilters>
{

    public DbSet<CredentialPermissionModel> CredentialPermissions { get; }

    public bool IsPublic { get; set; } = true;

    protected override IQueryable<CredentialModel> QueryItems(IQueryable<CredentialModel> items, CredentialFilters? filters = default)
    {
        if (filters?.Clean ?? true) items = items.Where(x => x.Active);
        if (filters?.Hint != null) items = items.Where(x => EF.Functions.Like(x.Identity, $"%{filters.Hint}%"));
        items = items.OrderBy(x => x.Identity).OrderByDescending(x => x.Active);
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
        if (IsPublic && result.Data != null)
        {
            result = new Page<CredentialModel>
            {
                Items = result.Data.Items.Select(x => x.ToPublic()).ToArray(),
                PageIndex = result.Data.PageIndex,
                PageCount = result.Data.PageCount
            };
        }
        return result;
    }

    public override async Task<Result<CredentialModel?>> GetAsync(Guid id)
    {
        var result = await base.GetAsync(id);
        if (IsPublic && result.Data != null)
        {
            result = result.Data.ToPublic();
        }
        return result;
    }

    public override Task<Result<Guid?>> InsertAsync(CredentialModel item)
    {
        if (IsPublic) item.Key = item.Key.GetHashString();
        else item.Key = item.Key.PadRight(64);
        return base.InsertAsync(item);
    }

    public override Task<Result<bool?>> UpdateAsync(Guid id, CredentialModel item)
    {
        if (IsPublic) item.Key = item.Key.GetHashString();
        else item.Key = item.Key.PadRight(64);
        return base.UpdateAsync(id, item);
    }

    public CredentialDatabaseCatalogue(DbContext dbContext) : base(dbContext)
    {
        CredentialPermissions = dbContext.Set<CredentialPermissionModel>();
    }

}