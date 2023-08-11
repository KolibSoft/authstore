using KolibSoft.Catalogue.Core;
using KolibSoft.Catalogue.Core.Abstractions;

namespace KolibSoft.AuthStore.Core.Models;

public class CredentialModel : Item, IUpdatable<CredentialModel>
{

    public string Identity { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public bool Active { get; set; } = true;

    public override bool Validate(ICollection<string>? errors = default)
    {
        var valid = base.Validate(errors);
        if (Id == Guid.Empty)
        {
            errors?.Add(CatalogueStatics.InvalidId);
            valid = false;
        }
        if (Identity.Length > 32 || string.IsNullOrWhiteSpace(Identity))
        {
            errors?.Add(AuthStoreStatics.InvalidIdentity);
            valid = false;
        }
        if (Key.Length > 64 || string.IsNullOrWhiteSpace(Key))
        {
            errors?.Add(AuthStoreStatics.InvalidKey);
            valid = false;
        }
        return valid;
    }

    public void Update(CredentialModel newState)
    {
        Identity = newState.Identity;
        Key = newState.Key;
        Active = newState.Active;
        UpdatedAt = newState.UpdatedAt;
    }

    public CredentialModel ToPublic() => new()
    {
        Id = Id,
        Identity = Identity,
        Key = new string('*', 8),
        Active = Active,
        UpdatedAt = UpdatedAt
    };

}