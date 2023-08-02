using KolibSoft.AuthStore.Core;
using KolibSoft.AuthStore.Core.Abstractions;
using KolibSoft.AuthStore.Core.Utils;
using KolibSoft.Catalogue.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KolibSoft.AuthStore.Server;

public class AuthController : ControllerBase, IAuthConnector
{

    public IAuthConnector AuthConnector { get; }

    [HttpPost]
    public virtual async Task<Result<AuthModel?>> AccessAsync([FromBody] LoginModel login)
    {
        login.Identity = login.Identity.Trim();
        login.Key = login.Key.Trim().GetHashString();
        var result = await AuthConnector.AccessAsync(login);
        Response.StatusCode = result.Errors?.Any() == true ? StatusCodes.Status400BadRequest : StatusCodes.Status200OK;
        return result;
    }

    [HttpGet("{id}")]
    public virtual async Task<Result<AuthModel?>> RefreshAsync([FromRoute] Guid id, [FromHeader(Name = "Authorization")] string refreshToken)
    {
        refreshToken = refreshToken.Trim()[7..];
        var result = await AuthConnector.RefreshAsync(id, refreshToken);
        Response.StatusCode = result.Errors?.Any() == true ? StatusCodes.Status400BadRequest : StatusCodes.Status200OK;
        return result;
    }

    public AuthController(IAuthConnector authConnector)
    {
        AuthConnector = authConnector;
    }

}