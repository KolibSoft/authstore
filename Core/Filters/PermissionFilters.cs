using KolibSoft.Catalogue.Core;

namespace KolibSoft.AuthStore.Core.Filters;

public class PermissionFilters : CatalogueFilters
{
    public bool? Clean { get; init; }
}