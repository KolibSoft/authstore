using KolibSoft.AuthStore.Core.Models;
using KolibSoft.Catalogue.Client;
using KolibSoft.Catalogue.Core;

namespace KolibSoft.AuthStore.Client;

public class CredentialPermissionService : CatalogueService<CredentialPermissionModel, CatalogueFilters>
{
    public CredentialPermissionService(HttpClient httpClient, string uri) : base(httpClient, uri) { }
}