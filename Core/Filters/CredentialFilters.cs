using KolibSoft.Catalogue.Core;

namespace KolibSoft.AuthStore.Core.Filters;

public class CredentialFilters : CatalogueFilters
{
    public bool? Clean { get; init; }
}