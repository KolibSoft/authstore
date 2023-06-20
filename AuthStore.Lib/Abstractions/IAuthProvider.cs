using KolibSoft.AuthStore.Filters;
using KolibSoft.AuthStore.Models;
using KolibSoft.Catalogue.Abstractions;

namespace KolibSoft.AuthStore.Abstractions;

public interface IAuthProvider
{

    public ICredentialCatalogue CredentialCatalogue { get; }
    public IPermissionCatalogue PermissionCatalogue { get; }

    public AuthModel? Login(LoginModel login);
    public AuthModel? Refresh(string refreshToken);

}