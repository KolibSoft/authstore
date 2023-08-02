using KolibSoft.AuthStore.Core.Models;
using KolibSoft.Catalogue.Client;
using KolibSoft.Catalogue.Core;

namespace KolibSoft.AuthStore.Client;

public class CredentialService : CatalogueService<CredentialModel, CatalogueFilters>
{
    public CredentialService(HttpClient httpClient, string uri) : base(httpClient, uri) { }
}