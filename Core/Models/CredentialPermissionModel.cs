using KolibSoft.Catalogue.Core;
using KolibSoft.Catalogue.Core.Abstractions;

namespace KolibSoft.AuthStore.Core.Models;

public class CredentialPermissionModel : Item, IUpdatable<CredentialPermissionModel>
{

    public Guid CredentialId { get; set; } = Guid.Empty;
    public Guid PermissionId { get; set; } = Guid.Empty;
    public bool Active { get; set; } = true;

    public override bool Validate(ICollection<string>? errors = default)
    {
        var valid = base.Validate(errors);
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