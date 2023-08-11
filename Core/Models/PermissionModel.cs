using KolibSoft.Catalogue.Core;
using KolibSoft.Catalogue.Core.Abstractions;

namespace KolibSoft.AuthStore.Core.Models;

public class PermissionModel : Item, IValidatable, IUpdatable<PermissionModel>
{

    public string Code { get; set; } = string.Empty;
    public bool Active { get; set; } = true;

    public override bool Validate(ICollection<string>? errors = default)
    {
        var valid = base.Validate(errors);
        if (Id == Guid.Empty)
        {
            errors?.Add(CatalogueStatics.InvalidId);
            valid = false;
        }
        if (Code == string.Empty || Code.Length < 8 || Code.Length > 32)
        {
            errors?.Add(AuthStoreStatics.InvalidCode);
            valid = false;
        }
        return valid;
    }

    public void Update(PermissionModel newState)
    {
        Code = newState.Code;
        Active = newState.Active;
        UpdatedAt = newState.UpdatedAt;
    }

}