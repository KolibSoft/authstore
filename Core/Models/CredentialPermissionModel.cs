using KolibSoft.Catalogue.Core;
using KolibSoft.Catalogue.Core.Abstractions;

namespace KolibSoft.AuthStore.Core.Models;

public class CredentialPermissionModel : IItem, IValidatable, IUpdatable<CredentialPermissionModel>
{

    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CredentialId { get; set; } = Guid.Empty;
    public Guid PermissionId { get; set; } = Guid.Empty;
    public bool Active { get; set; } = true;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public bool Validate(ICollection<string>? errors = default)
    {
        var invalid = false;
        if (invalid = (Id == Guid.Empty)) errors?.Add(CatalogueStatics.InvalidId);
        if (invalid = (CredentialId == Guid.Empty)) errors?.Add(AuthStoreStatics.InvalidCredential);
        if (invalid = (PermissionId == Guid.Empty)) errors?.Add(AuthStoreStatics.InvalidPermission);
        return !invalid;
    }

    public void Update(CredentialPermissionModel newState)
    {
        CredentialId = newState.CredentialId;
        PermissionId = newState.PermissionId;
        Active = newState.Active;
        UpdatedAt = newState.UpdatedAt;
    }

}