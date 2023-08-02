using KolibSoft.AuthStore.Core.Models;

namespace KolibSoft.AuthStore.Core;

public class AuthModel
{
    public CredentialModel Credential { get; set; } = new() { Id = Guid.Empty };
    public IEnumerable<PermissionModel> Permissions { get; set; } = Array.Empty<PermissionModel>();
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}