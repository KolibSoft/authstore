using KolibSoft.AuthStore.Core;
using Microsoft.AspNetCore.Mvc;

namespace KolibSoft.AuthStore.Server.Example;

[Route("auth")]
public class TestAuthController : AuthController
{
    public TestAuthController(AuthStoreContext context, TokenGenerator generator) : base(new DatabaseAuth(context, generator)) { }
}

[Route("credential")]
public class TestCredentialController : CredentialController
{
    public TestCredentialController(AuthStoreContext context) : base(new CredentialDatabaseCatalogue(context)) { }
}

[Route("permission")]
public class TestPermissionController : PermissionController
{
    public TestPermissionController(AuthStoreContext context) : base(new PermissionDatabaseCatalogue(context)) { }
}

[Route("credential-permission")]
[Route("permission-credential")]
public class TestCredentialPermissionController : CredentialPermissionController
{
    public TestCredentialPermissionController(AuthStoreContext context) : base(new CredentialPermissionDatabaseCatalogue(context)) { }
}