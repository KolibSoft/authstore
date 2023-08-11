using KolibSoft.AuthStore.Core;
using KolibSoft.AuthStore.Core.Filters;
using KolibSoft.AuthStore.Core.Models;
using KolibSoft.AuthStore.Core.Utils;
using KolibSoft.Catalogue.Core;
using KolibSoft.Catalogue.Core.Abstractions;
using KolibSoft.Catalogue.Server;
using Microsoft.AspNetCore.Authorization;

namespace KolibSoft.AuthStore.Server.Controllers;

public class CredentialController : CatalogueController<CredentialModel, CredentialFilters>
{

    [Authorize(AuthStoreStatics.CredentialReader)]
    public override async Task<Result<Page<CredentialModel>?>> PageAsync(CredentialFilters? filters = null)
    {
        var result = await base.PageAsync(filters);
        if (result.Data != null) result = new Page<CredentialModel>
        {
            Items = result.Data.Items.Select(x => x.ToPublic()).ToArray(),
            PageIndex = result.Data.PageIndex,
            PageCount = result.Data.PageCount
        };
        return result;
    }

    [Authorize(AuthStoreStatics.CredentialReader)]
    public override async Task<Result<CredentialModel?>> GetAsync(Guid id)
    {
        var result = await base.GetAsync(id);
        if (result.Data != null) result = result.Data.ToPublic();
        return result;
    }

    [Authorize(AuthStoreStatics.CredentialManager)]
    public override Task<Result<Guid?>> InsertAsync(CredentialModel item)
    {
        item.Identity = item.Identity.Trim();
        item.Key = item.Key.Trim().GetHashString();
        return base.InsertAsync(item);
    }

    [Authorize(AuthStoreStatics.CredentialManager)]
    public override Task<Result<bool?>> UpdateAsync(Guid id, CredentialModel item)
    {
        item.Identity = item.Identity.Trim();
        item.Key = item.Key.Trim().GetHashString();
        return base.UpdateAsync(id, item);
    }

    [Authorize(AuthStoreStatics.CredentialManager)]
    public override Task<Result<bool?>> DeleteAsync(Guid id)
    {
        return base.DeleteAsync(id);
    }

    public CredentialController(ICatalogueConnector<CredentialModel, CredentialFilters> catalogueConnector) : base(catalogueConnector) { }

}