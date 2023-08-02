using KolibSoft.Catalogue.Core;

namespace KolibSoft.AuthStore.Core;

public class CredentialFilters : CatalogueFilters
{
    public bool? Clean { get; init; }
}