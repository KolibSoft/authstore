using KolibSoft.AuthStore.Core;
using KolibSoft.AuthStore.Core.Models;
using KolibSoft.Catalogue.Core;
using KolibSoft.Catalogue.Core.Abstractions;
using KolibSoft.Catalogue.Server;
using Microsoft.AspNetCore.Authorization;

namespace KolibSoft.AuthStore.Server;

public class CredentialPermissionController : CatalogueController<CredentialPermissionModel, CatalogueFilters>
{

    [Authorize(AuthStoreStatics.CredentialPermissionReader)]
    public override Task<Result<Page<CredentialPermissionModel>?>> PageAsync(CatalogueFilters? filters = null)
    {
        return base.PageAsync(filters);
    }

    [Authorize(AuthStoreStatics.CredentialPermissionReader)]
    public override Task<Result<CredentialPermissionModel?>> GetAsync(Guid id)
    {
        return base.GetAsync(id);
    }

    [Authorize(AuthStoreStatics.CredentialPermissionManager)]
    public override Task<Result<Guid?>> InsertAsync(CredentialPermissionModel item)
    {
        return base.InsertAsync(item);
    }

    [Authorize(AuthStoreStatics.CredentialPermissionManager)]
    public override Task<Result<bool?>> UpdateAsync(Guid id, CredentialPermissionModel item)
    {
        return base.UpdateAsync(id, item);
    }

    [Authorize(AuthStoreStatics.CredentialPermissionManager)]
    public override Task<Result<bool?>> DeleteAsync(Guid id)
    {
        return base.DeleteAsync(id);
    }

    public CredentialPermissionController(ICatalogueConnector<CredentialPermissionModel, CatalogueFilters> catalogueConnector) : base(catalogueConnector) { }

}