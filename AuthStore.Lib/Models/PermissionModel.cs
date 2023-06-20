using System.ComponentModel.DataAnnotations.Schema;
using KolibSoft.AuthStore.Common;
using KolibSoft.Catalogue.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace KolibSoft.AuthStore.Models;


[Table("permission")]
[PrimaryKey("Id")]
public class PermissionModel : IRegister, IUpdatable<PermissionModel>
{

    public Guid Id { get; set; } = Guid.NewGuid();
    public string Code { get; set; } = string.Empty;
    public bool Active { get; set; } = false;

    public bool Validate(ICollection<string>? errors = null)
    {
        var valid = true;
        if (string.IsNullOrWhiteSpace(Code) || Code.Length > 32)
        {
            errors?.Add(ErrorCodes.InvalidCode);
            valid = false;
        }
        return valid;
    }

    public void Update(PermissionModel item)
    {
        Code = item.Code;
        Active = item.Active;
    }

}