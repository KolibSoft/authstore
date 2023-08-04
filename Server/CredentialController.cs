using KolibSoft.AuthStore.Core;
using KolibSoft.AuthStore.Core.Models;
using KolibSoft.AuthStore.Core.Utils;
using KolibSoft.Catalogue.Core;
using KolibSoft.Catalogue.Core.Abstractions;
using KolibSoft.Catalogue.Server;
using Microsoft.AspNetCore.Authorization;

namespace KolibSoft.AuthStore.Server;

public class CredentialController : CatalogueController<CredentialModel, CredentialFilters>
{

    [Authorize(AuthStoreStatics.CredentialReader)]
    public override Task<Result<Page<CredentialModel>?>> PageAsync(CredentialFilters? filters = null)
    {
        return base.PageAsync(filters);
    }

    [Authorize(AuthStoreStatics.CredentialReader)]
    public override Task<Result<CredentialModel?>> GetAsync(Guid id)
    {
        return base.GetAsync(id);
    }

    [Authorize(AuthStoreStatics.CredentialManager)]
    public override Task<Result<Guid?>> InsertAsync(CredentialModel item)
    {
        item.Identity = item.Identity.Trim();
        item.Key = item.Key.Trim();
        return base.InsertAsync(item);
    }

    [Authorize(AuthStoreStatics.CredentialManager)]
    public override Task<Result<bool?>> UpdateAsync(Guid id, CredentialModel item)
    {
        item.Identity = item.Identity.Trim();
        item.Key = item.Key.Trim();
        return base.UpdateAsync(id, item);
    }

    [Authorize(AuthStoreStatics.CredentialManager)]
    public override Task<Result<bool?>> DeleteAsync(Guid id)
    {
        return base.DeleteAsync(id);
    }

    public CredentialController(ICatalogueConnector<CredentialModel, CredentialFilters> catalogueConnector) : base(catalogueConnector) { }

}