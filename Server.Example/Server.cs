using KolibSoft.AuthStore.Core;
using Microsoft.AspNetCore.Mvc;

namespace KolibSoft.AuthStore.Server.Example;

[Route("auth")]
public class TestAuthController : AuthController
{
    public TestAuthController(AuthStoreContext context, TokenGenerator generator) : base(new DatabaseAuth(context, generator)) { }
}