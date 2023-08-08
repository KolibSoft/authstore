using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using KolibSoft.AuthStore.Core;
using KolibSoft.AuthStore.Core.Abstractions;
using KolibSoft.Catalogue.Core;
using KolibSoft.Catalogue.Core.Utils;

namespace KolibSoft.AuthStore.Client;

public class AuthService : IAuthConnector
{

    public HttpClient HttpClient { get; }
    public string Uri { get; }

    public virtual bool Available => NetworkInterface.GetAllNetworkInterfaces().Any(x => x.OperationalStatus == OperationalStatus.Up && x.NetworkInterfaceType != NetworkInterfaceType.Loopback);

    public virtual async Task<Result<AuthModel?>> AccessAsync(LoginModel login)
    {
        var uri = $"{Uri}";
        var response = await HttpClient.PostAsJsonAsync(uri, login);
        var result = await response.HandleResult<AuthModel?>();
        return result;
    }

    public virtual async Task<Result<AuthModel?>> RefreshAsync(Guid id, string refreshToken)
    {
        var uri = $"{Uri}/{id}";
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
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