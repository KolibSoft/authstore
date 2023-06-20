namespace KolibSoft.AuthStore.Models;

public class AuthModel
{
    public CredentialModel Credential { get; set; } = null!;
    public IEnumerable<PermissionModel> Permissions { get; set; } = null!;
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}