using KolibSoft.AuthStore.Core.Filters;
using KolibSoft.AuthStore.Core.Models;
using KolibSoft.Catalogue.Core;
using KolibSoft.Catalogue.Core.Catalogues;
using Microsoft.EntityFrameworkCore;

namespace KolibSoft.AuthStore.Core.Catalogues;

public class CredentialPermissionDatabaseCatalogue : DatabaseCatalogue<CredentialPermissionModel, CredentialPermissionFilters>
{

    public DbSet<CredentialModel> Credentials { get; }
    public DbSet<PermissionModel> Permissions { get; }

    protected override Task<IQueryable<CredentialPermissionModel>> QueryItems(IQueryable<CredentialPermissionModel> items, CredentialPermissionFilters? filters = default) => Task.Run(() =>
    {
        if (filters?.Clean ?? true) items = items.Where(x => x.Active);
        if (filters?.CredentialId != null) items = items.Where(x => x.CredentialId == filters.CredentialId);
        if (filters?.PermissionId != null) items = items.Where(x => x.PermissionId == filters.PermissionId);
        items = items.OrderByDescending(x => x.Active);
        return items;
    });

    protected override Task<bool> ValidateInsert(CredentialPermissionModel item) => Task.Run(() =>
    {
        if (!Credentials.Any(x => x.Id == item.CredentialId))
        {
            Errors.Add(AuthStoreStatics.InvalidCredential);
            return false;
        }
        if (!Permissions.Any(x => x.Id == item.PermissionId))
        {
            Errors.Add(AuthStoreStatics.InvalidPermission);
            return false;
        }
        if (DbSet.Any(x => x.CredentialId == item.CredentialId && x.PermissionId == item.PermissionId))
        {
            Errors.Add(CatalogueStatics.RepeatedItem);
            return false;
        }
        return true;
    });

    protected override Task<bool> ValidateUpdate(CredentialPermissionModel item) => Task.Run(() =>
    {
        if (!Credentials.Any(x => x.Id == item.CredentialId))
        {
            Errors.Add(AuthStoreStatics.InvalidCredential);
            return false;
        }
        if (!Permissions.Any(x => x.Id == item.PermissionId))
        {
            Errors.Add(AuthStoreStatics.InvalidPermission);
            return false;
        }
        if (DbSet.Any(x => x.CredentialId == item.CredentialId && x.PermissionId == item.PermissionId && x.Id != item.Id))
        {
            Errors.Add(CatalogueStatics.RepeatedItem);
            return false;
        }
        return true;
    });

    public CredentialPermissionDatabaseCatalogue(DbContext dbContext) : base(dbContext)
    {
        Credentials = dbContext.Set<CredentialModel>();
        Permissions = dbContext.Set<PermissionModel>();
    }

}