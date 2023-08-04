using System.Net.Http.Headers;

namespace KolibSoft.AuthStore.Client.Utils;

public static class HttpClientUtils
{

    public static void UseToken(this HttpClient httpClient, string? token)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
    }

}