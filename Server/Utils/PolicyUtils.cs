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

        options.AddPolicy(AuthStoreStatics.PermissionReader, config => config.RequireClaim(AuthStoreStatics.Permissions, AuthStoreStatics.PermissionReader, AuthStoreStatics.PermissionManager));
        options.AddPolicy(AuthStoreStatics.PermissionManager, config => config.RequireClaim(AuthStoreStatics.Permissions, AuthStoreStatics.PermissionManager));

        options.AddPolicy(AuthStoreStatics.CredentialPermissionReader, config => config.RequireClaim(AuthStoreStatics.Permissions, AuthStoreStatics.CredentialPermissionReader, AuthStoreStatics.CredentialPermissionManager));
        options.AddPolicy(AuthStoreStatics.CredentialPermissionManager, config => config.RequireClaim(AuthStoreStatics.Permissions, AuthStoreStatics.CredentialPermissionManager));
    }

}