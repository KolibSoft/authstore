using KolibSoft.AuthStore.Client.Example;
using KolibSoft.AuthStore.Client.Utils;
using System.Text.Json;

var uri = "http://localhost:5177";
var context = new AuthStoreContext();
var changes = File.Exists("changes.json") ? JsonSerializer.Deserialize<AuthStoreChanges>(File.ReadAllText("changes.json")) ?? new() : new();

var client = new AuthStoreClient(uri, context, changes);
try
{
    var auth = await client.Auth.AccessAsync(new()
    {
        Identity = "ROOT",
        Key = "ROOT"
    });
    client.HttpClient.UseToken(auth.Data!.AccessToken);
}
catch { }

var credentials = await client.Credentials.PageAsync();

_ = 0;