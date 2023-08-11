using KolibSoft.AuthStore.Core.Filters;
using KolibSoft.AuthStore.Core.Models;
using KolibSoft.Catalogue.Core;
using KolibSoft.Catalogue.Core.Catalogues;
using Microsoft.EntityFrameworkCore;

namespace KolibSoft.AuthStore.Core.Catalogues;

public class PermissionDatabaseCatalogue : DatabaseCatalogue<PermissionModel, PermissionFilters>
{

    public DbSet<CredentialPermissionModel> CredentialPermissions { get; }

    protected override Task<IQueryable<PermissionModel>> QueryItems(IQueryable<PermissionModel> items, PermissionFilters? filters = default) => Task.Run(() =>
    {
        if (filters?.Clean ?? true) items = items.Where(x => x.Active);
        if (filters?.Hint != null) items = items.Where(x => EF.Functions.Like(x.Code, $"%{filters.Hint}%"));
        items = items.OrderBy(x => x.Code).OrderByDescending(x => x.Active);
        return items;
    });

    protected override Task<bool> ValidateInsert(PermissionModel item) => Task.Run(() =>
    {
        if (DbSet.Any(x => x.Code == item.Code))
        {
            Errors?.Add(AuthStoreStatics.RepeatedCode);
            return false;
        }
        return true;
    });

    protected override Task<bool> ValidateUpdate(PermissionModel item) => Task.Run(() =>
    {
        if (DbSet.Any(x => x.Code == item.Code && x.Id != item.Id))
        {
            Errors?.Add(AuthStoreStatics.RepeatedCode);
            return false;
        }
        return true;
    });

    protected override Task<bool> ValidateDelete(PermissionModel item) => Task.Run(() =>
    {
        if (CredentialPermissions.Any(x => x.PermissionId == item.Id))
        {
            Errors?.Add(CatalogueStatics.UsedItem);
            return false;
        }
        return true;
    });

    public PermissionDatabaseCatalogue(DbContext dbContext) : base(dbContext)
    {
        CredentialPermissions = dbContext.Set<CredentialPermissionModel>();
    }

}