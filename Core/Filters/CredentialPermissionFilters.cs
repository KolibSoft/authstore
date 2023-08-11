using KolibSoft.Catalogue.Core;

namespace KolibSoft.AuthStore.Core.Filters;

public class CredentialPermissionFilters : CatalogueFilters
{
    public bool? Clean { get; init; }
    public Guid? CredentialId { get; init; }
    public Guid? PermissionId { get; init; }
}