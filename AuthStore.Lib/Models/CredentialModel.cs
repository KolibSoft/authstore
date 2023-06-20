using System.ComponentModel.DataAnnotations.Schema;
using KolibSoft.AuthStore.Common;
using KolibSoft.Catalogue.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace KolibSoft.AuthStore.Models;

[Table("credential")]
[PrimaryKey("Id")]
public class CredentialModel : IRegister, IUpdatable<CredentialModel>
{

    public Guid Id { get; set; } = Guid.NewGuid();
    public string Identity { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public bool Active { get; set; } = false;

    public bool Validate(ICollection<string>? errors = null)
    {
        var valid = true;
        if (string.IsNullOrWhiteSpace(Identity) || Identity.Length > 32)
        {
            errors?.Add(ErrorCodes.InvalidIdentity);
            valid = false;
        }
        if (string.IsNullOrWhiteSpace(Key) || Key.Length != 64)
        {
            errors?.Add(ErrorCodes.InvalidKey);
            valid = false;
        }
        return valid;
    }

    public void Update(CredentialModel item)
    {
        Identity = item.Identity;
        Key = item.Key;
        Active = item.Active;
    }

}