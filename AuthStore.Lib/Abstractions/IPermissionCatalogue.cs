using KolibSoft.AuthStore.Filters;
using KolibSoft.AuthStore.Models;
using KolibSoft.Catalogue.Abstractions;

namespace KolibSoft.AuthStore.Abstractions;

public interface IPermissionCatalogue : ICatalogue<CredentialModel, SearchFilters>
{
    public IEnumerable<CredentialModel> GetCredentialsOf(Guid id);
    public IEnumerable<PermissionModel> GrantCredential(Guid permissionId, Guid credentialId);
    public IEnumerable<PermissionModel> DenyCredential(Guid permissionId, Guid credentialId);
}