using KolibSoft.AuthStore.Core;
using Microsoft.AspNetCore.Authorization;

namespace KolibSoft.AuthStore.Server.Utils;

public static class PolicyUtils
{

    public static void AddAuthStore(this AuthorizationOptions options)
    {
        options.AddPolicy(AuthStoreStatics.Accessor, config => config.RequireClaim(AuthStoreStatics.Access, AuthStoreStatics.Permitted));
        options.AddPolicy(AuthStoreStatics.Refresher, config => config.RequireClaim(AuthStoreStatics.Refresh, AuthStoreStatics.Permitted));

        options.AddPolicy(AuthStoreStatics.CredentialReader, config => config.RequireClaim(AuthStoreStatics.Permissions, AuthStoreStatics.ReadCredential, AuthStoreStatics.ManageCredential));
        options.AddPolicy(AuthStoreStatics.CredentialManager, config => config.RequireClaim(AuthStoreStatics.Permissions, AuthStoreStatics.ManageCredential));

        options.AddPolicy(AuthStoreStatics.PermissionReader, config => config.RequireClaim(AuthStoreStatics.Permissions, AuthStoreStatics.ReadPermission, AuthStoreStatics.ManagePermission));
        options.AddPolicy(AuthStoreStatics.PermissionManager, config => config.RequireClaim(AuthStoreStatics.Permissions, AuthStoreStatics.ManagePermission));

        options.AddPolicy(AuthStoreStatics.CredentialPermissionReader, config => config.RequireClaim(AuthStoreStatics.Permissions, AuthStoreStatics.ReadCredentialPermission, AuthStoreStatics.ManageCredentialPermission));
        options.AddPolicy(AuthStoreStatics.CredentialPermissionManager, config => config.RequireClaim(AuthStoreStatics.Permissions, AuthStoreStatics.ManageCredentialPermission));
    }

}