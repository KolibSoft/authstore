import { AuthService } from "../lib/auth_service.js";
import { Catalogue } from "../lib/modules/catalogue.js";
import { AuthStoreClient, AuthStoreContext } from "./client.js";

let accessToken = null;

async function serviceFetch(uri, request) {
    if (accessToken) {
        request.headers ??= {};
        request.headers["Authorization"] = `bearer ${accessToken}`;
    }
    return await fetch(uri, request);
}

let uri = "http://localhost:5177";
let context = new AuthStoreContext();
let changes = JSON.parse(localStorage.changes ?? "{}");
let client = new AuthStoreClient(serviceFetch, uri, context, changes);
console.log(client);

if (client.auth.available) 
    try {
        let auth = await client.auth.accesAsync({ identity: "ROOT", key: "ROOT" });
        accessToken = auth.data.accessToken;
        await client.sync();
    } catch { }

// let credential = await client.credentials.insertAsync({ identity: "USER_NEW2" });

localStorage.changes = JSON.stringify(changes);