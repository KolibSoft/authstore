using KolibSoft.AuthStore.Core.Models;
using KolibSoft.AuthStore.Core.Utils;
using KolibSoft.Catalogue.Core;
using Microsoft.EntityFrameworkCore;

namespace KolibSoft.AuthStore.Core;

public class CredentialDatabaseCatalogue : DatabaseCatalogue<CredentialModel, CredentialFilters>
{

    public DbSet<CredentialPermissionModel> CredentialPermissions { get; }

    public bool IsPublic { get; set; } = true;

    protected override Task<IQueryable<CredentialModel>> QueryItems(IQueryable<CredentialModel> items, CredentialFilters? filters = default) => Task.Run<IQueryable<CredentialModel>>(() =>
    {
        if (filters?.Clean ?? true) items = items.Where(x => x.Active);
        if (filters?.Hint != null) items = items.Where(x => EF.Functions.Like(x.Identity, $"%{filters.Hint}%"));
        items = items.OrderBy(x => x.Identity).OrderByDescending(x => x.Active);
        return items;
    });

    protected override Task<bool> ValidateInsert(CredentialModel item) => Task.Run(() =>
    {
        if (DbSet.Any(x => x.Identity == item.Identity))
        {
            Errors.Add(AuthStoreStatics.RepeatedIdentity);
            return false;
        }
        return true;
    });

    protected override Task<bool> ValidateUpdate(CredentialModel item) => Task.Run(() =>
    {
        {
            if (DbSet.Any(x => x.Identity == item.Identity && x.Id != item.Id))
            {
                Errors.Add(AuthStoreStatics.RepeatedIdentity);
                return false;
            }
            return true;
        }
    });

    protected override Task<bool> ValidateDelete(CredentialModel item) => Task.Run(() =>
    {
        if (CredentialPermissions.Any(x => x.CredentialId == item.Id))
        {
            Errors.Add(CatalogueStatics.UsedItem);
            return false;
        }
        return true;
    });

    public CredentialDatabaseCatalogue(DbContext dbContext) : base(dbContext)
    {
        CredentialPermissions = dbContext.Set<CredentialPermissionModel>();
    }

}