using KolibSoft.Catalogue.Core;
using KolibSoft.Catalogue.Core.Abstractions;

namespace KolibSoft.AuthStore.Core.Models;

public class PermissionModel : IItem, IValidatable, IUpdatable<PermissionModel>
{

    public Guid Id { get; set; } = Guid.NewGuid();
    public string Code { get; set; } = string.Empty;
    public bool Active { get; set; } = true;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public bool Validate(ICollection<string>? errors = default)
    {
        Code = Code.Trim();
        var invalid = false;
        if (invalid = (Id == Guid.Empty)) errors?.Add(CatalogueStatics.InvalidId);
        if (invalid = (Code == string.Empty || Code.Length < 8 || Code.Length > 32)) errors?.Add(AuthStoreStatics.InvalidCode);
        return !invalid;
    }

    public void Update(PermissionModel newState)
    {
        Code = newState.Code;
        Active = newState.Active;
        UpdatedAt = newState.UpdatedAt;
    }

}