import { AuthService } from "../lib/auth_service.js";
import { Catalogue } from "../lib/modules/catalogue.js";
import { AuthStoreContext } from "./client.js";

async function fetchHandler(uri, request) {
    return await fetch(uri, request);
}

let uri = "http://localhost:5177";
let context = new AuthStoreContext();
let changes = JSON.parse(localStorage.changes);

let service = new AuthService(fetchHandler, `${uri}/auth`);
let access = await service.accesAsync({
    identity: "ROOT",
    key: "ROOT"
});
console.log(access);
await new Promise(resolve => setTimeout(resolve, 5000));
let refresh = await service.refreshAsync(access.data.credential.id, access.data.accessToken);
console.log(refresh);