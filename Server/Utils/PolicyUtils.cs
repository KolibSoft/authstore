using KolibSoft.AuthStore.Core;
using Microsoft.AspNetCore.Authorization;

namespace KolibSoft.AuthStore.Server.Utils;

public static class PolicyUtils
{

    public static void AddAuthStore(this AuthorizationOptions options)
    {
        options.AddPolicy(AuthStoreStatics.Accessor, config => config.RequireClaim(AuthStoreStatics.Access, AuthStoreStatics.Permitted));
        options.AddPolicy(AuthStoreStatics.Refresher, config => config.RequireClaim(AuthStoreStatics.Refresh, AuthStoreStatics.Permitted));
        options.AddPolicy(AuthStoreStatics.CredentialReader, config => config.RequireClaim(AuthStoreStatics.Permissions, AuthStoreStatics.CredentialReader, AuthStoreStatics.CredentialManager));
        options.AddPolicy(AuthStoreStatics.CredentialManager, config => config.RequireClaim(AuthStoreStatics.Permissions, AuthStoreStatics.CredentialManager));
    }

}