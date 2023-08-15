using KolibSoft.AuthStore.Core;
using KolibSoft.AuthStore.Core.Filters;
using KolibSoft.AuthStore.Core.Models;
using KolibSoft.Catalogue.Core;
using KolibSoft.Catalogue.Core.Abstractions;
using KolibSoft.Catalogue.Server;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KolibSoft.AuthStore.Server.Controllers;

[Route("_NO_ROUTE_")]
public class PermissionController : CatalogueController<PermissionModel, PermissionFilters>
{

    [Authorize(AuthStoreStatics.PermissionReader)]
    public override Task<Result<Page<PermissionModel>?>> PageAsync(PermissionFilters? filters = null)
    {
        return base.PageAsync(filters);
    }

    [Authorize(AuthStoreStatics.PermissionReader)]
    public override Task<Result<PermissionModel?>> GetAsync(Guid id)
    {
        return base.GetAsync(id);
    }

    [Authorize(AuthStoreStatics.PermissionManager)]
    public override Task<Result<Guid?>> InsertAsync(PermissionModel item)
    {
        item.Code = item.Code.Trim();
        return base.InsertAsync(item);
    }

    [Authorize(AuthStoreStatics.PermissionManager)]
    public override Task<Result<bool?>> UpdateAsync(Guid id, PermissionModel item)
    {
        item.Code = item.Code.Trim();
        return base.UpdateAsync(id, item);
    }

    [Authorize(AuthStoreStatics.PermissionManager)]
    public override Task<Result<bool?>> DeleteAsync(Guid id)
    {
        return base.DeleteAsync(id);
    }

    public PermissionController(ICatalogueConnector<PermissionModel, PermissionFilters> catalogueConnector) : base(catalogueConnector) { }

}