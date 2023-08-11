using KolibSoft.AuthStore.Core.Filters;
using KolibSoft.AuthStore.Core.Models;
using KolibSoft.Catalogue.Client;

namespace KolibSoft.AuthStore.Client;

public class CredentialPermissionService : CatalogueService<CredentialPermissionModel, CredentialPermissionFilters>
{
    public CredentialPermissionService(HttpClient httpClient, string uri) : base(httpClient, uri) { }
}