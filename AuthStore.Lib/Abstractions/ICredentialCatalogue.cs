using KolibSoft.AuthStore.Filters;
using KolibSoft.AuthStore.Models;
using KolibSoft.Catalogue.Abstractions;

namespace KolibSoft.AuthStore.Abstractions;

public interface ICredentialCatalogue : ICatalogue<CredentialModel, SearchFilters>
{
    public IEnumerable<PermissionModel> GetPermissionsOf(Guid id);
    public IEnumerable<PermissionModel> GrantPermission(Guid credentialId, Guid permissionId);
    public IEnumerable<PermissionModel> DenyPermission(Guid credentialId, Guid permissionId);
}