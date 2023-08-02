using KolibSoft.AuthStore.Core;
using KolibSoft.AuthStore.Core.Models;
using KolibSoft.Catalogue.Client;

namespace KolibSoft.AuthStore.Client;

public class CredentialService : CatalogueService<CredentialModel, CredentialFilters>
{
    public CredentialService(HttpClient httpClient, string uri) : base(httpClient, uri) { }
}