using KolibSoft.AuthStore.Core.Models;
using KolibSoft.Catalogue.Client;
using KolibSoft.Catalogue.Core;

namespace KolibSoft.AuthStore.Client;

public class PermissionService : CatalogueService<PermissionModel, CatalogueFilters>
{
    public PermissionService(HttpClient httpClient, string uri) : base(httpClient, uri) { }
}