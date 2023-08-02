using System.Net.Http.Headers;
using System.Net.Http.Json;
using KolibSoft.AuthStore.Core;
using KolibSoft.AuthStore.Core.Abstractions;
using KolibSoft.Catalogue.Core;
using KolibSoft.Catalogue.Core.Utils;

namespace KolibSoft.AuthStore.Client;

public class AuthService : IAuthConnector
{

    public HttpClient HttpClient { get; }
    public string Uri { get; }

    public virtual async Task<Result<AuthModel?>> AccessAsync(LoginModel login)
    {
        var uri = $"{Uri}/access";
        var response = await HttpClient.PostAsJsonAsync(uri, login);
        var result = await response.HandleResult<AuthModel?>();
        return result;
    }

    public virtual async Task<Result<AuthModel?>> RefreshAsync(string refreshToken)
    {
        var uri = $"{Uri}/refresh";
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        request.Headers.Authorization = new AuthenticationHeaderValue("bearer", refreshToken);
        var response = await HttpClient.SendAsync(request);
        var result = await response.HandleResult<AuthModel?>();
        return result;
    }

    public AuthService(HttpClient httpClient, string uri)
    {
        HttpClient = httpClient;
        Uri = uri;
    }

}