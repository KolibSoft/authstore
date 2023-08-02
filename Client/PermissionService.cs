using KolibSoft.AuthStore.Core;
using KolibSoft.AuthStore.Core.Models;
using KolibSoft.Catalogue.Client;

namespace KolibSoft.AuthStore.Client;

public class PermissionService : CatalogueService<PermissionModel, PermissionFilters>
{
    public PermissionService(HttpClient httpClient, string uri) : base(httpClient, uri) { }
}