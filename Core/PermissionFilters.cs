using KolibSoft.Catalogue.Core;

namespace KolibSoft.AuthStore.Core;

public class PermissionFilters : CatalogueFilters
{
    public bool? Clean { get; init; }
    public Guid? CredentialId { get; init; }
}