using KolibSoft.AuthStore.Client.Example;
using KolibSoft.AuthStore.Client.Utils;
using System.Text.Json;

var uri = "http://localhost:5177";
var context = new AuthStoreContext();
var changes = File.Exists("changes.json") ? JsonSerializer.Deserialize<AuthStoreChanges>(File.ReadAllText("changes.json")) ?? new() : new();

var client = new AuthStoreClient(uri, context, changes);
if (client.Auth.Available)
    try
    {
        var auth = await client.Auth.AccessAsync(new()
        {
            Identity = "ROOT",
            Key = "ROOT"
        });
        client.HttpClient.UseToken(auth.Data!.AccessToken);
        await client.Sync();
    }
    catch { }

var page = await client.Credentials.PageAsync();

/*
var insert = await client.Credentials.InsertAsync(new()
{
    Identity = "USER_NEW",
    Key = "USER_NEW"
});
*/

File.WriteAllText("changes.json", JsonSerializer.Serialize(changes));
_ = 0;