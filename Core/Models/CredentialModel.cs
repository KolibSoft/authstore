using KolibSoft.Catalogue.Core;
using KolibSoft.Catalogue.Core.Abstractions;

namespace KolibSoft.AuthStore.Core.Models;

public class CredentialModel : IItem, IValidatable, IUpdatable<CredentialModel>
{

    public Guid Id { get; set; } = Guid.NewGuid();
    public string Identity { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public bool Active { get; set; } = true;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public bool Validate(ICollection<string>? errors = default)
    {
        Identity = Identity.Trim();
        Key = Key.Trim();
        var invalid = false;
        if (invalid = (Id == Guid.Empty)) errors?.Add(CatalogueStatics.InvalidId);
        if (invalid = (Identity == string.Empty || Identity.Length < 8 || Identity.Length > 32)) errors?.Add(AuthStoreStatics.InvalidIdentity);
        if (invalid = (Key == string.Empty || Key.Length != 64)) errors?.Add(AuthStoreStatics.InvalidKey);
        return !invalid;
    }

    public void Update(CredentialModel newState)
    {
        Identity = newState.Identity;
        Key = newState.Key;
        Active = newState.Active;
        UpdatedAt = newState.UpdatedAt;
    }

}