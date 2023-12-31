using KolibSoft.AuthStore.Core;
using KolibSoft.AuthStore.Core.Abstractions;
using KolibSoft.AuthStore.Core.Utils;
using KolibSoft.Catalogue.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KolibSoft.AuthStore.Server.Controllers;

[Route("_NO_ROUTE_")]
public class AuthController : ControllerBase, IAuthConnector
{

    public IAuthConnector AuthConnector { get; }

    public virtual bool Available => AuthConnector.Available;

    [HttpPost]
    public virtual async Task<Result<AuthModel?>> AccessAsync([FromBody] LoginModel login)
    {
        login.Identity = login.Identity.Trim();
        login.Key = login.Key.Trim().GetHashString();
        var result = await AuthConnector.AccessAsync(login);
        if (result.Data != null) result = new AuthModel
        {
            Credential = result.Data.Credential.ToPublic(),
            Permissions = result.Data.Permissions,
            AccessToken = result.Data.AccessToken,
            RefreshToken = result.Data.RefreshToken
        };
        Response.StatusCode = result.Errors?.Any() == true ? StatusCodes.Status400BadRequest : StatusCodes.Status200OK;
        return result;
    }

    [HttpGet("{id}")]
    [Authorize(AuthStoreStatics.Refresher)]
    public virtual async Task<Result<AuthModel?>> RefreshAsync([FromRoute] Guid id, [FromHeader(Name = "Authorization")] string refreshToken)
    {
        if (!User.HasClaim(AuthStoreStatics.Id, id.ToString()))
        {
            Response.StatusCode = StatusCodes.Status403Forbidden;
            return new string[] { CatalogueStatics.InvalidId };
        }
        refreshToken = refreshToken.Trim()[7..];
        var result = await AuthConnector.RefreshAsync(id, refreshToken);
        if (result.Data != null) result = new AuthModel
        {
            Credential = result.Data.Credential.ToPublic(),
            Permissions = result.Data.Permissions,
            AccessToken = result.Data.AccessToken,
            RefreshToken = result.Data.RefreshToken
        };
        Response.StatusCode = result.Errors?.Any() == true ? StatusCodes.Status400BadRequest : StatusCodes.Status200OK;
        return result;
    }

    public AuthController(IAuthConnector authConnector)
    {
        AuthConnector = authConnector;
    }

}