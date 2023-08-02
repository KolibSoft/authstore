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
        var valid = true;
        if (Id == Guid.Empty)
        {
            errors?.Add(CatalogueStatics.InvalidId);
            valid = false;
        }
        if (CredentialId == Guid.Empty)
        {
            errors?.Add(AuthStoreStatics.InvalidCredential);
            valid = false;
        }
        if (PermissionId == Guid.Empty)
        {
            errors?.Add(AuthStoreStatics.InvalidPermission);
            valid = false;
        }
        return valid;
    }

    public void Update(CredentialPermissionModel newState)
    {
        CredentialId = newState.CredentialId;
        PermissionId = newState.PermissionId;
        Active = newState.Active;
        UpdatedAt = newState.UpdatedAt;
    }

}